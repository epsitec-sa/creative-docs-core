//	Copyright � 2004, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Cresus.Services
{
	/// <summary>
	/// La classe AbstractService sert de base � toutes les classes impl�mentant des
	/// services dans Cr�sus R�seau.
	/// </summary>
	public abstract class AbstractServiceEngine : System.MarshalByRefObject
	{
		protected AbstractServiceEngine(Engine engine, string service_name)
		{
			this.engine = engine;
			this.service_name = service_name;
		}
		
		
		public string							ServiceName
		{
			get
			{
				return this.service_name;
			}
		}
		
		
		protected Engine						engine;
		private string							service_name;
	}
}
