﻿using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Entities;

using Epsitec.Cresus.DataLayer;
using Epsitec.Cresus.DataLayer.Loader;
using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;


namespace Epsitec.Cresus.Core.Data
{


	public class StreetRepository : Repository
	{

		public StreetRepository(DataContext dataContext) : base (dataContext)
		{
		}


		public IEnumerable<StreetEntity> GetStreetsByExample(StreetEntity example)
		{
			return this.GetEntitiesByExample<StreetEntity> (example);
		}


		public IEnumerable<StreetEntity> GetStreetsByRequest(Request request)
		{
			return this.GetEntitiesByRequest<StreetEntity> (request);
		}


		public IEnumerable<StreetEntity> GetStreetsByExample(StreetEntity example, int index, int count)
		{
			return this.GetEntitiesByExample<StreetEntity> (example, index, count);
		}


		public IEnumerable<StreetEntity> GetStreetsByRequest(Request request, int index, int count)
		{
			return this.GetEntitiesByRequest<StreetEntity> (request, index, count);
		}


		public IEnumerable<StreetEntity> GetAllStreets()
		{
			StreetEntity example = this.CreateStreetExample ();

			return this.GetStreetsByExample (example);
		}


		public IEnumerable<StreetEntity> GetAllStreets(int index, int count)
		{
			StreetEntity example = this.CreateStreetExample ();

			return this.GetStreetsByExample (example, index, count);
		}


		public IEnumerable<StreetEntity> GetStreetsByName(FormattedText streetName)
		{
			StreetEntity example = this.CreateStreetExampleByName (streetName);

			return this.GetStreetsByExample (example);
		}


		public IEnumerable<StreetEntity> GetStreetsByName(FormattedText streetName, int index, int count)
		{
			StreetEntity example = this.CreateStreetExampleByName (streetName);

			return this.GetStreetsByExample (example, index, count);
		}


		public IEnumerable<StreetEntity> GetStreetsByComplement(FormattedText complement)
		{
			StreetEntity example = this.CreateStreetExampleByComplement (complement);

			return this.GetStreetsByExample (example);
		}


		public IEnumerable<StreetEntity> GetStreetsByComplement(FormattedText complement, int index, int count)
		{
			StreetEntity example = this.CreateStreetExampleByComplement (complement);

			return this.GetStreetsByExample (example, index, count);
		}


		public StreetEntity CreateStreetExample()
		{
			return this.CreateExample<StreetEntity> ();
		}


		private StreetEntity CreateStreetExampleByName(FormattedText streetName)
		{
			StreetEntity example = this.CreateStreetExample ();
			example.StreetName = streetName;

			return example;
		}


		private StreetEntity CreateStreetExampleByComplement(FormattedText complement)
		{
			StreetEntity example = this.CreateStreetExample ();
			example.Complement = complement;

			return example;
		}


	}


}
