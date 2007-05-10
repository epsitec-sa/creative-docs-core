//	Copyright � 2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Types;

using System.Collections.Generic;

namespace Epsitec.Common.Support.ResourceAccessors
{
	using CultureInfo=System.Globalization.CultureInfo;
	
	/// <summary>
	/// The <c>CaptionResourceAccessor</c> is used to access text resources,
	/// stored in the <c>Strings</c> resource bundle.
	/// </summary>
	public class CaptionResourceAccessor : AbstractCaptionResourceAccessor
	{
		public CaptionResourceAccessor()
		{
		}

		protected override string GetNameFromFieldName(string fieldName)
		{
			System.Diagnostics.Debug.Assert (fieldName.StartsWith ("Cap."));
			return fieldName.Substring (4);
		}

		protected override string GetFieldNameFromName(StructuredData data, string name)
		{
			return "Cap." + name;
		}

		protected override Caption GetCaptionFromData(StructuredData data, string name)
		{
			string description = data.GetValue (Res.Fields.ResourceCaption.Description) as string;
			string icon = data.GetValue (Res.Fields.ResourceCaption.Icon) as string;

			Caption caption = new Caption ();

			IEnumerable<string> labels = data.GetValue (Res.Fields.ResourceCaption.Labels) as IEnumerable<string>;

			if (labels != null)
			{
				foreach (string label in labels)
				{
					caption.Labels.Add (label);
				}
			}

			if (name != null)
			{
				caption.Name = name;
			}

			if (description != null)
			{
				caption.Description = description;
			}

			if (icon != null)
			{
				caption.Icon = icon;
			}

			return caption;
		}

		protected override void FillDataFromCaption(StructuredData data, Caption caption)
		{
			Types.Collections.ObservableList<string> labels = new Epsitec.Common.Types.Collections.ObservableList<string> ();
			labels.AddRange (caption.Labels);

			data.SetValue (Res.Fields.ResourceCaption.Labels, labels);
			data.LockValue (Res.Fields.ResourceCaption.Labels);

			if (caption.Description != null)
			{
				data.SetValue (Res.Fields.ResourceCaption.Description, caption.Description);
			}
			if (caption.Icon != null)
			{
				data.SetValue (Res.Fields.ResourceCaption.Icon, caption.Icon);
			}
		}

		protected override bool FilterField(ResourceBundle.Field field)
		{
			return (!string.IsNullOrEmpty (field.Name))
				&& (field.Name.StartsWith ("Cap."));
		}
	}
}
