﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Marc BETTEX, Maintainer: Marc BETTEX

using Epsitec.Common.Dialogs;
using Epsitec.Common.Drawing;
using Epsitec.Common.Printing;
using Epsitec.Common.Types;
using Epsitec.Common.Widgets;
using Epsitec.Common.Widgets.Validators;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Epsitec.App.BanquePiguet
{

	/// <summary>
	/// The PrintersManager class is a window which lets the user configure the
	/// printers used to print the bvs.
	/// </summary>
	class PrintersManager : Window
	{

		/// <summary>
		/// Initializes a new instance of the PrintersManager class.
		/// </summary>
		/// <param name="application">The Application creating this instance.</param>
		public PrintersManager(Application application)
		{
			this.Application = application;
			this.Printers = Printer.Load ();
			this.SetupWindow ();
			this.SetupButtons ();
			this.SetupPrintersCellTable ();
			this.SetupEvents ();
			this.AdjustWindowSize ();
		}

		/// <summary>
		/// Gets or sets the Application who created this instance..
		/// </summary>
		/// <value>The Application who created this instance.</value>
		protected Application Application
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets list of configured Printers.
		/// </summary>
		/// <value>The list of configured Printers.</value>
		protected List<Printer> Printers
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the CellTable displaying the Printers.
		/// </summary>
		/// <value>The CellTable displaying the Printers.</value>
		protected CellTable PrintersCellTable
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the button used to save the configuration.
		/// </summary>
		/// <value>The button used to save the configuration.</value>
		protected Button SaveButton
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the button used to close the PrintersManager.
		/// </summary>
		/// <value>The button used to close the PrintersManager.</value>
		protected Button CancelButton
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the button used to add a new Printer.
		/// </summary>
		/// <value>The button used to add a new Printer.</value>
		protected Button AddPrinterButton
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the button used to remove a printer.
		/// </summary>
		/// <value>The button used to remove a printer.</value>
		protected Button RemovePrinterButton
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the AbstractTextFields contained in PrintersCellTable.
		/// </summary>
		/// <value>The AbstractTextFields contained in PrintersCellTable.</value>
		/// <remarks>
		/// This property assumes that PrintersCellTable is properly initialized. Do not call
		/// it before or while it is initialized.
		/// </remarks>
		protected IEnumerable<AbstractTextField> TextFieldCells
		{
			get
			{
				for (int i = 0; i < this.Printers.Count; i++)
				{
					for (int j = 1; j < 5; j++)
					{
						yield return (AbstractTextField) this.PrintersCellTable[j, i].Children[0];
					}
				}
			}
		}

		/// <summary>
		/// Sets up the properties of the window of this instance.
		/// </summary>
		/// <remarks>
		/// This method is called at the initialization of this instance.
		/// </remarks>
		protected void SetupWindow()
		{
			this.Text = "Configuration des imprimantes";
			this.MakeFixedSizeWindow ();
		}

		/// <summary>
		/// Sets up PrintersCellTable according to Printers. If PrintersCellTable is allready
		/// initialized, that instance is removed and replaced by a new one. In addition, the
		/// events used to update Printers and the validator are also set up.
		/// </summary>
		/// <remarks>
		/// This method is called at the initialization of this instance and when
		/// a printer is added or removed.
		/// </remarks>
		protected void SetupPrintersCellTable()
		{
			if (this.PrintersCellTable != null)
			{
				this.PrintersCellTable.Dispose ();
			}

			this.PrintersCellTable = new CellTable ()
			{
				Dock = DockStyle.Fill,
				Margins = new Margins (10, 10, 10, 10),
				Parent = this.Root,

			};

			this.PrintersCellTable.StyleH  = CellArrayStyles.None;
			this.PrintersCellTable.StyleH |= CellArrayStyles.Header;
			this.PrintersCellTable.StyleH |= CellArrayStyles.Separator;
			this.PrintersCellTable.StyleH |= CellArrayStyles.Mobile;
			this.PrintersCellTable.StyleH |= CellArrayStyles.Stretch;

			this.PrintersCellTable.StyleV  = CellArrayStyles.None;
			this.PrintersCellTable.StyleV |= CellArrayStyles.ScrollNorm;
			this.PrintersCellTable.StyleV |= CellArrayStyles.Separator;
			this.PrintersCellTable.StyleV |= CellArrayStyles.SelectLine;

			this.PrintersCellTable.SetArraySize (5, this.Printers.Count);
			this.PrintersCellTable.SetWidthColumn (0, 25);

			this.PrintersCellTable.SetHeaderTextH (1, "Nom");
			this.PrintersCellTable.SetHeaderTextH (2, "Bac");
			this.PrintersCellTable.SetHeaderTextH (3, "Décalage x");
			this.PrintersCellTable.SetHeaderTextH (4, "Décalage y");

			for (int i = 0; i < this.Printers.Count; i++)
			{
				Printer printer = this.Printers[i];
				
				TextFieldCombo nameTextField = new TextFieldCombo ()
				{
					Dock = DockStyle.Fill,
					Text = printer.Name,
					
				};

				this.PopulateNameTextField (printer, nameTextField);

				TextFieldCombo trayTextField = new TextFieldCombo ()
				{
					Dock = DockStyle.Fill,
					Text = printer.Tray,
				};

				this.PopulateTraysTextField (printer, trayTextField);

				TextFieldUpDown xOffsetTextField = new TextFieldUpDown ()
				{
					Dock = DockStyle.Fill,
					MaxValue = decimal.MaxValue,
					MinValue = decimal.MinValue,
					Resolution = 0.1M,
					Step = 0.1M,
					Text = printer.XOffset.ToString (System.Globalization.CultureInfo.InvariantCulture),
				};

				TextFieldUpDown yOffsetTextField = new TextFieldUpDown ()
				{
					Dock = DockStyle.Fill,
					MaxValue = decimal.MaxValue,
					MinValue = decimal.MinValue,
					Resolution = 0.1M,
					Step = 0.1M,
					Text = printer.YOffset.ToString (System.Globalization.CultureInfo.InvariantCulture),
				};

				nameTextField.TextChanged += (sender) =>
				{
					printer.Name = FormattedText.Unescape (nameTextField.Text);
					this.PopulateTraysTextField (printer, trayTextField);
				};

				trayTextField.TextChanged += (sender) => printer.Tray = FormattedText.Unescape (trayTextField.Text);				

				xOffsetTextField.TextChanged += (sender) =>
				{
					string text = FormattedText.Unescape (xOffsetTextField.Text);
					double result;

					if (double.TryParse (text, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
					{
						printer.XOffset = result;
					}
				};

				yOffsetTextField.TextChanged += (sender) =>
				{
					string text = FormattedText.Unescape (yOffsetTextField.Text);
					double result;

					if (double.TryParse (text, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
					{
						printer.YOffset = result;
					}
				};

#warning Uncomment validators.

				/*new PredicateValidator (nameTextField, () => {
					string text = FormattedText.Unescape (nameTextField.Text);
					
					bool empty = (text.Trim ().Length == 0);
					bool startEndSpace = (text.Trim ().Length != text.Length);
					bool duplicate = this.Printers.Count (p => p.Name == text) > 1;

					return !empty && !startEndSpace && !duplicate;
				});

				new PredicateValidator (trayTextField, () => {
					string text = FormattedText.Unescape (trayTextField.Text);

					bool empty = (text.Trim ().Length == 0);
					bool startEndSpace = (text.Trim ().Length != text.Length);
					
					return !empty && !startEndSpace;
				});*/
				

				//System.Console.WriteLine (i + " PRINTER: " + printer.Name + " VALIDATOR START");

				//new PredicateValidator (nameTextField, () => { Console.WriteLine(printer.Name + " NAME: " + (FormattedText.Unescape (nameTextField.Text).Trim ().Length > 0)); return FormattedText.Unescape (nameTextField.Text).Trim ().Length > 0;});//.Validate ();
				//new PredicateValidator (trayTextField, () => { Console.WriteLine(printer.Name + " TRAY: " + (FormattedText.Unescape (trayTextField.Text).Trim ().Length > 0)); return FormattedText.Unescape (trayTextField.Text).Trim ().Length > 0;});//.Validate ();

				//new PredicateValidator (nameTextField, () => { System.Console.WriteLine(">" + printer.Name + "< NAME: " + (false)); return false;}).Validate ();
				//new PredicateValidator (trayTextField, () => { System.Console.WriteLine(">" + printer.Name + "< TRAY: " + (false)); return false;}).Validate ();

				//System.Console.WriteLine (i + " PRINTER: " + printer.Name + " VALIDATOR END");

				this.PrintersCellTable[1, i].Insert(nameTextField);
				this.PrintersCellTable[2, i].Insert (trayTextField);
				this.PrintersCellTable[3, i].Insert (xOffsetTextField);
				this.PrintersCellTable[4, i].Insert (yOffsetTextField);
			}

			this.PrintersCellTable.FinalSelectionChanged += (sender) => this.CheckRemovePrinterEnabled ();
			this.CheckRemovePrinterEnabled ();

			this.TextFieldCells.ToList ().ForEach (c => c.TextChanged += (sender) => this.CheckSaveEnabled ());
			this.CheckSaveEnabled ();
		}

		/// <summary>
		/// Populates the items of nameTextField with the installed printers and the name of printer
		/// and sets its current value to the name of printer.
		/// </summary>
		/// <param name="printer">The printer corresponding to nameTextField.</param>
		/// <param name="nameTextField">The nameTextField to populate.</param>
		/// <remarks>
		/// This method is called whenever a nameTextField is initialized.
		/// </remarks>
		protected void PopulateNameTextField(Printer printer, TextFieldCombo nameTextField)
		{
			List<string> printerNames = PrinterSettings.InstalledPrinters.ToList ();

			if (!printerNames.Contains (printer.Name) && printer.Name != "")
			{
				printerNames.Add (printer.Name);
			}

			printerNames.ForEach (printerName => nameTextField.Items.Add (printerName));
		}

		/// <summary>
		/// Populates the items of trayTextField with the trays of the installed printer corresponding
		/// to the name of printer and with the tray of printer and sets its current value to the tray
		/// of printer.
		/// </summary>
		/// <param name="printer">The printer.</param>
		/// <param name="trayTextField">The tray text field.</param>
		/// <remarks>
		/// This method is called whenever a trayTextField is initialized or when the value of the
		/// corresponding nameTextField changes.
		/// </remarks>
		protected void PopulateTraysTextField(Printer printer, TextFieldCombo trayTextField)
		{
			if (trayTextField.Items.Count > 0)
			{
				trayTextField.Items.Clear ();
			}

			List<string> trayNames = new List<string> ();

			PrinterSettings settings = PrinterSettings.FindPrinter (printer.Name);
			
			if (settings != null)
			{
				System.Array.ForEach (settings.PaperSources, paperSource => trayNames.Add (paperSource.Name));
			}

			if (!trayNames.Contains (printer.Tray) && printer.Tray != "")
			{
				trayNames.Add (printer.Tray);
			}

			trayNames.ForEach (trayName => trayTextField.Items.Add (trayName));
		}

		/// <summary>
		/// Sets up the buttons of this instance.
		/// </summary>
		/// <remarks>
		/// This method is called at the initialization of this instance.
		/// </remarks>
		protected void SetupButtons()
		{
			FrameBox buttonsFrameBox = new FrameBox ()
			{
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				Dock = DockStyle.Bottom,
				Parent = this.Root,
			};

			this.CancelButton = new Button ()
			{
				Dock = DockStyle.Right,
				Margins = new Margins (10, 10, 10, 10),
				Parent = buttonsFrameBox,
				Text = "Annuler",
			};

			this.SaveButton = new Button ()
			{
				Dock = DockStyle.Right,
				Margins = new Margins (10, 0, 10, 10),
				Parent = buttonsFrameBox,
				Text = "Sauvegarder",
			};

			this.AddPrinterButton = new Button ()
			{
				Dock = DockStyle.Left,
				Margins = new Margins (10, 10, 10, 10),
				Parent = buttonsFrameBox,
				Text = "Ajouter",
			};

			this.RemovePrinterButton = new Button ()
			{
				Dock = DockStyle.Left,
				Margins = new Margins (0, 0, 10, 10),
				Parent = buttonsFrameBox,
				Text = "Supprimer",
			};

		}

		/// <summary>
		/// Sets up the events of this instance.
		/// </summary>
		/// <remarks>
		/// This method is called at the initialization of this instance. Note that the events related
		/// to PrintersCellTable are not set up here, as they are set up in SetupPrintersCellTable.
		/// </remarks>
		protected void SetupEvents()
		{
			this.AddPrinterButton.Clicked += (sender, e) => this.AddPrinter ();
			this.RemovePrinterButton.Clicked += (sender, e) => this.RemovePrinter ();
			this.SaveButton.Clicked += (sender, e) => this.Save ();
			this.CancelButton.Clicked += (sender, e) => this.Exit ();
			this.WindowClosed += (sender) => this.Exit();
		}

		/// <summary>
		/// Enables or disables SaveButton according to the validity of the AbstractTextFields
		/// contained in PrintersCellTable.
		/// </summary>
		/// <remarks>
		/// This method is called whenver the text changes in on the the AbstractTextFields
		/// contained in PrintersCellTable.
		/// </remarks>
		protected void CheckSaveEnabled()
		{
			this.SaveButton.Enable = this.TextFieldCells.All (c => c.IsValid);
		}

		/// <summary>
		///Enables or disables RemovePrinterButton according to what is selected in PrintersCellTable.
		/// </summary>
		/// <remarks>
		/// This method is called whenever the selection changes in PrintersCellTable.
		/// </remarks>
		protected void CheckRemovePrinterEnabled()
		{
			this.RemovePrinterButton.Enable  = (this.PrintersCellTable.SelectedRow >= 0);
		}

		/// <summary>
		/// Saves the content of Printers and exits the PrintersManager.
		/// </summary>
		/// <remarks>
		/// This method is called whenever the SaveButton is clicked.
		/// </remarks>
		protected void Save()
		{
			try
			{
				Printer.Save (this.Printers);
			}
			catch (System.Exception e)
			{
				MessageDialog.CreateOk ("Erreur", DialogIcon.Warning, "Une erreur s'est produite lors de la sauvegarde des imprimantes").OpenDialog ();
				Tools.LogException (new System.Exception ("An error occured while saving the printers.", e));
			}

			this.Exit ();
		}

		/// <summary>
		/// Adds a new empty Printer to Printers and resets PrintersCellTable.
		/// </summary>
		/// <remarks>
		/// This method is called whenver the AddPrinterButton is clicked.
		/// </remarks>
		protected void AddPrinter()
		{
			this.Printers.Add (new Printer ("", "", 0, 0));
			this.SetupPrintersCellTable ();
		}

		/// <summary>
		/// Removes the Printer selected in PrintersCellTable from Printers and resets
		/// PrintersCellTable.
		/// </summary>
		/// <remarks>
		/// This method is called whenver the RemovePrinterButton is clicked.
		/// </remarks>
		protected void RemovePrinter()
		{
			int selectedRow = this.PrintersCellTable.SelectedRow;

			if (selectedRow >= 0)
			{
				this.Printers.RemoveAt (selectedRow);
				this.SetupPrintersCellTable ();
			}
		}

		/// <summary>
		/// Exits this window by calling Application.DisplayPrintersManager (false).
		/// </summary>
		/// <remarks>
		/// This method is called whenever CancelButton is clicked, when the close button is clicked
		/// or when Printers has been saved.
		/// </remarks>
		protected void Exit()
		{
			this.Application.DisplayPrintersManager (false);
		}

	}

}
