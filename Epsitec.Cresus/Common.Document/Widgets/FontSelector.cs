using Epsitec.Common.Widgets;
using Epsitec.Common.Drawing;
using Epsitec.Common.OpenType;

namespace Epsitec.Common.Document.Widgets
{
	/// <summary>
	/// La classe FontSelector est un widget permettant de choisir une police.
	/// </summary>
	public class FontSelector : Widget
	{
		public FontSelector()
		{
			this.AutoEngage = false;
			this.AutoFocus  = true;

			this.InternalState |= InternalState.Focusable;
			this.InternalState |= InternalState.Engageable;
		}

		public FontSelector(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}


		// Retourne la meilleure largeur.
		public static double BestWidth()
		{
			return 300;
		}

		// Retourne la meilleure hauteur possible, en principe plus petite que la hauteur demand�e.
		public static double BestHeight(double height, int totalLines)
		{
			int lines = (int) (height / FontSelector.sampleHeight);
			if ( lines == 0 )  lines ++;  // au moins une ligne, faut pas pousser

			lines = System.Math.Min(lines, totalLines);  // pas plus que n�cessaire

			return lines*FontSelector.sampleHeight;
		}


		// Liste des OpenType.FontIdentity repr�sent�e.
		public System.Collections.ArrayList FontList
		{
			get
			{
				return this.fontList;
			}

			set
			{
				this.fontList = value;
			}
		}

		// Nombre de fontes rapides en t�te de liste.
		public int QuickCount
		{
			get
			{
				return this.quickCount;
			}

			set
			{
				this.quickCount = value;
			}
		}

		// Liste des FontFace (string) s�lectionn� (s�lection multiple).
		public System.Collections.ArrayList SelectedList
		{
			get
			{
				return this.selectedList;
			}

			set
			{
				this.selectedList = value;
			}
		}

		// Police s�lectionn�e.
		public string SelectedFontFace
		{
			get
			{
				return this.fontFace;
			}

			set
			{
				if ( this.fontFace != value )
				{
					this.fontFace = value;

					this.selectedLine = this.FaceToRank(this.fontFace);
					
					if ( this.selectedLine < this.firstLine || this.selectedLine >= this.firstLine+this.samples.Length )
					{
						this.firstLine = System.Math.Min(this.selectedLine+this.samples.Length/2, this.fontList.Count-1);
						this.firstLine = System.Math.Max(this.firstLine-(this.samples.Length-1), 0);
					}

					this.UpdateScroller();
					this.UpdateList();
				}
			}
		}


		// Peuple le widget seulement lorsqu'il a les dimensions d�finitives.
		// Ensuite, il ne devra plus �tre redimensionn�.
		public void Build()
		{
			Rectangle rect = this.Client.Bounds;

			this.scroller = new VScroller(this);
			this.scroller.Bounds = new Rectangle(rect.Right-this.scroller.DefaultWidth, rect.Bottom, this.scroller.DefaultWidth, rect.Height);
			this.scroller.IsInverted = true;  // z�ro en haut
			this.scroller.ValueChanged += new Epsitec.Common.Support.EventHandler(this.ScrollerValueChanged);

			int lines = (int) (this.Height / FontSelector.sampleHeight);
			this.samples = new Common.Document.Widgets.FontSample[lines];

			rect.Width -= this.scroller.DefaultWidth;
			rect.Bottom = rect.Top-FontSelector.sampleHeight;
			for ( int i=0 ; i<lines ; i++ )
			{
				this.samples[i] = new Epsitec.Common.Document.Widgets.FontSample(this);
				this.samples[i].Bounds = rect;

				rect.Offset(0, -FontSelector.sampleHeight);
			}

			this.UpdateScroller();
			this.UpdateList();
		}


