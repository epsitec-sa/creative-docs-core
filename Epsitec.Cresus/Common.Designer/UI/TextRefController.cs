//	Copyright � 2004, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Statut : en chantier/PA

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;

namespace Epsitec.Common.Designer.UI
{
	/// <summary>
	/// Summary description for TextRefController.
	/// </summary>
	public class TextRefController : Epsitec.Common.UI.Controllers.AbstractController
	{
		public TextRefController()
		{
		}
		
		
		public override void CreateUI(Widget panel)
		{
			this.caption_label = new StaticText (panel);
			this.combo_text    = new TextFieldCombo (panel);
			this.label_bundle  = new StaticText (panel);
			this.combo_bundle  = new TextFieldCombo (panel);
			this.label_field   = new StaticText (panel);
			this.combo_field   = new TextFieldExList (panel);
			
			panel.Height = System.Math.Max (panel.Height, 82);
			
			this.caption_label.Width         = 80;
			this.caption_label.Anchor        = AnchorStyles.TopLeft;
			this.caption_label.AnchorMargins = new Drawing.Margins (0, 0, 8, 0);
			
			this.combo_text.Anchor           = AnchorStyles.LeftAndRight | AnchorStyles.Top;
			this.combo_text.AnchorMargins    = new Drawing.Margins (this.caption_label.Right, 0, 4, 0);
			this.combo_text.TextChanged     += new EventHandler (this.HandleComboTextTextChanged);
			this.combo_text.TextEdited      += new EventHandler (this.HandleComboTextTextEdited);
			this.combo_text.OpeningCombo    += new CancelEventHandler (this.HandleComboTextOpeningCombo);
			this.combo_text.ButtonGlyphShape = GlyphShape.Dots;
			
			this.label_bundle.Width          = this.caption_label.Width;
			this.label_bundle.Anchor         = this.caption_label.Anchor;
			this.label_bundle.AnchorMargins  = new Drawing.Margins (0, 0, 8+20+8, 0);
			this.label_bundle.Text           = "Bundle Name";
			
			this.combo_bundle.Anchor         = AnchorStyles.LeftAndRight | AnchorStyles.Top;
			this.combo_bundle.AnchorMargins  = new Drawing.Margins (this.caption_label.Right, 0, 4+20+8, 0);
			this.combo_bundle.IsReadOnly     = true;
			
			this.label_field.Width           = this.caption_label.Width;
			this.label_field.Anchor          = this.caption_label.Anchor;
			this.label_field.AnchorMargins   = new Drawing.Margins (0, 0, 8+20+8+20+4, 0);
			this.label_field.Text            = "Field Name";
			
			this.combo_field.Anchor          = AnchorStyles.LeftAndRight | AnchorStyles.Top;
			this.combo_field.AnchorMargins   = new Drawing.Margins (this.caption_label.Right, 0, 4+20+8+20+4, 0);
			this.combo_field.IsReadOnly      = true;
			this.combo_field.PlaceHolder     = "<b>&lt;create new field&gt;</b>";
			this.combo_field.AutoErasing    += new CancelEventHandler (this.HandleComboFieldAutoErasing);
			
			IValidator field_validator  = new Common.Widgets.Validators.RegexValidator (this.combo_field, RegexFactory.ResourceName, false);
			IValidator bundle_validator = new Common.Widgets.Validators.RegexValidator (this.combo_bundle, RegexFactory.ResourceName, false);
			
			this.combo_bundle.SelectedIndexChanged += new EventHandler (this.HandleComboBundleSelectedIndexChanged);
			this.combo_field.SelectedIndexChanged  += new EventHandler (this.HandleComboFieldSelectedIndexChanged);
			this.combo_field.EditionValidated      += new EventHandler (this.HandleComboFieldEditionValidated);
			this.combo_field.EditionCancelled      += new EventHandler (this.HandleComboFieldEditionCancelled);
			
			this.OnCaptionChanged ();
			
			this.SyncFromAdapter (Common.UI.SyncReason.Initialisation);
		}
		
		public override void SyncFromAdapter(Common.UI.SyncReason reason)
		{
			if (reason == Common.UI.SyncReason.SourceChanged)
			{
				this.State = InternalState.Passive;
			}
			
			TextRefAdapter adapter = this.Adapter as TextRefAdapter;
			
			if ((adapter != null) &&
				(this.combo_text != null))
			{
				string current_text = adapter.Value;
				string stored_text  = adapter.GetFieldValue ();
				
				this.combo_text.Text = current_text;
				
				if (current_text == stored_text)
				{
					string field  = adapter.FieldName;
					string bundle = adapter.BundleName;
					
					this.UpdateBundleList (adapter);
					this.UpdateFieldList (adapter, bundle);
					
					this.combo_bundle.Text   = bundle;
					this.combo_bundle.Cursor = 0;
					this.combo_field.Text    = field;
					this.combo_field.Cursor  = 0;
				}
				else if (stored_text == null)
				{
					this.State = InternalState.UsingUndefinedText;
				}
				
				if (reason != Common.UI.SyncReason.ValueChanged)
				{
					this.combo_text.SelectAll ();
				}
			}
		}
		
