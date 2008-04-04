using System.Collections.Generic;
using Epsitec.Common.Drawing;
using Epsitec.Common.Types;
using Epsitec.Common.UI;
using Epsitec.Common.Widgets;

namespace Epsitec.Common.Designer.FormEditor
{
	public sealed class ProxyManager
	{
		public ProxyManager(Viewers.Forms formViewer)
		{
			this.formViewer = formViewer;
			this.objectModifier = this.formViewer.FormEditor.ObjectModifier;
			this.widgets = new List<Widget>();
			this.proxies = new List<IProxy>();

			Common.Designer.Proxies.ObjectManagerForm objectManager = new Common.Designer.Proxies.ObjectManagerForm(this.objectModifier);
			Common.Designer.Proxies.ProxyForm proxy = new Common.Designer.Proxies.ProxyForm(this.formViewer);
			this.manager = new Common.Designer.Proxies.ProxyManager(objectManager, proxy);
		}

		public IEnumerable<Widget> Widgets
		{
			get
			{
				return this.widgets;
			}
		}

		public Viewers.Forms FormViewer
		{
			get
			{
				return this.formViewer;
			}
		}

		public ObjectModifier ObjectModifier
		{
			get
			{
				return this.objectModifier;
			}
		}
		
		public IEnumerable<IProxy> Proxies
		{
			get
			{
				return this.proxies;
			}
		}
		
		public void SetSelection(IEnumerable<Widget> collection)
		{
			//	Sp�cifie les objets s�lectionn�s et construit la liste des proxies n�cessaires.
			this.widgets = new List<Widget>();
			this.widgets.AddRange(collection);
			this.GenerateProxies();
		}

		public void CreateUserInterface(Widget container)
		{
			//	Cr�e l'interface utilisateur (panneaux) pour la liste des proxies.
#if true
			foreach (IProxy proxy in this.proxies)
			{
				this.CreateUserInterface(container, proxy);
			}
#else
			this.manager.CreateInterface(container, this.widgets);
#endif
		}

		public void UpdateUserInterface()
		{
			//	Met � jour l'interface utilisateur (panneaux), sans changer le nombre de
			//	propri�t�s visibles par panneau.
			if (this.proxies != null)
			{
				foreach (IProxy proxy in this.proxies)
				{
					proxy.Update();
				}
			}
		}

		public bool RegenerateProxies()
		{
			//	R�g�n�re la liste des proxies.
			//	Retourne true si la liste a chang�.
			return this.GenerateProxies();
		}

		public void ClearUserInterface(Widget container)
		{
			//	Supprime l'interface utilisateur (panneaux) pour la liste des proxies.
			foreach (Widget obj in container.Children)
			{
				if (obj is MyWidgets.PropertyPanel)
				{
					MyWidgets.PropertyPanel panel = obj as MyWidgets.PropertyPanel;
					panel.ExtendedSize -= new Epsitec.Common.Support.EventHandler(this.HandlePanelExtendedSize);
				}
			}

			container.Children.Clear();
		}


		public static bool EqualValues(IProxy a, IProxy b)
		{
			if (a == b)
			{
				return true;
			}
			
			if (a == null || b == null)
			{
				return false;
			}
			
			//	Ca ne vaut pas la peine de comparer les valeurs des deux proxies
			//	si leur rang est diff�rent, car cela implique qu'ils ne vont pas
			//	�tre repr�sent�s par des panneaux identiques :
			if (a.Rank != b.Rank)
			{
				return false;
			}
			
			return DependencyObject.EqualValues(a as DependencyObject, b as DependencyObject);
		}