		protected override void ProcessMessage(Message message, Drawing.Point pos)
		{
			if ( message.Type == MessageType.MouseDown )
			{
				if ( pos.X < this.Bounds.Right-this.scroller.Width )
				{
					int sel = this.firstLine + (int)((this.Bounds.Height-pos.Y) / FontSelector.sampleHeight);
					string face = this.RankToFace(sel);

					if ( this.selectedList == null )  // s�lection unique ?
					{
						this.SelectedFontFace = face;
						this.OnSelectionChanged();
					}
					else	// s�lection multiple ?
					{
						if ( this.selectedList.Contains(face) )
						{
							this.selectedList.Remove(face);
						}
						else
						{
							this.selectedList.Add(face);
						}
						this.UpdateList();
						this.OnSelectionChanged();
					}
				}
				
				message.Consumer = this;
			}
			else if ( message.Type == MessageType.MouseWheel )
			{
				if ( message.Wheel > 0 )
				{
					this.FirstLine = this.FirstLine-3;
				}
				if ( message.Wheel < 0 )
				{
					this.FirstLine = this.FirstLine+3;
				}
				
				message.Consumer = this;
			}
			else if ( message.Type == MessageType.KeyDown )
			{
				bool ok = true;
				
				switch ( message.KeyCode )
				{
					case KeyCode.Escape:
						break;
					case KeyCode.Return:
						this.OnSelectionChanged();
						break;
					case KeyCode.ArrowUp:
						this.FirstLine = this.FirstLine-1;
						break;
					case KeyCode.ArrowDown:
						this.FirstLine = this.FirstLine+1;
						break;
					case KeyCode.PageUp:
						this.FirstLine = this.FirstLine-this.samples.Length;
						break;
					case KeyCode.PageDown:
						this.FirstLine = this.FirstLine+this.samples.Length;
						break;
					case KeyCode.Home:
						this.FirstLine = 0;
						break;
					case KeyCode.End:
						this.FirstLine = 100000;
						break;
					case KeyCode.Back:
						if ( this.searchOnTheFly.Length > 0 )
						{
							this.searchOnTheFly = this.searchOnTheFly.Substring(0, this.searchOnTheFly.Length-1);
							this.searchTime = Types.Time.Now;
							this.FirstLine = this.StartToRank(this.searchOnTheFly.ToUpper());
							message.Consumer = this;
							ok = false;
						}
						break;
					default:
						ok = false;
						break;
				}
				
				// Indique que l'�v�nement clavier a �t� consomm�, sinon il sera
				// trait� par le parent, son parent, etc.
				
				if ( ok )
				{
					message.Consumer = this;
					this.searchOnTheFly = "";
				}
			}
			else if ( message.Type == MessageType.KeyPress )
			{
				System.TimeSpan time = Types.Time.Now - this.searchTime;
				double timeSeconds = time.TotalSeconds;
				
				if ( timeSeconds > 0.8 || timeSeconds < 0 )
				{
					this.searchOnTheFly = "";
				}
				
				char key = (char)message.KeyChar;
				int first = -1;
				int sel   = -1;

				if ( (key >= 'a' && key <= 'z') || (key >= 'A' && key <= 'Z') )
				{
					this.searchOnTheFly = string.Format("{0}{1}", this.searchOnTheFly, key);
					this.searchTime = Types.Time.Now;
					first = this.StartToRank(this.searchOnTheFly.ToUpper());
					sel = first;
				}
				else
				{
					this.searchOnTheFly = "";
				}

				if ( key >= '1' && key <= '9' )
				{
					int i = (int) key-'1';
					sel = System.Math.Min(i, this.quickCount-1);
					first = 0;
				}

				if ( first != -1 )
				{
					if ( sel != -1 )
					{
						this.selectedLine = sel;
						this.fontFace = this.RankToFace(sel);
						this.UpdateList();
					}
					this.FirstLine = first;
				}
				
				message.Consumer = this;
			}
		}

		// Premi�re ligne visible.
		protected int FirstLine
		{
			get
			{
				return this.firstLine;
			}

			set
			{
				value = System.Math.Max(value, 0);
				value = System.Math.Min(value, this.fontList.Count-this.samples.Length);

				if ( this.firstLine != value )
				{
					this.firstLine = value;

					this.UpdateScroller();
					this.UpdateList();
				}
			}
		}


