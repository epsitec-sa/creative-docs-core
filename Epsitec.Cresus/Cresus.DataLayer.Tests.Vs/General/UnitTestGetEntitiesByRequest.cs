﻿using Epsitec.Common.Support;

using Epsitec.Common.Types;

using Epsitec.Cresus.Database;

using Epsitec.Cresus.DataLayer.Context;
using Epsitec.Cresus.DataLayer.Infrastructure;
using Epsitec.Cresus.DataLayer.Loader;
using Epsitec.Cresus.DataLayer.Expressions;
using Epsitec.Cresus.DataLayer.Schema;
using Epsitec.Cresus.DataLayer.Tests.Vs.Entities;
using Epsitec.Cresus.DataLayer.Tests.Vs.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;



namespace Epsitec.Cresus.DataLayer.Tests.Vs.General
{


	[TestClass]
	public sealed class UnitTestGetEntitiesByRequest
	{


		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
			TestHelper.Initialize ();

			DatabaseCreator2.ResetPopulatedTestDatabase ();

			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				for (int i = 0; i < 5; i++)
				{
					int? rank = (i % 2 == 0) ? (int?) null : i;

					DataContextHelper.CreateContactRole (dataContext, "role" + i, rank);
				}

				dataContext.SaveChanges ();
			}
		}


		[TestMethod]
		public void UnaryComparisonTest()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example = new NaturalPersonEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new UnaryComparison (
						new Field (new Druid ("[J1AM1]")),
						UnaryComparator.IsNotNull
					)
				);

				NaturalPersonEntity[] persons = dataContext.GetByRequest<NaturalPersonEntity> (request).ToArray ();

				Assert.IsTrue (persons.Count () == 3);
				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckAlfred (p)));
				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckGertrude (p)));
				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckHans (p)));
			}
		}


		[TestMethod]
		public void BinaryComparisonFieldWithValueTest()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example = new NaturalPersonEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AL1]")),
						BinaryComparator.IsEqual,
						new Constant ("Alfred")
					)
				);

				NaturalPersonEntity[] persons = dataContext.GetByRequest<NaturalPersonEntity> (request).ToArray ();

				Assert.IsTrue (persons.Count () == 1);
				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckAlfred (p)));
			}
		}


		[TestMethod]
		public void BinaryComparisonFieldWithFieldTest()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example = new NaturalPersonEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldField (
						new Field (new Druid ("[J1AL1]")),
						BinaryComparator.IsEqual,
						new Field (new Druid ("[J1AM1]"))
					)
				);

				NaturalPersonEntity[] persons = dataContext.GetByRequest<NaturalPersonEntity> (request).ToArray ();

				Assert.IsTrue (persons.Count () == 0);
			}
		}


		[TestMethod]
		public void UnaryOperationTest()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example = new NaturalPersonEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new UnaryOperation (
						UnaryOperator.Not,
						new ComparisonFieldValue (
							new Field (new Druid ("[J1AL1]")),
							BinaryComparator.IsEqual,
							new Constant ("Hans")
						)
					)
				);

				NaturalPersonEntity[] persons = dataContext.GetByRequest<NaturalPersonEntity> (request).ToArray ();

				Assert.IsTrue (persons.Count () == 2);
				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckAlfred (p)));
				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckGertrude (p)));
			}
		}


		[TestMethod]
		public void BinaryOperationTest()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example = new NaturalPersonEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new BinaryOperation (
						new ComparisonFieldValue (
							new Field (new Druid ("[J1AL1]")),
							BinaryComparator.IsNotEqual,
							new Constant ("Hans")
						),
						BinaryOperator.And,
						new ComparisonFieldValue (
							new Field (new Druid ("[J1AL1]")),
							BinaryComparator.IsNotEqual,
							new Constant ("Gertrude")
						)
					)
				);

				NaturalPersonEntity[] persons = dataContext.GetByRequest<NaturalPersonEntity> (request).ToArray ();

				Assert.IsTrue (persons.Count () == 1);
				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckAlfred (p)));
			}
		}


		[TestMethod]
		public void DoubleRequest1()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example = new NaturalPersonEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AL1]")),
						BinaryComparator.IsEqual,
						new Constant ("Alfred")
					)
				);

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AM1]")),
						BinaryComparator.IsEqual,
						new Constant ("Dupond")
					)
				);

				NaturalPersonEntity[] persons = dataContext.GetByRequest<NaturalPersonEntity> (request).ToArray ();

				Assert.IsTrue (persons.Count () == 1);
				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckAlfred (p)));
			}
		}

		[TestMethod]
		public void DoubleRequest2()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example = new NaturalPersonEntity ()
				{
					Gender = new PersonGenderEntity (),
				};

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AL1]")),
						BinaryComparator.IsEqual,
						new Constant ("Alfred")
					)
				);

				request.AddLocalConstraint (example.Gender,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AR]")),
						BinaryComparator.IsEqual,
						new Constant ("Male")
					)
				);

				NaturalPersonEntity[] persons = dataContext.GetByRequest<NaturalPersonEntity> (request).ToArray ();

				Assert.IsTrue (persons.Count () == 1);
				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckAlfred (p)));
			}
		}


		[TestMethod]
		public void InnerRequest()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example = new NaturalPersonEntity ()
				{
					Gender = new PersonGenderEntity (),
				};

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example.Gender,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AR]")),
						BinaryComparator.IsEqual,
						new Constant ("Male")
					)
				);

				NaturalPersonEntity[] persons = dataContext.GetByRequest<NaturalPersonEntity> (request).ToArray ();

				Assert.IsTrue (persons.Count () == 1);
				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckAlfred (p)));
			}
		}


		[TestMethod]
		public void LikeRequest()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example = new NaturalPersonEntity ()
				{
					Gender = new PersonGenderEntity (),
				};

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example.Gender,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AR]")),
						BinaryComparator.IsLike,
						new Constant ("%ale")
					)
				);

				NaturalPersonEntity[] persons = dataContext.GetByRequest<NaturalPersonEntity> (request).ToArray ();

				Assert.IsTrue (persons.Count () == 2);
				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckAlfred (p)));
				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckGertrude (p)));
			}
		}


		[TestMethod]
		public void LikeEscapeRequest()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			{
				using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
				{
					CountryEntity country1 = DataContextHelper.CreateCountry (dataContext, "c1", "test%test");
					CountryEntity country2 = DataContextHelper.CreateCountry (dataContext, "c2", "test_test");
					CountryEntity country3 = DataContextHelper.CreateCountry (dataContext, "c2", "test#test");
					CountryEntity country4 = DataContextHelper.CreateCountry (dataContext, "c3", "testxxtest");

					dataContext.SaveChanges ();
				}

				using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
				{
					CountryEntity example = new CountryEntity ();

					Request request = new Request ()
					{
						RootEntity = example,
						RequestedEntity = example,
					};

					request.AddLocalConstraint (example,
						new ComparisonFieldValue (
							new Field (new Druid ("[J1A5]")),
							BinaryComparator.IsLike,
							new Constant ("test%test")
						)
					);

					CountryEntity[] countries = dataContext.GetByRequest<CountryEntity> (request).ToArray ();

					Assert.IsTrue (countries.Count () == 4);
					Assert.IsTrue (countries.Any (c => c.Name == "test%test"));
					Assert.IsTrue (countries.Any (c => c.Name == "test_test"));
					Assert.IsTrue (countries.Any (c => c.Name == "test#test"));
					Assert.IsTrue (countries.Any (c => c.Name == "testxxtest"));
				}

				using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
				{
					CountryEntity example = new CountryEntity ();

					Request request = new Request ()
					{
						RootEntity = example,
						RequestedEntity = example,
					};

					request.AddLocalConstraint (example,
						new ComparisonFieldValue (
							new Field (new Druid ("[J1A5]")),
							BinaryComparator.IsLike,
							new Constant ("test_test")
						)
					);

					CountryEntity[] countries = dataContext.GetByRequest<CountryEntity> (request).ToArray ();

					Assert.IsTrue (countries.Count () == 3);
					Assert.IsTrue (countries.Any (c => c.Name == "test%test"));
					Assert.IsTrue (countries.Any (c => c.Name == "test_test"));
					Assert.IsTrue (countries.Any (c => c.Name == "test#test"));
				}

				using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
				{
					CountryEntity example = new CountryEntity ();

					Request request = new Request ()
					{
						RootEntity = example,
						RequestedEntity = example,
					};

					string value = Constant.Escape ("test%test");

					request.AddLocalConstraint (example,
						new ComparisonFieldValue (
							new Field (new Druid ("[J1A5]")),
							BinaryComparator.IsLikeEscape,
							new Constant (value)
						)
					);

					CountryEntity[] countries = dataContext.GetByRequest<CountryEntity> (request).ToArray ();

					Assert.IsTrue (countries.Count () == 1);
					Assert.IsTrue (countries.Any (c => c.Name == "test%test"));
				}

				using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
				{
					CountryEntity example = new CountryEntity ();

					Request request = new Request ()
					{
						RootEntity = example,
						RequestedEntity = example,
					};

					string value = Constant.Escape ("test_test");

					request.AddLocalConstraint (example,
						new ComparisonFieldValue (
							new Field (new Druid ("[J1A5]")),
							BinaryComparator.IsLikeEscape,
							new Constant (value)
						)
					);

					CountryEntity[] countries = dataContext.GetByRequest<CountryEntity> (request).ToArray ();

					Assert.IsTrue (countries.Count () == 1);
					Assert.IsTrue (countries.Any (c => c.Name == "test_test"));
				}

				using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
				{
					CountryEntity example = new CountryEntity ();

					Request request = new Request ()
					{
						RootEntity = example,
						RequestedEntity = example,
					};

					string value = Constant.Escape ("test#test");

					request.AddLocalConstraint (example,
						new ComparisonFieldValue (
							new Field (new Druid ("[J1A5]")),
							BinaryComparator.IsLikeEscape,
							new Constant (value)
						)
					);

					CountryEntity[] countries = dataContext.GetByRequest<CountryEntity> (request).ToArray ();

					Assert.IsTrue (countries.Count () == 1);
					Assert.IsTrue (countries.Any (c => c.Name == "test#test"));
				}
			}
		}


		[TestMethod]
		public void RequestedEntityRequest1()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example = DatabaseCreator2.GetCorrectExample3 ();

				Request request = new Request ()
				{
					RootEntity = example,
				};

				NaturalPersonEntity[] persons = dataContext.GetByRequest<NaturalPersonEntity> (request).ToArray ();

				Assert.IsTrue (persons.Length == 2);

				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckAlfred (p)));
				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckGertrude (p)));
			}
		}


		[TestMethod]
		public void RequestedEntityRequest2()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example = DatabaseCreator2.GetCorrectExample3 ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example.Contacts[0],
				};

				UriContactEntity[] contacts = dataContext.GetByRequest<UriContactEntity> (request).ToArray ();

				Assert.IsTrue (contacts.Length == 3);

				Assert.IsTrue (contacts.Any (c => DatabaseCreator2.CheckUriContact (c, "alfred@coucou.com", "Alfred")));
				Assert.IsTrue (contacts.Any (c => DatabaseCreator2.CheckUriContact (c, "alfred@blabla.com", "Alfred")));
				Assert.IsTrue (contacts.Any (c => DatabaseCreator2.CheckUriContact (c, "gertrude@coucou.com", "Gertrude")));
			}
		}


		[TestMethod]
		public void RequestedEntityRequest3()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example = DatabaseCreator2.GetCorrectExample3 ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = (example.Contacts[0] as UriContactEntity).UriScheme,
				};

				UriSchemeEntity[] uriSchemes = dataContext.GetByRequest<UriSchemeEntity> (request).ToArray ();

				Assert.IsTrue (uriSchemes.Length == 1);

				Assert.IsTrue (uriSchemes.Any (s => s.Code == "mailto:" && s.Name == "email"));
			}
		}


		[TestMethod]
		public void RootEntityReferenceRequest1()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example = DatabaseCreator2.GetCorrectExample1 ();
				NaturalPersonEntity alfred = dataContext.GetByExample<NaturalPersonEntity> (example).First ();

				Request request = new Request ()
				{
					RootEntity = new NaturalPersonEntity (),
					RootEntityKey = dataContext.GetNormalizedEntityKey (alfred).Value.RowKey,
				};

				NaturalPersonEntity[] persons = dataContext.GetByRequest<NaturalPersonEntity> (request).ToArray ();

				Assert.IsTrue (persons.Length == 1);

				Assert.IsTrue (persons.Any (p => DatabaseCreator2.CheckAlfred (p)));
			}
		}


		[TestMethod]
		public void RootEntityReferenceRequest2()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example1 = DatabaseCreator2.GetCorrectExample1 ();
				NaturalPersonEntity alfred1 = dataContext.GetByExample<NaturalPersonEntity> (example1).First ();

				NaturalPersonEntity example2 = new NaturalPersonEntity ();
				example2.Contacts.Add (new UriContactEntity ()
				{
					UriScheme = new UriSchemeEntity ()
				});

				Request request = new Request ()
				{
					RootEntity = example2,
					RootEntityKey = dataContext.GetNormalizedEntityKey (alfred1).Value.RowKey,
					RequestedEntity = (example2.Contacts[0] as UriContactEntity).UriScheme,
				};

				UriSchemeEntity[] uriSchemes = dataContext.GetByRequest<UriSchemeEntity> (request).ToArray ();

				Assert.IsTrue (uriSchemes.Length == 1);

				Assert.IsTrue (uriSchemes.Any (s => s.Code == "mailto:" && s.Name == "email"));
			}
		}


		[TestMethod]
		public void RootEntityReferenceRequest3()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				NaturalPersonEntity example1 = DatabaseCreator2.GetCorrectExample1 ();
				NaturalPersonEntity alfred1 = dataContext.GetByExample<NaturalPersonEntity> (example1).First ();

				NaturalPersonEntity example2 = new NaturalPersonEntity ();
				example2.Contacts.Add (new AbstractContactEntity ());

				Request request = new Request ()
				{
					RootEntity = example2,
					RootEntityKey = dataContext.GetNormalizedEntityKey (alfred1).Value.RowKey,
					RequestedEntity = example2.Contacts[0],
				};

				UriContactEntity[] contacts = dataContext.GetByRequest<UriContactEntity> (request).ToArray ();

				Assert.IsTrue (contacts.Length == 2);

				Assert.IsTrue (contacts.Any (c => DatabaseCreator2.CheckUriContact (c, "alfred@coucou.com", "Alfred")));
				Assert.IsTrue (contacts.Any (c => DatabaseCreator2.CheckUriContact (c, "alfred@blabla.com", "Alfred")));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnBooleanField1()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1A82]")),
						BinaryComparator.IsEqual,
						new Constant (true)
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData1 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnBooleanField2()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1A82]")),
						BinaryComparator.IsNotEqual,
						new Constant (false)
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData1 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnByteArrayField1()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1A92]")),
						BinaryComparator.IsEqual,
						new Constant (new byte[] { 0x0F, 0xF0 })
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 1);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData1 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnByteArrayField2()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1A92]")),
						BinaryComparator.IsNotEqual,
						new Constant (new byte[] { 0x0F, 0xF0 })
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnDateTimeField1()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AA2]")),
						BinaryComparator.IsEqual,
						new Constant (new System.DateTime (1969, 7, 21, 4, 17, 0))
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 1);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData1 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnDateTimeField2()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AA2]")),
						BinaryComparator.IsNotEqual,
						new Constant (new System.DateTime (1969, 7, 21, 4, 17, 0))
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnDateTimeField3()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AA2]")),
						BinaryComparator.IsGreater,
						new Constant (new System.DateTime (1969, 7, 21, 4, 17, 0))
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnDateField1()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AB2]")),
						BinaryComparator.IsEqual,
						new Constant (new Date (1291, 8, 1))
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 1);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData1 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnDateField2()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AB2]")),
						BinaryComparator.IsNotEqual,
						new Constant (new Date (1291, 8, 1))
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnDateField3()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AB2]")),
						BinaryComparator.IsGreater,
						new Constant (new Date (1291, 8, 1))
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnDecimalField1()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AC2]")),
						BinaryComparator.IsEqual,
						new Constant (123.456m)
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 1);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData1 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnDecimalField2()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AC2]")),
						BinaryComparator.IsNotEqual,
						new Constant (123.456m)
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnDecimalField3()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AC2]")),
						BinaryComparator.IsGreater,
						new Constant (123.456m)
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 1);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnEnumField1()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AL2]")),
						BinaryComparator.IsEqual,
						new Constant (SimpleEnum.Value2)
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 1);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnEnumField2()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AL2]")),
						BinaryComparator.IsNotEqual,
						new Constant (SimpleEnum.Value2)
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData1 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnIntegerField1()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AD2]")),
						BinaryComparator.IsEqual,
						new Constant (42)
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();
	
				Assert.IsTrue (valueData.Count () == 1);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData1 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnIntegerField2()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AD2]")),
						BinaryComparator.IsNotEqual,
						new Constant (42)
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnIntegerField3()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AD2]")),
						BinaryComparator.IsLower,
						new Constant (42)
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 1);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnLongIntegerField1()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AE2]")),
						BinaryComparator.IsEqual,
						new Constant (4242)
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 1);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData1 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnLongIntegerField2()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AE2]")),
						BinaryComparator.IsNotEqual,
						new Constant (4242)
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnLongIntegerField3()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AE2]")),
						BinaryComparator.IsLower,
						new Constant (4242)
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnStringField1()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AF2]")),
						BinaryComparator.IsEqual,
						new Constant ("blupi")
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 1);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData1 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnStringField2()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AF2]")),
						BinaryComparator.IsNotEqual,
						new Constant ("blupi")
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnStringField3()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AF2]")),
						BinaryComparator.IsLower,
						new Constant ("blupi")
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 1);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnTimeField1()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AG2]")),
						BinaryComparator.IsEqual,
						new Constant (new Time (12, 12, 12))
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 1);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData1 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnTimeField2()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AG2]")),
						BinaryComparator.IsNotEqual,
						new Constant (new Time (12, 12, 12))
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


		[TestMethod]
		public void GetObjectBasedOnTimeField3()
		{
			
			using (DataInfrastructure dataInfrastructure = DataInfrastructureHelper.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (dataInfrastructure))
			{
				ValueDataEntity example = new ValueDataEntity ();

				Request request = new Request ()
				{
					RootEntity = example,
					RequestedEntity = example,
				};

				request.AddLocalConstraint (example,
					new ComparisonFieldValue (
						new Field (new Druid ("[J1AG2]")),
						BinaryComparator.IsLower,
						new Constant (new Time (12, 12, 12))
					)
				);

				var valueData = dataContext.GetByRequest<ValueDataEntity> (request).ToList ();

				Assert.IsTrue (valueData.Count () == 2);

				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData2 (vd)));
				Assert.IsTrue (valueData.Any (vd => DatabaseCreator2.CheckValueData3 (vd)));
			}
		}


	}


}