		private bool GenerateProxies()
		{
			//	G�n�re une liste (tri�e) de tous les proxies. Il se peut qu'il
			//	y ait plusieurs proxies de type identique si plusieurs widgets
			//	utilisent des r�glages diff�rents.
			//	Retourne true si la liste a chang�.
			List<IProxy> proxies = new List<IProxy>();

			foreach (Widget widget in this.widgets)
			{
				foreach (IProxy proxy in this.GenerateWidgetProxies(widget))
				{
					//	Evite les doublons pour des proxies qui seraient � 100%
					//	identiques :
					bool insert = true;

					proxy.AddWidget(widget);
					
					foreach (IProxy item in proxies)
					{
						if (ProxyManager.EqualValues(item, proxy))
						{
							//	Trouv� un doublon. On ajoute simplement le widget
							//	courant au proxy qui existe d�j� avec les m�mes
							//	valeurs :
							item.AddWidget(widget);
							insert = false;
							break;
						}
					}

					if (insert)
					{
						proxies.Add(proxy);
					}
				}
			}

			//	Trie les proxies selon leur rang :
			proxies.Sort(new Comparers.ProxyRank());

			if (ProxyManager.EqualLists(this.proxies, proxies))
			{
				for (int i=0; i<proxies.Count; i++)
				{
					this.proxies[i].ClearWidgets();

					foreach (Widget widget in proxies[i].Widgets)
					{
						this.proxies[i].AddWidget(widget);
					}
				}
				return false;
			}
			else
			{
				this.proxies = proxies;
				return true;
			}
		}

		private IEnumerable<IProxy> GenerateWidgetProxies(Widget widget)
		{
			yield return new Proxies.Geometry(this);
			yield return new Proxies.Style(this);
		}

		static private bool EqualLists(List<IProxy> list1, List<IProxy> list2)
		{
			//	Compare si deux listes contiennent des proxies identiques.
			if (list1 != null && list1.Count == list2.Count)
			{
				for (int i=0; i<list1.Count; i++)
				{
					IProxy item1 = list1[i];
					IProxy item2 = list2[i];
					if (!ProxyManager.EqualValues(item1, item2))
					{
						return false;
					}
				}
				return true;
			}

			return false;
		}

		private void CreateUserInterface(Widget container, IProxy proxy)
		{
			//	Cr�e un panneau pour repr�senter le proxy sp�cifi�.
			DependencyObject source = proxy as DependencyObject;

			MyWidgets.PropertyPanel panel = new MyWidgets.PropertyPanel(container);
			panel.Dock = DockStyle.Top;
			panel.Icon = proxy.IconName;
			panel.DataColumnWidth = proxy.DataColumnWidth;
			panel.RowsSpacing = proxy.RowsSpacing;
			panel.Title = source.GetType().Name;
			panel.Rank = proxy.Rank;
			//?panel.IsExtendedSize = this.formViewer.PanelsContext.IsExtendedProxies(proxy.Rank);
			panel.ExtendedSize += new Epsitec.Common.Support.EventHandler(this.HandlePanelExtendedSize);

			foreach (DependencyProperty property in source.DefinedProperties)
			{
				Placeholder placeholder = new Placeholder();
				Binding binding = new Binding(BindingMode.TwoWay, source, property.Name);
				placeholder.SetBinding(Placeholder.ValueProperty, binding);
				placeholder.Controller = "*";
				panel.AddPlaceHolder(placeholder);
			}
		}

		private void HandlePanelExtendedSize(object sender)
		{
			MyWidgets.PropertyPanel panel = sender as MyWidgets.PropertyPanel;
			System.Diagnostics.Debug.Assert(panel != null);

			//?this.formViewer.PanelsContext.SetExtendedProxies(panel.Rank, panel.IsExtendedSize);
		}


