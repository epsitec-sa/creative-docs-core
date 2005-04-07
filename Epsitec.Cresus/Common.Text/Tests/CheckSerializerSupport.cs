//	Copyright � 2005, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Text.Tests
{
	/// <summary>
	/// Summary description for CheckSerializerSupport.
	/// </summary>
	public sealed class CheckSerializerSupport
	{
		public static void RunTests()
		{
			Debug.Assert.IsTrue (@"[null]"     == SerializerSupport.SerializeString (null));
			Debug.Assert.IsTrue (@"\[\]\:x\\y" == SerializerSupport.SerializeString (@"[]/x\y"));
			
			Debug.Assert.IsTrue (@"[NaN]" == SerializerSupport.SerializeDouble (System.Double.NaN));
			Debug.Assert.IsTrue (@"10"    == SerializerSupport.SerializeDouble (10));
			
			Debug.Assert.IsTrue (@"[true]"  == SerializerSupport.SerializeBoolean (true));
			Debug.Assert.IsTrue (@"[false]" == SerializerSupport.SerializeBoolean (false));
			
			Debug.Assert.IsTrue (@"a/b/c" == SerializerSupport.Join ("a", "b", "c"));
			
			string[] abc = SerializerSupport.Split (@"a/b/c");
			
			Debug.Assert.IsTrue (abc.Length == 3);
			Debug.Assert.IsTrue (abc[0] == "a");
			Debug.Assert.IsTrue (abc[1] == "b");
			Debug.Assert.IsTrue (abc[2] == "c");
			
			Debug.Assert.IsTrue (double.IsNaN (SerializerSupport.DeserializeDouble ("[NaN]")));
			Debug.Assert.IsTrue (10 == SerializerSupport.DeserializeDouble ("10"));
			
			Debug.Assert.IsTrue (SerializerSupport.DeserializeBoolean ("[true]"));
			Debug.Assert.IsFalse (SerializerSupport.DeserializeBoolean ("[false]"));
		}
	}
}
