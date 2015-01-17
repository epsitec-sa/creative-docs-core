﻿//	Copyright © 2012-2014, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Marc BETTEX, Maintainer: Pierre ARNAUD

using Epsitec.Aider.Data.Common;
using Epsitec.Aider.Enumerations;

using Epsitec.Common.Support.Extensions;
using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Entities;

using Epsitec.Cresus.DataLayer.Expressions;
using Epsitec.Cresus.DataLayer.Loader;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Aider.Entities
{
	public partial class AiderMailingCategoryEntity
	{
		public bool HasNakedName
		{
			get
			{
				return this.DisplayName.Contains (",") == false;
			}
		}

		public override FormattedText GetCompactSummary()
		{
			return TextFormatter.FormatText (this.DisplayName);
		}

		public override FormattedText GetSummary()
		{
			return TextFormatter.FormatText (this.DisplayName,
											 "\nCode interne~", this.GroupPathCache);
		}

	
		public void RefreshCache()
		{
			this.UpdateGroupPathCache ();
			this.UpdateDisplayName ();
		}


		public static AiderMailingCategoryEntity Create(BusinessContext context, string name, AiderGroupEntity group)
		{
			var mailingCategory = context.CreateAndRegisterEntity<AiderMailingCategoryEntity> ();

			mailingCategory.Name  = name;
			mailingCategory.Group = group;

			mailingCategory.RefreshCache ();

			return mailingCategory;
		}

		public static void Delete(BusinessContext businessContext, AiderMailingCategoryEntity mailingCategory)
		{
			businessContext.DeleteEntity (mailingCategory);			
		}

		
		public static IEnumerable<AiderMailingCategoryEntity> GetCantonCategories(BusinessContext context, string groupPath)
		{
			return AiderMailingCategoryEntity.GetMailingCategories (context, AiderGroupIds.GlobalPrefix)
				.Where (x => x.Group.IsGlobalOrGlobalSubgroup ());
		}

		public static IEnumerable<AiderMailingCategoryEntity> GetRegionCategories(BusinessContext context, string groupPath)
		{
			return AiderMailingCategoryEntity.GetMailingCategories (context, groupPath.Substring (0, 5))
				.Where (x => x.Group.IsParishOrParishSubgroup () == false);
		}

		public static IEnumerable<AiderMailingCategoryEntity> GetParishCategories(BusinessContext context, string groupPath)
		{
			return AiderMailingCategoryEntity.GetMailingCategories (context, groupPath);
		}

		
		private void UpdateGroupPathCache()
		{
			this.GroupPathCache = this.Group.IsNull () ? "" : this.Group.Path;
		}

		private void UpdateDisplayName()
		{
			var groupName   = AiderMailingCategoryEntity.GetGroupName (this.Group);
			var groupSuffix = this.Name;

			if (string.IsNullOrWhiteSpace (groupSuffix))
			{
				this.DisplayName = groupName;
			}
			else
			{
				this.DisplayName = groupName + ", " + groupSuffix;
			}
		}

		
		private static IEnumerable<AiderMailingCategoryEntity> GetMailingCategories(BusinessContext context, string groupPath)
		{
			var dataContext = context.DataContext;

			var example = new AiderMailingCategoryEntity ();
			var request = Request.Create (example);

			request.AddCondition (dataContext, example, x => SqlMethods.Like (x.GroupPathCache, groupPath + "%"));

			var categories = dataContext.GetByRequest (request).ToList ();

			if ((groupPath.Length > 1) &&
				(categories.Any (x => x.HasNakedName) == false))
			{
				AiderMailingCategoryEntity.CreateDefaultNamedCategory (context, groupPath);
				categories = dataContext.GetByRequest (request).ToList ();
			}

			return categories;
		}

		private static void CreateDefaultNamedCategory(BusinessContext context, string groupPath)
		{
			using (var localContext = new BusinessContext (context.Data, false))
			{
				var group = AiderGroupEntity.FindGroups (localContext, groupPath).FirstOrDefault ();

				if (group.IsNotNull ())
				{
					AiderMailingCategoryEntity.Create (localContext, name: "", group: group);
					localContext.SaveChanges (LockingPolicy.ReleaseLock);
				}
			}
		}

		private static string GetGroupName(AiderGroupEntity group)
		{
			if ((group.IsNull ()) ||
				(group.GroupDef.IsNull ()))
			{
				return "";
			}

			if (group.GroupDef.IsParish ())
			{
				return group.Parent.Name + ", " + group.Name;
			}
			if (group.GroupDef.IsRegion ())
			{
				return group.Name;
			}

			return group.Name;
		}
	}
}
