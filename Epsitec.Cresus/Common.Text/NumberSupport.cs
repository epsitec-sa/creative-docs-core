//	Copyright © 2005, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Text
{
	/// <summary>
	/// La classe NumberSupport implémente des méthodes utiles pour la manipulation
	/// de nombres.
	/// </summary>
	public sealed class NumberSupport
	{
		private NumberSupport()
		{
		}
		
		
		public static double Combine(double a, double b)
		{
			//	Combine deux valeurs. Si b est NaN, alors retourne a. Sinon
			//	retourne b.
			
			if (double.IsNaN (b))
			{
				return a;
			}
			else
			{
				return b;
			}
		}
		
		public static bool Equal(double a, double b)
		{
			if (a == b)
			{
				return true;
			}
			
			if (double.IsNaN (a) &&
				double.IsNaN (b))
			{
				return true;
			}
			
			return false;
		}
		
		public static bool Different(double a, double b)
		{
			if (a == b)
			{
				return false;
			}
			
			if (double.IsNaN (a) &&
				double.IsNaN (b))
			{
				return false;
			}
			
			return true;
		}
	}
}
