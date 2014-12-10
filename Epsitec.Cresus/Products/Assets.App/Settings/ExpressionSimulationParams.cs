﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Data;

namespace Epsitec.Cresus.Assets.App.Settings
{
	public struct ExpressionSimulationParams
	{
		public ExpressionSimulationParams(DateRange range, Periodicity periodicity, decimal initialAmount)
		{
			this.Range         = range;
			this.Periodicity   = periodicity;
			this.InitialAmount = initialAmount;

			this.arguments = new Dictionary<ObjectField, object> ();
		}

		public Dictionary<ObjectField, object> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		//??public ExpressionSimulationParams(System.Xml.XmlReader reader)
		//??{
		//??}


		//??public void Serialize(System.Xml.XmlWriter writer, string name)
		//??{
		//??}


		public static ExpressionSimulationParams Default = new ExpressionSimulationParams (
			new DateRange (new System.DateTime (2000, 1, 1), new System.DateTime (2020, 1, 1)),
			Periodicity.Annual, 10000.0m);

		public readonly DateRange				Range;
		public readonly Periodicity				Periodicity;
		public readonly decimal					InitialAmount;
		private readonly Dictionary<ObjectField, object> arguments;
	}
}
