namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// La classe HScroller implémente l'ascenceur horizontal.
	/// </summary>
	public class HScroller : AbstractScroller
	{
		public HScroller() : base(false)
		{
		}
		
		public override double DefaultHeight
		{
			get
			{
				return AbstractScroller.defaultBreadth;
			}
		}
		
		public override Drawing.Size DefaultMinSize
		{
			get
			{
				return new Drawing.Size (AbstractScroller.minimalThumb+6, AbstractScroller.defaultBreadth);
			}
		}
	}
}
