﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Views.FieldControllers
{
	public class ComputedAmountFieldController : AbstractFieldController
	{
		public ComputedAmountFieldController(DataAccessor accessor)
			: base (accessor)
		{
		}


		public ComputedAmount?					Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (this.value != value)
				{
					this.value = value;

					if (this.controller != null)
					{
						if (this.ignoreChanges.IsZero)
						{
							using (this.ignoreChanges.Enter ())
							{
								this.controller.ComputedAmount = this.value;
								this.UpdateError ();
							}
						}
						else
						{
							using (this.ignoreChanges.Enter ())
							{
								this.controller.ComputedAmountNoEditing = this.value;
								this.UpdateError ();
							}
						}
					}
				}
			}
		}

		private void UpdateValue()
		{
			using (this.ignoreChanges.Enter ())
			{
				this.controller.UpdateValue ();
				this.UpdateError ();
			}
		}

		private void UpdateError()
		{
			if (this.Required)
			{
				bool error = !this.value.HasValue;
				if (this.hasError != error)
				{
					this.hasError = error;
					this.UpdatePropertyState ();
				}
			}
		}

		protected override void ClearValue()
		{
			this.Value = null;
			this.OnValueEdited (this.Field);
		}

		protected override void UpdatePropertyState()
		{
			base.UpdatePropertyState ();

			if (this.controller != null)
			{
				this.controller.PropertyState = this.PropertyState;
				this.controller.IsReadOnly    = this.isReadOnly;
				this.controller.HasError      = this.hasError;
			}
		}


		public override void CreateUI(Widget parent)
		{
			base.CreateUI (parent);

			this.controller = new ComputedAmountController ();
			this.controller.CreateUI (this.frameBox);
			this.controller.ComputedAmount = this.value;

			this.UpdateError ();
			this.UpdatePropertyState ();

			this.controller.ValueEdited += delegate
			{
				if (this.ignoreChanges.IsZero)
				{
					using (this.ignoreChanges.Enter ())
					{
						this.Value = this.controller.ComputedAmount;
						this.UpdateError ();
						this.OnValueEdited (this.Field);
					}
				}
			};

			this.controller.FocusEngage += delegate
			{
				base.SetFocus ();
			};

			this.controller.FocusLost += delegate
			{
				this.UpdateValue ();
			};
		}

		public override void SetFocus()
		{
			this.controller.SetFocus ();

			base.SetFocus ();
		}


		private ComputedAmountController		controller;
		private ComputedAmount?					value;
	}
}
