﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Widgets;
using Epsitec.Cresus.Assets.Server.NaiveEngine;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class SummaryController
	{
		public SummaryController(DataAccessor accessor, List<List<int>> fields)
		{
			this.accessor = accessor;
			this.fields = fields;
		}


		public void CreateUI(Widget parent)
		{
			this.informations = new StaticText
			{
				Parent  = parent,
				Dock    = DockStyle.Top,
				Margins = new Margins (0, 0, 0, 20),
			};

			this.frameBox = new FrameBox
			{
				Parent = parent,
				Dock   = DockStyle.Fill,
			};

			int columnsCount = this.ColumnsCount;
			int rowsCount    = this.RowsCount;

			for (int column = 0; column < columnsCount; column++ )
			{
				var columnFrame = new FrameBox
				{
					Parent         = this.frameBox,
					Dock           = DockStyle.Left,
					PreferredWidth = 120,
				};

				for (int row = 0; row < rowsCount; row++)
				{
					var st = new StaticText
					{
						Parent        = columnFrame,
						Dock          = DockStyle.Top,
						PreferredSize = new Size (100, 20),
						Margins       = new Margins (1),
						TextBreakMode = TextBreakMode.Ellipsis | TextBreakMode.Split | TextBreakMode.SingleLine,
					};

					var field = this.GetField (column, row);
					var desc = StaticDescriptions.GetObjectFieldDescription ((ObjectField) field.GetValueOrDefault (-1));
					if (!string.IsNullOrEmpty (desc))
					{
						ToolTip.Default.SetToolTip (st, desc);
					}
				}
			}
		}

		public void UpdateFields(Guid objectGuid, Timestamp? timestamp)
		{
			this.timestamp = timestamp;

			if (objectGuid.IsEmpty)
			{
				this.hasEvent = false;
				this.properties = null;
			}
			else
			{
				var ts = timestamp.GetValueOrDefault (new Timestamp (System.DateTime.MaxValue, 0));
				this.hasEvent = this.accessor.HasObjectEvent (objectGuid, ts);
				this.properties = this.accessor.GetObjectSyntheticProperties (objectGuid, ts);
			}

			this.UpdateInformations ();

			for (int column = 0; column < this.frameBox.Children.Count; column++)
			{
				var columnFrame = this.frameBox.Children[column] as FrameBox;

				for (int row = 0; row < columnFrame.Children.Count; row++)
				{
					var st = columnFrame.Children[row] as StaticText;
					this.UpdateStaticText (st, this.GetField (column, row));
				}
			}
		}


		private void UpdateInformations()
		{
			this.informations.Text = this.Informations;
		}

		private string Informations
		{
			get
			{
				if (this.hasEvent && this.properties != null)
				{
					return string.Format ("Cet événement définit {0} champs.", this.SinglePropertiesCount);
				}
				else if (!this.timestamp.HasValue)
				{
					return "Etat final de l'objet.";
				}
				else
				{
					return "Il n'y a pas d'événement à cette date.";
				}
			}
		}


		private void UpdateStaticText(StaticText st, int? field)
		{
			st.BackColor = this.GetBackgroundColor (field);

			if (field.HasValue && this.properties != null)
			{
				string s = DataAccessor.GetStringProperty (this.properties, field.Value);
				if (!string.IsNullOrEmpty (s))
				{
					st.Text = " " + s;
					st.ContentAlignment = ContentAlignment.MiddleLeft;
					return;
				}

				var d = DataAccessor.GetDecimalProperty (this.properties, field.Value);
				if (d.HasValue)
				{
					st.Text = d.Value.ToString () + " ";
					st.ContentAlignment = ContentAlignment.MiddleRight;
					return;
				}

				var i = DataAccessor.GetIntProperty (this.properties, field.Value);
				if (i.HasValue)
				{
					st.Text = i.Value.ToString () + " ";
					st.ContentAlignment = ContentAlignment.MiddleRight;
					return;
				}
			}

			st.Text = null;
		}

		private Color GetBackgroundColor(int? field)
		{
			if (field.HasValue)
			{
				switch (this.GetPropertyState (field.Value))
				{
					case PropertyState.Single:
						return ColorManager.EditSinglePropertyColor;

					default:
						return ColorManager.WindowBackgroundColor;
				}
			}
			else
			{
				return Color.Empty;
			}
		}

		private PropertyState GetPropertyState(int field)
		{
			if (this.hasEvent)
			{
				return DataAccessor.GetPropertyState (this.properties, field);
			}
			else
			{
				return PropertyState.Readonly;
			}
		}

		private int SinglePropertiesCount
		{
			get
			{
				if (this.properties == null)
				{
					return 0;
				}
				else
				{
					return this.properties.Where (x => x.State == PropertyState.Single).Count ();
				}
			}
		}

		private int? GetField(int column, int row)
		{
			if (column < this.fields.Count)
			{
				var rows = this.fields[column];

				if (row < rows.Count)
				{
					return rows[row];
				}
			}

			return null;
		}

		private int ColumnsCount
		{
			get
			{
				return this.fields.Count;
			}
		}

		private int RowsCount
		{
			get
			{
				int count = 0;

				foreach (var columns in this.fields)
				{
					count = System.Math.Max (count, columns.Count);
				}

				return count;
			}
		}


		private readonly DataAccessor				accessor;
		private readonly List<List<int>>			fields;

		private StaticText							informations;
		private FrameBox							frameBox;
		private Timestamp?							timestamp;
		private bool								hasEvent;
		private IEnumerable<AbstractDataProperty>	properties;
	}
}
