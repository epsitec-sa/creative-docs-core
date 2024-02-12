//	Automatically generated by ResGenerator, on 30.05.2006 08:16:05
//	Do not edit manually.

namespace Epsitec.Common.Tests.Support
{
    public sealed class TestRes
    {
        public sealed class Strings
        {
            public sealed class Label
            {
                public static string Hello
                {
                    get { return GetText("strings", "label", "Hello"); }
                }
                public static string OK
                {
                    get { return GetText("strings", "label", "OK"); }
                }
            }

            public sealed class Title
            {
                public static string AboutWindow
                {
                    get { return GetText("strings", "title", "AboutWindow"); }
                }
                public static string MainWindow
                {
                    get { return GetText("strings", "title", "MainWindow"); }
                }
                public static string SettingsWindow
                {
                    get { return GetText("strings", "title", "SettingsWindow"); }
                }

                public sealed class Dialog
                {
                    public static string Open
                    {
                        get { return GetText("strings", "title", "Dialog", "Open"); }
                    }
                    public static string Save
                    {
                        get { return GetText("strings", "title", "Dialog", "Save"); }
                    }
                }
            }

            public static string GetString(params string[] path)
            {
                string field = string.Join(".", path);
                return _bundle[field].AsString;
            }

            #region Internal Support Code
            private static string GetText(string bundle, params string[] path)
            {
                string field = string.Join(".", path);
                return _bundle[field].AsString;
            }

            private static Epsitec.Common.Support.ResourceBundle _bundle = _manager.GetBundle(
                "strings"
            );
            #endregion
        }

        public static void Initialize(System.Type type, string name)
        {
            _manager = new Epsitec.Common.Support.ResourceManager(type);
            _manager.DefineDefaultModuleName(name);
        }

        private static Epsitec.Common.Support.ResourceManager _manager = Epsitec
            .Common
            .Support
            .Resources
            .DefaultManager;
    }
}
