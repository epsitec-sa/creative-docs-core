namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// La classe StaticLine dessine une ligne de sÚparation horizontale, avec
	/// ou sans titre.
	/// </summary>
	public class StaticLine : Widget
	{
		public StaticLine()
		{
		}
		
		public StaticLine(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}
	}
}
