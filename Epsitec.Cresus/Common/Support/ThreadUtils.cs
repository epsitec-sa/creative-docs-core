﻿using System.Threading;


namespace Epsitec.Common.Support
{
	
	
	public static class ThreadUtils
	{


		public static WaitHandle WaitAny(params WaitHandle[] waitHandles)
		{
			int index = WaitHandle.WaitAny (waitHandles);

			if (index >= 0 && index <= waitHandles.Length)
			{
				return waitHandles[index];
			}
			else
			{
				return null;
			}
		}


	}


}
