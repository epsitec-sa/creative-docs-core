//	Copyright � 2004, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Statut : OK/PA, 21/03/2004

namespace Epsitec.Common.Printing
{
	/// <summary>
	/// L'interface IPrintEngine.
	/// </summary>
	public interface IPrintEngine
	{
		void StartingPrintJob();
		void FinishingPrintJob();
		
		void PrepareNewPage(PageSettings settings);
		PrintEngineStatus PrintPage(PageSettings settings);
	}
	
	public enum PrintEngineStatus
	{
		MorePages,
		FinishJob,
		CancelJob,
	}
}
