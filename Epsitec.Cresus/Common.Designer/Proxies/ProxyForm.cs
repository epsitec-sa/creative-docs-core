﻿using System.Collections.Generic;
using Epsitec.Common.Drawing;
using Epsitec.Common.Support;
using Epsitec.Common.Widgets;

namespace Epsitec.Common.Designer.Proxies
{
	/// <summary>
	/// Cette classe détermine l'ensemble des valeurs représentées par un proxy, qui sera matérialisé par un panneau.
	/// </summary>
	public class ProxyForm : AbstractProxy
	{
		public override IEnumerable<Panel> ProxyPanels
		{
			get
			{
				yield return Panel.FormGeometry;
				yield return Panel.FormStyle;
			}
		}

		public override IEnumerable<Type> ValueTypes(Panel proxyPanel)
		{
			switch (proxyPanel)
			{
				case Panel.FormGeometry:
					yield return Type.FormColumnsRequired;
					yield return Type.FormRowsRequired;
					yield return Type.FormPreferredWidth;
					yield return Type.FormSeparatorBottom;
					yield return Type.FormBoxFrameState;
					break;

				case Panel.FormStyle:
					yield return Type.FormBackColor;
					yield return Type.FormLabelFontColor;
					yield return Type.FormFieldFontColor;
					break;
			}
		}

		public override Widget CreateInterface(Widget parent, Panel proxyPanel, List<AbstractValue> values)
		{
			//	Crée un panneau complet.
			MyWidgets.PropertyPanel panel = new MyWidgets.PropertyPanel(parent);
			panel.Icon = this.GetIcon(proxyPanel);
			panel.Title = this.GetTitle(proxyPanel);
			panel.IsExtendedSize = true;

			foreach (Type valueType in this.ValueTypes(proxyPanel))
			{
				AbstractValue value = AbstractProxy.IndexOf(values, valueType);
				if (value != null)
				{
					Widget widget = value.CreateInterface(panel.Container);
					widget.Dock = DockStyle.Top;
					widget.Margins = new Margins(0, 0, 0, 1);
				}
			}

			return panel;
		}

		protected override string GetIcon(Panel proxyPanel)
		{
			switch (proxyPanel)
			{
				case Panel.FormGeometry:
					return "PropertyGeometry";

				case Panel.FormStyle:
					return "PropertyAspect";
			}

			return null;
		}

		protected override string GetTitle(Panel proxyPanel)
		{
			switch (proxyPanel)
			{
				case Panel.FormGeometry:
					return "Géométrie";

				case Panel.FormStyle:
					return "Style";
			}

			return null;
		}
	}
}
