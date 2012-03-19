﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;
using Epsitec.Common.Support;

using Epsitec.Cresus.Compta.Accessors;
using Epsitec.Cresus.Compta.Entities;
using Epsitec.Cresus.Compta.Widgets;
using Epsitec.Cresus.Compta.Helpers;
using Epsitec.Cresus.Compta.Fields.Controllers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Controllers
{
	/// <summary>
	/// Ce contrôleur gère le pied de page pour l'édition de la comptabilité.
	/// </summary>
	public class JournalEditorController : AbstractEditorController
	{
		public JournalEditorController(AbstractController controller)
			: base (controller)
		{
		}


		public override void CreateUI(FrameBox parent, System.Action updateArrayContentAction)
		{
			var editor = this.CreateEditorUI (parent);

			this.footer = new CustomFrameBox
			{
				Parent = editor,
				Dock   = DockStyle.Fill,
			};

			//	Crée les boîtes.
			this.infoFrameBox = new FrameBox
			{
				Parent          = this.footer,
				PreferredHeight = 39,
				Dock            = DockStyle.Bottom,
				Margins         = new Margins (0, 0, 0, 0),
			};

			this.infoFrameSeparator = new FrameBox
			{
				Parent          = this.footer,
				PreferredHeight = 5,
				Dock            = DockStyle.Bottom,
				Margins         = new Margins (0, 0, 0, 0),
			};

			{
				var linesBox = new FrameBox
				{
					Parent              = this.footer,
					ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
					Dock                = DockStyle.Bottom,
				};

				this.linesContainer = new TabCatcherFrameBox
				{
					Parent  = linesBox,
					Dock    = DockStyle.Fill,
				};

				this.linesContainer.TabPressed += new TabCatcherFrameBox.TabPressedEventHandler (this.HandleLinesContainerTabPressed);

				this.scroller = new VScroller
				{
					Parent     = linesBox,
					IsInverted = true,  // zéro en haut
					Dock       = DockStyle.Right,
				};

				this.scroller.ValueChanged += delegate
				{
					this.ChangeScroller ();
				};
			}

			//	Crée les lignes éditables.
			this.CreateLineUI (this.linesContainer);

			//	Crée les informations.
			this.débitInfoSeparator = new Separator
			{
				Parent          = this.infoFrameSeparator,
				IsVerticalLine  = true,
				DrawFrameWidth  = 3,
				PreferredHeight = 5,
				Dock            = DockStyle.Left,
			};

			this.créditInfoSeparator = new Separator
			{
				Parent          = this.infoFrameSeparator,
				IsVerticalLine  = true,
				DrawFrameWidth  = 3,
				PreferredHeight = 5,
				Dock            = DockStyle.Left,
			};

			this.débitInfoFrame = new FrameBox
			{
				Parent        = this.infoFrameBox,
				DrawFullFrame = true,
				BackColor     = UIBuilder.InfoColor,
				Dock          = DockStyle.Left,
				Margins       = new Margins (0, 1, 0, 0),
			};

			this.créditInfoFrame = new FrameBox
			{
				Parent        = this.infoFrameBox,
				DrawFullFrame = true,
				BackColor     = UIBuilder.InfoColor,
				Dock          = DockStyle.Left,
				Margins       = new Margins (0, 1, 0, 0),
			};

			UIBuilder.CreateInfoCompte (this.débitInfoFrame);
			UIBuilder.CreateInfoCompte (this.créditInfoFrame);

			base.CreateUI (this.footer, updateArrayContentAction);
		}

		private void CreateLineUI(Widget parent)
		{
			this.fieldControllers.Add (new List<AbstractFieldController> ());

			var editorFrame = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = 20,
				Dock            = DockStyle.Top,
				Margins         = new Margins (0, 0, 1, 0),
			};

			this.linesFrames.Add (editorFrame);
			int line = this.linesFrames.Count - 1;
			int tabIndex = 0;

			editorFrame.TabIndex = line+1;

			//	Met éventuellement en évidence le fond de la ligne.
			if (line < this.dataAccessor.EditionLine.Count && this.dataAccessor.EditionLine[line].GetText (ColumnType.Hilited) == "1")
			{
				editorFrame.BackColor = this.dataAccessor.IsModification ? UIBuilder.ModificationHiliteColor : UIBuilder.CreationHiliteColor;
			}

			//	Colle la ligne à la précédente s'il y a lieu.
			var type = this.GetTypeEcriture (line);
			if (type == TypeEcriture.CodeTVA)
			{
				editorFrame.Margins = new Margins (0, 0, -1, 2);  // la hauteur totale doit rester identique (1+0 = -1+2) !
			}

			var comptes = this.compta.PlanComptable.Where (x => x.Type != TypeDeCompte.Groupe);

			foreach (var mapper in this.columnMappers.Where (x => x.Show))
			{
				AbstractFieldController field;

				if (mapper.Column == ColumnType.Date)
				{
					field = new DateFieldController (this.controller, line, mapper, this.HandleClearFocus, this.HandleSetFocus, this.EditorTextChanged);
					field.CreateUI (editorFrame);
				}
				else if (mapper.Column == ColumnType.Débit)
				{
					field = new AutoCompleteFieldController (this.controller, line, mapper, this.HandleClearFocus, this.HandleSetFocus, this.EditorTextChanged);
					field.CreateUI (editorFrame);

					UIBuilder.UpdateAutoCompleteTextField (field.EditWidget as AutoCompleteTextField, comptes);
				}
				else if (mapper.Column == ColumnType.Crédit)
				{
					field = new AutoCompleteFieldController (this.controller, line, mapper, this.HandleClearFocus, this.HandleSetFocus, this.EditorTextChanged);
					field.CreateUI (editorFrame);

					UIBuilder.UpdateAutoCompleteTextField (field.EditWidget as AutoCompleteTextField, comptes);
				}
				else if (mapper.Column == ColumnType.Libellé)
				{
					field = new AutoCompleteFieldController (this.controller, line, mapper, this.HandleClearFocus, this.HandleSetFocus, this.EditorTextChanged);
					field.CreateUI (editorFrame);
					(field.EditWidget as AutoCompleteTextField).AcceptFreeText = true;

					UIBuilder.UpdateAutoCompleteTextField (field.EditWidget as AutoCompleteTextField, this.compta.GetLibellésDescriptions (this.période).ToArray ());

					this.CreateButtonModèleUI (field, line);
				}
				else if (mapper.Column == ColumnType.LibelléTVA)
				{
					field = new StaticTextController (this.controller, line, mapper, this.HandleClearFocus, this.HandleSetFocus, this.EditorTextChanged);
					field.CreateUI (editorFrame);
				}
				else if (mapper.Column == ColumnType.CodeTVA)
				{
					field = new AutoCompleteFieldController (this.controller, line, mapper, this.HandleClearFocus, this.HandleSetFocus, this.EditorTextChanged);
					field.CreateUI (editorFrame);

					UIBuilder.UpdateAutoCompleteTextField (field.EditWidget as AutoCompleteTextField, '#', this.compta.CodesTVAMenuDescription);
				}
				else if (mapper.Column == ColumnType.TauxTVA)
				{
					field = new AutoCompleteFieldController (this.controller, line, mapper, this.HandleClearFocus, this.HandleSetFocus, this.EditorTextChanged);
					field.CreateUI (editorFrame);
				}
				else if (mapper.Column == ColumnType.Journal)
				{
					field = new AutoCompleteFieldController (this.controller, line, mapper, this.HandleClearFocus, this.HandleSetFocus, this.EditorTextChanged);
					field.CreateUI (editorFrame);

					var journaux = this.compta.Journaux.Select (x => x.Nom);
					UIBuilder.UpdateAutoCompleteTextField (field.EditWidget as AutoCompleteTextField, journaux.ToArray ());
				}
				else if (mapper.Column == ColumnType.Type)
				{
					field = new FixedTextController (this.controller, line, mapper, this.HandleClearFocus, this.HandleSetFocus, this.EditorTextChanged);
					field.CreateUI (editorFrame);

					(field as FixedTextController).TextConverter = x => ComptaEcritureEntity.GetShortType (Converters.StringToTypeEcriture (x));
				}
				else
				{
					field = new TextFieldController (this.controller, line, mapper, this.HandleClearFocus, this.HandleSetFocus, this.EditorTextChanged);
					field.CreateUI (editorFrame);
				}

				if (mapper.Column == ColumnType.Montant   ||
					mapper.Column == ColumnType.MontantTTC)
				{
					field.EditWidget.ContentAlignment = ContentAlignment.MiddleRight;
				}

				field.Box.TabIndex = ++tabIndex;

				this.fieldControllers[line].Add (field);
			}
		}

		private void ResetLineUI()
		{
			//	Recrée une seule ligne.
			using (this.controller.IgnoreChanges.Enter ())
			{
				this.linesContainer.Children.Clear ();
				this.fieldControllers.Clear ();
				this.linesFrames.Clear ();

				this.CreateLineUI (this.linesContainer);
			}
		}

		protected override FormattedText GetOperationDescription(bool modify)
		{
			return modify ? "Modification d'une écriture :" : "Création d'une écriture :";
		}


		public override void AcceptAction()
		{
			if (!this.controller.GetCommandEnable (Res.Commands.Edit.Accept))
			{
				return;
			}

			int lineToSelect = -1;
			ColumnType columnToSelect = ColumnType.None;

			if (this.Complete (ref lineToSelect, ref columnToSelect))  // essaie de compléter l'écriture
			{
				this.UpdateEditorContent ();

				if (lineToSelect != -1 && columnToSelect != ColumnType.None)
				{
					this.selectedLine = lineToSelect;
					this.EditorSelect (columnToSelect);
				}

				return;
			}

			base.AcceptAction();

			this.UpdateLibellés ();
		}

		private void UpdateLibellés()
		{
			//	Met à jour les libellés usuels dans les widgets combo pour les libellés.
			var libellés = this.compta.GetLibellésDescriptions (this.période).ToArray ();

			for (int line = 0; line < this.fieldControllers.Count; line++)
			{
				var field = this.GetFieldController (ColumnType.Libellé, line);
				UIBuilder.UpdateAutoCompleteTextField (field.EditWidget as AutoCompleteTextField, libellés);
			}
		}


		private void ClearLineHilite()
		{
			for (int line = 0; line < this.dataAccessor.CountEditedRow; line++)
			{
				this.dataAccessor.EditionLine[line].SetText (ColumnType.Hilited, "0");
			}
		}

		private bool Complete(ref int lineToSelect, ref ColumnType columnToSelect)
		{
			//	Avant de créer réellement l'écriture, on regarde si elle peut être "complétée", c'est-à-dire:
			//		- Si on a donné 1000 / ..., on complète avec 3 lignes pour démarrer une écriture multiple.
			//		- Si on a donné un compte avec code TVA, on complète par une deuxième ligne avec code/taux.
			//	Dans tous les cas, si l'écriture est complétée, on ne la crée pas.

			this.ClearLineHilite ();

			if (!this.isMulti && !this.IsTVA (0) && !this.dataAccessor.IsModification && this.dataAccessor.CountEditedRow == 1)
			{
				if (this.IsDébitMulti (0) || this.IsCréditMulti (0))
				{
					this.isMulti = true;
					this.PrepareFirstMulti (ref lineToSelect, ref columnToSelect);
					return true;
				}
			}

			bool complete = false;

			int line = 0;
			while (line < this.dataAccessor.CountEditedRow)
			{
				var type = this.GetTypeEcriture (line);

				if (type == TypeEcriture.Nouveau ||  // ligne fraichement créée ?
					type == TypeEcriture.Vide    )
				{
					if (this.IsTVA (line))
					{
						line += this.ExplodeForTVA (line, ref lineToSelect, ref columnToSelect);
						complete = true;
					}
					else
					{
						if (!(this.dataAccessor.EditionLine[line] as JournalEditionLine).IsEmptyLine)
						{
							this.dataAccessor.EditionLine[line].SetText (ColumnType.Type, Converters.TypeEcritureToString (TypeEcriture.Normal));
						}
						line++;
					}
				}
				else
				{
					line++;
				}
			}

			return complete;
		}

		private void PrepareFirstMulti(ref int lineToSelect, ref ColumnType columnToSelect)
		{
			//	Met à jour l'interface pour permettre de créer une écriture multiple, lorsqu'on passe de 1 ligne à 3.
			bool isDébitMulti = this.IsDébitMulti (0);
			var multiActiveColumn   =  isDébitMulti ? ColumnType.Crédit : ColumnType.Débit;
			var multiInactiveColumn = !isDébitMulti ? ColumnType.Crédit : ColumnType.Débit;

			this.dataAccessor.InsertEditionLine (-1);  // 2ème ligne
			this.dataAccessor.InsertEditionLine (-1);  // contrepartie

			//	Met à jour les données de la 1ère ligne.
			this.dataAccessor.EditionLine[0].SetText (ColumnType.Type,             Converters.TypeEcritureToString (TypeEcriture.Nouveau));

			//	Met à jour les données de la 2ème ligne.
			this.dataAccessor.EditionLine[1].SetText (ColumnType.Type,             Converters.TypeEcritureToString (TypeEcriture.Nouveau));
			this.dataAccessor.EditionLine[1].SetText (ColumnType.Date,             this.dataAccessor.EditionLine[0].GetText (ColumnType.Date));
			this.dataAccessor.EditionLine[1].SetText (multiInactiveColumn,         JournalDataAccessor.multi);
			this.dataAccessor.EditionLine[1].SetText (ColumnType.Montant,          Converters.MontantToString (0));
			this.dataAccessor.EditionLine[1].SetText (ColumnType.Journal,          this.dataAccessor.EditionLine[0].GetText (ColumnType.Journal));
			this.dataAccessor.EditionLine[1].SetText (ColumnType.Hilited,          "1");
																				   
			//	Met à jour les données de la contrepartie.						   
			this.dataAccessor.EditionLine[2].SetText (ColumnType.Type,             Converters.TypeEcritureToString (TypeEcriture.Nouveau));
			this.dataAccessor.EditionLine[2].SetText (ColumnType.Date,             this.dataAccessor.EditionLine[0].GetText (ColumnType.Date));
			this.dataAccessor.EditionLine[2].SetText (multiActiveColumn,           JournalDataAccessor.multi);
			this.dataAccessor.EditionLine[2].SetText (ColumnType.Libellé,          "Total");
			this.dataAccessor.EditionLine[2].SetText (ColumnType.Journal,          this.dataAccessor.EditionLine[0].GetText (ColumnType.Journal));
			this.dataAccessor.EditionLine[2].SetText (ColumnType.TotalAutomatique, "1");
			this.dataAccessor.EditionLine[2].SetText (ColumnType.Hilited,          "1");

			if (this.PlusieursPièces)
			{
				var nomJournal = this.dataAccessor.EditionLine[0].GetText (ColumnType.Journal);
				var journal = this.compta.Journaux.Where (x => x.Nom == nomJournal).FirstOrDefault ();

				this.dataAccessor.EditionLine[1].SetText (ColumnType.Pièce, this.controller.MainWindowController.PiècesGenerator.GetProchainePièce (journal, 1));
				this.dataAccessor.EditionLine[2].SetText (ColumnType.Pièce, this.controller.MainWindowController.PiècesGenerator.GetProchainePièce (journal, 2));
			}
			else
			{
				this.dataAccessor.EditionLine[1].SetText (ColumnType.Pièce, this.dataAccessor.EditionLine[0].GetText (ColumnType.Pièce));
				this.dataAccessor.EditionLine[2].SetText (ColumnType.Pièce, this.dataAccessor.EditionLine[0].GetText (ColumnType.Pièce));
			}

			lineToSelect = 1;  // dans la 2ème ligne
			columnToSelect = multiActiveColumn;
		}

		private int ExplodeForTVA(int line, ref int lineToSelect, ref ColumnType columnToSelect)
		{
			bool isDébitMulti = !this.IsDébitTVA (line);
			var multiActiveColumn   =  isDébitMulti ? ColumnType.Crédit : ColumnType.Débit;
			var multiInactiveColumn = !isDébitMulti ? ColumnType.Crédit : ColumnType.Débit;

			var p  = this.dataAccessor.EditionLine[line].GetText (multiActiveColumn);
			var cp = this.dataAccessor.EditionLine[line].GetText (multiInactiveColumn);
			var compteP  = this.compta.PlanComptable.Where (x => x.Numéro == p ).FirstOrDefault ();
			var compteCP = this.compta.PlanComptable.Where (x => x.Numéro == cp).FirstOrDefault ();
			var taux = compteP.CodeTVA.DefaultTauxValue.GetValueOrDefault ();

			var montantTTC = Converters.ParseMontant (this.dataAccessor.EditionLine[line].GetText (ColumnType.MontantTTC)).GetValueOrDefault ();
			var montantHT  = Converters.ParseMontant (this.dataAccessor.EditionLine[line].GetText (ColumnType.Montant   )).GetValueOrDefault ();

			if (montantTTC == 0)
			{
				montantTTC  = TVA.CalculeTTC (montantHT, taux);
			}
			else
			{
				montantHT  = TVA.CalculeHT (montantTTC, taux);
			}

			var montantTVA = TVA.CalculeTVA (montantHT, taux);

			int total = (this.dataAccessor.CountEditedRow == 1) ? 3 : 2;

			this.dataAccessor.InsertEditionLine (line+1);  // insère la 2ème ligne après l'actuelle

			if (total == 3)
			{
				this.dataAccessor.InsertEditionLine (line+2);  // insère la 3ème ligne après la 2ème
			}

			//	Met à jour les données de la 1ère ligne (BaseTVA).
			this.dataAccessor.EditionLine[line+0].SetText (ColumnType.Type,       Converters.TypeEcritureToString (TypeEcriture.BaseTVA));
			this.dataAccessor.EditionLine[line+0].SetText (multiInactiveColumn,   JournalDataAccessor.multi);
			this.dataAccessor.EditionLine[line+0].SetText (ColumnType.MontantTTC, Converters.MontantToString (montantTTC));
			this.dataAccessor.EditionLine[line+0].SetText (ColumnType.Montant,    Converters.MontantToString (montantHT));

			//	Met à jour les données de la 2ème ligne (CodeTVA).
			this.dataAccessor.EditionLine[line+1].SetText (ColumnType.Type,       Converters.TypeEcritureToString (TypeEcriture.CodeTVA));
			this.dataAccessor.EditionLine[line+1].SetText (ColumnType.Date,       this.dataAccessor.EditionLine[line].GetText (ColumnType.Date));
			this.dataAccessor.EditionLine[line+1].SetText (multiActiveColumn,     compteP.CodeTVA.Compte.Numéro);
			this.dataAccessor.EditionLine[line+1].SetText (multiInactiveColumn,   JournalDataAccessor.multi);
			this.dataAccessor.EditionLine[line+1].SetText (ColumnType.CodeTVA,    compteP.CodeTVA.Code);
			this.dataAccessor.EditionLine[line+1].SetText (ColumnType.TauxTVA,    Converters.PercentToString (compteP.CodeTVA.DefaultTauxValue));
			this.dataAccessor.EditionLine[line+1].SetText (ColumnType.Montant,    Converters.MontantToString (montantTVA));
			this.dataAccessor.EditionLine[line+1].SetText (ColumnType.Journal,    this.dataAccessor.EditionLine[line].GetText (ColumnType.Journal));
			this.dataAccessor.EditionLine[line+1].SetText (ColumnType.Hilited,    "1");

			if (total == 3)
			{
				//	Met à jour les données de la contrepartie.						   
				this.dataAccessor.EditionLine[line+2].SetText (ColumnType.Type,             Converters.TypeEcritureToString (TypeEcriture.Normal));
				this.dataAccessor.EditionLine[line+2].SetText (ColumnType.Date,             this.dataAccessor.EditionLine[line+0].GetText (ColumnType.Date));
				this.dataAccessor.EditionLine[line+2].SetText (multiActiveColumn,           JournalDataAccessor.multi);
				this.dataAccessor.EditionLine[line+2].SetText (multiInactiveColumn,         (compteCP == null) ? null : compteCP.Numéro);
				this.dataAccessor.EditionLine[line+2].SetText (ColumnType.Libellé,          this.dataAccessor.EditionLine[line+0].GetText (ColumnType.Libellé));
				this.dataAccessor.EditionLine[line+2].SetText (ColumnType.Journal,          this.dataAccessor.EditionLine[line+0].GetText (ColumnType.Journal));
				this.dataAccessor.EditionLine[line+2].SetText (ColumnType.TotalAutomatique, "1");
				this.dataAccessor.EditionLine[line+2].SetText (ColumnType.Hilited,          (compteCP == null) ? "1" : "0");
			}

			if (this.PlusieursPièces)
			{
				var nomJournal = this.dataAccessor.EditionLine[line].GetText (ColumnType.Journal);
				var journal = this.compta.Journaux.Where (x => x.Nom == nomJournal).FirstOrDefault ();

				this.dataAccessor.EditionLine[line+1].SetText (ColumnType.Pièce, this.controller.MainWindowController.PiècesGenerator.GetProchainePièce (journal, 1));

				if (total == 3)
				{
					this.dataAccessor.EditionLine[line+2].SetText (ColumnType.Pièce, this.controller.MainWindowController.PiècesGenerator.GetProchainePièce (journal, 2));
				}
			}
			else
			{
				this.dataAccessor.EditionLine[line+1].SetText (ColumnType.Pièce, this.dataAccessor.EditionLine[line].GetText (ColumnType.Pièce));

				if (total == 3)
				{
					this.dataAccessor.EditionLine[line+2].SetText (ColumnType.Pièce, this.dataAccessor.EditionLine[line].GetText (ColumnType.Pièce));
				}
			}

			if (lineToSelect == -1)
			{
				lineToSelect = line+1;
				//?columnToSelect = multiActiveColumn;
				columnToSelect = ColumnType.CodeTVA;
			}

			return total;
		}

		public override void MultiInsertLineAction()
		{
			//	Insère une nouvelle ligne après la ligne courante.
			this.ClearLineHilite ();

			bool isDébitMulti = this.IsDébitMulti (this.selectedLine);
			var multiActiveColumn   =  isDébitMulti ? ColumnType.Crédit : ColumnType.Débit;
			var multiInactiveColumn = !isDébitMulti ? ColumnType.Crédit : ColumnType.Débit;

			this.selectedLine++;
			this.dataAccessor.InsertEditionLine (this.selectedLine);

			this.dataAccessor.EditionLine[this.selectedLine].SetText (multiInactiveColumn, JournalDataAccessor.multi);
			this.dataAccessor.EditionLine[this.selectedLine].SetText (ColumnType.Type, Converters.TypeEcritureToString (TypeEcriture.Nouveau));

			int cp = this.IndexTotalAutomatique;
			if (cp != -1)
			{
				this.dataAccessor.EditionLine[this.selectedLine].SetText (ColumnType.Date,    this.dataAccessor.EditionLine[cp].GetText (ColumnType.Date));
				this.dataAccessor.EditionLine[this.selectedLine].SetText (ColumnType.Journal, this.dataAccessor.EditionLine[cp].GetText (ColumnType.Journal));

				if (this.PlusieursPièces)
				{
					var nomJournal = this.dataAccessor.EditionLine[cp].GetText (ColumnType.Journal);
					var journal = this.compta.Journaux.Where (x => x.Nom == nomJournal).FirstOrDefault ();

					this.dataAccessor.EditionLine[this.selectedLine].SetText (ColumnType.Pièce, this.controller.MainWindowController.PiècesGenerator.GetProchainePièce (journal, this.dataAccessor.EditionLine.Count-1));
				}
				else
				{
					this.dataAccessor.EditionLine[this.selectedLine].SetText (ColumnType.Pièce, this.dataAccessor.EditionLine[cp].GetText (ColumnType.Pièce));
				}
			}

			this.dirty = true;
			this.UpdateEditorContent ();
			this.SelectedLineChanged ();
			this.EditorSelect (multiActiveColumn, this.selectedLine);
		}

		public override void MultiDeleteLineAction()
		{
			//	Supprime la ligne courante.
			var type = this.GetTypeEcriture (this.selectedLine);

			if (type == TypeEcriture.BaseTVA)
			{
				this.dataAccessor.RemoveAtEditionLine (this.selectedLine);

				if (this.GetTypeEcriture (this.selectedLine) == TypeEcriture.CodeTVA)
				{
					this.dataAccessor.RemoveAtEditionLine (this.selectedLine);
				}
			}
			else if (type == TypeEcriture.CodeTVA)
			{
				if (this.GetTypeEcriture (this.selectedLine-1) == TypeEcriture.BaseTVA)
				{
					this.SetTypeEcriture (this.selectedLine-1, TypeEcriture.Normal);
					this.dataAccessor.EditionLine[this.selectedLine-1].SetText (ColumnType.Montant, this.dataAccessor.EditionLine[this.selectedLine-1].GetText (ColumnType.MontantTTC));
				}

				this.dataAccessor.RemoveAtEditionLine (this.selectedLine);
			}
			else
			{
				this.dataAccessor.RemoveAtEditionLine (this.selectedLine);
			}

			this.selectedLine = System.Math.Min (this.selectedLine, this.dataAccessor.CountEditedRow-1);

			this.dirty = true;
			this.UpdateEditorContent ();
			this.SelectedLineChanged ();
			this.EditorSelect (this.selectedColumn, this.selectedLine);
		}

		public override void MultiMoveLineAction(int direction)
		{
			//	Monte ou descend la ligne courante.
			this.SwapLine (this.selectedLine, this.selectedLine+direction);
			this.selectedLine += direction;

			this.dirty = true;
			this.UpdateEditorContent ();
			this.SelectedLineChanged ();
			this.EditorSelect (this.selectedColumn, this.selectedLine);
		}

		public override void MultiLineSwapAction()
		{
			//	Permute le débit et le crédit dans la ligne courante.
			var débit  = this.dataAccessor.EditionLine[this.selectedLine].GetText (ColumnType.Débit);
			var crédit = this.dataAccessor.EditionLine[this.selectedLine].GetText (ColumnType.Crédit);

			this.dataAccessor.EditionLine[this.selectedLine].SetText (ColumnType.Débit,  crédit);
			this.dataAccessor.EditionLine[this.selectedLine].SetText (ColumnType.Crédit, débit);

			this.dirty = true;
			this.UpdateEditorContent ();
		}

		public override void MultiLineAutoAction()
		{
			//	Met le total automatique dans la ligne courante.
			for (int line = 0; line < this.dataAccessor.EditionLine.Count; line++)
			{
				this.dataAccessor.EditionLine[line].SetText (ColumnType.TotalAutomatique, (line == this.selectedLine) ? "1" : "0");
			}

			this.dirty = true;
			this.UpdateEditorContent ();
		}

		private void SwapLine(int line1, int line2)
		{
			//	Permute deux lignes (pour les opérations monte/descend).
			var t                                = this.dataAccessor.EditionLine[line2];
			this.dataAccessor.EditionLine[line2] = this.dataAccessor.EditionLine[line1];
			this.dataAccessor.EditionLine[line1] = t;
		}

		public override void InsertModèle(int n)
		{
			RaccourciModèle rm = RaccourciModèle.Ctrl0 + n;
			string srm = Converters.RaccourciToString (rm);
			var modèle = this.compta.Modèles.Where (x => x.Raccourci == srm).FirstOrDefault ();

			if (modèle != null)
			{
				this.InsertModèle (modèle, this.selectedLine);
			}
		}


		protected override void HandleClearFocus(int line, ColumnType columnType)
		{
		}

		protected override void UpdateAfterValidate()
		{
			//	Met à jour les décorations des champs.
			//	Met des cadres rouges pointillés aux champs qui empêchent de créer l'écriture (complément nécessaire).
			//	Met des hachures grises aux champs vides d'une ligne vide.

			//	On commence par enlever toutes les décorations.
			for (int line = 0; line < this.fieldControllers.Count; line++)
			{
				for (int column = 0; column < this.fieldControllers[line].Count; column++)
				{
					this.fieldControllers[line][column].ToComplete = false;
					this.fieldControllers[line][column].EmptyLine  = false;
				}
			}

			//	Met les décorations s'il s'agit de la création d'une nouvelle écriture multiple.
			if (this.dataAccessor.EditionLine.Count != 0 && !this.isMulti && !this.IsTVA (0) && !this.dataAccessor.IsModification && this.dataAccessor.CountEditedRow == 1)
			{
				if (this.IsDébitMulti (0))
				{
					this.GetFieldController (ColumnType.Débit, 0).ToComplete = true;
				}

				if (this.IsCréditMulti (0))
				{
					this.GetFieldController (ColumnType.Crédit, 0).ToComplete = true;
				}
			}

			//	Met les autres décorations.
			for (int line = 0; line < this.dataAccessor.EditionLine.Count; line++)
			{
				var type = this.GetTypeEcriture (line);

				if (type == TypeEcriture.Nouveau ||  // ligne fraichement créée ?
					type == TypeEcriture.Vide)
				{
					if (this.IsDébitTVA (line))
					{
						this.GetFieldController (ColumnType.Débit, line).ToComplete = true;
					}

					if (this.IsCréditTVA (line))
					{
						this.GetFieldController (ColumnType.Crédit, line).ToComplete = true;
					}
				}

				if (type == TypeEcriture.Vide)
				{
					bool emptyLine = (this.dataAccessor.EditionLine[line] as JournalEditionLine).IsEmptyLine;

					for (int column = 0; column < this.fieldControllers[line].Count; column++)
					{
						this.fieldControllers[line][column].EmptyLine = emptyLine;
					}
				}
			}
		}

		protected override void UpdateEditionWidgets(int line, ColumnType columnType)
		{
			//	Met à jour toutes les données en édition.
			if (this.controller.IgnoreChanges.IsNotZero || !this.dataAccessor.EditionLine.Any ())
			{
				return;
			}

			var type = this.GetTypeEcriture (line);

			if ((type == TypeEcriture.Nouveau || type == TypeEcriture.Vide) && this.IsTVA (line))
			{
				this.NouveauMontant_TTC_HT_Changed (line, columnType);
			}

			if (type == TypeEcriture.BaseTVA && line < this.dataAccessor.EditionLine.Count-1)
			{
				if (columnType == ColumnType.MontantTTC)
				{
					this.MontantTTCChanged (line);
				}

				if (columnType == ColumnType.Montant)
				{
					this.MontantHTChanged (line);
				}
			}

			if (type == TypeEcriture.CodeTVA && line > 0)
			{
				if (columnType == ColumnType.CodeTVA)
				{
					this.CodeChanged (line-1);
				}

				if (columnType == ColumnType.TauxTVA)
				{
					this.TauxChanged (line-1);
				}

				if (columnType == ColumnType.Montant)
				{
					this.MontantTVAChanged (line-1);
				}
			}

			bool éditeMontantTVA = this.settingsList.GetBool (SettingsType.EcritureEditeMontantTVA);
			bool éditeMontantHT  = this.settingsList.GetBool (SettingsType.EcritureEditeMontantHT);
			bool éditeCodeTVA    = this.settingsList.GetBool (SettingsType.EcritureEditeCodeTVA);
			bool éditeTauxTVA    = this.settingsList.GetBool (SettingsType.EcritureEditeTauxTVA);

			for (int i = 0; i < this.dataAccessor.EditionLine.Count; i++)
			{
				type = this.GetTypeEcriture (i);

				if (this.isMulti)
				{
					if (this.IsDébitMulti (i))
					{
						this.SetWidgetVisibility (ColumnType.Débit,  i, false);
						this.SetWidgetVisibility (ColumnType.Crédit, i, true);
					}

					if (this.IsCréditMulti (i))
					{
						this.SetWidgetVisibility (ColumnType.Débit,  i, true);
						this.SetWidgetVisibility (ColumnType.Crédit, i, false);
					}

					bool totalAutomatique = (this.dataAccessor.EditionLine[i].GetText (ColumnType.TotalAutomatique) == "1");

					this.SetWidgetVisibility (ColumnType.Date,    i, totalAutomatique);
					this.SetWidgetVisibility (ColumnType.Journal, i, totalAutomatique);

					if (!this.PlusieursPièces)
					{
						this.SetWidgetVisibility (ColumnType.Pièce, i, totalAutomatique);
					}

					this.dataAccessor.GetEditionData (i, ColumnType.Montant).Enable = !totalAutomatique;

					this.SetWidgetVisibility (ColumnType.CodeTVA, i, !totalAutomatique);
					this.SetWidgetVisibility (ColumnType.TauxTVA, i, !totalAutomatique);
				}
				else
				{
					this.SetWidgetVisibility (ColumnType.Date,    i, true);
					this.SetWidgetVisibility (ColumnType.Débit,   i, true);
					this.SetWidgetVisibility (ColumnType.Crédit,  i, true);
					this.SetWidgetVisibility (ColumnType.Pièce,   i, true);
					this.SetWidgetVisibility (ColumnType.Journal, i, true);
				}

				//	Décide de la visibilité du champ 'Montant TTC'.
				bool showTTC = false;

				if (type == TypeEcriture.BaseTVA)
				{
					showTTC = true;
				}
				else if ((type == TypeEcriture.Nouveau || type == TypeEcriture.Vide) && this.IsTVA (i))
				{
					showTTC = true;
				}

				if (this.GetWidgetVisibility (ColumnType.MontantTTC, i) != showTTC)
				{
					this.SetWidgetVisibility (ColumnType.MontantTTC, i, showTTC);

					if (showTTC)
					{
						var montantTTC = Converters.ParseMontant (this.dataAccessor.EditionLine[i].GetText (ColumnType.MontantTTC));
						if (!montantTTC.HasValue)
						{
							this.dataAccessor.EditionLine[i].SetText (ColumnType.MontantTTC, Converters.MontantToString (0));
						}
					}
				}
			}

			this.UpdateMultiEditionData ();  // recalcule le total
		}

		private void NouveauMontant_TTC_HT_Changed(int line, ColumnType columnType)
		{
			//	Lorsqu'on crée une nouvelle ligne avec TVA, il faut permettre d'éditer soit le montant TTC,
			//	soit le montant HT, mais pas les deux.
			var montantTTC = Converters.ParseMontant (this.dataAccessor.EditionLine[line].GetText (ColumnType.MontantTTC));
			var montantHT  = Converters.ParseMontant (this.dataAccessor.EditionLine[line].GetText (ColumnType.Montant));

			if (columnType == ColumnType.MontantTTC)  // montant TTC ?
			{
				if (montantTTC.HasValue)
				{
					this.dataAccessor.EditionLine[line].SetText (ColumnType.Montant, FormattedText.Empty);  // vide le montant HT
				}
			}
			else if (columnType == ColumnType.Montant)  // montant HT ?
			{
				if (montantHT.HasValue)
				{
					this.dataAccessor.EditionLine[line].SetText (ColumnType.MontantTTC, FormattedText.Empty);  // vide le montant TTC
				}
			}
			else
			{
				if (montantTTC.GetValueOrDefault () == 0 && montantHT.GetValueOrDefault () == 0)
				{
					this.dataAccessor.EditionLine[line].SetText (ColumnType.Montant,    Converters.MontantToString (0));
					this.dataAccessor.EditionLine[line].SetText (ColumnType.MontantTTC, Converters.MontantToString (0));
				}
			}
		}

		private void CodeChanged(int line)
		{
			var code = this.dataAccessor.EditionLine[line+1].GetText (ColumnType.CodeTVA).ToSimpleText ();
			var codeTVA = this.compta.CodesTVA.Where (x => x.Code == code).FirstOrDefault ();

			if (codeTVA == null)
			{
				this.dataAccessor.EditionLine[line+1].SetText (ColumnType.TauxTVA, Converters.PercentToString (0));
			}
			else
			{
				this.dataAccessor.EditionLine[line+1].SetText (ColumnType.TauxTVA, Converters.PercentToString (codeTVA.DefaultTauxValue));
			}

			this.UpdateMontantHT (line);
			this.UpdateMontantTVA (line);
		}

		private void TauxChanged(int line)
		{
			this.UpdateMontantHT (line);
			this.UpdateMontantTVA (line);
		}

		private void MontantTVAChanged(int line)
		{
			var montantTTC = Converters.ParseMontant (this.dataAccessor.EditionLine[line+0].GetText (ColumnType.MontantTTC));
			var montantTVA = Converters.ParseMontant (this.dataAccessor.EditionLine[line+1].GetText (ColumnType.Montant));

			if (montantTTC.HasValue && montantTVA.HasValue)
			{
				var montantHT = montantTTC.Value - montantTVA.Value;
				this.dataAccessor.EditionLine[line+0].SetText (ColumnType.Montant, Converters.MontantToString (montantHT));
			}
		}

		private void MontantHTChanged(int line)
		{
			this.UpdateMontantTTC (line);
			this.UpdateMontantTVA (line);
		}

		private void MontantTTCChanged(int line)
		{
			this.UpdateMontantHT (line);
			this.UpdateMontantTVA (line);
		}

		private void UpdateMontantHT(int line)
		{
			var montantTTC = Converters.ParseMontant (this.dataAccessor.EditionLine[line+0].GetText (ColumnType.MontantTTC));
			var tauxTVA    = Converters.ParsePercent (this.dataAccessor.EditionLine[line+1].GetText (ColumnType.TauxTVA));

			if (montantTTC.HasValue && tauxTVA.HasValue)
			{
				var montantHT = TVA.CalculeHT (montantTTC.Value, tauxTVA.Value);
				this.dataAccessor.EditionLine[line+0].SetText (ColumnType.Montant, Converters.MontantToString (montantHT));
			}
		}

		private void UpdateMontantTTC(int line)
		{
			var montantHT = Converters.ParseMontant (this.dataAccessor.EditionLine[line+0].GetText (ColumnType.Montant));
			var tauxTVA   = Converters.ParsePercent (this.dataAccessor.EditionLine[line+1].GetText (ColumnType.TauxTVA));

			if (montantHT.HasValue && tauxTVA.HasValue)
			{
				var montantTTC = TVA.CalculeTTC (montantHT.Value, tauxTVA.Value);
				this.dataAccessor.EditionLine[line+0].SetText (ColumnType.MontantTTC, Converters.MontantToString (montantTTC));
			}
		}

		private void UpdateMontantTVA(int line)
		{
			var montantHT  = Converters.ParseMontant (this.dataAccessor.EditionLine[line+0].GetText (ColumnType.Montant));
			var montantTTC = Converters.ParseMontant (this.dataAccessor.EditionLine[line+0].GetText (ColumnType.MontantTTC));

			if (montantHT.HasValue && montantTTC.HasValue)
			{
				var montantTVA = montantTTC.Value - montantHT.Value;
				this.dataAccessor.EditionLine[line+1].SetText (ColumnType.Montant, Converters.MontantToString (montantTVA));
			}
		}


		private void UpdateMultiEditionData()
		{
			//	Recalcule le total de l'écriture multiple.
			if (!this.isMulti)
			{
				return;
			}

			int cp = this.IndexTotalAutomatique;
			if (cp != -1)
			{
				decimal totalDébit  = 0;
				decimal totalCrédit = 0;

				for (int line = 0; line < this.dataAccessor.EditionLine.Count; line++ )
				{
					var type  = this.GetTypeEcriture (line);
					var type2 = this.GetTypeEcriture (line+1);

					if (type == TypeEcriture.BaseTVA && type2 == TypeEcriture.CodeTVA)  // ligne BaseTVA suivie d'une CodeTVA ?
					{
						var code = this.dataAccessor.EditionLine[line+1].GetText (ColumnType.CodeTVA).ToSimpleText ();
						var taux = this.dataAccessor.EditionLine[line+1].GetText (ColumnType.TauxTVA);

						this.dataAccessor.EditionLine[line+0].SetText (ColumnType.CodeTVA, code);
						this.dataAccessor.EditionLine[line+0].SetText (ColumnType.TauxTVA, taux);
						this.dataAccessor.EditionLine[line+0].SetText (ColumnType.MontantComplément, this.dataAccessor.EditionLine[line+1].GetText (ColumnType.Montant));
						this.dataAccessor.EditionLine[line+1].SetText (ColumnType.MontantComplément, this.dataAccessor.EditionLine[line+0].GetText (ColumnType.Montant));

						//	Le libellé de la ligne CodeTVA reprend toujours celui de la ligne BaseTVA. Selon les présentations,
						//	on affichera directement ce libellé, ou autre chose de calculé. Par exemple, dans un journal, on
						//	désire voir un texte qui résumé la TVA (code et taux), alors que dans un extrait de compte, le
						//	libellé natif convient parfaitement.
						this.dataAccessor.EditionLine[line+1].SetText (ColumnType.Libellé, this.dataAccessor.EditionLine[line+0].GetText (ColumnType.Libellé));
					}

					if (line != cp)
					{
						this.dataAccessor.EditionLine[line].SetText (ColumnType.Date,    this.dataAccessor.EditionLine[cp].GetText (ColumnType.Date));
						this.dataAccessor.EditionLine[line].SetText (ColumnType.Journal, this.dataAccessor.EditionLine[cp].GetText (ColumnType.Journal));

						if (!this.PlusieursPièces)
						{
							this.dataAccessor.EditionLine[line].SetText (ColumnType.Pièce, this.dataAccessor.EditionLine[cp].GetText (ColumnType.Pièce));
						}

						decimal montant = Converters.ParseMontant (this.dataAccessor.EditionLine[line].GetText (ColumnType.Montant)).GetValueOrDefault ();

						if (this.IsDébitMulti (line))
						{
							totalCrédit += montant;
						}

						if (this.IsCréditMulti (line))
						{
							totalDébit += montant;
						}
					}
				}

				decimal? total = 0;

				if (this.IsDébitMulti (cp))
				{
					total = totalDébit - totalCrédit;
				}

				if (this.IsCréditMulti (cp))
				{
					total = totalCrédit - totalDébit;
				}

#if false
				if (total == 0)
				{
					total = null;
				}
#endif

				this.dataAccessor.EditionLine[cp].SetText (ColumnType.Montant, Converters.MontantToString (total));
				this.dataAccessor.EditionLine[cp].SetText (ColumnType.CodeTVA, null);
			}
		}

		private bool IsDébitMulti(int line)
		{
			return this.dataAccessor.EditionLine[line].GetText (ColumnType.Débit) == JournalDataAccessor.multi;
		}

		private bool IsCréditMulti(int line)
		{
			return this.dataAccessor.EditionLine[line].GetText (ColumnType.Crédit) == JournalDataAccessor.multi;
		}

		private bool IsTVA(int line)
		{
			//	Retourne true si le compte au débit ou au crédit a un code TVA.
			return this.IsDébitTVA (line) || this.IsCréditTVA (line);
		}

		private bool IsDébitTVA(int line)
		{
			//	Retourne true si le compte au débit a un code TVA.
			var numéro = this.dataAccessor.EditionLine[line].GetText (ColumnType.Débit);
			var compte = this.compta.PlanComptable.Where (x => x.Numéro == numéro).FirstOrDefault ();
			return compte != null && compte.CodeTVA != null;
		}

		private bool IsCréditTVA(int line)
		{
			//	Retourne true si le compte au crédit a un code TVA.
			var numéro = this.dataAccessor.EditionLine[line].GetText (ColumnType.Crédit);
			var compte = this.compta.PlanComptable.Where (x => x.Numéro == numéro).FirstOrDefault ();
			return compte != null && compte.CodeTVA != null;
		}

		private int IndexTotalAutomatique
		{
			//	Retourne l'index de la ligne qui contient le total automatique.
			get
			{
				return this.dataAccessor.EditionLine.FindIndex (x => x.GetText (ColumnType.TotalAutomatique) == "1");
			}
		}


		protected override void UpdateAfterSelectedLineChanged()
		{
#if false
			int sel = this.multiFirstRow + this.selectedLine;

			if (this.arrayController.SelectedRow != sel)
			{
				this.controller.IgnoreChanged = true;
				this.arrayController.SelectedRow = sel;
				this.controller.IgnoreChanged = false;
			}
#endif

			this.UpdateToolbar ();
			this.UpdateInsertionRow ();
			this.UpdateEditorInfo ();
		}


		public override void UpdateEditorContent()
		{
			this.UpdateArrayColumns ();

			int count = this.dataAccessor.CountEditedRow;
			this.selectedLine = System.Math.Min (this.selectedLine, count-1);
			this.isMulti = (count > 1);

			base.UpdateEditorContent ();
		}


		public override void UpdateEditorGeometry()
		{
			base.UpdateEditorGeometry ();

			double w1 = this.arrayController.GetColumnsAbsoluteWidth (0);
			double w2 = this.arrayController.GetColumnsAbsoluteWidth (1);
			double w3 = this.arrayController.GetColumnsAbsoluteWidth (2);

			this.débitInfoSeparator.Margins  = new Margins (w1, 0, 0, 0);
			this.débitInfoSeparator.PreferredWidth  = w2-1;
			this.créditInfoSeparator.PreferredWidth = w3-1;

			this.débitInfoFrame.PreferredWidth  = w1+w2;
			this.créditInfoFrame.PreferredWidth = w1+w2-1;
		}

		protected override bool GetColumnGeometry(int line, ColumnType columnType, out double left, out double width)
		{
			left = width = 0;

			var type = this.GetTypeEcriture (line);

			if (type == TypeEcriture.CodeTVA)
			{
				if (columnType == ColumnType.Libellé)
				{
					return false;  // cache le libellé
				}
				else if (columnType == ColumnType.LibelléTVA ||
						 columnType == ColumnType.CodeTVA    ||
						 columnType == ColumnType.TauxTVA    )
				{
					this.arrayController.GetColumnGeometry (ColumnType.Libellé, out left, out width);

					double codeWidth = System.Math.Min (100, System.Math.Floor (width/3));
					double tauxWidth = System.Math.Min ( 55, System.Math.Floor (width/3));

					if (columnType == ColumnType.LibelléTVA)
					{
						width -= codeWidth + tauxWidth;
					}
					else if (columnType == ColumnType.CodeTVA)
					{
						left += width - tauxWidth - codeWidth;
						width = codeWidth;
					}
					else
					{
						left += width - tauxWidth;
						width = tauxWidth;
					}

					return true;
				}
				else
				{
					return this.arrayController.GetColumnGeometry (columnType, out left, out width);
				}
			}
			else
			{
				if (columnType == ColumnType.LibelléTVA ||
					columnType == ColumnType.CodeTVA    ||
					columnType == ColumnType.TauxTVA    )
				{
					return false;  // cache les champs pour la TVA
				}
				else
				{
					return this.arrayController.GetColumnGeometry (columnType, out left, out width);
				}
			}
		}

		protected override void UpdateArrayColumns()
		{
			//	Si nécessaire, adapte l'interface pour accueillir le nombre de lignes et de colonnes requis.
			int count = this.dataAccessor.CountEditedRow;

			if (this.linesFrames.Count != count ||
				this.fieldControllers[0].Count != this.columnMappers.Where (x => x.Show).Count ())
			{
				this.ResetLineUI ();  // crée la première ligne

				for (int i = 0; i < count-1; i++)
				{
					this.CreateLineUI (this.linesContainer);  // crée les lignes suivantes
				}

				this.UpdateEditorGeometry ();
			}

			this.UpdateAfterFirstLineChanged ();
		}

		protected override void SelectedLineChanged()
		{
			//	Appelé lorsque la ligne sélectionnée a changé. On détermine ici la première ligne affichée,
			//	afin de montrer la ligne sélectionnée au mieux.
			int visibleLines = System.Math.Min (this.dataAccessor.CountEditedRow, this.maxLines);

			int first = this.selectedLine;
			first = System.Math.Min (first + visibleLines/2, this.dataAccessor.CountEditedRow-1);
			first = System.Math.Max (first - (visibleLines-1), 0);

			if (this.firstLine != first)
			{
				this.firstLine = first;
				this.UpdateAfterFirstLineChanged ();
			}
		}

		private void ChangeScroller()
		{
			//	Appelé lorsque l'ascenseur a été bougé.
			int value = (int) this.scroller.Value;

			if (this.firstLine != value)
			{
				this.firstLine = value;
				this.UpdateAfterFirstLineChanged ();
			}
		}

		protected override void UpdateAfterFirstLineChanged()
		{
			//	Met à jour les lignes visibles.
			this.firstLine = System.Math.Min (this.firstLine, this.dataAccessor.CountEditedRow - this.maxLines);
			this.firstLine = System.Math.Max (this.firstLine, 0);

			for (int i = 0; i < this.linesFrames.Count; i++)
			{
				this.linesFrames[i].Visibility = (i >= this.firstLine && i < this.firstLine+this.maxLines);
			}

			//	Met à jour l'ascenseur.
			if (this.dataAccessor.CountEditedRow > this.maxLines)
			{
				this.scroller.Visibility = true;

				decimal totalHeight = this.dataAccessor.CountEditedRow;
				decimal areaHeight = this.maxLines;

				this.scroller.MaxValue          = totalHeight-areaHeight;
				this.scroller.VisibleRangeRatio = areaHeight/totalHeight;
				this.scroller.Value             = this.firstLine;
				this.scroller.SmallChange       = 1;
				this.scroller.LargeChange       = areaHeight/2;
			}
			else
			{
				this.scroller.Visibility = false;
			}
		}


		protected override void UpdateEditorInfo()
		{
			this.infoFrameSeparator.Visibility = this.ShowInfoPanel;
			this.infoFrameBox.Visibility       = this.ShowInfoPanel;

			if (this.ShowInfoPanel)
			{
				this.UpdateEditorInfo (this.dataAccessor.GetEditionText (this.selectedLine, ColumnType.Débit ), isDébit: true);
				this.UpdateEditorInfo (this.dataAccessor.GetEditionText (this.selectedLine, ColumnType.Crédit), isDébit: false);
			}
		}

		private void UpdateEditorInfo(FormattedText numéro, bool isDébit)
		{
			FormattedText title;
			decimal? solde;

			this.GetInfoCompte (numéro, out title, out solde);
			UIBuilder.UpdateInfoCompte (isDébit ? this.débitInfoFrame : this.créditInfoFrame, title, solde);
		}

		public override void UpdateToolbar()
		{
			base.UpdateToolbar ();

			bool enable = this.dataAccessor.IsActive;
			int count = this.linesFrames.Count;
			int cp = this.IndexTotalAutomatique;

			this.controller.SetCommandEnable (Res.Commands.Multi.Insert,    this.InsertEnable);
			this.controller.SetCommandEnable (Res.Commands.Multi.InsertTVA, this.InsertEnable);
			this.controller.SetCommandEnable (Res.Commands.Multi.Delete,    this.DeleteEnable);
			this.controller.SetCommandEnable (Res.Commands.Multi.Up,        enable && count >  1 && this.selectedLine > 0);
			this.controller.SetCommandEnable (Res.Commands.Multi.Down,      enable && count >  1 && this.selectedLine < count-1);
			this.controller.SetCommandEnable (Res.Commands.Multi.Swap,      enable && count != 0 && this.selectedLine != -1);
			this.controller.SetCommandEnable (Res.Commands.Multi.Auto,      enable && count >  1 && this.selectedLine != cp);
		}

		private bool InsertEnable
		{
			get
			{
				if (!this.dataAccessor.IsActive)
				{
					return false;
				}

				if (this.dataAccessor.CountEditedRowWithoutEmpty <= 1)
				{
					return false;
				}

				if (this.GetTypeEcriture (this.selectedLine+0) == TypeEcriture.BaseTVA &&
					this.GetTypeEcriture (this.selectedLine+1) == TypeEcriture.CodeTVA)
				{
					return false;
				}

				return true;
			}
		}

		private bool DeleteEnable
		{
			get
			{
				if (!this.dataAccessor.IsActive)
				{
					return false;
				}

				if (this.dataAccessor.CountEditedRowWithoutEmpty <= 2)
				{
					return false;
				}

				var type = this.GetTypeEcriture (this.selectedLine);

				if (type == TypeEcriture.Vide   ||
					type == TypeEcriture.CodeTVA)
				{
					return true;
				}

				if (this.dataAccessor.CountEditedRowWithoutEmpty == 3 &&
					this.GetTypeEcriture(0) == TypeEcriture.BaseTVA   &&
					this.GetTypeEcriture(1) == TypeEcriture.CodeTVA   )
				{
					return false;
				}

				if (this.selectedLine == this.IndexTotalAutomatique)
				{
					return false;
				}

				return true;
			}
		}


		protected override FormattedText EditionDescription
		{
			get
			{
				int n = this.dataAccessor.CountEditedRow;

				if (n <= 1)
				{
					return FormattedText.Empty;
				}
				else
				{
					return FormattedText.Concat (n.ToString (), " lignes");
				}
			}
		}


		private void SetTypeEcriture(int line, TypeEcriture type)
		{
			if (line >= 0 && line < this.dataAccessor.EditionLine.Count)
			{
				this.dataAccessor.EditionLine[line].SetText (ColumnType.Type, Converters.TypeEcritureToString (type));
			}
		}

		private TypeEcriture GetTypeEcriture(int line)
		{
			var type = TypeEcriture.Normal;

			if (line >= 0 && line < this.dataAccessor.EditionLine.Count)
			{
				type = Converters.StringToTypeEcriture (this.dataAccessor.EditionLine[line].GetText (ColumnType.Type));
			}

			return type;
		}

		private void GetInfoCompte(FormattedText numéro, out FormattedText titre, out decimal? solde)
		{
			var compte = this.compta.PlanComptable.Where (x => x.Numéro == numéro).FirstOrDefault ();

			if (compte == null)
			{
				titre = FormattedText.Empty;
				solde = null;
			}
			else
			{
				titre = compte.Titre;
				solde = this.dataAccessor.SoldesJournalManager.GetSolde (compte);
			}
		}

		private bool PlusieursPièces
		{
			//	Retourne true si les écritures multiples peuvent avoir une pièce par ligne.
			get
			{
				return this.settingsList.GetBool (SettingsType.EcriturePlusieursPièces);
			}
		}


#if false
		protected override bool HasAssistant
		{
			get
			{
				return true;
			}
		}

		protected override void CreateAssistantEditorUI()
		{
			bool hasTVA = this.settingsList.GetBool (SettingsType.EcritureTVA);

			{
				var button = UIBuilder.CreateButton (this.editorForegroundFrameBox, "Edit.Create", "Normal");
				button.PreferredWidth = 250;
				button.Dock = DockStyle.Left;

				button.Clicked += delegate
				{
					this.CreateAction ();
				};
			}

			if (hasTVA)
			{
				var button = UIBuilder.CreateButton (this.editorForegroundFrameBox, "Edit.Create", "Prestation avec TVA sur une période");
				button.PreferredWidth = 250;
				button.Dock = DockStyle.Left;

				button.Clicked += delegate
				{
					this.CreateAssistant ();
					this.CreateAction ();
				};
			}
		}

		private void CreateAssistant()
		{
			this.editorAssistantFrameBox.Children.Clear ();
			this.editorAssistantFrameBox.BackColor =  UIBuilder.CreationBackColor;
			this.editorAssistantFrameBox.Padding = new Margins (0);

			this.assistantController = new Assistants.Controllers.AssistantEcritureTVAController (this.controller);
			this.assistantController.CreateUI (this.editorAssistantFrameBox);
			this.assistantController.UpdateContent ();
			this.assistantController.UpdateGeometry ();
		}
#endif


		#region Menu des écritures modèles
		private void CreateButtonModèleUI(AbstractFieldController fieldController, int line)
		{
			if (this.compta.Modèles.Any ())
			{
				var button = new Button
				{
					Parent          = fieldController.Box,
					ButtonStyle     = ButtonStyle.Icon,
					Text            = "M",
					Index           = line,
					PreferredWidth  = UIBuilder.ComboButtonWidth,
					PreferredHeight = 20,
					Dock            = DockStyle.Right,
					Margins         = new Margins (-1, 0, 0, 0),
				};

				button.Clicked += delegate
				{
					this.ShowMenuModèles (button, button.Index);
				};

				ToolTip.Default.SetToolTip (button, "Choix d'une écriture modèle");
			}
		}

		private void ShowMenuModèles(Widget parentButton, int line)
		{
			//	Affiche le menu permettant de choisir le mode.
			var menu = new VMenu ();

			int index = 0;
			foreach (var modèle in this.compta.Modèles)
			{
				var item = new MenuItem ()
				{
					FormattedText = modèle.ShortSummary,
					Index         = index++,
				};

				item.Clicked += delegate
				{
					this.InsertModèle (this.compta.Modèles[item.Index], line);
				};

				menu.Items.Add (item);
			}

			TextFieldCombo.AdjustComboSize (parentButton, menu, false);

			menu.Host = parentButton.Window;
			menu.ShowAsComboList (parentButton, Point.Zero, parentButton);
		}

		private void InsertModèle(ComptaModèleEntity modèle, int line)
		{
			if (modèle.Débit != null)
			{
				this.dataAccessor.EditionLine[line].SetText (ColumnType.Débit, modèle.Débit.Numéro);
			}

			if (modèle.Crédit != null)
			{
				this.dataAccessor.EditionLine[line].SetText (ColumnType.Crédit, modèle.Crédit.Numéro);
			}

			if (!modèle.Pièce.IsNullOrEmpty)
			{
				this.dataAccessor.EditionLine[line].SetText (ColumnType.Pièce, modèle.Pièce);
			}

			int cursor = -1;
			if (!modèle.Libellé.IsNullOrEmpty)
			{
				cursor = modèle.Libellé.ToString ().IndexOf ("@");
				var m = modèle.Libellé.ToString ().Replace ("@", "");
				this.dataAccessor.EditionLine[line].SetText (ColumnType.Libellé, m);
			}

			if (modèle.Montant.HasValue)
			{
				this.dataAccessor.EditionLine[line].SetText (ColumnType.Montant, Converters.MontantToString (modèle.Montant));
			}

			this.UpdateEditorContent ();
			this.EditorSelect (ColumnType.Libellé);

			var fc = this.GetFieldController (ColumnType.Libellé, line);
			var field = fc.EditWidget as AbstractTextField;
			field.Focus ();
			field.Cursor = (cursor == -1) ? field.Text.Length : cursor;
		}
		#endregion


		private CustomFrameBox						footer;
		private TabCatcherFrameBox					linesContainer;
		private FrameBox							infoFrameSeparator;
		private FrameBox							infoFrameBox;
		private Separator							débitInfoSeparator;
		private Separator							créditInfoSeparator;
		private FrameBox							débitInfoFrame;
		private FrameBox							créditInfoFrame;
		private VScroller							scroller;

		private bool								isMulti;
	}
}
