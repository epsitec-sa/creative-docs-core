//	Copyright � 2005, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Text.Internal
{
	/// <summary>
	/// La structure Cursor d�crit une marque qui suit le texte et
	/// qui peut �tre utilis�e pour naviguer � travers des instances
	/// de la class TextChunk.
	/// </summary>
	internal struct Cursor
	{
		public Cursor(Internal.Cursor cursor)
		{
			this.chunk_id   = cursor.chunk_id;
			this.cached_pos = cursor.cached_pos;
			
			this.free_link  = cursor.free_link;
			
			//	Indique explicitement que ceci est une copie :
			
			this.cursor_state = Internal.CursorState.Copied;
		}
		
		
		public static Internal.Cursor		Empty = new Internal.Cursor ();
		
		public Internal.TextChunkId			TextChunkId
		{
			get
			{
				return this.chunk_id;
			}
			set
			{
				this.chunk_id = value;
			}
		}
		
		public int							CachedPosition
		{
			get
			{
				return this.cached_pos-1;
			}
			set
			{
				this.cached_pos = value+1;
			}
		}
		
		public Internal.CursorId			FreeListLink
		{
			get
			{
				return this.free_link;
			}
			set
			{
				this.free_link = value;
			}
		}
		
		public Internal.CursorState			CursorState
		{
			get
			{
				return this.cursor_state;
			}
		}
		
		
		public override bool Equals(object obj)
		{
			if (obj is Cursor)
			{
				Cursor that = (Cursor) obj;
				return this == that;
			}
			
			return false;
		}
		
		public override int GetHashCode()
		{
			return this.free_link
				 ^ this.chunk_id;
		}

		
		internal void DefineCursorState(Internal.CursorState state)
		{
			this.cursor_state = state;
		}
		
		
		public static bool operator ==(Cursor a, Cursor b)
		{
			return (a.free_link == b.free_link)
				&& (a.chunk_id == b.chunk_id);
		}
		
		public static bool operator !=(Cursor a, Cursor b)
		{
			return (a.free_link != b.free_link)
				|| (a.chunk_id != b.chunk_id);
		}
		
		
		//
		//	ATTENTION:
		//
		//	CursorTable manipule ces champs manuellement; si des nouveaux champs
		//	sont rajout�s ici, il faut mettre � jour la m�thode dans les m�thodes
		//	suivantes :
		//
		//	- CursorTable.WriteCursor
		//	- CursorTable.RecycleCursor
		//
		
		private Internal.TextChunkId		chunk_id;
		private int							cached_pos;
		
		private Internal.CursorId			free_link;
		private Internal.CursorState		cursor_state;
	}
}