		// Met � jour l'ascenseur.
		protected void UpdateScroller()
		{
			this.ignoreChange = true;

			if ( this.samples.Length >= this.fontList.Count )
			{
				this.scroller.Enable = false;
				this.scroller.MinValue = 0M;
				this.scroller.MaxValue = 1M;
				this.scroller.VisibleRangeRatio = 1M;
				this.scroller.Value = 0M;
			}
			else
			{
				this.scroller.Enable = true;
				this.scroller.MinValue = 0M;
				this.scroller.MaxValue = (decimal) (this.fontList.Count - this.samples.Length);
				this.scroller.VisibleRangeRatio = (decimal) ((double)this.samples.Length / this.fontList.Count);
				this.scroller.Resolution = 1M;
				this.scroller.SmallChange = 1M;
				this.scroller.LargeChange = (decimal) this.samples.Length;
				this.scroller.Value = (decimal) this.firstLine;
			}

			this.ignoreChange = false;
		}

		// Met � jour le contenu de la liste.
		public void UpdateList()
		{
			for ( int i=0 ; i<samples.Length ; i++ )
			{
				int ii = this.firstLine+i;
				
				if ( ii < this.fontList.Count )
				{
					this.samples[i].FontIdentity = this.fontList[ii] as Common.OpenType.FontIdentity;

					if ( this.selectedList == null )  // s�lection unique ?
					{
						this.samples[i].SetSelected(ii == this.selectedLine);
					}
					else	// s�lection multiple ?
					{
						string face = this.samples[i].FontIdentity.InvariantFaceName;
						this.samples[i].SetSelected(this.selectedList.Contains(face));
					}
				}
				else
				{
					this.samples[i].FontIdentity = null;
					this.samples[i].SetSelected(false);
				}

				this.samples[i].Separator = (ii == this.quickCount-1 && this.quickCount != this.fontList.Count);
				this.samples[i].Last      = (i == samples.Length-1);
			}
		}


		// La valeur de l'ascenseur a chang�.
		private void ScrollerValueChanged(object sender)
		{
			if ( this.ignoreChange )  return;

			if ( this.firstLine != (int) this.scroller.Value )
			{
				this.firstLine = (int) this.scroller.Value;
				this.UpdateList();
			}
		}


		protected int StartToRank(string start)
		{
			for ( int i=this.quickCount ; i<this.fontList.Count ; i++ )
			{
				Common.OpenType.FontIdentity id = this.fontList[i] as Common.OpenType.FontIdentity;
				if ( id.InvariantFaceName.ToUpper().StartsWith(start) )  return i;
			}
			return -1;
		}

		protected int FaceToRank(string face)
		{
			for ( int i=0 ; i<this.fontList.Count ; i++ )
			{
				Common.OpenType.FontIdentity id = this.fontList[i] as Common.OpenType.FontIdentity;
				if ( id.InvariantFaceName == face )  return i;
			}
			return -1;
		}

		protected string RankToFace(int rank)
		{
			if ( rank == -1 )  return null;
			Common.OpenType.FontIdentity id = this.fontList[rank] as Common.OpenType.FontIdentity;
			return id.InvariantFaceName;
		}


		// G�n�re un �v�nement pour dire que la fermeture est n�cessaire.
		protected virtual void OnSelectionChanged()
		{
			if ( this.SelectionChanged != null )  // qq'un �coute ?
			{
				this.SelectionChanged(this);
			}
		}

		public event Support.EventHandler SelectionChanged;

		
		protected static readonly double				sampleHeight = 30;

		protected string								fontFace;
		protected System.Collections.ArrayList			fontList = null;
		protected int									quickCount = 0;
		protected System.Collections.ArrayList			selectedList = null;
		protected Common.Document.Widgets.FontSample[]	samples;
		protected VScroller								scroller;
		protected int									firstLine = 0;
		protected int									selectedLine = 0;
		protected bool									ignoreChange = false;
		protected Types.Time							searchTime;
		protected string								searchOnTheFly = "";
	}
}
