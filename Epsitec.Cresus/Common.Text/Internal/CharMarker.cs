//	Copyright � 2005, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Text.Internal
{
	/// <summary>
	/// La classe CharMarker g�re l'attribution des marqueurs temporaires
	/// associ�s au texte; ces marqueurs sont stock�s comme des fanions aux
	/// bits 24..31 de chaque caract�re.
	/// </summary>
	internal sealed class CharMarker : System.Collections.IEnumerable
	{
		public CharMarker()
		{
			this.keys = new object[6];
			this.bits = new ulong[6];
			
			ulong bit = 0x1 << CharMarker.MarkerShift;
			
			for (int i = 0; i < 6; i++)
			{
				this.keys[i] = null;
				this.bits[i] = bit << i;
			}
		}
		
		
		/*
		 *	Structure d'un caract�re en m�moire :
		 *
		 *	[bbbb bbb][c:cccc cc][dd:dddd dddd:dddd dddd]:[mmmm mm][ss:s][xx][y yyyy:yyyy yyyy:yyyy yyyy]
		 *
		 *	- 63..57 : b,  7-bit, "extra index"			1..100
		 *	- 56..50 : c,  7-bit, "local index"			1..100
		 *	- 49..32 : d, 18-bit, "style index"			1..250'000
		 *	- 31..26 : m,  6-bit, "markers"
		 *  - 25..23 : s,  3-bit, "line break status"
		 *  - 22..21 : x,  2-bit, "unicode flags"
		 *	- 20...0 : y, 21-bit, "unicode code"
		 *
		 *	- Le "extra index" pointe (au sein du style) sur un descripteur qui
		 *	  d�finit des propri�t�s non typographiques (couleur, langue, etc.)
		 *
		 *	- Le "local index" pointe (au sein du style) sur un descripteur qui
		 *	  d�finit des ajustement locaux (cr�nage, variante de glyphe, etc.)
		 *
		 *	- Le "style index" pointe soit sur un descripteur qui d�finit toues
		 *	  les propri�t�s principales (fonte, style, paragraphe, etc.)
		 *
		 *	- Les "markers" sont des bits de marquage temporaire (g�r�s par la
		 *	  class CharMarker).
		 *
		 *	- Les "unicode flags" et "unicode code" forment le caract�re Unicode
		 *	  � proprement dit.
		 */
		
		public const ulong						MarkerMask  = 0x00000000FC000000ul;
		public const int						MarkerShift = 26;
		
		public ulong							this[object key]
		{
			get
			{
				if (key != null)
				{
					for (int i = 0; i < this.keys.Length; i++)
					{
						if (this.keys[i] == key)
						{
							return this.bits[i];
						}
					}
				}
				
				throw new System.ArgumentException ("Invalid key.");
			}
		}
		
		
		public bool Add(object key)
		{
			if (key != null)
			{
				int free = -1;
				
				for (int i = 0; i < this.keys.Length; i++)
				{
					if (this.keys[i] == key)
					{
						throw new System.ArgumentException ("Same key specified twice.");
					}
					if ((this.keys[i] == null) &&
						(free < 0))
					{
						free = i;
					}
				}
				
				if (free != -1)
				{
					this.keys[free] = key;
					return true;
				}
			}
			
			return false;
		}
		
		public void Remove(object key)
		{
			for (int i = 0; i < this.keys.Length; i++)
			{
				if (this.keys[i] == key)
				{
					this.keys[i] = null;
					return;
				}
			}
			
			throw new System.ArgumentException ("Invalid key.");
		}
		
		
		public static ulong GetMarker(ulong code)
		{
			return code & CharMarker.MarkerMask;
		}
		
		public static bool SetMarkers(ulong marker, ulong[] text, int offset, int length)
		{
			//	Place le marqueur sp�cifi� sur le fragment de texte et retourne
			//	'true' si des modifications ont �t� apport�es au texte.
			
			Debug.Assert.IsTrue (marker != 0);
			Debug.Assert.IsTrue ((marker & ~CharMarker.MarkerMask) == 0);
			
			ulong acc = marker;
			int   end = offset + length;
			
			for (int i = offset; i < end; i++)
			{
				acc     &= text[i];
				text[i] |= marker;
			}
			
			return ((acc & marker) == 0) ? true : false;
		}
		
		public static bool ClearMarkers(ulong marker, ulong[] text, int offset, int length)
		{
			//	Efface le marqueur sp�cifi� sur le fragment de texte et retourne
			//	'true' si des modifications ont �t� apport�es au texte.
			
			Debug.Assert.IsTrue (marker != 0);
			Debug.Assert.IsTrue ((marker & ~CharMarker.MarkerMask) == 0);
			
			ulong mask = ~marker;
			ulong acc  = 0;
			int   end  = offset + length;
			
			for (int i = offset; i < end; i++)
			{
				acc     |= text[i];
				text[i] &= mask;
			}
			
			return ((acc & marker) != 0) ? true : false;
		}
		
		public static bool ClearAllMarkers(ulong[] text, int offset, int length)
		{
			//	Efface tous les marqueurs sur le fragment de texte et retourne
			//	'true' si des modifications ont �t� apport�es au texte.
			
			ulong all  = CharMarker.MarkerMask;
			ulong mask = ~all;
			ulong acc  = 0;
			int   end  = offset + length;
			
			for (int i = offset; i < end; i++)
			{
				acc     |= text[i];
				text[i] &= mask;
			}
			
			return ((acc & all) != 0) ? true : false;
		}
		
		
		public static ulong Accumulate(ulong[] text, int offset, int length)
		{
			//	Accumule (avec un "ou" logique) les marqueurs du texte pass�
			//	en entr�e.
			
			int   end = offset + length;
			ulong acc = 0;
			
			for (int i = offset; i < end; i++)
			{
				acc |= text[i] & CharMarker.MarkerMask;
			}
			
			return acc;
		}
		
		
		public static int GetStyleIndex(ulong code)
		{
			return (int)((code >> 32) & 0x000000000003FFFFul);
		}
		
		public static int GetLocalIndex(ulong code)
		{
			return (int)((code >> 50) & 0x000000000000003Ful);
		}
		
		public static int GetExtraIndex(ulong code)
		{
			return (int)((code >> 57) & 0x000000000000003Ful);
		}
		
		
		public static void SetStyleIndex(ref ulong code, int value)
		{
			code &= 0xFFFC0000FFFFFFFFul;
			code |= (ulong) (value & 0x0003FFFF) << 32;
		}
		
		public static void SetLocalIndex(ref ulong code, int value)
		{
			code &= 0xFE03FFFFFFFFFFFFul;
			code |= (ulong) (value & 0x0000007F) << 50;
		}
		
		public static void SetExtraIndex(ref ulong code, int value)
		{
			code &= 0x01FFFFFFFFFFFFFFul;
			code |= (ulong) (value & 0x0000007F) << 57;
		}
		
		
		public static bool HasStyleOrSettings(ulong code)
		{
			return ((code & 0xFFFFFFFF00000000ul) == 0) ? false : true;
		}
		
		public static bool HasSettings(ulong code)
		{
			return ((code & 0xFFFC000000000000ul) == 0) ? false : true;
		}
		
		
		
		#region IEnumerable Members
		public System.Collections.IEnumerator GetEnumerator()
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList ();
			
			for (int i = 0; i < this.keys.Length; i++)
			{
				if (this.keys[i] != null)
				{
					list.Add (this.keys[i]);
				}
			}
			
			return list.GetEnumerator ();
		}
		#endregion
		
		private object[]						keys;
		private ulong[]							bits;
	}
}
