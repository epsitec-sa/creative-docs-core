﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD


using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Entities;

using Epsitec.Cresus.DataLayer;
using Epsitec.Cresus.DataLayer.Loader;
using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;


namespace Epsitec.Cresus.Core.Data
{
	public class CountryRepository : Repository<CountryEntity>
	{
		public CountryRepository(DataContext dataContext) : base (dataContext)
		{	
		}


		public IEnumerable<CountryEntity> GetCountriesByExample(CountryEntity example)
		{
			return this.GetEntitiesByExample<CountryEntity> (example);
		}

		public IEnumerable<CountryEntity> GetCountriesByRequest(Request request)
		{
			return this.GetEntitiesByRequest<CountryEntity> (request);
		}


		public IEnumerable<CountryEntity> GetCountriesByExample(CountryEntity example, int index, int count)
		{
			return this.GetEntitiesByExample<CountryEntity> (example, index, count);
		}


		public IEnumerable<CountryEntity> GetCountriesByRequest(Request request, int index, int count)
		{
			return this.GetEntitiesByRequest<CountryEntity> (request, index, count);
		}
		

		public IEnumerable<CountryEntity> GetAllCountries()
		{
			CountryEntity example = this.CreateCountryExample ();

			return this.GetCountriesByExample (example);
		}


		public IEnumerable<CountryEntity> GetAllCountries(int index, int count)
		{
			CountryEntity example = this.CreateCountryExample ();

			return this.GetCountriesByExample (example, index, count);
		}

		
		public IEnumerable<CountryEntity> GetCountriesByCode(string code)
		{
			CountryEntity example = this.CreateCountryExampleByCode (code);

			return this.GetCountriesByExample (example);
		}


		public IEnumerable<CountryEntity> GetCountriesByCode(string code, int index, int count)
		{
			CountryEntity example = this.CreateCountryExampleByCode (code);

			return this.GetCountriesByExample (example, index, count);
		}


		public IEnumerable<CountryEntity> GetCountriesByName(FormattedText name)
		{
			CountryEntity example = this.CreateCountryExampleByName (name);

			return this.GetCountriesByExample (example);
		}


		public IEnumerable<CountryEntity> GetCountriesByName(FormattedText name, int index, int count)
		{
			CountryEntity example = this.CreateCountryExampleByName (name);

			return this.GetCountriesByExample (example, index, count);
		}


		public CountryEntity CreateCountryExample()
		{
			return this.CreateExample<CountryEntity> ();
		}


		private CountryEntity CreateCountryExampleByCode(string code)
		{
			CountryEntity example = this.CreateCountryExample ();
			example.Code = code;

			return example;
		}


		private CountryEntity CreateCountryExampleByName(FormattedText name)
		{
			CountryEntity example = this.CreateCountryExample ();
			example.Name = name;

			return example;
		}


	}


}
