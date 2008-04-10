﻿using System.Collections.Generic;
using Epsitec.Common.Drawing;
using Epsitec.Common.Support;
using Epsitec.Common.Widgets;

namespace Epsitec.Common.Designer.Proxies
{
	/// <summary>
	/// Cette classe gère les objets associés à un proxy de type Panel.
	/// </summary>
	public class ObjectManagerPanel : AbstractObjectManager
	{
		public ObjectManagerPanel(object objectModifier) : base(objectModifier)
		{
		}

		public override List<AbstractValue> GetValues(Widget selectedObject)
		{
			//	Retourne la liste des valeurs nécessaires pour représenter un objet.
			List<AbstractValue> list = new List<AbstractValue>();

			//	Panel.Content:
			if (PanelEditor.ObjectModifier.HasDruid(selectedObject))
			{
				PanelEditor.ObjectModifier.ObjectType type = PanelEditor.ObjectModifier.GetObjectType(selectedObject);
				if (type == PanelEditor.ObjectModifier.ObjectType.SubPanel)
				{
					//?this.DruidPanel = this.ObjectModifier.GetDruid(selectedObject);
				}
				else
				{
					//?this.DruidCaption = this.ObjectModifier.GetDruid(selectedObject);
				}
			}

			if (PanelEditor.ObjectModifier.HasBinding(selectedObject))
			{
				//?this.Binding = this.ObjectModifier.GetBinding(selectedObject);
				//this.AddValue(list, selectedObject, AbstractProxy.Type.PanelBinding, Res.Captions.???, Res.Types.ObjectModifier.???);
			}

			if (PanelEditor.ObjectModifier.GetObjectType(selectedObject) == PanelEditor.ObjectModifier.ObjectType.Table)
			{
				//?UI.TablePlaceholder table = selectedObject as UI.TablePlaceholder;
				//?this.TableColumns = new List<UI.ItemTableColumn>(table.Columns);
			}

			if (PanelEditor.ObjectModifier.HasStructuredType(selectedObject))
			{
				//?this.StructuredType = this.ObjectModifier.GetStructuredType(selectedObject);
				//this.AddValue(list, selectedObject, AbstractProxy.Type.PanelStructuredType, Res.Captions.???, Res.Types.ObjectModifier.???);
			}

			//	Panel.Aspect:
			if (this.ObjectModifier.HasButtonClass(selectedObject))
			{
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelButtonAspect, Res.Captions.Aspect.ButtonAspect, Res.Types.Widgets.ButtonClass);
			}
			
