/*
This file is part of CreativeDocs.

Copyright © 2003-2024, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland

CreativeDocs is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
any later version.

CreativeDocs is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/


using System.Collections.Generic;

namespace Epsitec.Common.Support
{
    /// <summary>
    /// La classe Globals permet de stocker des variables globales à une application.
    /// </summary>
    public sealed class Globals
    {
        private Globals()
        {
            this.propertyHash = new Dictionary<string, object>();
        }

        static Globals()
        {
            Globals.properties = new Globals();
        }

        public static Globals Properties
        {
            get { return Globals.properties; }
        }

        public static string ExecutableName
        {
            get
            {
                /*
                string name = System.Windows.Forms.Application.ExecutablePath;
                return System.IO.Path.GetFileName(name);
                */
                throw new System.NotImplementedException();
                return null;
            }
        }

        public static string ExecutablePath
        {
            get
            {
                /*
                string name = System.Windows.Forms.Application.ExecutablePath;
                return name;
                */
                throw new System.NotImplementedException();
                return null;
            }
        }

        public static string ExecutableDirectory
        {
            //get { return System.Windows.Forms.Application.StartupPath; }
            get
            {
                throw new System.NotImplementedException();
                return null;
            }
        }

        public object this[string key]
        {
            //	On peut accéder aux propriétés globales très simplement au moyen de l'opérateur [],
            //	mais contrairement à GetProperty, une exception est levée si la propriété demandée
            //	n'existe pas.

            get
            {
                if (this.IsPropertyDefined(key))
                {
                    return this.GetProperty(key);
                }

                throw new System.ArgumentOutOfRangeException(
                    "key",
                    key,
                    string.Format("Cannot find the global property named '{0}'.", key)
                );
            }
            set { this.SetProperty(key, value); }
        }

        public static bool IsDebugBuild
        {
            get
            {
                if (Globals.isDebugBuildInitialized == false)
                {
                    Globals.isDebugBuild =
                        typeof(Globals).Assembly.Location.Contains("Debug")
                        || Globals.Directories.ExecutableRoot.Contains(
                            Globals.EpsitecCresusSubPath
                        );
                    Globals.isDebugBuildInitialized = true;
                }

                return Globals.isDebugBuild;
            }
        }

        public static string DebugBuildSourcePath
        {
            get
            {
                if (Globals.IsDebugBuild)
                {
                    var app = Globals.Directories.ExecutableRoot;
                    int pos = app.IndexOf(Globals.EpsitecCresusSubPath);
                    return app.Substring(0, pos + Globals.EpsitecCresusSubPath.Length - 1);
                }

                throw new System.InvalidOperationException(
                    "No source path for a deployed application"
                );
            }
        }

        public string[] GetPropertyNames()
        {
            string[] names = new string[this.propertyHash.Count];
            this.propertyHash.Keys.CopyTo(names, 0);
            System.Array.Sort(names);

            return names;
        }

        public void SetProperty(string key, object value)
        {
            lock (this)
            {
                if (Types.UndefinedValue.IsUndefinedValue(value))
                {
                    this.propertyHash.Remove(key);
                }
                else
                {
                    this.propertyHash[key] = value;
                }
            }
        }

        public object GetProperty(string key)
        {
            lock (this)
            {
                object value;

                if (this.propertyHash.TryGetValue(key, out value))
                {
                    return value;
                }
            }

            return Types.UndefinedValue.Value;
        }

        public T GetProperty<T>(string key)
        {
            object value = this.GetProperty(key);

            if (Types.UndefinedValue.IsUndefinedValue(value))
            {
                return default(T);
            }
            else
            {
                return (T)value;
            }
        }

        public T GetProperty<T>(string key, T defaultValue)
        {
            object value = this.GetProperty(key);

            if (Types.UndefinedValue.IsUndefinedValue(value))
            {
                return defaultValue;
            }
            else
            {
                return (T)value;
            }
        }

        public bool IsPropertyDefined(string key)
        {
            lock (this)
            {
                return this.propertyHash.ContainsKey(key);
            }
        }

        public void ClearProperty(string key)
        {
            lock (this)
            {
                this.propertyHash.Remove(key);
            }
        }

        #region Directories Class

        public static class Directories
        {
            public static string CommonAppDataRevision
            {
                //get { return System.Windows.Forms.Application.CommonAppDataPath; }
                get { return null; }
            }

            public static string CommonAppData
            {
                get
                {
                    string path = Directories.CommonAppDataRevision;
                    return System.IO.Path.GetDirectoryName(path);
                }
            }

            public static string UserAppDataRevision
            {
                //get { return System.Windows.Forms.Application.UserAppDataPath; }
                get { return null; }
            }

            public static string UserAppData
            {
                get
                {
                    string path = Directories.UserAppDataRevision;
                    return System.IO.Path.GetDirectoryName(path);
                }
            }

            public static string Executable
            {
                get { return System.Reflection.Assembly.GetExecutingAssembly().Location; }
            }

            public static string ExecutableRoot
            {
                get { return IO.PathTools.RemoveUntilDir("bin", Directories.Executable); }
            }

            public static string InitialDirectory
            {
                get { return Directories.initialDirectory; }
            }

            public static string ProgramFiles
            {
                get
                {
                    return System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.ProgramFiles
                    );
                }
            }

            public static string Windows
            {
                get
                {
                    return System.IO.Path.GetDirectoryName(
                        System.Environment.GetFolderPath(System.Environment.SpecialFolder.System)
                    );
                }
            }

            private static readonly string initialDirectory =
                System.IO.Directory.GetCurrentDirectory();
        }

        #endregion

        private const string EpsitecCresusSubPath = @"\Epsitec.Cresus\";

        private Dictionary<string, object> propertyHash;
        private static Globals properties;
        private static bool isDebugBuild;
        private static bool isDebugBuildInitialized;
    }
}
