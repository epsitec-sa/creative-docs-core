﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;
using Epsitec.Common.Support;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Widgets.Tiles;
using Epsitec.Cresus.Core.Library;
using Epsitec.Cresus.Core.Business;

using Epsitec.Cresus.Compta.Helpers;

using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Controllers
{
	/// <summary>
	/// Ce contrôleur gère le ruban supérieur de la comptabilité.
	/// </summary>
	public class RibbonController
	{
		public RibbonController(Application app)
		{
			this.app = app;

			this.sectionGroupFrames = new List<FrameBox> ();
			this.sectionIconFrames  = new List<FrameBox> ();
			this.sectionTitleFrames = new List<StaticText> ();
			this.sectionTitles      = new List<FormattedText> ();

			this.ribbonViewMode = RibbonViewMode.Default;
		}


		public void CreateUI(Widget parent)
		{
			//	Construit le faux ruban.
			this.container = new GradientFrameBox
			{
				Parent              = parent,
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				BackColor1          = RibbonController.GetBackgroundColor1 (),
				BackColor2          = RibbonController.GetBackgroundColor2 (),
				IsVerticalGradient  = true,
				BottomPercentOffset = 1.0 - 0.15,  // ombre dans les 15% supérieurs
				Dock                = DockStyle.Top,
				Margins             = new Margins (-1, -1, 0, 0),
			};

			var separator = new Separator
			{
				Parent           = parent,
				PreferredHeight  = 1,
				IsHorizontalLine = true,
				Dock             = DockStyle.Bottom,
			};

			this.sectionGroupFrames.Clear ();
			this.sectionIconFrames.Clear ();
			this.sectionTitleFrames.Clear ();
			this.sectionTitles.Clear ();

			//	|-->
			{
				var section = this.CreateSection (this.container, DockStyle.Left, "Fichier");

				Widget topSection, bottomSection;
				this.CreateSubsections (section, out topSection, out bottomSection);

				topSection.Children.Add (this.CreateButton (Res.Commands.File.New, large: false));
				topSection.Children.Add (this.CreateButton (Res.Commands.File.Save, large: false));

				bottomSection.Children.Add (this.CreateButton (Res.Commands.File.Open, large: false));
				bottomSection.Children.Add (this.CreateButton (Res.Commands.File.SaveAs, large: false));

				section.Children.Add (this.CreateButton (Res.Commands.File.Print));
			}

			{
				var section = this.CreateSection (this.container, DockStyle.Left, "Présentation");

				section.Children.Add (this.CreateButton (Res.Commands.Présentation.Journal));
				section.Children.Add (this.CreateButton (Res.Commands.Présentation.PlanComptable));
				section.Children.Add (this.CreateButton (Res.Commands.Présentation.Balance));
				section.Children.Add (this.CreateButton (Res.Commands.Présentation.Extrait));
				section.Children.Add (this.CreateGap ());

				Widget topSection, bottomSection;
				this.CreateSubsections (section, out topSection, out bottomSection);

				topSection.Children.Add (this.CreateButton (Res.Commands.Présentation.Bilan, large: false));
				topSection.Children.Add (this.CreateButton (Res.Commands.Présentation.PP, large: false));
				topSection.Children.Add (this.CreateButton (Res.Commands.Présentation.Exploitation, large: false));
				topSection.Children.Add (this.CreateButton (Res.Commands.Présentation.Budgets, large: false));

				bottomSection.Children.Add (this.CreateButton (Res.Commands.Présentation.Change, large: false));
				bottomSection.Children.Add (this.CreateButton (Res.Commands.Présentation.RésuméPériodique, large: false));
				bottomSection.Children.Add (this.CreateButton (Res.Commands.Présentation.RésuméTVA, large: false));
				bottomSection.Children.Add (this.CreateButton (Res.Commands.Présentation.DécompteTVA, large: false));

				section.Children.Add (this.CreateGap ());
				this.présentationButton = this.CreateButton (Res.Commands.Présentation.New);
				section.Children.Add (this.présentationButton);
			}

			{
				var section = this.CreateSection (this.container, DockStyle.Left, "Réglages");

				Widget topSection, bottomSection;
				this.CreateSubsections (section, out topSection, out bottomSection);

				topSection.Children.Add (this.CreateButton (Res.Commands.Présentation.Libellés, large: false));
				topSection.Children.Add (this.CreateButton (Res.Commands.Présentation.Modèles, large: false));

				bottomSection.Children.Add (this.CreateButton (Res.Commands.Présentation.Journaux, large: false));
				bottomSection.Children.Add (this.CreateButton (Res.Commands.Présentation.Périodes, large: false));
			}

			{
				Widget topSection1, bottomSection1;
				var section = this.CreateSection (this.container, DockStyle.Left, "Edition");

				this.CreateSubsections (section, out topSection1, out bottomSection1);

				topSection1.Children.Add (this.CreateButton (Res.Commands.Select.Up, large: false));
				bottomSection1.Children.Add (this.CreateButton (Res.Commands.Select.Down, large: false));
				bottomSection1.Children.Add (this.CreateButton (Res.Commands.Select.Home, large: false));

				section.Children.Add (this.CreateButton (Res.Commands.Edit.Accept));
				section.Children.Add (this.CreateButton (Res.Commands.Edit.Cancel));
				section.Children.Add (this.CreateGap ());

				Widget topSection2, bottomSection2;
				this.CreateSubsections (section, out topSection2, out bottomSection2);

				topSection2.Children.Add (this.CreateButton (Res.Commands.Edit.Up, large: false));
				topSection2.Children.Add (this.CreateButton (Res.Commands.Edit.Duplicate, large: false));
				topSection2.Children.Add (this.CreateButton (Res.Commands.Multi.Insert, large: false));
				topSection2.Children.Add (this.CreateButton (Res.Commands.Multi.Up, large: false));
				topSection2.Children.Add (this.CreateButton (Res.Commands.Multi.Swap, large: false));

				bottomSection2.Children.Add (this.CreateButton (Res.Commands.Edit.Down, large: false));
				bottomSection2.Children.Add (this.CreateButton (Res.Commands.Edit.Delete, large: false));
				bottomSection2.Children.Add (this.CreateButton (Res.Commands.Multi.Delete, large: false));
				bottomSection2.Children.Add (this.CreateButton (Res.Commands.Multi.Down, large: false));
				bottomSection2.Children.Add (this.CreateButton (Res.Commands.Multi.Auto, large: false));
			}

			//	<--|
			{
				var section = this.CreateSection (this.container, DockStyle.Right, "Général");

				section.Children.Add (this.CreateButton (Res.Commands.Global.Settings));
			}

			this.UpdateRibbon ();


			//	Bouton 'v'
			var showRibbonButton = new GlyphButton
			{
				Parent        = container.Window.Root,
				Anchor        = AnchorStyles.TopRight,
				PreferredSize = new Size (14, 14),
				Margins       = new Margins (0, -1, -1, 0),
				GlyphShape    = GlyphShape.Menu,
				ButtonStyle   = ButtonStyle.Icon,
			};

			ToolTip.Default.SetToolTip (showRibbonButton, "Mode d'affichage de la barre d'icônes");

			showRibbonButton.Clicked += delegate
			{
				this.ShowRibbonModeMenu (showRibbonButton);
			};
		}

		private IEnumerable<Command> PrésentationCommands
		{
			get
			{
				yield return Res.Commands.Présentation.Modèles;
				yield return Res.Commands.Présentation.Libellés;
				yield return Res.Commands.Présentation.Périodes;
				yield return Res.Commands.Présentation.Journaux;
				yield return Res.Commands.Présentation.Journal;
				yield return Res.Commands.Présentation.PlanComptable;
				yield return Res.Commands.Présentation.Balance;
				yield return Res.Commands.Présentation.Extrait;
				yield return Res.Commands.Présentation.Bilan;
				yield return Res.Commands.Présentation.PP;
				yield return Res.Commands.Présentation.Exploitation;
				yield return Res.Commands.Présentation.Budgets;
				yield return Res.Commands.Présentation.Change;
				yield return Res.Commands.Présentation.RésuméPériodique;
				yield return Res.Commands.Présentation.RésuméTVA;
				yield return Res.Commands.Présentation.DécompteTVA;
			}
		}

		public void PrésentationCommandsUpdate(Command c)
		{
			foreach (var command in this.PrésentationCommands)
			{
				CommandState cs = this.app.CommandContext.GetCommandState (command);
				cs.ActiveState = (command == c) ? ActiveState.Yes : ActiveState.No;
			}
		}


		#region Ribbon mode menu
		private void ShowRibbonModeMenu(Widget parentButton)
		{
			//	Affiche le menu permettant de choisir le mode pour le ruban.
			var menu = new VMenu ();

			this.AddRibbonModeToMenu (menu, "Pas de barre d'icônes",           RibbonViewMode.Hide);
			menu.Items.Add (new MenuSeparator ());
			this.AddRibbonModeToMenu (menu, "Barre d'icônes minimaliste",      RibbonViewMode.Minimal);
			this.AddRibbonModeToMenu (menu, "Barre d'icônes compacte",         RibbonViewMode.Compact);
			menu.Items.Add (new MenuSeparator ());
			this.AddRibbonModeToMenu (menu, "Barre d'icônes standard",         RibbonViewMode.Default);
			this.AddRibbonModeToMenu (menu, "Barre d'icônes aérée",            RibbonViewMode.Large);
			this.AddRibbonModeToMenu (menu, "Barre d'icônes pour grand écran", RibbonViewMode.Hires);

			TextFieldCombo.AdjustComboSize (parentButton, menu, false);

			menu.Host = this.container;
			menu.ShowAsComboList (parentButton, Point.Zero, parentButton);
		}

		private void AddRibbonModeToMenu(VMenu menu, FormattedText text, RibbonViewMode mode)
		{
			bool selected = (this.ribbonViewMode == mode);

			var item = new MenuItem ()
			{
				IconUri       = UIBuilder.GetRadioStateIconUri (selected),
				FormattedText = text,
				Name          = mode.ToString (),
			};

			item.Clicked += delegate
			{
				this.ribbonViewMode = (RibbonViewMode) System.Enum.Parse (typeof (RibbonViewMode), item.Name);
				this.UpdateRibbon ();
			};

			menu.Items.Add (item);
		}
		#endregion


		#region New window menu
		public void ShowNewWindowMenu()
		{
			this.ShowNewWindowMenu (this.présentationButton);
		}

		private void ShowNewWindowMenu(Widget parentButton)
		{
			//	Affiche le menu permettant de choisir le mode pour le ruban.
			var menu = new VMenu ();

			this.AddNewWindowToMenu (menu, Res.CommandIds.NouvellePrésentation.Balance);
			this.AddNewWindowToMenu (menu, Res.CommandIds.NouvellePrésentation.Extrait);
			this.AddNewWindowToMenu (menu, Res.CommandIds.NouvellePrésentation.Bilan);
			this.AddNewWindowToMenu (menu, Res.CommandIds.NouvellePrésentation.PP);
			this.AddNewWindowToMenu (menu, Res.CommandIds.NouvellePrésentation.Exploitation);
			this.AddNewWindowToMenu (menu, Res.CommandIds.NouvellePrésentation.Change);
			this.AddNewWindowToMenu (menu, Res.CommandIds.NouvellePrésentation.RésuméPériodique);
			this.AddNewWindowToMenu (menu, Res.CommandIds.NouvellePrésentation.RésuméTVA);
			this.AddNewWindowToMenu (menu, Res.CommandIds.NouvellePrésentation.DécompteTVA);

			TextFieldCombo.AdjustComboSize (parentButton, menu, false);

			menu.Host = this.container;
			menu.ShowAsComboList (parentButton, Point.Zero, parentButton);
		}

		private void AddNewWindowToMenu(VMenu menu, Druid commandId)
		{
			var item = new MenuItem ()
			{
				CommandId = commandId,
			};

			menu.Items.Add (item);
		}
		#endregion

	
		private void UpdateRibbon()
		{
			//	Met à jour le faux ruban en fonction du RibbonViewMode en cours.
			var mode = this.ribbonViewMode;

			if (mode == RibbonViewMode.Hide)
			{
				this.container.Visibility = false;
			}
			else
			{
				this.container.Visibility = true;

				double  frameGap    = 0;
				Margins iconMargins = Margins.Zero;
				double  buttonWidth = 0;
				double  gapWidth    = 0;
				double  titleHeight = 0;
				double  titleSize   = 0;

				switch (mode)
				{
					case RibbonViewMode.Minimal:
						frameGap    = -1;  // les sections se chevauchent
						iconMargins = new Margins (0);
						buttonWidth = RibbonController.ButtonLargeWidth;
						gapWidth    = 3;
						titleHeight = 0;
						titleSize   = 0;
						break;

					case RibbonViewMode.Compact:
						frameGap    = -1;  // les sections se chevauchent
						iconMargins = new Margins (3);
						buttonWidth = RibbonController.ButtonLargeWidth;
						gapWidth    = 5;
						titleHeight = 0;
						titleSize   = 0;
						break;

					case RibbonViewMode.Default:
						frameGap    = 2;
						iconMargins = new Margins (3, 3, 3, 3-1);
						buttonWidth = RibbonController.ButtonLargeWidth;
						gapWidth    = 6;
						titleHeight = 11;
						titleSize   = 8;
						break;

					case RibbonViewMode.Large:
						frameGap    = 3;
						iconMargins = new Margins (3, 3, 3, 3-1);
						buttonWidth = RibbonController.ButtonLargeWidth+2;
						gapWidth    = 8;
						titleHeight = 14;
						titleSize   = 10;
						break;

					case RibbonViewMode.Hires:
						frameGap    = 4;
						iconMargins = new Margins (5, 5, 5, 5-1);
						buttonWidth = RibbonController.ButtonLargeWidth+6;
						gapWidth    = 10;
						titleHeight = 18;
						titleSize   = 12;
						break;
				}

				for (int i = 0; i < this.sectionGroupFrames.Count; i++)
				{
					//	Met à jour le panneau du groupe de la section.
					{
						var groupFrame = this.sectionGroupFrames[i];

						double leftMargin  = (groupFrame.Dock == DockStyle.Right) ? frameGap : 0;
						double rightMargin = (groupFrame.Dock == DockStyle.Left) ? frameGap : 0;

						groupFrame.Margins = new Margins (leftMargin, rightMargin, -1, -1);
					}

					//	Met à jour le panneau des icônes de la section.
					{
						var iconFrame = this.sectionIconFrames[i];

						iconFrame.Padding = iconMargins;

						foreach (var gap in iconFrame.FindAllChildren ().Where (x => x.Name == "Gap"))
						{
							gap.PreferredWidth = gapWidth;
						}

						foreach (var widget in iconFrame.FindAllChildren ())
						{
							if (widget is IconButton || widget is RibbonIconButton)
							{
								var button = widget as IconButton;

								if (button.PreferredIconSize.Width == RibbonController.IconSmallWidth)
								{
									button.PreferredSize = new Size (buttonWidth/2, buttonWidth/2);
								}
								else
								{
									button.PreferredSize = new Size (buttonWidth, buttonWidth);
								}
							}
						}
					}

					//	Met à jour le titre de la section.
					{
						var titleFrame = this.sectionTitleFrames[i];
						var title = this.sectionTitles[i].ApplyFontSize (titleSize).ApplyFontColor (Color.FromBrightness (1.0)).ApplyBold ();

						titleFrame.Visibility      = (mode != RibbonViewMode.Minimal && mode != RibbonViewMode.Compact);
						titleFrame.FormattedText   = title;
						titleFrame.PreferredHeight = titleHeight;
					}
				}
			}
		}

	
		private Widget CreateSection(Widget frame, DockStyle dockStyle, FormattedText description)
		{
			//	Crée une section dans le faux ruban.
			var groupFrame = new FrameBox
			{
				Parent              = frame,
				DrawFullFrame       = true,
				BackColor           = RibbonController.GetSectionBackgroundColor (),
				ContainerLayoutMode = ContainerLayoutMode.VerticalFlow,
				PreferredWidth      = 10,
				Dock                = dockStyle,
			};

			var iconFrame = new FrameBox
			{
				Parent              = groupFrame,
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth      = 10,
				Dock                = DockStyle.Fill,
			};

			var titleFrame = new StaticText
			{
				Parent           = groupFrame,
				ContentAlignment = ContentAlignment.MiddleCenter,
				TextBreakMode    = TextBreakMode.Ellipsis | TextBreakMode.Split | TextBreakMode.SingleLine,
				BackColor        = RibbonController.GetTitleBackgroundColor (),
				PreferredWidth   = 10,
				Dock             = DockStyle.Bottom,
				Margins          = new Margins (1, 1, 0, 1),
			};

			this.sectionGroupFrames.Add (groupFrame);
			this.sectionIconFrames.Add (iconFrame);
			this.sectionTitleFrames.Add (titleFrame);
			this.sectionTitles.Add (description);

			return iconFrame;
		}

		private void CreateSubsections(Widget section, out Widget topSection, out Widget bottomSection)
		{
			//	Crée deux sous-sections dans le faux ruban.
			var frame = new FrameBox
			{
				Parent              = section,
				ContainerLayoutMode = ContainerLayoutMode.VerticalFlow,
				PreferredWidth      = 10,
				Dock                = DockStyle.StackBegin,
			};

			topSection = new FrameBox
			{
				Parent              = frame,
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth      = 10,
				Dock                = DockStyle.Top,
			};

			bottomSection = new FrameBox
			{
				Parent              = frame,
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth      = 10,
				Dock                = DockStyle.Bottom,
			};
		}

		private IconButton CreateButton(Command command = null, DockStyle dockStyle = DockStyle.StackBegin, CommandEventHandler handler = null, bool large = true)
		{
			if (command != null && handler != null)
			{
				this.app.CommandDispatcher.Register (command, handler);
			}

			double buttonWidth = large ? RibbonController.ButtonLargeWidth : RibbonController.ButtonLargeWidth/2;
			double iconWidth   = large ? RibbonController.IconLargeWidth   : RibbonController.IconSmallWidth;

			return new IconButton
			{
				CommandObject       = command,
				PreferredIconSize   = new Size (iconWidth, iconWidth),
				PreferredSize       = new Size (buttonWidth, buttonWidth),
				Dock                = dockStyle,
				Name                = (command == null) ? null : command.Name,
				VerticalAlignment   = VerticalAlignment.Top,
				HorizontalAlignment = HorizontalAlignment.Center,
				AutoFocus           = false,
			};
		}

		private Widget CreateGap()
		{
			var gap = new FrameBox
			{
				Name = "Gap",
				Dock = DockStyle.StackBegin,
			};

			return gap;
		}


		#region Color manager
		private static Color GetBackgroundColor1()
		{
			//	Couleur pour l'ombre en haut des zones libres du ruban.
			return RibbonController.GetColor (RibbonController.GetBaseColor (), saturation: 0.06, value: 0.9);
		}

		private static Color GetBackgroundColor2()
		{
			//	Couleur pour le fond des zones libres du ruban.
			return RibbonController.GetColor (RibbonController.GetBaseColor (), saturation: 0.06, value: 0.7);
		}

		private static Color GetSectionBackgroundColor()
		{
			//	Couleur pour le fond d'une section du ruban.
			return RibbonController.GetColor (RibbonController.GetBaseColor (), saturation: 0.02, value: 0.95);
		}

		private static Color GetTitleBackgroundColor()
		{
			//	Couleur pour le fond du titre d'une section du ruban.
			return RibbonController.GetColor (RibbonController.GetBaseColor (), saturation: 0.2, value: 0.7);
		}

		private static Color GetBaseColor()
		{
			//	Couleur de base pour le ruban, dont on utilise la teinte (hue).
			IAdorner adorner = Common.Widgets.Adorners.Factory.Active;
			return adorner.ColorBorder;
		}

		private static Color GetColor(Color color, double? saturation = null, double? value = null)
		{
			//	Retourne une couleur en forçant éventuellement la saturation et la valeur.
			double h, s, v;
			Color.ConvertRgbToHsv (color.R, color.G, color.B, out h, out s, out v);

			if (saturation.HasValue)
			{
				s = saturation.Value;
			}

			if (value.HasValue)
			{
				v = value.Value;
			}

			double r, g, b;
			Color.ConvertHsvToRgb (h, s, v, out r, out g, out b);

			return new Color (r, g, b);
		}
		#endregion




		private enum RibbonViewMode
		{
			Hide,		// pas de ruban
			Minimal,	// sans titres et très compact
			Compact,	// sans titres et compact
			Default,	// mode standard
			Large,		// aéré
			Hires,		// pour grand écran
		}


		private const double ButtonLargeWidth	= 2 * ((RibbonController.IconLargeWidth + 1) / 2 + 5);
		private const double ButtonSmallWidth	= 2 * ((RibbonController.IconSmallWidth + 1) / 2 + 5);

		private const int IconSmallWidth		= 20;
		private const int IconLargeWidth		= 32;

		private readonly Application				app;
		private readonly List<FrameBox>				sectionGroupFrames;
		private readonly List<FrameBox>				sectionIconFrames;
		private readonly List<StaticText>			sectionTitleFrames;
		private readonly List<FormattedText>		sectionTitles;

		private Widget								container;
		private RibbonViewMode						ribbonViewMode;
		private IconButton							présentationButton;
	}
}
