//	Copyright � 2003-2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.App.Dolphin.MyWidgets
{
	/// <summary>
	/// Permet d'�diter une instruction cod�e.
	/// </summary>
	public class Code : AbstractGroup
	{
		public Code() : base()
		{
			this.PreferredHeight = 20;

			this.valueAddress = -1;

			this.widgetAddress = new StaticText(this);
			this.widgetAddress.ContentAlignment = ContentAlignment.MiddleLeft;
			this.widgetAddress.PreferredHeight = 20;
			this.widgetAddress.PreferredWidth = 25;
			this.widgetAddress.Margins = new Margins(5, 3, 0, 0);
			this.widgetAddress.Dock = DockStyle.Left;

			MyWidgets.Panel groupCodes = new Panel(this);
			groupCodes.PreferredSize = new Size(20*Code.maxCodes, 20);
			groupCodes.Margins = new Margins(0, 0, 0, 0);
			groupCodes.Dock = DockStyle.Left;

			this.widgetCodes = new List<TextField>();
			for (int i=0; i<Code.maxCodes; i++)
			{
				TextField code = new TextField(groupCodes);
				code.IsReadOnly = true;
				code.PreferredSize = new Size(20, 20);
				code.Margins = new Margins(0, -1, 0, 0);
				code.Dock = DockStyle.Left;

				this.widgetCodes.Add(code);
			}

			this.widgetInstruction = new TextFieldEx(this);
			this.widgetInstruction.ButtonShowCondition = ShowCondition.WhenModified;
			this.widgetInstruction.DefocusAction = DefocusAction.AutoAcceptOrRejectEdition;
			this.widgetInstruction.PreferredHeight = 20;
			this.widgetInstruction.PreferredWidth = 150;
			this.widgetInstruction.Margins = new Margins(0, 0, 0, 0);
			this.widgetInstruction.Dock = DockStyle.Left;
			this.widgetInstruction.EditionAccepted += new EventHandler(this.HandleInstructionEditionAccepted);
			this.widgetInstruction.EditionRejected += new EventHandler(this.HandleInstructionEditionRejected);
			this.widgetInstruction.IsFocusedChanged += new EventHandler<Epsitec.Common.Types.DependencyPropertyChangedEventArgs>(this.HandleFieldIsFocusedChanged);
		}

		public Code(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.widgetAddress.Dispose();

				this.widgetInstruction.EditionAccepted -= new EventHandler(this.HandleInstructionEditionAccepted);
				this.widgetInstruction.EditionRejected -= new EventHandler(this.HandleInstructionEditionRejected);
			}

			base.Dispose(disposing);
		}


		public void SetTabIndex(int index)
		{
			//	Sp�cifie l'ordre pour la navigation avec Tab.
			//	Attention, il ne doit pas y avoir 2x les m�mes num�ros, m�me dans des widgets de parents diff�rents !
			this.widgetInstruction.TabIndex = index;
			this.widgetInstruction.TabNavigationMode = TabNavigationMode.None;  // gestion maison, dans MainPanel
		}

		public Components.AbstractProcessor Processor
		{
			//	Processeur �mul� affich�e/modif�e par ce widget.
			get
			{
				return this.processor;
			}
			set
			{
				this.processor = value;
			}
		}

		public CodeAccessor CodeAccessor
		{
			//	CodeAccessor associ� au widget, facultatif.
			get
			{
				return this.codeAccessor;
			}
			set
			{
				this.codeAccessor = value;
			}
		}

		public void SetCode(int address, List<int> codes)
		{
			//	Sp�cifie les codes de l'instruction repr�sent� par ce widget.
			if (this.valueAddress != address)
			{
				this.valueAddress = address;
				this.widgetAddress.Text = string.Concat("<b>", this.valueAddress.ToString("X3"), "</b>");;
			}

			this.valueCodes = new List<int>();
			for (int i=0; i<this.widgetCodes.Count; i++)
			{
				if (i < codes.Count)
				{
					this.valueCodes.Add(codes[i]);

					this.widgetCodes[i].Visibility = true;
					this.widgetCodes[i].Text = codes[i].ToString("X2");
				}
				else
				{
					this.widgetCodes[i].Visibility = false;
					this.widgetCodes[i].Text = "";
				}
			}

			this.widgetInstruction.Text = this.processor.DessassemblyInstruction(this.valueCodes);
		}

		public void GetCode(out int address, List<int> codes)
		{
			//	Retourne les codes de l'instruction repr�sent� par ce widget.
			address = this.valueAddress;

			codes.Clear();
			foreach (int code in this.valueCodes)
			{
				codes.Add(code);
			}
		}


		protected MainPanel MainPanel
		{
			//	Retourne le premier parent de type MainPanel.
			get
			{
				Widget my = this;

				while (my.Parent != null)
				{
					my = my.Parent;
					if (my is MainPanel)
					{
						return my as MainPanel;
					}
				}

				return null;
			}
		}


		protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			Color color = this.BackColor;
			if (!color.IsEmpty)
			{
				graphics.AddFilledRectangle(this.Client.Bounds);
				graphics.RenderSolid(color);  // dessine un fond rouge si MarkPC
			}
		}


		private void HandleInstructionEditionAccepted(object sender)
		{
			//	L'�dition de l'instruction a �t� accept�e.
			this.OnInstructionChanged();
		}

		private void HandleInstructionEditionRejected(object sender)
		{
			//	L'�dition de l'instruction a �t� rejet�e.
		}

		private void HandleFieldIsFocusedChanged(object sender, Common.Types.DependencyPropertyChangedEventArgs e)
		{
			//	La ligne �ditable a pris ou perdu le focus.
			Widget widget = sender as Widget;
			bool focused = (bool) e.NewValue;

			if (focused)  // focus pris ?
			{
				MainPanel mp = this.MainPanel;
				if (mp != null)
				{
					mp.SetDolphinFocusedWidget(widget);
				}
			}
			else  // focus perdu ?
			{
			}
		}


		#region EventHandler
		protected virtual void OnInstructionChanged()
		{
			//	G�n�re un �v�nement pour dire qu'une cellule a �t� s�lectionn�e.
			EventHandler handler = (EventHandler) this.GetUserEventHandler("InstructionChanged");
			if (handler != null)
			{
				handler(this);
			}
		}

		public event Common.Support.EventHandler InstructionChanged
		{
			add
			{
				this.AddUserEventHandler("InstructionChanged", value);
			}
			remove
			{
				this.RemoveUserEventHandler("InstructionChanged", value);
			}
		}
		#endregion


		protected static readonly int			maxCodes = 4;

		protected Components.AbstractProcessor	processor;
		protected CodeAccessor					codeAccessor;
		protected int							valueAddress;
		protected List<int>						valueCodes;

		protected StaticText					widgetAddress;
		protected List<TextField>				widgetCodes;
		protected TextFieldEx					widgetInstruction;
	}
}
