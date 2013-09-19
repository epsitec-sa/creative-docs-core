//	Copyright � 2004-2013, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

namespace Epsitec.Common.Support
{
	/// <summary>
	/// La classe Sync permet de simplifier les attentes sur de multiples
	/// �v�nements, tout en traitant aussi l'�v�nement global AbortEvent.
	/// </summary>
	public sealed class Sync
	{
		public static int Wait(params System.Threading.WaitHandle[] wait)
		{
			//	Retourne l'index de l'�v�nement qui a re�u le signal. En cas de timeout,
			//	retourne -1; en cas d'avortement, retourne 'n'.
			
			int n = wait.Length;
			
			System.Threading.WaitHandle[] handles = new System.Threading.WaitHandle[n + 1];
			
			wait.CopyTo (handles, 0);
			
			handles[n] = Common.Support.Globals.AbortEvent;
			
			int handleIndex = System.Threading.WaitHandle.WaitAny (handles);
			
			//	G�re le cas particulier d�crit dans la documentation o� l'index peut �tre
			//	incorrect dans certains cas :
			
			if (handleIndex >= 128)
			{
				handleIndex -= 128;
			}
			
			return handleIndex;
		}
	}
}
