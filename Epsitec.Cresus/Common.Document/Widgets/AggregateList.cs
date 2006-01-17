using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Document.Widgets
{
	/// <summary>
	/// AggregateList est un widget "CellTable" pour les agr�gats.
	/// </summary>
	public class AggregateList : CellTable
	{
		public AggregateList()
		{
			this.StyleH |= CellArrayStyle.ScrollNorm;
			this.StyleH |= CellArrayStyle.Header;
			this.StyleH |= CellArrayStyle.Separator;
			this.StyleH |= CellArrayStyle.Mobile;

			this.StyleV |= CellArrayStyle.ScrollNorm;
			this.StyleV |= CellArrayStyle.Separator;
			this.StyleV |= CellArrayStyle.SelectCell;

			this.DefHeight = 32;
			this.headerHeight = 16;
		}

		public Document Document
		{
			get
			{
				return this.document;
			}

			set
			{
				this.document = value;
			}
		}

		public UndoableList List
		{
			get
			{
				return this.list;
			}

			set
			{
				if ( value == null )
				{
					this.list = new UndoableList(this.document, UndoableListType.AggregatesChildrens);
				}
				else
				{
					this.list = value;
				}
			}
		}

		public bool HScroller
		{
			get
			{
				return (this.StyleH & CellArrayStyle.ScrollNorm) != 0;
			}

			set
			{
				if ( value )
				{
					this.StyleH |= CellArrayStyle.ScrollNorm;
					this.StyleH |= CellArrayStyle.Mobile;
				}
				else
				{
					this.StyleH &= ~CellArrayStyle.ScrollNorm;
					this.StyleH &= ~CellArrayStyle.Mobile;
				}
			}
		}

		public bool VScroller
		{
			get
			{
				return (this.StyleV & CellArrayStyle.ScrollNorm) != 0;
			}

			set
			{
				if ( value )
				{
					this.StyleV |= CellArrayStyle.ScrollNorm;
				}
				else
				{
					this.StyleV &= ~CellArrayStyle.ScrollNorm;
				}
			}
		}

		public int ExcludeRank
		{
			//	Ligne �ventuelle � exclure.
			get
			{
				return this.excludeRank;
			}

			set
			{
				this.excludeRank = value;
			}
		}

		public bool IsDeep
		{
			//	Attributs cherch�s en profondeur, dans les enfants.
			get
			{
				return this.isDeep;
			}

			set
			{
				this.isDeep = value;
			}
		}

		public bool IsNoneLine
		{
			//	Premi�re ligne avec <aucun>.
			get
			{
				return this.isNoneLine;
			}

			set
			{
				this.isNoneLine = value;
			}
		}

		public bool IsHiliteColumn
		{
			//	Premi�re colonne pour les mises en �vidences.
			get
			{
				return this.isHiliteColumn;
			}

			set
			{
				this.isHiliteColumn = value;
				if ( this.isHiliteColumn )  this.IsOrderColumn = false;
			}
		}

		public bool IsOrderColumn
		{
			//	Premi�re colonne pour les num�ros d'ordre.
			get
			{
				return this.isOrderColumn;
			}

			set
			{
				this.isOrderColumn = value;
				if ( this.isOrderColumn )  this.isHiliteColumn = false;
			}
		}

		public bool IsChildrensColumn
		{
			//	Colonne pour les enfants.
			get
			{
				return this.isChildrensColumn;
			}

			set
			{
				this.isChildrensColumn = value;
			}
		}

		public bool IsInitialSelection
		{
			//	S�lection initiale.
			get
			{
				return this.isInitialSelection;
			}

			set
			{
				this.isInitialSelection = value;
			}
		}

		public int SelectedPropertyRow
		{
			//	Retourne le rang de la ligne s�lectionn�.
			get
			{
				int nc = this.NameColumn;

				for ( int i=0 ; i<this.Rows ; i++ )
				{
					if ( this.IsCellSelected(i, nc) )
					{
						return i;
					}
				}
				return -1;
			}
		}

		public void UpdateContent()
		{
			//	Met � jour le contenu de la table.
			System.Diagnostics.Debug.Assert(this.document != null);
			System.Diagnostics.Debug.Assert(this.list != null);
			this.typesDirty = true;

			int fix = this.FixColumns;
			int rows = this.list.Count;
			int initialColumns = this.Columns;
			this.SetArraySize(fix+1, rows);
			int i;

			if ( initialColumns != this.Columns )
			{
				i = 0;
				if ( this.isHiliteColumn )     this.SetWidthColumn(i++,  12);
				if ( this.isOrderColumn )      this.SetWidthColumn(i++,  20);
				                               this.SetWidthColumn(i++, 125);  // noms
				if ( this.isChildrensColumn )  this.SetWidthColumn(i++,  20);
				                               this.SetWidthColumn(i++,  60);  // �chantillons
			}

			i = 0;
			if ( this.isHiliteColumn || this.isOrderColumn )
			{
				this.SetHeaderTextH(i++, "");
			}

			this.SetHeaderTextH(i++, Res.Strings.Aggregates.Header.Name);
			
			if ( this.isChildrensColumn )
			{
				this.SetHeaderTextH(i, Misc.Image("AggregateChildrens"));
				ToolTip.Default.SetToolTip(this.FindButtonH(i), Res.Strings.Panel.AggregateChildrens.Label.Name);
			}

			for ( i=0 ; i<rows ; i++ )
			{
				this.FillRow(i);
				this.UpdateRow(i);
			}
		}

		protected void FillRow(int row)
		{
			//	Peuple une ligne de la table, si n�cessaire.
			int nc = this.NameColumn;
			int fix = this.FixColumns;

			if ( this.isHiliteColumn )
			{
				if ( this[0, row].IsEmpty )
				{
					GlyphButton gb = new GlyphButton();
					gb.ButtonStyle = ButtonStyle.None;
					gb.Dock = DockStyle.Fill;
					this[0, row].Insert(gb);
				}
			}

			if ( this.isOrderColumn )
			{
				if ( this[0, row].IsEmpty )
				{
					StaticText st = new StaticText();
					st.Alignment = ContentAlignment.MiddleCenter;
					st.Dock = DockStyle.Fill;
					st.DockMargins = new Margins(2, 2, 0, 0);
					this[0, row].Insert(st);
				}
			}

			if ( this[nc, row].IsEmpty )
			{
				StaticText st = new StaticText();
				st.Alignment = ContentAlignment.MiddleLeft;
				st.Dock = DockStyle.Fill;
				st.DockMargins = new Margins(4, 4, 0, 0);
				this[nc, row].Insert(st);
			}

			if ( this.isChildrensColumn )
			{
				if ( this[fix-1, row].IsEmpty )
				{
					StaticText st = new StaticText();
					st.Alignment = ContentAlignment.MiddleCenter;
					st.Dock = DockStyle.Fill;
					st.DockMargins = new Margins(2, 2, 0, 0);
					this[fix-1, row].Insert(st);
				}
			}

			if ( this[fix, row].IsEmpty )
			{
				Sample sm = new Sample();
				sm.Document = this.document;
				sm.Dock = DockStyle.Fill;
				this[fix, row].Insert(sm);
			}
		}

		public void UpdateRow(int row)
		{
			//	Met � jour le contenu d'une ligne de la table.
			System.Diagnostics.Debug.Assert(this.document != null);
			System.Diagnostics.Debug.Assert(this.list != null);
			int rank = this.RowToRank(row);
			Properties.Aggregate agg = null;
			if ( rank != -1 )
			{
				agg = this.list[rank] as Properties.Aggregate;
			}
			bool selected = (rank == this.list.Selected && this.isInitialSelection);
			int nc = this.NameColumn;
			int fix = this.FixColumns;
			GlyphButton gb;
			StaticText st;
			Sample sm;

			if ( this.isHiliteColumn )
			{
				gb = this[0, row].Children[0] as GlyphButton;
				gb.GlyphShape = GlyphShape.None;
				this[0, row].IsHilite = false;
			}

			if ( this.isOrderColumn )
			{
				st = this[0, row].Children[0] as StaticText;
				st.Text = (row+1).ToString();
				this.SelectCell(0, row, selected);
			}

			st = this[nc, row].Children[0] as StaticText;
			st.Text = (agg==null) ? Res.Strings.Aggregates.NoneLine : agg.AggregateName;
			this.SelectCell(nc, row, selected);

			if ( this.isChildrensColumn )
			{
				string text = "";
				if ( agg != null )
				{
					int count = agg.Childrens.Count;
					if ( count != 0 )
					{
						text = count.ToString();
					}
				}
				st = this[fix-1, row].Children[0] as StaticText;
				st.Text = text;
				this.SelectCell(fix-1, row, selected);
			}

			sm = this[fix, row].Children[0] as Sample;
			if ( agg == null )
			{
				sm.Property = null;
			}
			else
			{
				if ( this.isDeep )
				{
					//?sm.Property = agg.Property(this.types[i], true);
				}
				else
				{
					//?sm.Property = agg.Property(this.types[i], false);
					//?sm.Dots = (agg.Property(this.types[i], true) != null);
				}
			}
			this.SelectCell(fix, row, selected);
			sm.Invalidate();
		}

		public void HiliteRow(int row, bool hilite)
		{
			//	Hilite une ligne de la table.
			System.Diagnostics.Debug.Assert(this.list != null);
			if ( !this.isHiliteColumn )  return;

			if ( this[0, row].IsHilite != hilite )
			{
				this[0, row].IsHilite = hilite;
				GlyphButton gb = this[0, row].Children[0] as GlyphButton;
				gb.GlyphShape = hilite ? GlyphShape.ArrowRight : GlyphShape.None;
			}
		}


		public int RankToRow(int rank)
		{
			//	Conversion d'un rang d'agr�gat en num�ro de ligne.
			if ( this.isNoneLine )  rank ++;
			if ( this.excludeRank != -1 && rank-1 == this.excludeRank )  return -1;
			if ( this.excludeRank != -1 && rank > this.excludeRank )  rank --;
			return rank;
		}

		public int RowToRank(int row)
		{
			//	Conversion d'un num�ro de ligne en rang d'agr�gat.
			if ( this.isNoneLine )  row --;
			if ( this.excludeRank != -1 && row >= this.excludeRank )  row ++;
			return row;
		}


		protected int NameColumn
		{
			//	Retourne le rang de la colonne pour le nom.
			get
			{
				return (this.isHiliteColumn || this.IsOrderColumn) ? 1 : 0;
			}
		}

		protected int FixColumns
		{
			//	Retourne le nombre de colonnes initiales fixes.
			get
			{
				int fix = 1;
				if ( this.isHiliteColumn    )  fix ++;
				if ( this.IsOrderColumn     )  fix ++;
				if ( this.isChildrensColumn )  fix ++;
				return fix;
			}
		}


		protected Document						document;
		protected UndoableList					list;
		protected int							excludeRank = -1;
		protected bool							isDeep = false;
		protected bool							isNoneLine = false;
		protected bool							isHiliteColumn = true;
		protected bool							isOrderColumn = false;
		protected bool							isChildrensColumn = true;
		protected bool							isInitialSelection = true;
		protected bool							typesDirty = true;
	}
}
