using NUnit.Framework;

namespace Epsitec.Common.Script
{
	[TestFixture] public class SourceTest
	{
		[SetUp] public void Initialise()
		{
			Common.Widgets.Widget.Initialise ();
			Common.Pictogram.Engine.Initialise ();
			Common.Widgets.Adorner.Factory.SetActive ("LookMetal");
		}
		
		[Test] public void CheckSourceGeneration()
		{
			Common.UI.Data.Record record  = new Epsitec.Common.UI.Data.Record ();
			Types.IDataValue[]    values  = this.CreateValues (out record);
			
			Source source = this.CreateSource (values);
			
			string script_source = source.GenerateAssemblySource ();
			
			System.Console.Out.WriteLine (script_source);
			
			Engine engine = new Engine ();
			Script script = engine.Compile (script_source);
			
			if (script.HasErrors)
			{
				foreach (string error in script.Errors)
				{
					System.Console.Out.WriteLine (error);
				}
			}
			
			object[] a_in  = { 12, "Hello" };
			object[] a_out;
			
			script.Attach (record);
			
			Assert.IsTrue (script.Execute ("Main"));
			Assert.IsTrue (script.Execute ("Mysterious", a_in, out a_out));
			Assert.IsFalse (script.Execute ("MissingMethod"));
			
			Assert.IsNotNull (a_out);
			Assert.AreEqual (2, a_out.Length);
			Assert.AreEqual ("HELLO", a_out[0]);
			Assert.AreEqual (24, a_out[1]);
			Assert.AreEqual ("hello", record["UserName"].Value);
			
			script.Dispose ();
		}
		
		[Test] public void CheckParameterInfoStore()
		{
			Widgets.Window window = new Widgets.Window ();
			Helpers.ParameterInfoStore store = new Helpers.ParameterInfoStore ();
			Support.CommandDispatcher dispatcher = new Support.CommandDispatcher ("Table", true);
			
			store.SetContents (this.CreateSource (null).Methods[1].Parameters);
			
			window.Text             = "CheckParameterInfoStore";
			window.Root.DockPadding = new Drawing.Margins (4, 4, 8, 8);
			
			Widgets.EditArray            edit  = new Widgets.EditArray (window.Root);
			Widgets.EditArray.Header     title = new Widgets.EditArray.Header (edit);
			Widgets.EditArray.Controller ctrl  = new Widgets.EditArray.Controller (edit, "Table");
			
			edit.AutoResolveResRef = false;
			edit.CommandDispatcher = dispatcher;
			edit.Dock              = Widgets.DockStyle.Fill;
			edit.ColumnCount       = 3;
			edit.RowCount          = 0;
			
			Widgets.TextFieldCombo column_0_edit_model = new Widgets.TextFieldCombo ();
			Widgets.TextFieldCombo column_1_edit_model = new Widgets.TextFieldCombo ();
			Widgets.TextFieldEx    column_2_edit_model = new Widgets.TextFieldEx ();
			
			column_0_edit_model.IsReadOnly = true;
			column_0_edit_model.Items.AddRange (new string[] { "In", "Out", "InOut" });
			column_1_edit_model.IsReadOnly = true;
			column_1_edit_model.Items.AddRange (new string[] { "Integer", "Decimal", "String" });
			
			column_2_edit_model.ButtonShowCondition = Widgets.ShowCondition.WhenModified;
			column_2_edit_model.DefocusAction       = Widgets.DefocusAction.Modal;
			
			new Widgets.Validators.RegexValidator (column_2_edit_model, Support.RegexFactory.AlphaNumName, false);
			new Widgets.EditArray.UniqueValueValidator (column_2_edit_model, 2);
			
			edit.Columns[0].HeaderText = "Dir.";
			edit.Columns[0].Width      = 60;
			edit.Columns[0].EditionWidgetModel = column_0_edit_model;
			edit.Columns[1].HeaderText = "Type";
			edit.Columns[1].EditionWidgetModel = column_1_edit_model;
			edit.Columns[1].Width      = 80;
			edit.Columns[1].Elasticity = 0.5;
			edit.Columns[2].HeaderText = "Name";
			edit.Columns[2].EditionWidgetModel = column_2_edit_model;
			edit.Columns[2].Elasticity = 1.0;
			
			ctrl.CreateCommands ();
			ctrl.CreateToolBarButtons ();
			ctrl.StartReadOnly ();
			
			edit.TextArrayStore = store;
			
			window.Show ();
			
		}
		
		[Test] public void CheckDeveloper1()
		{
			Widgets.Window window = new Widgets.Window ();
			
			Developer.Panels.MethodProtoPanel   panel_1 = new Developer.Panels.MethodProtoPanel ();
			Developer.Panels.ParameterInfoPanel panel_2 = new Developer.Panels.ParameterInfoPanel ();
			
			window.Text             = "CheckDeveloper1";
			window.Root.DockPadding = new Drawing.Margins (4, 4, 8, 8);
			window.ClientSize       = new Drawing.Size (8+200, 16+120);
			
			Source        source = this.CreateSource (null);
			Source.Method method = source.Methods[1];
			
			panel_1.MethodName    = method.Name;
			panel_1.MethodType    = method.ReturnType;
			panel_1.Widget.Parent = window.Root;
			panel_1.Widget.Dock   = Widgets.DockStyle.Top;
			
			panel_2.Parameters    = method.Parameters;
			panel_2.Widget.Parent = window.Root;
			panel_2.Widget.Dock   = Widgets.DockStyle.Fill;
			
			window.Show ();
		}
		
		[Test] public void CheckDeveloper2()
		{
			Widgets.Window window = new Widgets.Window ();
			
			Developer.Panels.MethodEditionPanel panel = new Developer.Panels.MethodEditionPanel ();
			
			window.Text             = "CheckDeveloper2";
			window.Root.DockPadding = new Drawing.Margins (4, 4, 8, 8);
			window.ClientSize       = new Drawing.Size (8+panel.Size.Width, 16+panel.Size.Height);
			
			Source        source = this.CreateSource (null);
			Source.Method method = source.Methods[1];
			
			panel.Widget.Parent = window.Root;
			panel.Widget.Dock   = Widgets.DockStyle.Fill;
			panel.Method        = method;
			
			window.Show ();
		}
		
		[Test] public void CheckDeveloper3()
		{
			Widgets.Window window = new Widgets.Window ();
			
			Common.UI.Data.Record record  = new Epsitec.Common.UI.Data.Record ();
			Types.IDataValue[]    values  = this.CreateValues (out record);
			
			Developer.EditionController controller = new Developer.EditionController ();
			
			window.Text       = "CheckDeveloper3";
			window.ClientSize = new Drawing.Size (600, 400);
			
			Source source = this.CreateSource (values);
			
			controller.Source = source;
			controller.CreateWidgets (window.Root);
			
			window.Show ();
		}
		
		private Source CreateSource(Types.IDataValue[] values)
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
		
		private Types.IDataValue[] CreateValues(out Common.UI.Data.Record record)
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
