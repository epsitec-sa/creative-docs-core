/*
This file is part of CreativeDocs.

Copyright © 2003-2024, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland

CreativeDocs is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
any later version.

CreativeDocs is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/


using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Support;
using Epsitec.Common.Types;

namespace Epsitec.Common.Support.EntityEngine
{
    /// <summary>
    /// The <c>EntityInfo{T}</c> class is used to retrieve runtime information
    /// about an entity of type <typeparamref name="T"/>, such as its DRUID.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public static class EntityInfo<T>
        where T : AbstractEntity, new()
    {
        /// <summary>
        /// Gets the structured type id for the specified entity.
        /// </summary>
        /// <returns>The entity id.</returns>
        public static Druid GetTypeId()
        {
            return EntityInfo<T>.instance.Id;
        }

        /// <summary>
        /// Gets the structured type key for the specified entity.
        /// </summary>
        /// <returns>The entity key.</returns>
        public static string GetTypeKey()
        {
            return EntityInfo<T>.instance.Key;
        }

        /// <summary>
        /// Gets the name of the entity class, without the <c>Entity</c> suffix.
        /// </summary>
        /// <returns>The naked name of the entity.</returns>
        public static string GetNakedName()
        {
            string name = typeof(T).Name;
            const string suffix = "Entity";

            if (name.EndsWith(suffix))
            {
                return name.Substring(0, name.Length - suffix.Length);
            }

            throw new System.ArgumentException(
                string.Format(
                    "The type {0} does not follow the entity naming conventions",
                    typeof(T).FullName
                )
            );
        }

        /// <summary>
        /// Checks whether the entity implements the specified interface.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <returns><c>true</c> if entity of type <c>T</c> implements <c>TInterface</c>; otherwise, <c>false</c>.</returns>
        public static bool Implements<TInterface>()
        {
            return InterfaceImplementationTester<T, TInterface>.Check();
        }

        #region TypeIdProvider Class

        private class TypeIdProvider
        {
            public TypeIdProvider()
            {
                var entity = EmptyEntityContext.Instance.CreateEmptyEntity();
                this.id = entity.GetEntityStructuredTypeId();
                this.key = entity.GetEntityStructuredTypeKey();
            }

            public Druid Id
            {
                get { return this.id; }
            }

            public string Key
            {
                get { return this.key; }
            }

            private readonly Druid id;
            private readonly string key;
        }

        #endregion

        #region EmptyEntityContext class

        /// <summary>
        /// The <c>EmptyEntityContext</c> class will be used when creating empty entities
        /// for analysis purposes only.
        /// </summary>
        private class EmptyEntityContext : EntityContext
        {
            public EmptyEntityContext()
                : base(
                    SafeResourceResolver.Instance,
                    EntityLoopHandlingMode.Throw,
                    "Analysis/EmptyEntities"
                ) { }

            public T CreateEmptyEntity()
            {
                return this.CreateEmptyEntity<T>();
            }

            public static EmptyEntityContext Instance = new EmptyEntityContext();
        }

        #endregion

        private static readonly TypeIdProvider instance = new TypeIdProvider();
    }
}
