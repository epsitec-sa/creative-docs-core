//	Copyright � 2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Collections;

using System.Collections.Generic;

namespace Epsitec.Common.Support.ResourceAccessors
{
	using CultureInfo=System.Globalization.CultureInfo;
	
	/// <summary>
	/// The <c>CommandResourceAccessor</c> is used to access command resources,
	/// stored in the <c>Captions</c> resource bundle and which have a field
	/// name prefixed with <c>"Cmd."</c>.
	/// </summary>
	public class CommandResourceAccessor : CaptionResourceAccessor
	{
		public CommandResourceAccessor()
		{
		}

		public override IDataBroker GetDataBroker(StructuredData container, string fieldId)
		{
			if (fieldId == Res.Fields.ResourceCommand.Shortcuts.ToString ())
			{
				return new Broker ();
			}
			else
			{
				return base.GetDataBroker (container, fieldId);
			}
		}
		
		protected override string Prefix
		{
			get
			{
				return "Cmd.";
			}
		}

		protected override IStructuredType GetStructuredType()
		{
			return Res.Types.ResourceCommand;
		}

		/// <summary>
		/// Creates a caption based on the definitions stored in a data record.
		/// </summary>
		/// <param name="sourceBundle">The source bundle.</param>
		/// <param name="data">The data record.</param>
		/// <param name="name">The name of the caption.</param>
		/// <param name="twoLetterISOLanguageName">The two letter ISO language name.</param>
		/// <returns>A <see cref="Caption"/> instance.</returns>
		protected override Caption CreateCaptionFromData(ResourceBundle sourceBundle, Types.StructuredData data, string name, string twoLetterISOLanguageName)
		{
			Caption caption = base.CreateCaptionFromData (sourceBundle, data, name, twoLetterISOLanguageName);

			if (!Types.UndefinedValue.IsUndefinedValue (data.GetValue (Res.Fields.ResourceCommand.Statefull)))
			{
				Widgets.Command.SetStatefull (caption, (bool) data.GetValue (Res.Fields.ResourceCommand.Statefull));
			}
			if (!Types.UndefinedValue.IsUndefinedValue (data.GetValue (Res.Fields.ResourceCommand.DefaultParameter)))
			{
				Widgets.Command.SetDefaultParameter (caption, (string) data.GetValue (Res.Fields.ResourceCommand.DefaultParameter));
			}
			if (!Types.UndefinedValue.IsUndefinedValue (data.GetValue (Res.Fields.ResourceCommand.Group)))
			{
				Widgets.Command.SetGroup (caption, (string) data.GetValue (Res.Fields.ResourceCommand.Group));
			}

			IList<StructuredData> shortcuts = data.GetValue (Res.Fields.ResourceCommand.Shortcuts) as IList<StructuredData>;

			if (shortcuts != null)
			{
				IList<Widgets.Shortcut> target = Widgets.Shortcut.GetShortcuts (caption);
				target.Clear ();

				foreach (StructuredData item in shortcuts)
				{
					Widgets.Shortcut shortcut = new Widgets.Shortcut ();
					shortcut.SetValue (Widgets.Shortcut.KeyCodeProperty, item.GetValue (Res.Fields.Shortcut.KeyCode) as string);
					target.Add (shortcut);
				}
			}
			
			return caption;
		}

		/// <summary>
		/// Fills the data record from a given caption.
		/// </summary>
		/// <param name="item">The item associated with the data record.</param>
		/// <param name="data">The data record.</param>
		/// <param name="caption">The caption.</param>
		/// <param name="mode">The creation mode for the data record.</param>
		protected override void FillDataFromCaption(CultureMap item, Types.StructuredData data, Caption caption, DataCreationMode mode)
		{
			base.FillDataFromCaption (item, data, caption, mode);

			ObservableList<StructuredData> shortcuts = data.GetValue (Res.Fields.ResourceCommand.Shortcuts) as ObservableList<StructuredData>;

			if (shortcuts == null)
			{
				shortcuts = new ObservableList<StructuredData> ();
			}
			else if (Widgets.Shortcut.HasShortcuts (caption))
			{
				shortcuts.Clear ();
			}

			if (Widgets.Shortcut.HasShortcuts (caption))
			{
				foreach (Widgets.Shortcut shortcut in Widgets.Shortcut.GetShortcuts (caption))
				{
					StructuredData x = new StructuredData (Res.Types.Shortcut);
					
					x.SetValue (Res.Fields.Shortcut.KeyCode, shortcut.GetValue (Widgets.Shortcut.KeyCodeProperty));
					shortcuts.Add (x);
					
					if (mode == DataCreationMode.Public)
					{
						item.NotifyDataAdded (x);
					}
				}
			}

			if (UndefinedValue.IsUndefinedValue (data.GetValue (Res.Fields.ResourceCommand.Shortcuts)))
			{
				data.SetValue (Res.Fields.ResourceCommand.Shortcuts, shortcuts);
				data.LockValue (Res.Fields.ResourceCommand.Shortcuts);

				if (mode == DataCreationMode.Public)
				{
					shortcuts.CollectionChanged += new Listener (this, item).HandleCollectionChanged;
				}
			}

			object statefullValue        = caption.GetValue (Widgets.Command.StatefullProperty);
			object defaultParameterValue = caption.GetValue (Widgets.Command.DefaultParameterProperty);
			object groupValue            = caption.GetValue (Widgets.Command.GroupProperty);

			if (!Types.UndefinedValue.IsUndefinedValue (statefullValue))
			{
				data.SetValue (Res.Fields.ResourceCommand.Statefull, statefullValue);
			}
			if (!Types.UndefinedValue.IsUndefinedValue (defaultParameterValue))
			{
				data.SetValue (Res.Fields.ResourceCommand.DefaultParameter, defaultParameterValue);
			}
			if (!Types.UndefinedValue.IsUndefinedValue (groupValue))
			{
				data.SetValue (Res.Fields.ResourceCommand.Group, groupValue);
			}
		}