		public override void SyncFromUI()
		{
			TextRefAdapter adapter = this.Adapter as TextRefAdapter;
			
			if ((adapter != null) &&
				(this.combo_text != null))
			{
				adapter.Value = this.combo_text.Text;
			}
		}
		
		
		protected void UpdateBundleList(TextRefAdapter adapter)
		{
			if (adapter != null)
			{
				this.combo_bundle.Items.Clear ();
				this.combo_bundle.Items.AddRange (adapter.StringController.GetStringBundleNames ());
			}
		}
		
		protected void UpdateFieldList(TextRefAdapter adapter, string name)
		{
			if (adapter != null)
			{
				if (name == "")
				{
					this.combo_field.SetEnabled (false);
				}
				else
				{
					this.combo_field.SetEnabled (true);
					this.combo_field.Items.Clear ();
					this.combo_field.Items.AddRange (adapter.StringController.GetStringFieldNames (name));
				}
			}
		}
		
		
		private void HandleComboTextTextChanged(object sender)
		{
			this.SyncFromUI ();
		}
		
		private void HandleComboTextTextEdited(object sender)
		{
			this.State = InternalState.UsingCustomText;
		}
		
		private void HandleComboTextOpeningCombo(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			
			//	TODO: ouvre le dialogue permettant de choisir un texte.
		}
		
		private void HandleComboBundleSelectedIndexChanged(object sender)
		{
			System.Diagnostics.Debug.Assert (sender == this.combo_bundle);
			TextRefAdapter adapter = this.Adapter as TextRefAdapter;
			this.UpdateFieldList (adapter, this.combo_bundle.SelectedItem);
		}
		
		private void HandleComboFieldSelectedIndexChanged(object sender)
		{
			System.Diagnostics.Debug.Assert (sender == this.combo_field);
			TextRefAdapter adapter = this.Adapter as TextRefAdapter;
			
			string bundle = this.combo_bundle.SelectedItem;
			string field  = this.combo_field.SelectedItem;
			
			if ((bundle != "") &&
				(field != ""))
			{
				adapter.XmlRefTarget = ResourceBundle.MakeTarget (bundle, field);
				
				this.SyncFromAdapter (Common.UI.SyncReason.AdapterChanged);
				this.State = InternalState.UsingExistingText;
			}
			else
			{
				this.State = InternalState.Passive;
			}
		}
		
		private void HandleComboFieldEditionValidated(object sender)
		{
			System.Diagnostics.Debug.Assert (sender == this.combo_field);
			
			//	Il faut cr�er un nouveau champ dans le bundle.
			
			System.Diagnostics.Debug.WriteLine (string.Format ("Create field {0}#{1}.", this.combo_bundle.Text, this.combo_field.Text));
		}
		
		private void HandleComboFieldEditionCancelled(object sender)
		{
			System.Diagnostics.Debug.Assert (sender == this.combo_field);
			
			System.Diagnostics.Debug.WriteLine (string.Format ("Cancelled field, restored to {0}#{1}.", this.combo_bundle.Text, this.combo_field.Text));
		}
		
		private void HandleComboFieldAutoErasing(object sender, CancelEventArgs e)
		{
			System.Diagnostics.Debug.Assert (sender == this.combo_field);
			
			if (this.combo_field.Text != this.combo_field.PlaceHolder)
			{
				e.Cancel = true;
			}
		}
		
		
		
		private InternalState					State
		{
			get
			{
				return this.state;
			}
			set
			{
				if (this.state != value)
				{
					switch (value)
					{
						case InternalState.Passive:
							this.combo_field.CancelEdition ();
							break;
						case InternalState.UsingCustomText:
							this.combo_field.StartPassiveEdition (this.combo_field.PlaceHolder);
							break;
						case InternalState.UsingExistingText:
							this.combo_field.CancelEdition ();
							break;
						case InternalState.UsingUndefinedText:
							this.combo_field.StartPassiveEdition (this.combo_field.PlaceHolder);
							break;
					}
					
					this.state = value;
				}
			}
		}
		
		
		private enum InternalState
		{
			None,
			Passive,
			UsingCustomText,
			UsingUndefinedText,
			UsingExistingText
		}
		
		
		private InternalState					state;
		private TextFieldCombo					combo_text;
		
		private StaticText						label_field;
		private StaticText						label_bundle;
		
		private TextFieldCombo					combo_bundle;
		private TextFieldExList					combo_field;
	}
}
