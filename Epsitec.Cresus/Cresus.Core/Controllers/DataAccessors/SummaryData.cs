﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Widgets.Tiles;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.DataAccessors
{
	public class SummaryData : System.IComparable<SummaryData>, ITileController
	{
		public SummaryData()
		{
			this.bindings = new List<AccessorBinding> ();
		}

		public SummaryData(SummaryData template)
			: this ()
		{
			if (template != null)
			{
				this.Name      = template.Name;
				this.Rank      = template.Rank;
				this.IconUri   = template.IconUri;
				this.AutoGroup = template.AutoGroup;
				this.DataType  = template.DataType;
			}
		}

		
		public string Name
		{
			get;
			set;
		}

		public int Rank
		{
			get;
			set;
		}
		
		public string IconUri
		{
			get;
			set;
		}

		public bool AutoGroup
		{
			get;
			set;
		}

		public bool IsCompact
		{
			get;
			set;
		}

		public SummaryDataType DataType
		{
			get;
			set;
		}

		public FormattedText Title
		{
			get;
			set;
		}

		public FormattedText Text
		{
			get;
			set;
		}

		public FormattedText CompactTitle
		{
			get;
			set;
		}

		public FormattedText CompactText
		{
			get;
			set;
		}


		public TitleTile TitleTile
		{
			get
			{
				return this.titleTile;
			}
			set
			{
				if (this.titleTile != value)
				{
					if ((this.titleTile != null) &&
						(this.SummaryTile != null))
                    {
						this.titleTile.Items.Remove (this.SummaryTile);
                    }

					this.titleTile = value;

					if ((this.titleTile != null) &&
						(this.SummaryTile != null))
					{
						this.titleTile.Items.Add (this.SummaryTile);
					}
				}
			}
		}

		public SummaryTile SummaryTile
		{
			get;
			set;
		}


		public System.Action AddNewItem
		{
			get;
			set;
		}

		public System.Action DeleteItem
		{
			get;
			set;
		}

		public FormattedText DefaultTitle
		{
			get
			{
				if (this.AutoGroup)
				{
					return this.CompactTitle;
				}
				else
				{
					return this.Title;
				}
			}
		}


		public System.Func<AbstractEntity> EntityAccessor
		{
			get;
			set;
		}

		public Accessor<FormattedText> TitleAccessor
		{
			set
			{
				if (value != null)
				{
					this.bindings.Add (AccessorBinding.Create (value, x => this.Title = x));
				}
			}
		}

		public Accessor<FormattedText> TextAccessor
		{
			set
			{
				if (value != null)
				{
					this.bindings.Add (AccessorBinding.Create (value, x => this.Text = x));
				}
			}
		}

		public Accessor<FormattedText> CompactTitleAccessor
		{
			set
			{
				if (value != null)
				{
					this.bindings.Add (AccessorBinding.Create (value, x => this.CompactTitle = x));
				}
			}
		}

		public Accessor<FormattedText> CompactTextAccessor
		{
			set
			{
				if (value != null)
				{
					this.bindings.Add (AccessorBinding.Create (value, x => this.CompactText = x));
				}
			}
		}

		public void ExecuteAccessors()
		{
			this.bindings.ForEach (x => x.Execute ());
		}


		public static string BuildName(string prefix, int index)
		{
			return string.Concat (prefix, ".", index.ToString (System.Globalization.CultureInfo.InvariantCulture));
		}

		public static string GetNamePrefix(string name)
		{
			if (name == null)
            {
				return null;
            }

			int pos = name.LastIndexOf ('.');

			if (pos < 0)
			{
				return name;
			}
			else
			{
				return name.Substring (0, pos);
			}
		}

		
		#region ITileController Members

		EntityViewController ITileController.CreateSubViewController(Orchestrators.DataViewOrchestrator orchestrator)
		{
			if (this.EntityAccessor != null)
			{
				var entity = this.EntityAccessor ();

				if (entity != null)
				{
					return EntityViewController.CreateEntityViewController ("ViewController", entity, ViewControllerMode.Edition, orchestrator);
				}
			}

			return null;
		}

		#endregion
		
		#region IComparable<SummaryData> Members

		public int CompareTo(SummaryData other)
		{
			int rankA = this.Rank / 1000;
			int rankB = other.Rank / 1000;

			if (rankA < rankB)
			{
				return -1;
			}
			else if (rankA > rankB)
			{
				return 1;
			}

			var options = System.Globalization.CompareOptions.StringSort | System.Globalization.CompareOptions.IgnoreCase;
			var culture = System.Globalization.CultureInfo.CurrentCulture;
			int result  = string.Compare (this.CompactTitle.ToSimpleText (), other.CompactTitle.ToSimpleText (), culture, options);

			if (result == 0)
            {
				if (this.Rank < other.Rank)
				{
					return -1;
				}
				else if (this.Rank > other.Rank)
                {
					return 1;
                }
            }

			return result;
		}

		#endregion

		private readonly List<AccessorBinding> bindings;
		private TitleTile titleTile;
	}
}