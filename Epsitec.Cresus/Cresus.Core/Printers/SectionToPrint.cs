﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Epsitec.Cresus.Core.Printers
{
	/// <summary>
	/// Une instance de SectionToPrint représente une section d'une ou plusieurs pages contigües à imprimer
	/// sur une imprimante physique et un seul bac (Printer).
	/// Initialement, on crée des sections d'une seule page. Elles sont regroupées par la suite.
	/// </summary>
	public class SectionToPrint
	{
		public SectionToPrint(Printer printer, string job, int firstPage, int entityRank, AbstractEntityPrinter entityPrinter)
		{
			//	Crée une section d'une page.
			this.printer       = printer;
			this.job           = job;
			this.firstPage     = firstPage;
			this.PageCount     = 1;
			this.entityRank    = entityRank;
			this.entityPrinter = entityPrinter;
		}

		public Printer Printer
		{
			get
			{
				return this.printer;
			}
		}

		public string Job
		{
			get
			{
				return this.job;
			}
		}

		public int FirstPage
		{
			get
			{
				return this.firstPage;
			}
		}

		public int PageCount
		{
			get;
			set;
		}

		public int EntityRank
		{
			get
			{
				return this.entityRank;
			}
		}

		public AbstractEntityPrinter EntityPrinter
		{
			get
			{
				return this.entityPrinter;
			}
		}

		public override string ToString()
		{
			// Pratique pour le debug.
			return string.Format ("PrinterLogicalName={0}, PrinterPhysicalName={1}, Job={2}, FirstPage={3}, PageCount={4}, EntityRank={5}", this.printer.LogicalName, this.printer.PhysicalPrinterName, this.job, this.firstPage, this.PageCount, this.entityRank);
		}


		public static int CompareSectionToPrint(SectionToPrint x, SectionToPrint y)
		{
			//	Détermine comment regrouper les pages. On cherche à grouper les pages ansi:
			//	- par jobs
			//	- par impriante physique
			//	- par pages croissantes
			int result;

			result = string.Compare (x.Job, y.Job);
			if (result != 0)
			{
				return result;
			}

			result = string.Compare (x.Printer.PhysicalPrinterName, y.Printer.PhysicalPrinterName);
			if (result != 0)
			{
				return result;
			}

			if (x.FirstPage != y.FirstPage)
			{
				return (x.FirstPage < y.FirstPage) ? -1 : 1;
			}

			return 0;
		}


		private readonly Printer				printer;
		private readonly string					job;
		private readonly int					firstPage;
		private readonly int					entityRank;
		private readonly AbstractEntityPrinter	entityPrinter;
	}
}
