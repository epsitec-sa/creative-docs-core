//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business.Finance.PriceCalculators;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.CorePlugIn.TableDesigner
{
	public class MainController
	{
		public MainController(Core.Business.BusinessContext businessContext, PriceCalculatorEntity priceCalculatorEntity)
		{
			this.businessContext = businessContext;
			this.priceCalculatorEntity = priceCalculatorEntity;

			this.table = this.priceCalculatorEntity.GetPriceTable ();
		}

		public void CreateUI(Widget parent)
		{
			var editorGroup = new FrameBox(parent);
			editorGroup.Dock = DockStyle.Fill;

			//	Crée les grands blocs de widgets.
			var band = new FrameBox(editorGroup);
			band.Dock = DockStyle.Fill;

			string s = string.Format ("Name = {0}, Dimensions = {1}<br/>", this.priceCalculatorEntity.Name.ToString (), this.table.Dimensions.Count ());

			foreach (var d in this.table.Dimensions)
			{
				s += MainController.GetDesciption (d);
			}

			var t = new StaticText
			{
				Parent = band,
				Text = s,
				Dock = DockStyle.Fill,
			};
		}


		private static string GetDesciption(AbstractDimension dimension)
		{
			var builder = new System.Text.StringBuilder ();

			builder.Append (dimension.Name);
			builder.Append (": ");

			foreach (var v in dimension.Values)
			{
				if (v is decimal)
				{
					decimal s = (decimal) v;
					builder.Append (s.ToString ());
					builder.Append (", ");
				}
			}

			builder.Append ("<br/>");

			return builder.ToString ();
		}


		public void SaveDesign()
		{
		}


		private readonly Core.Business.BusinessContext	businessContext;
		private readonly PriceCalculatorEntity			priceCalculatorEntity;
		private readonly DimensionTable					table;
	}
}
