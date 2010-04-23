﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

namespace Epsitec.Cresus.Core.Controllers
{
	/// <summary>
	/// Ce contrôleur représente une bande verticale dans laquelle on empile des tuiles AbstractViewController.
	/// </summary>
	public class DataViewController : CoreViewController
	{
		public DataViewController(string name)
			: base (name)
		{
			this.viewControllers = new Stack<EntityViewController> ();
		}

		public override IEnumerable<CoreController> GetSubControllers()
		{
			return this.viewControllers;
		}

		public override void CreateUI(Widget container)
		{
			this.frame = new FrameBox ()
			{
				Parent = container,
				Dock = DockStyle.Fill,
			};

			this.viewLayoutController = new ViewLayoutController (this.Name + ".ViewLayout", this.frame);
		}

		public void SelectEntity(AbstractEntity entity)
		{
			this.ClearActiveEntity ();

			this.entity = entity;

			this.PushViewController (EntityViewController.CreateViewController ("ViewController", this.entity, ViewControllerMode.Compact));
		}

		public void PushViewController(EntityViewController controller)
		{
			System.Diagnostics.Debug.Assert (controller != null);

			var column = this.viewLayoutController.CreateColumn ();

			this.viewControllers.Push (controller);
			
			controller.CreateUI (column);
		}

		public void PopViewController()
		{
			System.Diagnostics.Debug.Assert (this.viewControllers.Count > 0);

			var controller = this.viewControllers.Pop ();

			controller.Dispose ();
			this.viewLayoutController.DeleteColumn ();
		}

		public void ClearActiveEntity()
		{
			if (this.entity != null)
            {
				while (this.viewControllers.Count > 0)
                {
					this.PopViewController ();
                }
				
				this.entity = null;
            }
		}


		private readonly Stack<EntityViewController> viewControllers;
		
		private ViewLayoutController viewLayoutController;
		private FrameBox frame;
		
		private AbstractEntity entity;
	}
}
