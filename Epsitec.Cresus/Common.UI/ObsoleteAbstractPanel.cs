//	Copyright � 2004-2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Widgets;
using System.Collections.Generic;

namespace Epsitec.Common.UI
{
	/// <summary>
	/// La classe AbstractPanel est la base de toutes les classes XyzPanel.
	/// </summary>
	public abstract class ObsoleteAbstractPanel : System.IDisposable
	{
		public ObsoleteAbstractPanel()
		{
		}
		
		
		public Drawing.Size						Size
		{
			get
			{
				return this.size;
			}
		}
		
		public Widget							Widget
		{
			get
			{
				if (this.widget == null)
				{
					this.CreateWidget ();
				}
				
				return this.widget;
			}
		}
		
		#region IDisposable Members
		public void Dispose()
		{
			this.Dispose (true);
			System.GC.SuppressFinalize (this);
		}
		#endregion
		
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.widget.Dispose ();
				
				this.widget     = null;
			}
		}
		
		protected virtual void CreateWidget()
		{
			this.widget = new Widget ();
			
			this.widget.PreferredSize = this.Size;
			this.widget.MinSize = this.Size;
			this.widget.TabNavigation = TabNavigationMode.ForwardTabPassive;
			
			this.CreateWidgets (this.widget);
		}

		protected abstract void CreateWidgets(Widget parent);
		
		protected Drawing.Size					size;
		protected Widget						widget;
	}
}
