using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using System.Globalization;

namespace Epsitec.Common.Designer
{
	/// <summary>
	/// La classe StringEditController g�re l'�dition d'un bundle de ressources
	/// textuelles.
	/// </summary>
	public class StringEditController
	{
		public StringEditController(CommandDispatcher dispatcher)
		{
			this.bundles    = new System.Collections.ArrayList ();
			this.panels     = new System.Collections.ArrayList ();
			this.dispatcher = dispatcher;
			
			this.dispatcher.RegisterController (this);
		}
		
		
		public void AttachNewBundle(string full_id, string prefix, ResourceLevel level, CultureInfo culture)
		{
			ResourceBundle bundle = ResourceBundle.Create (full_id, prefix, level, culture);
			
			this.AttachExistingBundle (bundle);
			this.ActivateTabBookPage ();
		}
		
		public void AttachExistingBundle(ResourceBundle bundle)
		{
			if (this.bundles.Contains (bundle))
			{
				throw new System.InvalidOperationException (string.Format ("Cannot attach bundle {0} twice.", bundle.Name));
			}
			
			this.bundles.Add (bundle);
			this.panels.Add (this.CreatePanel (bundle));
			
			this.ActivateTabBookPage ();
		}
		
		
		public ResourceBundle					ActiveBundle
		{
			get
			{
				int active = this.tab_book.ActivePageIndex;
				
				if (active > -1)
				{
					return this.bundles[active] as ResourceBundle;
				}
				
				return null;
			}
		}
		
		public Panels.StringEditPanel			ActivePanel
		{
			get
			{
				int active = this.tab_book.ActivePageIndex;
				
				if (active > -1)
				{
					return this.panels[active] as Panels.StringEditPanel;
				}
				
				return null;
			}
		}
		
		public Window							Window
		{
			get
			{
				if (this.window == null)
				{
					this.CreateWindow ();
				}
				
				return this.window;
			}
		}
		
		
		protected class Store : Support.Data.ITextArrayStore
		{
			public Store(StringEditController controller, ResourceBundle bundle)
			{
				this.bundle     = bundle;
				this.controller = controller;
				
				this.bundle.FieldsChanged += new Epsitec.Common.Support.EventHandler (this.HandleBundleFieldsChanged);
			}
			
			
			private void HandleBundleFieldsChanged(object sender)
			{
				if (this.changing == 0)
				{
					if (this.StoreChanged != null)
					{
						this.StoreChanged (this);
					}
				}
			}
			
			
			#region ITextArrayStore Members
			public void InsertRows(int row, int num)
			{
				// TODO:  Add TextBundleArrayStore.InsertRows implementation
				
				if (row == this.CountBundleFields)
				{
					ResourceBundle.Field field = this.bundle.CreateEmptyField (ResourceFieldType.Data);
					this.bundle.Add (field);
				}
			}
			
			public void RemoveRows(int row, int num)
			{
				// TODO:  Add TextBundleArrayStore.RemoveRows implementation
			}
			
			public string GetCellText(int row, int column)
			{
				if (row == this.CountBundleFields)
				{
					return "";
				}
				
				ResourceBundle.Field field = this.bundle[row];
				
				switch (column)
				{
					case 0:
						return TextLayout.ConvertToTaggedText (field.Name);
					case 1:
						return field.AsString;
				}
				
				return null;
			}
			
			public void SetCellText(int row, int column, string value)
			{
				if (row == this.CountBundleFields)
				{
					switch (column)
					{
						case 0:
							this.col_0_cache = value;
							break;
						case 1:
							this.col_1_cache = value;
							if ((this.col_0_cache != "") &&
								(this.col_1_cache != ""))
							{
								this.InsertRows (row, 1);
								this.changing++;
								this.SetCellText (row, 0, this.col_0_cache);
								this.SetCellText (row, 1, this.col_1_cache);
								this.changing--;
							}
							break;
					}
				}
				else
				{
					this.changing++;
					
					ResourceBundle.Field field = this.bundle[row];
					
					switch (column)
					{
						case 0:
							field.SetName (TextLayout.ConvertToSimpleText (value));
							break;
						case 1:
							field.SetStringValue (value);
							break;
					}
					
					this.changing--;
				}
			}
			
			public int GetColumnCount()
			{
				return 2;
			}
			
			public int GetRowCount()
			{
				return this.CountBundleFields + 1;
			}
			
			public event Support.EventHandler	StoreChanged;
			#endregion
			
