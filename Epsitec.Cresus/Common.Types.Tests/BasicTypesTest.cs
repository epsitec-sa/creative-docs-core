using NUnit.Framework;

namespace Epsitec.Common.Types
{
	[TestFixture] public class BasicTypesTest
	{
		[Test] public void CheckEnumType1()
		{
			System.Type type = typeof (MyEnum);
			
			EnumType et = new EnumType (type);
			
			Assert.AreEqual (5, et.Values.Length);
			Assert.IsFalse (et.IsCustomizable);
			Assert.IsFalse (et.IsFlags);
			
			Assert.AreEqual ("None",   et.Values[0].Name);
			Assert.AreEqual ("First",  et.Values[1].Name);
			Assert.AreEqual ("Second", et.Values[2].Name);
			Assert.AreEqual ("Third",  et.Values[3].Name);
			Assert.AreEqual ("Extra",  et.Values[4].Name);
			
			Assert.IsFalse (et.Values[0].IsHidden);
			Assert.IsTrue  (et.Values[4].IsHidden);
			
			Assert.AreEqual (-1, et["None"]  .Rank);
			Assert.AreEqual ( 1, et["First"] .Rank);
			Assert.AreEqual ( 2, et["Second"].Rank);
			Assert.AreEqual ( 3, et["Third"] .Rank);
			Assert.AreEqual (99, et["Extra"] .Rank);
			
			Assert.AreEqual ("None"  , et[-1].Name);
			Assert.AreEqual ("First" , et[ 1].Name);
			Assert.AreEqual ("Second", et[ 2].Name);
			Assert.AreEqual ("Third" , et[ 3].Name);
			Assert.AreEqual ("Extra" , et[99].Name);
			
			Assert.IsTrue (et.CheckConstraint ("3"));
			Assert.IsTrue (et.CheckConstraint (-1));
			Assert.IsTrue (et.CheckConstraint ("Extra"));
			Assert.IsFalse (et.CheckConstraint (18));
			Assert.IsFalse (et.CheckConstraint ("{Other}"));
		}
		
		[Test] public void CheckEnumType2()
		{
			System.Type type = typeof (MyFlags);
			
			EnumType et = new EnumType (type);
			
			Assert.AreEqual (5, et.Values.Length);
			Assert.IsFalse (et.IsCustomizable);
			Assert.IsTrue (et.IsFlags);
			
			Assert.AreEqual ("None",   et.Values[0].Name);
			Assert.AreEqual ("Flag1",  et.Values[1].Name);
			Assert.AreEqual ("Flag2", et.Values[2].Name);
			Assert.AreEqual ("Flag3",  et.Values[3].Name);
			Assert.AreEqual ("Flag4",  et.Values[4].Name);
			
			Assert.AreEqual (0, et["None"] .Rank);
			Assert.AreEqual (1, et["Flag1"].Rank);
			Assert.AreEqual (2, et["Flag2"].Rank);
			Assert.AreEqual (4, et["Flag3"].Rank);
			Assert.AreEqual (8, et["Flag4"].Rank);
			
			Assert.AreEqual ("None",  et[0].Name);
			Assert.AreEqual ("Flag1", et[1].Name);
			Assert.AreEqual ("Flag2", et[2].Name);
			Assert.AreEqual ("Flag3", et[4].Name);
			Assert.AreEqual ("Flag4", et[8].Name);
			
			Assert.IsTrue (et.CheckConstraint ("0"));
			Assert.IsTrue (et.CheckConstraint (0xf));
			Assert.IsTrue (et.CheckConstraint ("Flag1, Flag2"));
			Assert.IsFalse (et.CheckConstraint (0x18));
			Assert.IsFalse (et.CheckConstraint ("{Other}"));
		}
		
		[Test] public void CheckEnumType3()
		{
			System.Type type = typeof (MyEnum);
			
			EnumType et = new EnumType (type);
			
			et.DefineCaptionsFromResources ("Enums#A");
			
			Assert.AreEqual ("[res:Enums#A]", et.Caption);
			
			Assert.AreEqual ("[res:Enums#A.None]",   et["None"].Caption);
			Assert.AreEqual ("[res:Enums#A.First]",  et["First"].Caption);
			Assert.AreEqual ("[res:Enums#A.Second]", et["Second"].Caption);
			Assert.AreEqual ("[res:Enums#A.Third]",  et["Third"].Caption);
			Assert.AreEqual ("[res:Enums#A.Extra]",  et["Extra"].Caption);
		}
		
		[Test] public void CheckOpenEnumType()
		{
			EnumType et = new OpenEnumType (typeof (MyEnum));
			
			Assert.AreEqual (5, et.Values.Length);
			Assert.IsTrue (et.IsCustomizable);
			
			Assert.AreEqual ("None",   et.Values[0].Name);
			Assert.AreEqual ("First",  et.Values[1].Name);
			Assert.AreEqual ("Second", et.Values[2].Name);
			Assert.AreEqual ("Third",  et.Values[3].Name);
			Assert.AreEqual ("Extra",  et.Values[4].Name);
			
			Assert.AreEqual (-1, et["None"]  .Rank);
			Assert.AreEqual ( 1, et["First"] .Rank);
			Assert.AreEqual ( 2, et["Second"].Rank);
			Assert.AreEqual ( 3, et["Third"] .Rank);
			Assert.AreEqual (99, et["Extra"] .Rank);
			
			Assert.AreEqual ("None"  , et[-1].Name);
			Assert.AreEqual ("First" , et[ 1].Name);
			Assert.AreEqual ("Second", et[ 2].Name);
			Assert.AreEqual ("Third" , et[ 3].Name);
			Assert.AreEqual ("Extra" , et[99].Name);
			
			Assert.IsTrue (et.CheckConstraint ("3"));
			Assert.IsTrue (et.CheckConstraint (-1));
			Assert.IsTrue (et.CheckConstraint ("Extra"));
			Assert.IsFalse (et.CheckConstraint (18));
			Assert.IsTrue (et.CheckConstraint ("{Other}"));
		}
		
		private enum MyEnum
		{
			None	= -1,
			First	=  1,
			Second	=  2,
			Third	=  3,
			
			[Hide] Extra	= 99
		}
		
		[System.Flags]
		private enum MyFlags
		{
			None	= 0,
			Flag1	= 1,
			Flag2	= 2,
			Flag3	= 4,
			Flag4	= 8
		}
	}
}
