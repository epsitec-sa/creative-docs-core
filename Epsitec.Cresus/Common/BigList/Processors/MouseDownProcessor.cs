//	Copyright � 2012, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Common.BigList.Processors
{
	public class MouseDownProcessor : EventProcessor
	{
		public MouseDownProcessor(IEventProcessorHost host, Message message, Point pos)
		{
			this.host   = host;
			this.selectionProcessor = host as ISelectionProcessor;
			this.detectionProcessor = host as IDetectionProcessor;
			this.policy = host.GetPolicy<MouseDownProcessorPolicy> ();
			this.button = message.Button;
			this.origin = pos;

			int index = this.detectionProcessor.Detect (pos);

			if (index < 0)
			{
				return;
			}

			this.host.Register (this);

			this.originalIndex = index;
			this.originalSelection = this.selectionProcessor.IsSelected (index);

			this.selectionProcessor.Select (index, ItemSelection.Toggle);
		}

		public override bool ProcessMessage(Message message, Point pos)
		{
			switch (message.MessageType)
			{
				case MessageType.MouseMove:
					this.ProcessMove (pos);
					break;

				case MessageType.MouseUp:

					this.ProcessMove (pos);

					if (message.Button == this.button)
					{
						this.selectionProcessor.Select (this.originalIndex, ItemSelection.Focus);
						this.host.Remove (this);

						return true;
					}
					break;
			}

			return false;
		}

		private void ProcessMove(Point pos)
		{
			if (this.policy.AutoFollow)
			{
				int oldIndex = this.originalIndex;
				int newIndex = this.detectionProcessor.Detect (pos);

				if (newIndex != oldIndex)
				{
					bool newSelection = this.selectionProcessor.IsSelected (newIndex);

					this.selectionProcessor.Select (newIndex, ItemSelection.Toggle);
					this.selectionProcessor.Select (oldIndex, this.originalSelection ? ItemSelection.Select : ItemSelection.Deselect);
					this.selectionProcessor.Select (newIndex, ItemSelection.Activate);

					this.originalIndex     = newIndex;
					this.originalSelection = newSelection;
				}
			}
		}

		private readonly IEventProcessorHost	host;
		private readonly ISelectionProcessor	selectionProcessor;
		private readonly IDetectionProcessor	detectionProcessor;
		private readonly MouseDownProcessorPolicy policy;
		private readonly MouseButtons			button;
		private readonly Point					origin;
		private bool							originalSelection;
		private int								originalIndex;
	}
}
