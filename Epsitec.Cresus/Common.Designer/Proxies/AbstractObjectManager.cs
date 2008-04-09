﻿using System.Collections.Generic;
using Epsitec.Common.Drawing;
using Epsitec.Common.Support;
using Epsitec.Common.Widgets;

namespace Epsitec.Common.Designer.Proxies
{
	/// <summary>
	/// Cette classe gère les objets associés à un proxy.
	/// </summary>
	public abstract class AbstractObjectManager
	{
		public AbstractObjectManager(object objectModifier)
		{
			this.objectModifier = objectModifier;
		}

		public virtual List<AbstractValue> GetValues(Widget selectedObject)
		{
			//	Retourne la liste des valeurs nécessaires pour représenter un objet.
			return null;
		}

		public virtual bool IsEnable(AbstractValue value)
		{
			//	Indique si la valeur pour représenter un objet est enable.
			return true;
		}


		protected void AddValue(List<AbstractValue> list, Widget selectedObject, AbstractProxy.Type type, Types.Caption caption, double min, double max, double step, double resolution)
		{
			//	Ajoute une valeur de type numérique.
			ValueNumeric value = new ValueNumeric(min, max, step, resolution);
			value.SelectedObjects.Add(selectedObject);
			value.Type = type;
			value.Caption = caption;
			value.ValueChanged += new EventHandler(this.HandleValueChanged);
			this.SendObjectToValue(value);

			list.Add(value);
		}

		protected void AddValue(List<AbstractValue> list, Widget selectedObject, AbstractProxy.Type type, Types.Caption caption, Types.EnumType enumType)
		{
			//	Ajoute une valeur de type énumération.
			ValueEnum value = new ValueEnum(enumType);
			value.SelectedObjects.Add(selectedObject);
			value.Type = type;
			value.Caption = caption;
			value.ValueChanged += new EventHandler(this.HandleValueChanged);
			this.SendObjectToValue(value);

			list.Add(value);
		}

		private void HandleValueChanged(object sender)
		{
			if (this.IsNotSuspended)
			{
				AbstractValue value = sender as AbstractValue;
				this.SendValueToObject(value);
			}
		}


		public virtual void SendObjectToValue(AbstractValue value)
		{
			//	Tous les objets ont la même valeur. Il suffit donc de s'occuper du premier objet.
		}

		protected virtual void SendValueToObject(AbstractValue value)
		{
			//	Il faut envoyer la valeur à tous les objets sélectionnés.
		}


		protected bool IsSuspended
		{
			get
			{
				return this.suspendChanges != 0;
			}
		}

		protected bool IsNotSuspended
		{
			get
			{
				return this.suspendChanges == 0;
			}
		}

		protected void SuspendChanges()
		{
			//	Suspend les changements jusqu'au prochain ResumeChanges.
			this.suspendChanges++;
		}

		protected void ResumeChanges()
		{
			//	Reprend les changements après un SuspendChanges.
			this.suspendChanges--;
			System.Diagnostics.Debug.Assert(this.suspendChanges >= 0);
		}


		protected object objectModifier;
		protected int suspendChanges;
	}
}
