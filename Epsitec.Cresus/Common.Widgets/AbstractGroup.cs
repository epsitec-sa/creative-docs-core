//	Copyright © 2003-2004, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// La classe AbstractGroup sert de base aux autres classes qui
	/// implémentent des groupes de widgets.
	/// </summary>
	
	[Support.SuppressBundleSupport]
	
	public abstract class AbstractGroup : Widget
	{
		public AbstractGroup()
		{
			this.InternalState |= InternalState.PossibleContainer;
			this.TabNavigation  = Widget.TabNavigationMode.ForwardTabPassive;
		}
		
		public AbstractGroup(Widget embedder) : this ()
		{
			this.SetEmbedder(embedder);
		}
	}
}