		static ProxyManager()
		{
			StringType druidCaptionStringType = new StringType();
			druidCaptionStringType.DefineDefaultController("Druid", "Caption");  // utilise DruidController
			ProxyManager.DruidCaptionStringType = druidCaptionStringType;

			StringType druidPanelStringType = new StringType();
			druidPanelStringType.DefineDefaultController("Druid", "Panel");  // utilise DruidController
			ProxyManager.DruidPanelStringType = druidPanelStringType;

			InternalBindingType bindingType = new InternalBindingType();
			bindingType.DefineDefaultController("Binding", "");  // utilise BindingController
			ProxyManager.BindingType = bindingType;

			InternalTableType tableType = new InternalTableType();
			tableType.DefineDefaultController("Table", "");  // utilise TableController
			ProxyManager.TableType = tableType;

			InternalStructuredType structuredType = new InternalStructuredType();
			structuredType.DefineDefaultController("Structured", "");  // utilise StructuredController
			ProxyManager.StructuredType = structuredType;

			DoubleType locationNumericType = new DoubleType(-9999, 9999, 1.0M);
			locationNumericType.DefinePreferredRange(new DecimalRange(0, 1000, 2));
			ProxyManager.LocationNumericType = locationNumericType;

			DoubleType sizeNumericType = new DoubleType(0, 9999, 1.0M);
			sizeNumericType.DefinePreferredRange(new DecimalRange(0, 1000, 1));
			ProxyManager.SizeNumericType = sizeNumericType;

			DoubleType frameWidthNumericType = new DoubleType(1, 5, 1.0M);
			frameWidthNumericType.DefinePreferredRange(new DecimalRange(1, 5, 1));
			ProxyManager.FrameWidthNumericType = frameWidthNumericType;

			DoubleType preferredWidthNumericType = new DoubleType(1, 1000, 1.0M);
			preferredWidthNumericType.DefinePreferredRange(new DecimalRange(1, 1000, 1));
			ProxyManager.PreferredWidthNumericType = preferredWidthNumericType;

			DoubleType marginNumericType = new DoubleType(-1, 9999, 1.0M);
			marginNumericType.DefinePreferredRange(new DecimalRange(0, 200, 1));
			ProxyManager.MarginNumericType = marginNumericType;
			
			IntegerType columnsRequiredNumericType = new IntegerType(0, FormEngine.Engine.MaxColumnsRequired);
			columnsRequiredNumericType.DefinePreferredRange(new DecimalRange(0, FormEngine.Engine.MaxColumnsRequired, 1));
			ProxyManager.ColumnsRequiredNumericType = columnsRequiredNumericType;
			
			IntegerType rowsRequiredNumericType = new IntegerType(1, FormEngine.Engine.MaxRowsRequired);
			rowsRequiredNumericType.DefinePreferredRange(new DecimalRange(1, FormEngine.Engine.MaxRowsRequired, 1));
			ProxyManager.RowsRequiredNumericType = rowsRequiredNumericType;
		}

		private class InternalBindingType : AbstractType
		{
			public override System.Type SystemType
			{
				get
				{
					return typeof(Binding);
				}
			}

			public override TypeCode TypeCode
			{
				get
				{
					return TypeCode.Other;
				}
			}

			public override bool IsValidValue(object value)
			{
				if (value == null)
				{
					return true;
				}
				else
				{
					return value is Binding;
				}
			}
		}

		private class InternalTableType : AbstractType
		{
			public override System.Type SystemType
			{
				get
				{
					return typeof(List<UI.ItemTableColumn>);
				}
			}
			
			public override TypeCode TypeCode
			{
				get
				{
					return TypeCode.Other;
				}
			}

			public override bool IsValidValue(object value)
			{
				if (value == null)
				{
					return true;
				}
				else
				{
					return value is List<UI.ItemTableColumn>;
				}
			}
		}

		private class InternalStructuredType : AbstractType
		{
			public override System.Type SystemType
			{
				get
				{
					return typeof(StructuredType);
				}
			}
			
			public override TypeCode TypeCode
			{
				get
				{
					return TypeCode.Other;
				}
			}

			public override bool IsValidValue(object value)
			{
				if (value == null)
				{
					return true;
				}
				else
				{
					return value is StructuredType;
				}
			}
		}

		public static readonly IStringType  DruidCaptionStringType;
		public static readonly IStringType  DruidPanelStringType;
		public static readonly INamedType	BindingType;
		public static readonly INamedType	TableType;
		public static readonly INamedType	StructuredType;
		public static readonly INumericType LocationNumericType;
		public static readonly INumericType SizeNumericType;
		public static readonly INumericType FrameWidthNumericType;
		public static readonly INumericType PreferredWidthNumericType;
		public static readonly INumericType MarginNumericType;
		public static readonly INumericType ColumnsRequiredNumericType;
		public static readonly INumericType RowsRequiredNumericType;

		private Viewers.Forms				formViewer;
		private ObjectModifier				objectModifier;
		private List<Widget>				widgets;
		private List<IProxy>				proxies;
		private Common.Designer.Proxies.ProxyManager manager;
	}
}