		/// <summary>
		/// Determines whether the specified data record describes an empty
		/// caption.
		/// </summary>
		/// <param name="data">The data record.</param>
		/// <returns>
		/// 	<c>true</c> if this is an empty caption; otherwise, <c>false</c>.
		/// </returns>
		protected override bool IsEmptyCaption(StructuredData data)
		{
			if (base.IsEmptyCaption (data))
			{
				object statefull        = data.GetValue (Res.Fields.ResourceCommand.Statefull);
				string defaultParameter = data.GetValue (Res.Fields.ResourceCommand.DefaultParameter) as string;
				string group            = data.GetValue (Res.Fields.ResourceCommand.Group) as string;

				IList<StructuredData> shortcuts = data.GetValue (Res.Fields.ResourceCommand.Shortcuts) as IList<StructuredData>;

				if ((UndefinedValue.IsUndefinedValue (statefull) || ((bool)statefull == false)) &&
					(ResourceBundle.Field.IsNullString (defaultParameter)) &&
					(ResourceBundle.Field.IsNullString (group)) &&
					((shortcuts == null) || (shortcuts.Count == 0)))
				{
					return true;
				}
			}
			
			return false;
		}

		/// <summary>
		/// Computes the difference between a raw data record and a reference
		/// data record and fills the patch data record with the resulting
		/// delta.
		/// </summary>
		/// <param name="rawData">The raw data record.</param>
		/// <param name="refData">The reference data record.</param>
		/// <param name="patchData">The patch data, which will be filled with the delta.</param>
		protected override void ComputeDataDelta(StructuredData rawData, StructuredData refData, StructuredData patchData)
		{
			base.ComputeDataDelta (rawData, refData, patchData);

			object refStatefull        = refData.GetValue (Res.Fields.ResourceCommand.Statefull);
			string refDefaultParameter = refData.GetValue (Res.Fields.ResourceCommand.DefaultParameter) as string;
			string refGroup            = refData.GetValue (Res.Fields.ResourceCommand.Group) as string;
			
			IList<StructuredData> refShortcuts = refData.GetValue (Res.Fields.ResourceCommand.Shortcuts) as IList<StructuredData>;
			
			object rawStatefull        = rawData.GetValue (Res.Fields.ResourceCommand.Statefull);
			string rawDefaultParameter = rawData.GetValue (Res.Fields.ResourceCommand.DefaultParameter) as string;
			string rawGroup            = rawData.GetValue (Res.Fields.ResourceCommand.Group) as string;
			
			IList<StructuredData> rawShortcuts = rawData.GetValue (Res.Fields.ResourceCommand.Shortcuts) as IList<StructuredData>;

			if ((!UndefinedValue.IsUndefinedValue (rawStatefull)) &&
				(refStatefull != rawStatefull))
			{
				patchData.SetValue (Res.Fields.ResourceCommand.Statefull, rawStatefull);
			}
			if ((!ResourceBundle.Field.IsNullString (rawDefaultParameter)) &&
				(refDefaultParameter != rawDefaultParameter))
			{
				patchData.SetValue (Res.Fields.ResourceCommand.DefaultParameter, rawDefaultParameter);
			}
			if ((!ResourceBundle.Field.IsNullString (rawGroup)) &
				(refGroup != rawGroup))
			{
				patchData.SetValue (Res.Fields.ResourceCommand.Group, rawGroup);
			}

			if ((rawShortcuts != null) &&
				(rawShortcuts.Count > 0) &&
				(!Types.Collection.CompareEqual (rawShortcuts, refShortcuts)))
			{
				patchData.SetValue (Res.Fields.ResourceCommand.Shortcuts, new List<StructuredData> (rawShortcuts));
			}
		}

		#region Broker Class

		private class Broker : IDataBroker
		{
			#region IDataBroker Members

			public StructuredData CreateData(CultureMap container)
			{
				return new StructuredData (Res.Types.Shortcut);
			}

			#endregion
		}

		#endregion
	}
}
