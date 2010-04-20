﻿//	Copyright © 2008-2010, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Splash;

using System.Collections.Generic;

namespace Epsitec.Cresus.Core
{
	static class CoreProgram
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[System.STAThread]
		static void Main(string[] args)
		{
			using (var splash = new SplashScreen ("logo.png"))
			{
				CoreProgram.ExecuteCoreProgram (splash);
			}
		}

		/// <summary>
		/// Gets the application object.
		/// </summary>
		/// <value>The application object.</value>
		public static CoreApplication Application
		{
			get
			{
				return CoreProgram.application;
			}
		}


		static void ExecuteCoreProgram(SplashScreen splash)
		{
			Epsitec.Common.Debug.GeneralExceptionCatcher.Setup ();
			
			UI.Initialize ();

			CoreProgram.application = new CoreApplication ();

			System.Diagnostics.Debug.Assert (CoreProgram.application.ResourceManagerPool.PoolName == "Core");

			CoreProgram.application.SetupData ();
			CoreProgram.application.SetupInterface ();
			
			CoreProgram.application.Window.Show ();
			CoreProgram.application.Window.Run ();

			UI.ShutDown ();

			CoreProgram.application.Dispose ();
			CoreProgram.application = null;
		}

		private static CoreApplication application;
	}
}