			public int							CountBundleFields
			{
				get
				{
					if (this.bundle != null)
					{
						return this.bundle.CountFields;
					}
					
					return 0;
				}
			}
			
			
			protected ResourceBundle			bundle;
			protected StringEditController		controller;
			protected string					col_0_cache;
			protected string					col_1_cache;
			protected int						changing;
		}
		
		
		protected Panels.StringEditPanel CreatePanel(ResourceBundle bundle)
		{
			Panels.StringEditPanel panel  = new Panels.StringEditPanel (new Store (this, bundle));
			
			Widget  widget = panel.Widget;
			Window  window = this.Window;
			TabPage page   = new TabPage ();
			
			System.Diagnostics.Debug.Assert (this.window != null);
			System.Diagnostics.Debug.Assert (this.tab_book != null);
			
			window.Root.DockMargins = new Drawing.Margins (8, 8, 8, 8);
			
			widget.Dock   = DockStyle.Fill;
			widget.Parent = page;
			
			string name = bundle.Name;
			
			switch (bundle.ResourceLevel)
			{
				case ResourceLevel.Default:
					break;
				case ResourceLevel.Localised:
					name = string.Format ("{0} ({1})", name, bundle.Culture.TwoLetterISOLanguageName);
					break;
				case ResourceLevel.Customised:
					name = string.Format ("{0} ({1})", name, "X");
					break;
			}
			
			page.TabTitle = name;
			
			this.tab_book.Items.Add (page);
			
			return panel;
		}
		
		protected void CreateWindow()
		{
			Drawing.Size size = Panels.StringEditPanel.DefaultSize;
			
			this.window = new Window ();
			this.window.ClientSize = size + new Drawing.Size (20, 60);
			this.window.CommandDispatcher = this.dispatcher;
			this.Window.Text = "Ressources textuelles";
			
			this.tab_book = new TabBook ();
			this.tab_book.Dock = DockStyle.Fill;
			this.tab_book.Parent = this.window.Root;
			this.tab_book.HasCloseButton = true;
			this.tab_book.CloseClicked += new EventHandler (this.HandleTabBookCloseClicked);
		}
		
		
		[Command ("CreateStringBundle")] void CommandCreateStringBundle(CommandDispatcher d, CommandEventArgs e)
		{
			if (e.CommandArgs.Length == 0)
			{
				Dialogs.BundleName dialog = new Dialogs.BundleName ("CreateStringBundle (\"{0}\", \"{1}\", {2}, {3})", this.dispatcher);
				dialog.Owner = this.Window;
				dialog.Show ();
			}
			else if (e.CommandArgs.Length == 4)
			{
				string bundle_prefix = e.CommandArgs[0];
				string bundle_id     = e.CommandArgs[1];
				string bundle_level  = e.CommandArgs[2];
				string culture_code  = e.CommandArgs[3];
				
				string      full_id = bundle_prefix + bundle_id;
				ResourceLevel level = (ResourceLevel) System.Enum.Parse (typeof (ResourceLevel), bundle_level);
				CultureInfo culture = Resources.FindCultureInfo (culture_code);
				
				this.AttachNewBundle (full_id, bundle_prefix, level, culture);
			}
			else
			{
				this.ThrowInvalidOperationException (e, 1);
			}
		}
		
		[Command ("OpenStringBundle")]  void CommandOpenStringBundle(CommandDispatcher d, CommandEventArgs e)
		{
			if (e.CommandArgs.Length == 0)
			{
				Dialogs.OpenExistingBundle dialog = new Dialogs.OpenExistingBundle ("OpenStringBundle (\"{0}\", {1})", this.dispatcher);
				dialog.Owner = this.Window;
				dialog.Show ();
			}
			else if (e.CommandArgs.Length == 2)
			{
				ResourceLevel  level  = (ResourceLevel) System.Enum.Parse (typeof (ResourceLevel), e.CommandArgs[1]);
				ResourceBundle bundle = Resources.GetBundle (e.CommandArgs[0], level);
				this.AttachExistingBundle (bundle);
			}
			else
			{
				this.ThrowInvalidOperationException (e, 2);
			}
		}
		
		[Command ("SaveStringBundle")] void CommandSaveStringBundle(CommandDispatcher d, CommandEventArgs e)
		{
			if (e.CommandArgs.Length > 0)
			{
				this.ThrowInvalidOperationException (e, 0);
			}
			
			ResourceBundle bundle = this.ActiveBundle;
			
			if (bundle != null)
			{
				Resources.SetBundle (bundle, ResourceSetMode.Write);
			}
		}
		
		private void ThrowInvalidOperationException(CommandEventArgs e, int num_expected)
		{
			string command   = e.CommandName;
			int    num_found = e.CommandArgs.Length;
			
			throw new System.InvalidOperationException (string.Format ("Command {0} requires {1} argument(s), got {2}.", command, num_expected, num_found));
		}
		
		private void HandleTabBookCloseClicked(object sender)
		{
			ResourceBundle bundle = this.ActiveBundle;
			
			if (bundle != null)
			{
				//	Ferme la page active...
				
				this.tab_book.Items.RemoveAt (this.tab_book.ActivePageIndex);
				this.bundles.Remove (bundle);
			}
		}
		
		private void ActivateTabBookPage()
		{
			this.tab_book.ActivePageIndex = this.bundles.Count - 1;
			this.tab_book.Invalidate ();
		}
		
		
		protected System.Collections.ArrayList	bundles;
		protected System.Collections.ArrayList	panels;
		protected Window						window;
		protected TabBook						tab_book;
		protected Support.CommandDispatcher		dispatcher;
	}
}
