﻿using Epsitec.Common.Support.Extensions;

using Epsitec.Cresus.Core.Business.Finance.PriceCalculators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using System.Linq;


namespace Epsitec.Cresus.Core.UnitTests.Business.Finance.PriceCalculators
{


	[TestClass]
	public class UnitTestArrayEqualityComparer
	{


		[TestMethod]
		public void TestEquals()
		{
			List<string[]> arrays = new List<string[]> ();

			System.Random dice = new System.Random ();

			for (int i = 1; i < 10; i++)
			{
				for (int j = 0; j < 100; j++)
				{
					arrays.Add (Enumerable.Range (0, i).Select (v => dice.NextDouble ().ToString ()).Shuffle ().ToArray ());
				}
			}

			ArrayEqualityComparer pcec = new ArrayEqualityComparer ();

			foreach (string[] array1 in arrays)
			{
				foreach (string[]array2 in arrays)
				{
					// Actually, this test is too restrictive, since it could happen that two random
					// arrays could have the same value, even if the probability is low. We assume in
					// this test that we are lucky.
					// Marc

					Assert.AreEqual ((array1 == array2), pcec.Equals (array1, array2));
				}
			}

			Assert.IsFalse (pcec.Equals (arrays.First (), null));
			Assert.IsFalse (pcec.Equals (null, arrays.First ()));
			Assert.IsFalse (pcec.Equals (null, null));
		}


		[TestMethod]
		public void TestHashCode()
		{
			List<string[]> arrays = new List<string[]> ();

			System.Random dice = new System.Random ();

			for (int i = 1; i < 100; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					arrays.Add (Enumerable.Range (0, i).Select (v => dice.NextDouble ().ToString ()).ToArray ());
				}
			}

			ArrayEqualityComparer pcec = new ArrayEqualityComparer ();

			foreach (string[] array1 in arrays)
			{
				foreach (string[]array2 in arrays)
				{
					// Actually, this test is too restrictive, as two different arrays could have
					// the same hash code. We assume in this test that we are lucky.
					// Marc

					Assert.AreEqual (pcec.Equals (array1, array2), (pcec.GetHashCode (array1) == pcec.GetHashCode (array2)));
				}
			}

			Assert.AreEqual (0, pcec.GetHashCode (null));
		}


	}


}
