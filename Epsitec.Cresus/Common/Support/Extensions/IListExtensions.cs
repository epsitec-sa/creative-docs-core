//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Common.Support.Extensions
{
	public static class IListExtensions
	{
		public static void AddRange<T>(this IList<T> list, IEnumerable<T> collection)
		{
			foreach (T item in collection)
			{
				list.Add (item);
			}
		}

		/// <summary>
		/// Gets a random element out of <paramref name="list"/>.
		/// </summary>
		/// <typeparam name="T">The type of the elements within <paramref name="list"/></typeparam>
		/// <param name="list">The <see cref="List{T}"/> of which to get a random element.</param>
		/// <returns>A randomly chosen element of <paramref name="list"/>.</returns>
		/// <exception cref="System.ArgumentNullException">If <paramref name="list"/> is <c>null</c>.</exception>
		/// <exception cref="System.ArgumentException">If <paramref name="list"/> is empty.</exception>
		public static T GetRandomElement<T>(this IList<T> list)
		{
			list.ThrowIfNull ("sequence");
			list.ThrowIf (l => l.Count == 0, "list is empty");

			int index = IListExtensions.Dice.Next (0, list.Count);

			return list[index];
		}

		private static System.Random Dice
		{
			get
			{
				if (IListExtensions.dice == null)
				{
					IListExtensions.dice = new System.Random ();
				}

				return IListExtensions.dice;
			}
		}

		[System.ThreadStatic]
		private static System.Random dice;
	}
}
