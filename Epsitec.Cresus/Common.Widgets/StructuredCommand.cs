//	Copyright � 2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using System.Collections.Generic;

using Epsitec.Common.Widgets.Collections;
using Epsitec.Common.Types;

namespace Epsitec.Common.Widgets
{
	public class StructuredCommand : Command, IStructuredType
	{
		public StructuredCommand(string name) : base (name)
		{
			this.StateObjectType = Types.DependencyObjectType.FromSystemType (typeof (StructuredState));
		}
		
		#region IStructuredType Members

		string[] IStructuredType.GetFieldNames()
		{
			throw new System.Exception ("The method or operation is not implemented.");
		}

		object IStructuredType.GetFieldTypeObject(string name)
		{
			throw new System.Exception ("The method or operation is not implemented.");
		}
		
		#endregion

		private class StructuredState : CommandState, IStructuredData
		{
			public StructuredState()
			{
			}

			#region IStructuredData Members

			void IStructuredData.AttachListener(string path, Epsitec.Common.Support.EventHandler<DependencyPropertyChangedEventArgs> handler)
			{
				
			}

			void IStructuredData.DetachListener(string path, Epsitec.Common.Support.EventHandler<DependencyPropertyChangedEventArgs> handler)
			{
				
			}

			string[] IStructuredData.GetValueNames()
			{
				IStructuredType type = this.Command as IStructuredType;
				return type.GetFieldNames ();
			}

			object IStructuredData.GetValue(string name)
			{
				return null;
			}

			void IStructuredData.SetValue(string name, object value)
			{
				
			}

			bool IStructuredData.HasImmutableRoots
			{
				get
				{
					return true;
				}
			}

			#endregion
		}
	}
}
