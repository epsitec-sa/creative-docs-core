using NUnit.Framework;
using Epsitec.Common.UI.Data;
using Epsitec.Common.Script;

namespace Epsitec.Common.Dialogs
{
	[TestFixture] public class DialogTest
	{
		[SetUp] public void SetUp()
		{
			Epsitec.Common.UI.Engine.Initialise ();
			Epsitec.Common.Pictogram.Engine.Initialise ();
		}
		
		[Test] public void CheckLoadDesignerFactory()
		{
			Assert.IsTrue (Dialog.LoadDesignerFactory ());
		}
		
		[Test] public void CheckCreateDesigner()
		{
			IDialogDesigner designer = Dialog.CreateDesigner ();
			
			Assert.IsNotNull (designer);
		}
		
		[Test] public void CheckCreateEmptyDialog()
		{
			IDialogDesigner designer = Dialog.CreateDesigner ();
			Widgets.Window  window   = new Widgets.Window ();
			
			Assert.IsNotNull (designer);
			
			designer.DialogWindow = window;
			designer.StartDesign ();
		}
		
		[Test] public void CheckLoad1Unknown()
		{
			Dialog dialog = new Dialog ();
			dialog.Load ("unknown_dialog");
			
			Assert.IsTrue (dialog.IsLoaded);
			Assert.IsFalse (dialog.IsReady);
			
			dialog.OpenDialog ();
		}
		
		[Test] public void CheckLoad2SimpleTest()
		{
			Dialog dialog = new Dialog ();
			dialog.Load ("test");
			dialog.OpenDialog ();
		}
		
		[Test] public void CheckLoad3WithData()
		{
			Epsitec.Common.Widgets.Adorner.Factory.SetActive ("LookPastel");
			
			Dialog dialog = new Dialog ("CheckLoad3WithData");
			
			dialog.CommandDispatcher.RegisterController (this);
			
			Record record = new Record ("Rec", "dialog_with_data_strings");
			
			record.AddField ("UserName", "Test", new Types.StringType (), new Support.RegexConstraint (Support.PredefinedRegex.Alpha));
			record.AddField ("UserAge",  10);
			record.AddField ("Representation", Representation.None);
			
			record.FieldChanged += new Support.EventHandler (this.HandleFieldChanged);
			
			dialog.AddRule (record.Validator, "Ok;Apply");
			
			Assertion.AssertEquals ("Test", record["UserName"].Value);
			Assertion.AssertEquals (10, record["UserAge"].Value);
			Assertion.AssertEquals (Representation.None, record["Representation"].Value);
			
//			ScriptWrapper script = new ScriptWrapper ();
			
//			script.Source = DialogTest.CreateSource (null);
			
//			dialog.CommandDispatcher.RegisterExtraDispatcher (script);
			
			dialog.Load ("dialog_with_data");
			dialog.Data = record;
//			dialog.Script = script;
			
			this.dialog = dialog;
			this.dialog.StoreInitialData ();
			
			//	Ouvre le dialogue modal (ce qui bloque !)
			
			dialog.OpenDialog ();
			
			
//			object document = Editor.Engine.CreateDocument (script);
//			Editor.Engine.ShowMethod (document, "Main");
		}
		
		private Dialog dialog;
		
		[Support.Command ("Ok")] private void CommandOk()
		{
			System.Diagnostics.Debug.WriteLine ("OK executed.");
		}
		
		[Support.Command ("Cancel")] private void CommandCancel()
		{
			this.dialog.RestoreInitialData ();
			System.Diagnostics.Debug.WriteLine ("Cancel executed.");
		}
		
		[Support.Command ("Apply")] private void CommandApply()
		{
			this.dialog.StoreInitialData ();
			System.Diagnostics.Debug.WriteLine ("Apply executed.");
		}
		
		
		private void HandleFieldChanged(object sender)
		{
			Field field = sender as Field;
			
			System.Diagnostics.Debug.WriteLine ("Field " + field.Name + " changed to " + field.Value.ToString () + (field.IsValueValid ? "" : "(invalid)"));
		}
		
		
		public static Source CreateSource(Types.IDataValue[] values)
		{
			Source.Method[]      methods = new Source.Method[2];
			Source.CodeSection[] code_1  = new Source.CodeSection[1];
			Source.CodeSection[] code_2  = new Source.CodeSection[1];
			Source.ParameterInfo[] par_2 = new Source.ParameterInfo[3];
			
			string code_1_source = "System.Diagnostics.Debug.WriteLine (&quot;Executing the &apos;Main&apos; script. UserName set to &apos;&quot; + this.UserName + &quot;&apos;.&quot;);<br/>";
			string code_2_source = "System.Diagnostics.Debug.WriteLine (&quot;Executing the &apos;Mysterious&apos; script. arg1=&quot; + arg1 + &quot;, arg2=&quot; + arg2);<br/>arg2 = arg2.ToUpper ();<br/>arg3 = arg1 * 2;<br/>this.UserName = arg2.ToLower ();<br/>";
			
			code_1[0]  = new Source.CodeSection (Source.CodeType.Local, code_1_source);
			code_2[0]  = new Source.CodeSection (Source.CodeType.Local, code_2_source);
			
			par_2[0] = new Source.ParameterInfo (Source.ParameterDirection.In, new Types.IntegerType (), "arg1");
			par_2[1] = new Source.ParameterInfo (Source.ParameterDirection.InOut, new Types.StringType (), "arg2");
			par_2[2] = new Source.ParameterInfo (Source.ParameterDirection.Out, new Types.IntegerType (), "arg3");
			
			methods[0] = new Source.Method ("Main", Types.VoidType.Default, null, code_1);
			methods[1] = new Source.Method ("Mysterious", Types.VoidType.Default, par_2, code_2);
			
			return new Source ("Hello", methods, values, "");
		}
		
		public static Types.IDataValue[] CreateValues(out Common.UI.Data.Record record)
		{
			record  = new Epsitec.Common.UI.Data.Record ();
			
			Types.IDataValue[]    values  = new Types.IDataValue[1];
			Common.UI.Data.Field  field_1 = new Epsitec.Common.UI.Data.Field ("UserName", "anonymous", new Types.StringType ());
			
			record.Add (field_1);
			
			values[0] = field_1;
			
			return values;
		}
	}
}
