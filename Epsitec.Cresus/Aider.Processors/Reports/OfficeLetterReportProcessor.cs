﻿//	Copyright © 2014, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.WebCore.Server.Core;
using Epsitec.Cresus.WebCore.Server.Core.Extraction;
using Epsitec.Cresus.WebCore.Server.Core.IO;

using Epsitec.Aider.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Aider.Processors.Reports
{
	public class OfficeLetterReportProcessor : Epsitec.Cresus.WebCore.Server.Processors.IReportingProcessor
	{
		public OfficeLetterReportProcessor(CoreServer coreServer)
		{
			this.coreServer = coreServer;
		}


		#region IName Members

		public string Name
		{
			get
			{
				return "officeletter";
			}
		}

		#endregion

		#region IReportingProcessor Members

		public string CreateReport(System.IO.Stream stream, WorkerApp workerApp, BusinessContext businessContext, dynamic parameters)
		{
			string settingsId	= parameters.settings;
			string letterId		= parameters.letter;

			var settings = EntityIO.ResolveEntity (businessContext, settingsId) as AiderOfficeSenderEntity;
			var letter  = EntityIO.ResolveEntity (businessContext, letterId)	as AiderOfficeLetterReportEntity;

			return this.GenerateDocument (stream, workerApp, businessContext, settings, letter);
		}

		#endregion

		private string GenerateDocument(System.IO.Stream stream, WorkerApp workerApp, BusinessContext context, AiderOfficeSenderEntity sender, AiderOfficeLetterReportEntity letter)
		{
			var userManager = workerApp.UserManager;

			//	Do something with this entity...
			
			var layout = LabelLayout.Sheet_A4_Simple;
			var doc    = new Pdf.OfficeLetterDocumentWriter (context, sender, layout);

			doc.WriteStream (stream, letter);
			letter.ProcessDate = System.DateTime.Now;
			context.SaveChanges (LockingPolicy.ReleaseLock);

			return letter.Name + ".pdf";
		}


		private readonly CoreServer				coreServer;
	}
}
