//	Copyright � 2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using System.Collections.Generic;

namespace Epsitec.Common.Types
{
	/// <summary>
	/// The Collection class provides a few useful methods to operate on
	/// enumerable collections.
	/// </summary>
	public class Collection
	{
		public static int Count<T>(IEnumerable<T> collection)
		{
			int n = 0;

			foreach (T item in collection)
			{
				n++;
			}

			return n;
		}

		public static T Extract<T>(IEnumerable<T> collection, int index)
		{
			int n = 0;

			foreach (T item in collection)
			{
				if (index == n)
				{
					return item;
				}
				
				n++;
			}

			return default (T);
		}

		public static List<T> ToList<T>(IEnumerable<T> collection)
		{
			List<T> list = new List<T> ();
			
			if (collection != null)
			{
				list.AddRange (collection);
			}
			
			return list;
		}

		public static T[] ToArray<T>(IList<T> list)
		{
			T[] array = new T[list.Count];
			list.CopyTo (array, 0);
			return array;
		}

		public static T[] ToArray<T>(IEnumerable<T> collection)
		{
			List<T> list = new List<T> ();

			if (collection != null)
			{
				list.AddRange (collection);
			}

			return list.ToArray ();
		}

		public static T[] ToArray<T>(System.Collections.IEnumerable collection)
		{
			List<T> list = new List<T> ();

			if (collection != null)
			{
				foreach (T item in collection)
				{
					list.Add (item);
				}
			}

			return list.ToArray ();
		}

		public static object[] ToObjectArray<T>(IEnumerable<T> collection)
		{
			List<T> list = new List<T> ();

			if (collection != null)
			{
				list.AddRange (collection);
			}

			object[] items = new object[list.Count];

			for (int i = 0; i < list.Count; i++)
			{
				items[i] = list[i];
			}

			return items;
		}

		public static T[] ToSortedArray<T>(IEnumerable<T> collection)
		{
			List<T> list = new List<T> ();

			if (collection != null)
			{
				list.AddRange (collection);
			}

			T[] array = list.ToArray ();
			System.Array.Sort (array);
			return array;
		}
		
		public static bool ContainsAll<T>(IEnumerable<T> collection, ICollection<T> values)
		{
			List<T> ignore = new List<T> ();
			
			foreach (T item in collection)
			{
				if (ignore.Contains (item))
				{
					continue;
				}
				if (values.Contains (item))
				{
					ignore.Add (item);
				}
			}

			return ignore.Count == values.Count;
		}

		public static IEnumerable<T> Filter<T>(IEnumerable<T> collection, System.Predicate<T> predicate)
		{
			foreach (T item in collection)
			{
				if (predicate.Invoke (item))
				{
					yield return item;
				}
			}
		}

		public static IEnumerable<object> EnumerateObjects(System.Collections.IEnumerable collection)
		{
			foreach (object item in collection)
			{
				yield return item;
			}
		}

		public static IEnumerable<T> Reverse<T>(IEnumerable<T> collection)
		{
			List<T> list = new List<T> (collection);
			list.Reverse ();
			return list;
		}
	}
}