			//	Panel.Geometry:
			if (this.ObjectModifier.HasMargins(selectedObject))
			{
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelLeftMargin,   Res.Captions.Geometry.LeftMargin,   -1, 9999, 1, 1);
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelRightMargin,  Res.Captions.Geometry.RightMargin,  -1, 9999, 1, 1);
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelTopMargin,    Res.Captions.Geometry.TopMargin,    -1, 9999, 1, 1);
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelBottomMargin, Res.Captions.Geometry.BottomMargin, -1, 9999, 1, 1);
			}

			if (this.ObjectModifier.HasBounds(selectedObject))
			{
				if (this.ObjectModifier.HasBounds(selectedObject, PanelEditor.ObjectModifier.BoundsMode.OriginX))
				{
					this.AddValue(list, selectedObject, AbstractProxy.Type.PanelOriginX, Res.Captions.Geometry.OriginX, -9999, 9999, 1, 1);
				}

				if (this.ObjectModifier.HasBounds(selectedObject, PanelEditor.ObjectModifier.BoundsMode.OriginY))
				{
					this.AddValue(list, selectedObject, AbstractProxy.Type.PanelOriginY, Res.Captions.Geometry.OriginX, -9999, 9999, 1, 1);
				}

				if (this.ObjectModifier.HasBounds(selectedObject, PanelEditor.ObjectModifier.BoundsMode.Width))
				{
					this.AddValue(list, selectedObject, AbstractProxy.Type.PanelWidth, Res.Captions.Geometry.Width, 0, 9999, 1, 1);
				}

				if (this.ObjectModifier.HasBounds(selectedObject, PanelEditor.ObjectModifier.BoundsMode.Height))
				{
					this.AddValue(list, selectedObject, AbstractProxy.Type.PanelHeight, Res.Captions.Geometry.Height, 0, 9999, 1, 1);
				}
			}
			else
			{
				if (this.ObjectModifier.HasWidth(selectedObject))
				{
					this.AddValue(list, selectedObject, AbstractProxy.Type.PanelWidth, Res.Captions.Geometry.Width, 0, 9999, 1, 1);
				}

				if (this.ObjectModifier.HasHeight(selectedObject))
				{
					this.AddValue(list, selectedObject, AbstractProxy.Type.PanelHeight, Res.Captions.Geometry.Height, 0, 9999, 1, 1);
				}
			}

			//	Panel.Padding:
			if (this.ObjectModifier.HasPadding(selectedObject))
			{
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelLeftPadding,   Res.Captions.Padding.Left,   -1, 9999, 1, 1);
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelRightPadding,  Res.Captions.Padding.Right,  -1, 9999, 1, 1);
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelTopPadding,    Res.Captions.Padding.Top,    -1, 9999, 1, 1);
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelBottomPadding, Res.Captions.Padding.Bottom, -1, 9999, 1, 1);
			}

			//	Panel.Layout:
			if (this.ObjectModifier.HasChildrenPlacement(selectedObject))
			{
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelChildrenPlacement, Res.Captions.Layout.ChildrenPlacement, Res.Types.ObjectModifier.ChildrenPlacement);
			}

			if (this.ObjectModifier.AreChildrenAnchored(selectedObject.Parent))
			{
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelAnchoredHorizontalAttachment, Res.Captions.Layout.AnchoredHorizontalAttachment, Res.Types.ObjectModifier.AnchoredHorizontalAttachment);
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelAnchoredVerticalAttachment,   Res.Captions.Layout.AnchoredVerticalAttachment,   Res.Types.ObjectModifier.AnchoredVerticalAttachment);
			}

			if (this.ObjectModifier.HasStackedHorizontalAttachment(selectedObject))
			{
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelStackedHorizontalAttachment, Res.Captions.Layout.StackedHorizontalAttachment, Res.Types.ObjectModifier.StackedHorizontalAttachment);
			}

			if (this.ObjectModifier.HasStackedVerticalAttachment(selectedObject))
			{
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelStackedVerticalAttachment, Res.Captions.Layout.StackedVerticalAttachment, Res.Types.ObjectModifier.StackedVerticalAttachment);
			}

			if (this.ObjectModifier.HasStackedHorizontalAlignment(selectedObject))
			{
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelStackedHorizontalAlignment, Res.Captions.Layout.StackedHorizontalAlignment, Res.Types.ObjectModifier.StackedHorizontalAlignment);
			}

			if (this.ObjectModifier.HasStackedVerticalAlignment(selectedObject))
			{
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelStackedVerticalAlignment, Res.Captions.Layout.StackedVerticalAlignment, Res.Types.ObjectModifier.StackedVerticalAlignment);
			}

			//	Panel.Grid:
			if (this.ObjectModifier.AreChildrenGrid(selectedObject))
			{
				//	Utilise des boutons +/-, car la valeur ne peut pas être éditée, ni le slider utilisé, car les
				//	proxies sont entièrement reconstruits à chaque modification !
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridColumnsCount, Res.Captions.Grid.ColumnsCount, 1, 100);
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridRowsCount,    Res.Captions.Grid.RowsCount,    1, 100);
			}

			if (this.ObjectModifier.AreChildrenGrid(selectedObject.Parent))
			{
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridColumnSpan, Res.Captions.Grid.ColumnSpan, 1, 100, 1, 1);
				this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridRowSpan,    Res.Captions.Grid.RowSpan,    1, 100, 1, 1);
			}

			if (this.ObjectModifier.AreChildrenGrid(selectedObject))
			{
				PanelEditor.GridSelection gs = PanelEditor.GridSelection.Get(selectedObject);
				if (gs != null && gs.Count > 0)
				{
					foreach (PanelEditor.GridSelection.OneItem item in gs)
					{
						if (item.Unit == PanelEditor.GridSelection.Unit.Column)
						{
							this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridColumnMode, Res.Captions.Grid.ColumnMode, Res.Types.ObjectModifier.GridMode);

							if (this.ObjectModifier.GetGridColumnMode(selectedObject, item.Index) != PanelEditor.ObjectModifier.GridMode.Auto)
							{
								this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridColumnWidth, Res.Captions.Grid.ColumnWidth, 0, 999, 1, 1);
							}

							this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridColumnMinWidth, Res.Captions.Grid.MinWidth, 0, 999, 1, 1);
							this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridColumnMaxWidth, Res.Captions.Grid.MaxWidth, 0, 999, 1, 1);

							this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridLeftBorder,  Res.Captions.Grid.LeftBorder,  0, 999, 1, 1);
							this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridRightBorder, Res.Captions.Grid.RightBorder, 0, 999, 1, 1);
						}

						if (item.Unit == PanelEditor.GridSelection.Unit.Row)
						{
							this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridRowMode, Res.Captions.Grid.RowMode, Res.Types.ObjectModifier.GridMode);

							if (this.ObjectModifier.GetGridRowMode(selectedObject, item.Index) != PanelEditor.ObjectModifier.GridMode.Auto)
							{
								this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridRowHeight, Res.Captions.Grid.RowHeight, 0, 999, 1, 1);
							}

							this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridRowMinHeight, Res.Captions.Grid.MinHeight, 0, 999, 1, 1);
							this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridRowMaxHeight, Res.Captions.Grid.MaxHeight, 0, 999, 1, 1);

							this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridTopBorder,    Res.Captions.Grid.TopBorder,    0, 999, 1, 1);
							this.AddValue(list, selectedObject, AbstractProxy.Type.PanelGridBottomBorder, Res.Captions.Grid.BottomBorder, 0, 999, 1, 1);
						}
					}
				}
			}

			return list;
		}

		public override bool IsEnable(AbstractValue value)
		{
			//	Indique si la valeur pour représenter un objet est enable.
			return true;
		}


		public override void SendObjectToValue(AbstractValue value)
		{
			//	Tous les objets ont la même valeur. Il suffit donc de s'occuper du premier objet.
			this.SuspendChanges();

			try
			{
				Widget selectedObject = value.SelectedObjects[0];
				PanelEditor.GridSelection gs = PanelEditor.GridSelection.Get(selectedObject);

				switch (value.Type)
				{
					case AbstractProxy.Type.PanelBinding:
						value.Value = PanelEditor.ObjectModifier.GetBinding(selectedObject);
						break;

					case AbstractProxy.Type.PanelStructuredType:
						value.Value = PanelEditor.ObjectModifier.GetStructuredType(selectedObject);
						break;

					case AbstractProxy.Type.PanelButtonAspect:
						value.Value = this.ObjectModifier.GetButtonClass(selectedObject);
						break;

					case AbstractProxy.Type.PanelLeftMargin:
						value.Value = this.ObjectModifier.GetMargins(selectedObject).Left;
						break;

					case AbstractProxy.Type.PanelRightMargin:
						value.Value = this.ObjectModifier.GetMargins(selectedObject).Right;
						break;

					case AbstractProxy.Type.PanelTopMargin:
						value.Value = this.ObjectModifier.GetMargins(selectedObject).Top;
						break;

					case AbstractProxy.Type.PanelBottomMargin:
						value.Value = this.ObjectModifier.GetMargins(selectedObject).Bottom;
						break;

					case AbstractProxy.Type.PanelOriginX:
						value.Value = this.ObjectModifier.GetBounds(selectedObject).Left;
						break;

					case AbstractProxy.Type.PanelOriginY:
						value.Value = this.ObjectModifier.GetBounds(selectedObject).Bottom;
						break;

					case AbstractProxy.Type.PanelWidth:
						if (this.ObjectModifier.HasBounds(selectedObject))
						{
							value.Value = this.ObjectModifier.GetBounds(selectedObject).Width;
						}
						else
						{
							value.Value = this.ObjectModifier.GetWidth(selectedObject);
						}
						break;

					case AbstractProxy.Type.PanelHeight:
						if (this.ObjectModifier.HasBounds(selectedObject))
						{
							value.Value = this.ObjectModifier.GetBounds(selectedObject).Height;
						}
						else
						{
							value.Value = this.ObjectModifier.GetHeight(selectedObject);
						}
						break;

					case AbstractProxy.Type.PanelLeftPadding:
						value.Value = this.ObjectModifier.GetPadding(selectedObject).Left;
						break;

					case AbstractProxy.Type.PanelRightPadding:
						value.Value = this.ObjectModifier.GetPadding(selectedObject).Right;
						break;

					case AbstractProxy.Type.PanelTopPadding:
						value.Value = this.ObjectModifier.GetPadding(selectedObject).Top;
						break;

					case AbstractProxy.Type.PanelBottomPadding:
						value.Value = this.ObjectModifier.GetPadding(selectedObject).Bottom;
						break;

					case AbstractProxy.Type.PanelChildrenPlacement:
						value.Value = this.ObjectModifier.GetChildrenPlacement(selectedObject);
						break;

					case AbstractProxy.Type.PanelAnchoredHorizontalAttachment:
						value.Value = this.ObjectModifier.GetAnchoredHorizontalAttachment(selectedObject);
						break;

					case AbstractProxy.Type.PanelAnchoredVerticalAttachment:
						value.Value = this.ObjectModifier.GetAnchoredVerticalAttachment(selectedObject);
						break;

					case AbstractProxy.Type.PanelStackedHorizontalAttachment:
						value.Value = this.ObjectModifier.GetStackedHorizontalAttachment(selectedObject);
						break;

					case AbstractProxy.Type.PanelStackedVerticalAttachment:
						value.Value = this.ObjectModifier.GetStackedVerticalAttachment(selectedObject);
						break;

					case AbstractProxy.Type.PanelStackedHorizontalAlignment:
						value.Value = this.ObjectModifier.GetStackedHorizontalAlignment(selectedObject);
						break;

					case AbstractProxy.Type.PanelStackedVerticalAlignment:
						value.Value = this.ObjectModifier.GetStackedVerticalAlignment(selectedObject);
						break;

					case AbstractProxy.Type.PanelGridColumnsCount:
						value.Value = this.ObjectModifier.GetGridColumnsCount(selectedObject);
						break;

					case AbstractProxy.Type.PanelGridRowsCount:
						value.Value = this.ObjectModifier.GetGridRowsCount(selectedObject);
						break;

					case AbstractProxy.Type.PanelGridColumnSpan:
						value.Value = this.ObjectModifier.GetGridColumnSpan(selectedObject);
						break;

					case AbstractProxy.Type.PanelGridRowSpan:
						value.Value = this.ObjectModifier.GetGridRowSpan(selectedObject);
						break;

					case AbstractProxy.Type.PanelGridColumnMode:
						foreach (PanelEditor.GridSelection.OneItem item in gs)
						{
							value.Value = this.ObjectModifier.GetGridColumnMode(selectedObject, item.Index);
						}
						break;

					case AbstractProxy.Type.PanelGridColumnWidth:
						foreach (PanelEditor.GridSelection.OneItem item in gs)
						{
							value.Value = this.ObjectModifier.GetGridColumnWidth(selectedObject, item.Index);
						}
						break;

					case AbstractProxy.Type.PanelGridColumnMinWidth:
						foreach (PanelEditor.GridSelection.OneItem item in gs)
						{
							value.Value = this.ObjectModifier.GetGridColumnMinWidth(selectedObject, item.Index);
						}
						break;

					case AbstractProxy.Type.PanelGridColumnMaxWidth:
						foreach (PanelEditor.GridSelection.OneItem item in gs)
						{
							value.Value = this.ObjectModifier.GetGridColumnMaxWidth(selectedObject, item.Index);
						}
						break;

					case AbstractProxy.Type.PanelGridLeftBorder:
						foreach (PanelEditor.GridSelection.OneItem item in gs)
						{
							value.Value = this.ObjectModifier.GetGridColumnLeftBorder(selectedObject, item.Index);
						}
						break;

					case AbstractProxy.Type.PanelGridRightBorder:
						foreach (PanelEditor.GridSelection.OneItem item in gs)
						{
							value.Value = this.ObjectModifier.GetGridColumnRightBorder(selectedObject, item.Index);
						}
						break;

					case AbstractProxy.Type.PanelGridRowMode:
						foreach (PanelEditor.GridSelection.OneItem item in gs)
						{
							value.Value = this.ObjectModifier.GetGridRowMode(selectedObject, item.Index);
						}
						break;

					case AbstractProxy.Type.PanelGridRowHeight:
						foreach (PanelEditor.GridSelection.OneItem item in gs)
						{
							value.Value = this.ObjectModifier.GetGridRowHeight(selectedObject, item.Index);
						}
						break;

					case AbstractProxy.Type.PanelGridRowMinHeight:
						foreach (PanelEditor.GridSelection.OneItem item in gs)
						{
							value.Value = this.ObjectModifier.GetGridRowMinHeight(selectedObject, item.Index);
						}
						break;

					case AbstractProxy.Type.PanelGridRowMaxHeight:
						foreach (PanelEditor.GridSelection.OneItem item in gs)
						{
							value.Value = this.ObjectModifier.GetGridRowMaxHeight(selectedObject, item.Index);
						}
						break;

					case AbstractProxy.Type.PanelGridTopBorder:
						foreach (PanelEditor.GridSelection.OneItem item in gs)
						{
							value.Value = this.ObjectModifier.GetGridRowTopBorder(selectedObject, item.Index);
						}
						break;

					case AbstractProxy.Type.PanelGridBottomBorder:
						foreach (PanelEditor.GridSelection.OneItem item in gs)
						{
							value.Value = this.ObjectModifier.GetGridRowBottomBorder(selectedObject, item.Index);
						}
						break;

				}
			}
			finally
			{
				this.ResumeChanges();
			}
		}

		protected override void SendValueToObject(AbstractValue value)
		{
			//	Il faut envoyer la valeur à tous les objets sélectionnés.
			this.SuspendChanges();

			bool regenerateProxies = false;
			bool regenerateDimensions = false;

			try
			{
				foreach (Widget selectedObject in value.SelectedObjects)
				{
					PanelEditor.GridSelection gs = PanelEditor.GridSelection.Get(selectedObject);
					Margins m;
					Rectangle r;

					switch (value.Type)
					{
						case AbstractProxy.Type.PanelBinding:
							Types.StructuredType structuredType = PanelEditor.ObjectModifier.GetStructuredType(selectedObject);
							PanelEditor.ObjectModifier.SetBinding(selectedObject, (Types.Binding) value.Value, structuredType);
							break;

						case AbstractProxy.Type.PanelStructuredType:
							PanelEditor.ObjectModifier.SetStructuredType(selectedObject, (Types.StructuredType) value.Value);
							break;

						case AbstractProxy.Type.PanelButtonAspect:
							this.ObjectModifier.SetButtonClass(selectedObject, (ButtonClass) value.Value);
							break;

						case AbstractProxy.Type.PanelLeftMargin:
							m = this.ObjectModifier.GetMargins(selectedObject);
							m.Left = (double) value.Value;
							this.ObjectModifier.SetMargins(selectedObject, m);
							break;

						case AbstractProxy.Type.PanelRightMargin:
							m = this.ObjectModifier.GetMargins(selectedObject);
							m.Right = (double) value.Value;
							this.ObjectModifier.SetMargins(selectedObject, m);
							break;

						case AbstractProxy.Type.PanelTopMargin:
							m = this.ObjectModifier.GetMargins(selectedObject);
							m.Top = (double) value.Value;
							this.ObjectModifier.SetMargins(selectedObject, m);
							break;

						case AbstractProxy.Type.PanelBottomMargin:
							m = this.ObjectModifier.GetMargins(selectedObject);
							m.Bottom = (double) value.Value;
							this.ObjectModifier.SetMargins(selectedObject, m);
							break;

						case AbstractProxy.Type.PanelOriginX:
							r = this.ObjectModifier.GetBounds(selectedObject);
							r.Left = (double) value.Value;
							this.ObjectModifier.SetBounds(selectedObject, r);
							break;

						case AbstractProxy.Type.PanelOriginY:
							r = this.ObjectModifier.GetBounds(selectedObject);
							r.Bottom = (double) value.Value;
							this.ObjectModifier.SetBounds(selectedObject, r);
							break;

						case AbstractProxy.Type.PanelWidth:
							if (this.ObjectModifier.HasBounds(selectedObject))
							{
								r = this.ObjectModifier.GetBounds(selectedObject);
								r.Width = (double) value.Value;
								this.ObjectModifier.SetBounds(selectedObject, r);
							}
							else
							{
								this.ObjectModifier.SetWidth(selectedObject, (double) value.Value);
							}
							break;

						case AbstractProxy.Type.PanelHeight:
							if (this.ObjectModifier.HasBounds(selectedObject))
							{
								r = this.ObjectModifier.GetBounds(selectedObject);
								r.Height = (double) value.Value;
								this.ObjectModifier.SetBounds(selectedObject, r);
							}
							else
							{
								this.ObjectModifier.SetHeight(selectedObject, (double) value.Value);
							}
							break;

						case AbstractProxy.Type.PanelLeftPadding:
							m = this.ObjectModifier.GetPadding(selectedObject);
							m.Left = (double) value.Value;
							this.ObjectModifier.SetPadding(selectedObject, m);
							break;

						case AbstractProxy.Type.PanelRightPadding:
							m = this.ObjectModifier.GetPadding(selectedObject);
							m.Right = (double) value.Value;
							this.ObjectModifier.SetPadding(selectedObject, m);
							break;

						case AbstractProxy.Type.PanelTopPadding:
							m = this.ObjectModifier.GetPadding(selectedObject);
							m.Top = (double) value.Value;
							this.ObjectModifier.SetPadding(selectedObject, m);
							break;

						case AbstractProxy.Type.PanelBottomPadding:
							m = this.ObjectModifier.GetPadding(selectedObject);
							m.Bottom = (double) value.Value;
							this.ObjectModifier.SetPadding(selectedObject, m);
							break;

						case AbstractProxy.Type.PanelChildrenPlacement:
							PanelEditor.GeometryCache.FixBounds(selectedObject, this.ObjectModifier);
							this.ObjectModifier.SetChildrenPlacement(selectedObject, (PanelEditor.ObjectModifier.ChildrenPlacement) value.Value);
							PanelEditor.GeometryCache.AdaptBounds(selectedObject, this.ObjectModifier, (PanelEditor.ObjectModifier.ChildrenPlacement) value.Value);
							regenerateProxies = true;
							regenerateDimensions = true;
							break;

						case AbstractProxy.Type.PanelAnchoredHorizontalAttachment:
							r = this.ObjectModifier.GetBounds(selectedObject);
							this.ObjectModifier.SetAnchoredHorizontalAttachment(selectedObject, (PanelEditor.ObjectModifier.AnchoredHorizontalAttachment) value.Value);
							this.ObjectModifier.SetBounds(selectedObject, r);
							regenerateProxies = true;
							break;

						case AbstractProxy.Type.PanelAnchoredVerticalAttachment:
							r = this.ObjectModifier.GetBounds(selectedObject);
							this.ObjectModifier.SetAnchoredVerticalAttachment(selectedObject, (PanelEditor.ObjectModifier.AnchoredVerticalAttachment) value.Value);
							this.ObjectModifier.SetBounds(selectedObject, r);
							regenerateProxies = true;
							break;

						case AbstractProxy.Type.PanelStackedHorizontalAttachment:
							this.ObjectModifier.SetStackedHorizontalAttachment(selectedObject, (PanelEditor.ObjectModifier.StackedHorizontalAttachment) value.Value);
							regenerateProxies = true;
							break;

						case AbstractProxy.Type.PanelStackedVerticalAttachment:
							this.ObjectModifier.SetStackedVerticalAttachment(selectedObject, (PanelEditor.ObjectModifier.StackedVerticalAttachment) value.Value);
							regenerateProxies = true;
							break;

						case AbstractProxy.Type.PanelStackedHorizontalAlignment:
							this.ObjectModifier.SetStackedHorizontalAlignment(selectedObject, (PanelEditor.ObjectModifier.StackedHorizontalAlignment) value.Value);
							regenerateProxies = true;
							break;

						case AbstractProxy.Type.PanelStackedVerticalAlignment:
							this.ObjectModifier.SetStackedVerticalAlignment(selectedObject, (PanelEditor.ObjectModifier.StackedVerticalAlignment) value.Value);
							regenerateProxies = true;
							break;

						case AbstractProxy.Type.PanelGridColumnsCount:
							this.ObjectModifier.SetGridColumnsCount(selectedObject, (int) value.Value);
							regenerateProxies = true;
							regenerateDimensions = true;
							break;

						case AbstractProxy.Type.PanelGridRowsCount:
							this.ObjectModifier.SetGridRowsCount(selectedObject, (int) value.Value);
							regenerateProxies = true;
							regenerateDimensions = true;
							break;

						case AbstractProxy.Type.PanelGridColumnSpan:
							this.ObjectModifier.SetGridColumnSpan(selectedObject, (int) value.Value);
							regenerateDimensions = true;
							break;

						case AbstractProxy.Type.PanelGridRowSpan:
							this.ObjectModifier.SetGridRowSpan(selectedObject, (int) value.Value);
							regenerateDimensions = true;
							break;

						case AbstractProxy.Type.PanelGridColumnMode:
							foreach (PanelEditor.GridSelection.OneItem item in gs)
							{
								if (item.Unit == PanelEditor.GridSelection.Unit.Column)
								{
									this.ObjectModifier.SetGridColumnMode(selectedObject, item.Index, (PanelEditor.ObjectModifier.GridMode) value.Value);
								}
							}
							regenerateProxies = true;
							regenerateDimensions = true;
							break;

						case AbstractProxy.Type.PanelGridColumnWidth:
							foreach (PanelEditor.GridSelection.OneItem item in gs)
							{
								if (item.Unit == PanelEditor.GridSelection.Unit.Column)
								{
									this.ObjectModifier.SetGridColumnWidth(selectedObject, item.Index, (double) value.Value);
								}
							}
							break;

						case AbstractProxy.Type.PanelGridColumnMinWidth:
							foreach (PanelEditor.GridSelection.OneItem item in gs)
							{
								if (item.Unit == PanelEditor.GridSelection.Unit.Column)
								{
									this.ObjectModifier.SetGridColumnMinWidth(selectedObject, item.Index, (double) value.Value);
								}
							}
							break;

						case AbstractProxy.Type.PanelGridColumnMaxWidth:
							foreach (PanelEditor.GridSelection.OneItem item in gs)
							{
								if (item.Unit == PanelEditor.GridSelection.Unit.Column)
								{
									this.ObjectModifier.SetGridColumnMaxWidth(selectedObject, item.Index, (double) value.Value);
								}
							}
							break;

						case AbstractProxy.Type.PanelGridLeftBorder:
							foreach (PanelEditor.GridSelection.OneItem item in gs)
							{
								if (item.Unit == PanelEditor.GridSelection.Unit.Column)
								{
									this.ObjectModifier.SetGridColumnLeftBorder(selectedObject, item.Index, (double) value.Value);
								}
							}
							break;

						case AbstractProxy.Type.PanelGridRightBorder:
							foreach (PanelEditor.GridSelection.OneItem item in gs)
							{
								if (item.Unit == PanelEditor.GridSelection.Unit.Column)
								{
									this.ObjectModifier.SetGridColumnRightBorder(selectedObject, item.Index, (double) value.Value);
								}
							}
							break;

						case AbstractProxy.Type.PanelGridRowMode:
							foreach (PanelEditor.GridSelection.OneItem item in gs)
							{
								if (item.Unit == PanelEditor.GridSelection.Unit.Row)
								{
									this.ObjectModifier.SetGridRowMode(selectedObject, item.Index, (PanelEditor.ObjectModifier.GridMode) value.Value);
								}
							}
							regenerateProxies = true;
							regenerateDimensions = true;
							break;

						case AbstractProxy.Type.PanelGridRowHeight:
							foreach (PanelEditor.GridSelection.OneItem item in gs)
							{
								if (item.Unit == PanelEditor.GridSelection.Unit.Row)
								{
									this.ObjectModifier.SetGridRowHeight(selectedObject, item.Index, (double) value.Value);
								}
							}
							break;

						case AbstractProxy.Type.PanelGridRowMinHeight:
							foreach (PanelEditor.GridSelection.OneItem item in gs)
							{
								if (item.Unit == PanelEditor.GridSelection.Unit.Row)
								{
									this.ObjectModifier.SetGridRowMinHeight(selectedObject, item.Index, (double) value.Value);
								}
							}
							break;

						case AbstractProxy.Type.PanelGridRowMaxHeight:
							foreach (PanelEditor.GridSelection.OneItem item in gs)
							{
								if (item.Unit == PanelEditor.GridSelection.Unit.Row)
								{
									this.ObjectModifier.SetGridRowMaxHeight(selectedObject, item.Index, (double) value.Value);
								}
							}
							break;

						case AbstractProxy.Type.PanelGridTopBorder:
							foreach (PanelEditor.GridSelection.OneItem item in gs)
							{
								if (item.Unit == PanelEditor.GridSelection.Unit.Row)
								{
									this.ObjectModifier.SetGridRowTopBorder(selectedObject, item.Index, (double) value.Value);
								}
							}
							break;

						case AbstractProxy.Type.PanelGridBottomBorder:
							foreach (PanelEditor.GridSelection.OneItem item in gs)
							{
								if (item.Unit == PanelEditor.GridSelection.Unit.Row)
								{
									this.ObjectModifier.SetGridRowBottomBorder(selectedObject, item.Index, (double) value.Value);
								}
							}
							break;

					}
				}
			}
			finally
			{
				this.Viewer.ProxyManager.UpdateInterface();

				if (regenerateProxies && regenerateDimensions)
				{
					Application.QueueAsyncCallback(this.ObjectModifier.PanelEditor.ViewersPanels.RegenerateProxiesAndDimensions);
				}
				else if (regenerateProxies)
				{
					Application.QueueAsyncCallback(this.ObjectModifier.PanelEditor.ViewersPanels.RegenerateProxies);
				}
				else if (regenerateDimensions)
				{
					Application.QueueAsyncCallback(this.ObjectModifier.PanelEditor.ViewersPanels.RegenerateDimensions);
				}

				this.ObjectModifier.PanelEditor.Module.AccessPanels.SetLocalDirty();

				this.ResumeChanges();
			}
		}


		protected Viewers.Panels Viewer
		{
			get
			{
				return this.ObjectModifier.PanelEditor.ViewersPanels;
			}
		}

		protected PanelEditor.ObjectModifier ObjectModifier
		{
			get
			{
				return this.objectModifier as PanelEditor.ObjectModifier;
			}
		}
	}
}
