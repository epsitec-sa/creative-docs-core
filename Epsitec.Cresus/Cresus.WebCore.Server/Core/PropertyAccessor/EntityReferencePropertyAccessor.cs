using Epsitec.Common.Support.EntityEngine;

using Epsitec.Common.Types;

using System.Linq.Expressions;


namespace Epsitec.Cresus.WebCore.Server.Core.PropertyAccessor
{


	/// <summary>
	/// This class is the property accessor class used to read, write and check values for entity
	/// fields that are references to a single entity.
	/// </summary>
	internal sealed class EntityReferencePropertyAccessor : AbstractPropertyAccessor
	{


		public EntityReferencePropertyAccessor(LambdaExpression lambda, string id)
			: base (lambda, FieldType.EntityReference, id)
		{
		}


		public override IValidationResult CheckValue(object value)
		{
			var valid = this.CheckValueInternal (value);

			return ValidationResult.Create (valid);
		}


		private bool CheckValueInternal(object value)
		{
			if (value == null)
			{
				return this.Property.IsNullable;
			}

			var entity = value as AbstractEntity;

			if (entity == null)
			{
				return false;
			}

			return this.Type.IsAssignableFrom (value.GetType ());
		}


	}


}
