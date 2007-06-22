//	Copyright � 2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;
using System.Globalization;

namespace Epsitec.Common.Support
{
	/// <summary>
	/// The <c>ResourceModule</c> class is used to manipulate module related
	/// information (see <see cref="ResourceModuleInfo"/>).
	/// </summary>
	public static class ResourceModule
	{
		/// <summary>
		/// Loads the module information for a specified module path. The information
		/// is loaded from a manifest file (<see cref="ResourceModule.ManifestFileName"/>).
		/// </summary>
		/// <param name="modulePath">The module path.</param>
		/// <returns>The module information or <c>ResourceModuleInfo.Empty</c>.</returns>
		public static ResourceModuleInfo LoadManifest(string modulePath)
		{
			if (System.IO.Directory.Exists (modulePath))
			{
				System.IO.TextReader textReader = null;
				string           moduleInfoPath = System.IO.Path.Combine (modulePath, ResourceModule.ManifestFileName);

				if (System.IO.File.Exists (moduleInfoPath))
				{
					try
					{
						textReader = System.IO.File.OpenText (moduleInfoPath);
					}
					catch (System.IO.FileNotFoundException)
					{
					}
					catch (System.IO.PathTooLongException)
					{
					}
				}

				if (textReader != null)
				{
					using (textReader)
					{
						System.Xml.XmlDocument xml = new System.Xml.XmlDocument ();
						xml.Load (textReader);

						System.Xml.XmlElement root = xml.DocumentElement;

						if (root.Name == ResourceModule.XmlModuleInfo)
						{
							int                 moduleId    = -1;
							ResourceModuleLayer moduleLayer = ResourceModuleLayer.Undefined;
							string              moduleName  = null;

							//	The module.info file contains a root node <ModuleInfo>
							//	which defines following attributes :
							//	- id, the numeric identifier for the module
							//	- name, the textual identifier for the module
							//	- layer, the code for the resource module layer

							string idAttribute    = root.GetAttribute (ResourceModule.XmlAttributeId);
							string layerAttribute = root.GetAttribute (ResourceModule.XmlAttributeLayer);
							string nameAttribute  = root.GetAttribute (ResourceModule.XmlAttributeName);

							if (string.IsNullOrEmpty (idAttribute))
							{
								throw new System.FormatException (string.Format ("{0} specifies no {1} attribute", ResourceModule.XmlModuleInfo, ResourceModule.XmlAttributeId));
							}
							if (string.IsNullOrEmpty (layerAttribute))
							{
								throw new System.FormatException (string.Format ("{0} specifies no {1} attribute", ResourceModule.XmlModuleInfo, ResourceModule.XmlAttributeLayer));
							}
							if (string.IsNullOrEmpty (nameAttribute))
							{
								throw new System.FormatException (string.Format ("{0} specifies no {1} attribute", ResourceModule.XmlModuleInfo, ResourceModule.XmlAttributeName));
							}

							int.TryParse (idAttribute, NumberStyles.Integer, CultureInfo.InvariantCulture, out moduleId);
							moduleLayer = ResourceModuleInfo.ConvertPrefixToLayer (layerAttribute);
							moduleName  = nameAttribute;

							return new ResourceModuleInfo (moduleName, modulePath, moduleId, moduleLayer);
						}
					}
				}
			}

			return ResourceModuleInfo.Empty;
		}

		/// <summary>
		/// Saves the module definition information into the associated manifest
		/// file (<see cref="ResourceModule.ManifestFileName"/>).
		/// </summary>
		/// <param name="info">The module information.</param>
		public static void SaveManifest(ResourceModuleInfo info)
		{
			string modulePath     = info.Path;
			string moduleInfoPath = System.IO.Path.Combine (modulePath, ResourceModule.ManifestFileName);
	
			System.Xml.XmlDocument xml = new System.Xml.XmlDocument ();
			System.Xml.XmlElement root = xml.CreateElement (ResourceModule.XmlModuleInfo);

			root.SetAttribute (ResourceModule.XmlAttributeId, info.Id.ToString (System.Globalization.CultureInfo.InvariantCulture));
			root.SetAttribute (ResourceModule.XmlAttributeName, info.Name);
			root.SetAttribute (ResourceModule.XmlAttributeLayer, ResourceModuleInfo.ConvertLayerToPrefix (info.Layer));
			
			xml.InsertBefore (xml.CreateXmlDeclaration ("1.0", "utf-8", null), null);
			xml.AppendChild (root);

			xml.Save (moduleInfoPath);
		}

		/// <summary>
		/// Finds all possible module paths given a root path. This will walk
		/// through the full tree, attempting to locate directories with a
		/// manifest file.
		/// </summary>
		/// <param name="rootPath">The root path.</param>
		/// <returns>The enumeration of plausible module paths.</returns>
		public static IEnumerable<string> FindModulePaths(string rootPath)
		{
			foreach (string path in System.IO.Directory.GetDirectories (rootPath, "*", System.IO.SearchOption.AllDirectories))
			{
				string probe = System.IO.Path.Combine (path, ResourceModule.ManifestFileName);
				bool   found = false;

				try
				{
					found = System.IO.File.Exists (probe);
				}
				catch (System.IO.IOException)
				{
					//	Ignore file related exceptions; we don't want to stop if
					//	we try to analyze some protected directory: just skip it.
				}

				if (found)
				{
					yield return path;
				}
			}
		}
		
		public const string ManifestFileName = "module.info";

		#region Internal constants

		internal const string XmlModuleInfo = "ModuleInfo";

		internal const string XmlAttributeId    = "id";
		internal const string XmlAttributeLayer = "layer";
		internal const string XmlAttributeName  = "name";

		#endregion
	}
}
