﻿//	Copyright © 2014, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Samuel LOUP, Maintainer: Samuel LOUP

using Epsitec.Aider.Enumerations;

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Support.Extensions;
using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Entities;

using Epsitec.Data.Platform;

using System;

using System.Linq;
using System.Collections.Generic;
using System.Text;



namespace Epsitec.Aider.Entities
{
	public partial class AiderOfficeGroupParticipantReportEntity
	{
		public override FormattedText GetCompactSummary()
		{
			return TextFormatter.FormatText (this.Name);		
		}

		public override FormattedText GetSummary()
		{
			return new FormattedText (this.Name + "<br/><a href='" + this.ProcessorUrl +"'>Générer</a>");	
		}

		public string GetReportContent()
		{
			char[] chars = new char[this.Content.Length / sizeof (char)];
			System.Buffer.BlockCopy (this.Content, 0, chars, 0, this.Content.Length);
			return new string (chars);
		}

		partial void GetParticipants(ref IList<AiderGroupParticipantEntity> value)
		{
			value = this.GetParticipants ().AsReadOnlyCollection ();
		}

		private IList<AiderGroupParticipantEntity> GetParticipants()
		{
			if (this.participants == null)
			{
				this.participants = this.ExecuteWithDataContext (d => this.Group.FindParticipants (d, this.Group.FindParticipantCount (d)), () => new List<AiderGroupParticipantEntity> ());
			}

			return this.participants;
		}

		public static AiderOfficeGroupParticipantReportEntity Create(BusinessContext context, AiderGroupEntity group, AiderOfficeSenderEntity sender, string documentName, string title, string content)
		{
			var report = context.CreateAndRegisterEntity<AiderOfficeGroupParticipantReportEntity> ();

			report.Name				= documentName;
			report.Title			= title;
			report.CreationDate		= Date.Today;
			report.Content			= AiderOfficeGroupParticipantReportEntity.ConvertContent (content);
			report.Group			= group;
			report.Office			= sender.Office;

			//Add document to the sender office document management
			sender.Office.AddDocumentInternal (report);
			return report;
		}

		private static byte[] ConvertContent(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof (char)];
			System.Buffer.BlockCopy (str.ToCharArray (), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		private IList<AiderGroupParticipantEntity>	participants;
	}
}
