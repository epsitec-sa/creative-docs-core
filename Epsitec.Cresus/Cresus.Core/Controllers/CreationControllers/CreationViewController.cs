﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Core.BusinessLogic;

using Epsitec.Cresus.DataLayer;
using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.CreationControllers
{
	public abstract class CreationViewController<T> : EntityViewController<T>, ICreationController
		where T : AbstractEntity, new ()
	{
		protected CreationViewController(string name, T entity)
			: base (name, entity)
		{
			System.Diagnostics.Debug.Assert (this.Orchestrator != null);
			System.Diagnostics.Debug.Assert (this.Orchestrator.Data.IsDummyEntity (entity));
		}

		public override IEnumerable<CoreController> GetSubControllers()
		{
			if (this.replacementController != null)
            {
				yield return this.replacementController;
            }
		}

		#region ICreationController Members

		ViewControllerMode ICreationController.UpgradeControllerMode
		{
			get
			{
				return this.GetUpgradeControllerMode ();
			}
		}

		#endregion

		public override CoreViewController GetReplacementController()
		{
			var upgradeMode = this.GetUpgradeControllerMode ();

			if (upgradeMode == ViewControllerMode.Creation)
			{
				return base.GetReplacementController ();
			}
			else
			{
				if ((this.replacementController != null) &&
					(this.replacementController.Mode != upgradeMode))
				{
					this.replacementController.Dispose ();
					this.replacementController = null;
				}

				if (this.replacementController == null)
				{
					this.CreateReplacementController (upgradeMode);
				}
				
				return this.replacementController;
			}
		}

		protected void UpgradeController()
		{
			var upgradeMode = this.GetUpgradeControllerMode ();

			if (upgradeMode == ViewControllerMode.Creation)
			{
				//	Do not upgrade the controller: we are already the creation mode controller
			}
			else
			{
				var orchestrator = this.Orchestrator;
				orchestrator.ReplaceView (this, this);
			}
		}

		protected virtual ViewControllerMode GetUpgradeControllerMode()
		{
			switch (this.EditionStatus)
			{
				case EditionStatus.Empty:
					return ViewControllerMode.Creation;

				case EditionStatus.Invalid:
				case EditionStatus.Valid:
					return ViewControllerMode.Summary;

				case EditionStatus.Unknown:
				default:
					throw new System.InvalidOperationException (string.Format ("EditionStatus may not be set to {0}", this.EditionStatus));
			}
		}

		private void CreateReplacementController(ViewControllerMode upgradeMode)
		{
			var controller = EntityViewControllerFactory.Create (this.Name, this.Entity, upgradeMode, this.Orchestrator) as EntityViewController<T>;

			System.Diagnostics.Debug.Assert (controller.DataContext.Contains (controller.Entity));
//-			controller.EnsureRealEntity ();

			this.replacementController = controller;
		}
		
		internal void CreateRealEntity(System.Action<BusinessContext, T> initializer = null)
		{
			var orchestrator = this.Orchestrator;
			var business     = this.BusinessContext;

			System.Diagnostics.Debug.Assert (business != null);

			var entity = business.CreateEntity<T> ();

			if (initializer != null)
			{
				initializer (business, entity);
			}

			this.ReplaceEntity (entity);
			this.UpgradeController ();
		}

		protected EntityViewController replacementController;
	}
}