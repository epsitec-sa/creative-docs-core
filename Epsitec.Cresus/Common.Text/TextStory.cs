//	Copyright � 2005-2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Text
{
	using EventHandler      = Epsitec.Common.Support.EventHandler;
	using OpletEventHandler = Epsitec.Common.Support.OpletEventHandler;
	using OpletEventArgs	= Epsitec.Common.Support.OpletEventArgs;
	
	/// <summary>
	/// La classe TextStory repr�sente un texte complet, avec tous ses attributs
	/// typographiques, ses curseurs, sa gestion du undo, etc.
	/// </summary>
	public class TextStory : System.IDisposable
	{
		public TextStory()
		{
			this.SetupTextStory ();
			this.SetupOpletQueue (new Common.Support.OpletQueue ());
			this.SetupContext (new TextContext ());
		}
		
		public TextStory(Common.Support.OpletQueue oplet_queue)
		{
			this.SetupTextStory ();
			this.SetupOpletQueue (oplet_queue);
			this.SetupContext (new TextContext ());
		}
		
		public TextStory(TextContext context)
		{
			this.SetupTextStory ();
			this.SetupOpletQueue (new Common.Support.OpletQueue ());
			this.SetupContext (context);
		}
		
		public TextStory(Common.Support.OpletQueue oplet_queue, TextContext context)
		{
			this.SetupTextStory ();
			this.SetupOpletQueue (oplet_queue);
			this.SetupContext (context);
		}
		
		
		public int								TextLength
		{
			get
			{
				return this.text_length;
			}
		}
		
		public int								UndoLength
		{
			get
			{
				return this.undo_length;
			}
		}
		
		public Common.Support.OpletQueue		OpletQueue
		{
			get
			{
				return this.oplet_queue;
			}
		}
		
		public StyleList						StyleList
		{
			get
			{
				return this.context.StyleList;
			}
		}
		
		public Text.TextContext					TextContext
		{
			get
			{
				return this.context;
			}
		}
		
		public long								Version
		{
			get
			{
				return this.text.Version;
			}
		}
		
		
		public int								TextChangeMarkStart
		{
			//	Les marques de changement qui sont activ�es dans le texte � la moindre
			//	modification sont toujours situ�es dans une plage comprise entre 'start'
			//	et 'end'. La version permet de savoir quand les marques ont chang� pour
			//	la derni�re fois.
			
			get
			{
				return System.Math.Min (this.text_change_mark_start, this.TextLength);
			}
		}
		
		public int								TextChangeMarkEnd
		{
			get
			{
				return System.Math.Min (this.text_change_mark_end, this.TextLength);
			}
		}
		
		public bool								HasTextChangeMarks
		{
			get
			{
				return this.TextChangeMarkStart < this.TextChangeMarkEnd;
			}
		}
		
		
		internal Internal.TextTable				TextTable
		{
			get
			{
				return this.text;
			}
		}
		
		public bool								DebugDisableOpletQueue
		{
			get
			{
				return this.debug_disable_oplet;
			}
			set
			{
				this.debug_disable_oplet = value;
			}
		}
		
		public bool								DebugDisableOpletMerge
		{
			get
			{
				return this.debug_disable_merge;
			}
			set
			{
				this.debug_disable_merge = value;
			}
		}
		
		public bool								IsOpletQueueEnabled
		{
			get
			{
				return (this.DebugDisableOpletQueue || (this.oplet_queue == null)) ? false : (this.oplet_queue_disable == 0);
			}
		}
		
		
		public void NewCursor(ICursor cursor)
		{
			this.text.NewCursor (cursor);
			
			if (cursor.Attachment != CursorAttachment.Temporary)
			{
				this.InternalAddOplet (new CursorNewRecycleOplet (this, cursor, -1, 0));
			}
		}
		
		public void MoveCursor(ICursor cursor, int distance)
		{
			if (distance == 0)
			{
				return;
			}
			
			if (cursor.Attachment == CursorAttachment.Temporary)
			{
				this.text.MoveCursor (cursor.CursorId, distance);
			}
			else
			{
				int old_pos = this.text.GetCursorPosition (cursor.CursorId);
				int old_dir = this.text.GetCursorDirection (cursor.CursorId);
				
				int direction = (distance > 0) ? 1 : (distance < 0) ? -1 : 0;
				
				this.text.MoveCursor (cursor.CursorId, distance);
				this.text.SetCursorDirection (cursor.CursorId, direction);
				
				int new_pos = this.text.GetCursorPosition (cursor.CursorId);
				int new_dir = this.text.GetCursorDirection (cursor.CursorId);
				
				System.Diagnostics.Debug.Assert (new_dir == direction);
				
				if ((old_pos != new_pos) &&
					(this.IsOpletQueueEnabled))
				{
					this.InternalAddOplet (new CursorMoveOplet (this, cursor, old_pos, old_dir));
				}
			}
		}
		
		public void RecycleCursor(ICursor cursor)
		{
			//	ATTENTION: la suppression d'un curseur doit �tre g�r�e avec
			//	prudence, car les m�canismes de Undo/Redo doivent pouvoir y
			//	faire r�f�rence en tout temps via ICursor.
			
			int pos = this.text.GetCursorPosition (cursor.CursorId);
			int dir = this.text.GetCursorDirection (cursor.CursorId);
			
			this.text.RecycleCursor (cursor.CursorId);
			
			if (cursor.Attachment != CursorAttachment.Temporary)
			{
				this.InternalAddOplet (new CursorNewRecycleOplet (this, cursor, pos, dir));
			}
		}
		
		
		public void DisableOpletQueue()
		{
			this.oplet_queue_disable++;
		}
		
		public void EnableOpletQueue()
		{
			this.oplet_queue_disable--;
		}
		
		
		public System.IDisposable BeginAction()
		{
			return this.BeginAction (null);
		}
		
		public System.IDisposable BeginAction(string name)
		{
			if (this.IsOpletQueueEnabled)
			{
				return this.oplet_queue.BeginAction (name);
			}
			else
			{
				return null;
			}
		}
		
		public void Insert(Common.Support.IOplet oplet)
		{
			this.InternalAddOplet (oplet);
		}
		
		public void ValidateAction()
		{
			if (this.IsOpletQueueEnabled)
			{
				this.oplet_queue.ValidateAction ();
				
				bool force = false;
				
				Common.Support.OpletQueue.MergeMode merge_1 = this.oplet_queue.LastActionMergeMode;
				Common.Support.OpletQueue.MergeMode merge_2 = this.oplet_queue.LastActionMinusOneMergeMode;
				
				if ((merge_1 == Common.Support.OpletQueue.MergeMode.Disabled) ||
					(merge_2 == Common.Support.OpletQueue.MergeMode.Disabled))
				{
					//	On ne fusionne pas !
				}
				else
				{
					Common.Support.IOplet[] oplets_1 = this.oplet_queue.LastActionOplets;
					Common.Support.IOplet[] oplets_2 = this.oplet_queue.LastActionMinusOneOplets;
					
					if ((oplets_1.Length == 1) &&
						(oplets_2.Length == 1))
					{
						if (this.MergeOplets (oplets_2[0], oplets_1[0]))
						{
							this.oplet_queue.PurgeSingleUndo ();
						}
					}
					else if (oplets_1.Length == 1)
					{
						if (this.MergeOplets (oplets_2[oplets_2.Length-1], oplets_1[0]))
						{
							this.oplet_queue.PurgeSingleUndo ();
						}
					}
				}
				
				this.ApplyAutomaticOpletName (force);
			}
		}
		
		
		public void GetOpletsText(System.Collections.ICollection raw_oplets, out string inserted, out string deleted)
		{
			Common.Support.IOplet[] text_oplets = this.FindTextOplets (raw_oplets);
			
			System.Text.StringBuilder insert = new System.Text.StringBuilder ();
			System.Text.StringBuilder delete = new System.Text.StringBuilder ();
			
			foreach (Common.Support.IOplet oplet in text_oplets)
			{
				if (oplet is TextInsertOplet)
				{
					TextInsertOplet ti = oplet as TextInsertOplet;
					insert.Append (ti.ExtractText ());
				}
				else if (oplet is TextDeleteOplet)
				{
					TextDeleteOplet td = oplet as TextDeleteOplet;
					delete.Append (td.ExtractText ());
				}
			}
			
			inserted = TextStory.CleanUp (insert.ToString ());
			deleted  = TextStory.CleanUp (delete.ToString ());
		}
		
		
		private static string CleanUp(string value)
		{
			System.Text.StringBuilder buffer = new System.Text.StringBuilder ();
			
			foreach (char c in value.Trim ())
			{
				if (Unicode.DefaultBreakAnalyzer.IsControl (c))
				{
					//	Saute les caract�res de contr�le.
				}
				else if (c >= ' ')
				{
					buffer.Append (c);
				}
			}

			return buffer.ToString ();
		}
		
		public void ApplyAutomaticOpletName()
		{
			this.ApplyAutomaticOpletName (false);
		}
		
		public void ApplyAutomaticOpletName(bool force)
		{
			if ((this.oplet_queue.LastActionName == null) ||
				(force))
			{
				Common.Support.IOplet[] oplets = this.FindTextOplets (this.oplet_queue.LastActionOplets);
				
				if (oplets.Length > 0)
				{
					System.Text.StringBuilder buffer = new System.Text.StringBuilder ();
					
					foreach (Common.Support.IOplet oplet in oplets)
					{
						if (buffer.Length > 0)
						{
							buffer.Append ("_");
						}
						buffer.Append (oplet.GetType ().Name.Replace ("Oplet", ""));
					}
					
					string name = buffer.ToString ();
					
					if (this.RenamingOplet != null)
					{
						string result = name;
						
						this.RenamingOplet (name, ref result);
						
						name = result;
					}
					
					this.oplet_queue.ChangeLastActionName (name);
				}
			}
		}
		
		
		public int GetCursorPosition(ICursor cursor)
		{
			return cursor == null ? -1 : this.text.GetCursorPosition (cursor.CursorId);
		}
		
		public int GetCursorDirection(ICursor cursor)
		{
			return cursor == null ? 0 : cursor.Direction;
		}
		
		public void SetCursorPosition(ICursor cursor, int position)
		{
			this.SetCursorPosition (cursor, position, 0);
		}
		
		public void SetCursorPosition(ICursor cursor, int position, int direction)
		{
			if (cursor.Attachment == CursorAttachment.Temporary)
			{
				this.text.SetCursorPosition (cursor.CursorId, position);
				this.text.SetCursorDirection (cursor.CursorId, direction);
			}
			else
			{
				int old_pos = this.text.GetCursorPosition (cursor.CursorId);
				int old_dir = this.text.GetCursorDirection (cursor.CursorId);
				
				this.text.SetCursorPosition (cursor.CursorId, position);
				this.text.SetCursorDirection (cursor.CursorId, direction);
				
				int new_pos = this.GetCursorPosition (cursor);
				int new_dir = this.GetCursorDirection (cursor);
				
				if ((old_pos != new_pos) ||
					(old_dir != new_dir))
				{
					this.InternalAddOplet (new CursorMoveOplet (this, cursor, old_pos, old_dir));
				}
			}
		}
		
		
		public void InsertText(ICursor cursor, ulong[] text)
		{
			int position  = this.text.GetCursorPosition (cursor.CursorId);
			int direction = this.text.GetCursorDirection (cursor.CursorId);
			int length    = text.Length;
			
			//	Passe en revue tous les caract�res et met � jour les compteurs
			//	d'utilisation pour les styles associ�s :
			
			this.IncrementUserCount (text, length);
			
			this.text.InsertText (cursor.CursorId, text);
			this.text_length += length;
			
			this.UpdateTextBreakInformation (position, length);
			
			if (cursor.Attachment == CursorAttachment.Temporary)
			{
				this.InternalAddOplet (new TextInsertOplet (this, position, length));
			}
			else
			{
				this.InternalAddOplet (new TextInsertOplet (this, position, length, cursor, direction));
				
				if (direction != 1)
				{
					cursor.Direction = 1;
				}
			}
		}
		
		public void DeleteText(ICursor cursor, int length)
		{
			int position  = this.text.GetCursorPosition (cursor.CursorId);
			int direction = this.text.GetCursorDirection (cursor.CursorId);
			
			CursorInfo[] cursors;
			
			this.InternalSaveCursorPositions (position, length, out cursors);
			
			int undo_start = this.text_length + 1;
			int undo_end   = undo_start + this.undo_length;
				
			this.InternalMoveText (position, undo_end - length, length);
				
			this.text_length -= length;
			this.undo_length += length;
			
			this.UpdateTextBreakInformation (position, 0);
			
			if (cursor.Attachment == CursorAttachment.Temporary)
			{
				this.InternalAddOplet (new TextDeleteOplet (this, position, length, cursors));
			}
			else
			{
				this.InternalAddOplet (new TextDeleteOplet (this, position, length, cursors, cursor, direction));
				
				if (direction != 1)
				{
					cursor.Direction = 1;
				}
			}
		}
		
		
		public bool ReplaceText(ICursor cursor, int length, string simple_text)
		{
			//	Cette m�thode permet de remplace un texte par un autre, sur place,
			//	en conservant les m�mes attributs typographiques.
			//	Retourne true en cas de modification.
			//	Contrairement � InPlaceReplaceText, cette m�thode est annulable.
			
			System.Diagnostics.Debug.Assert (length > 0);
			System.Diagnostics.Debug.Assert (simple_text.Length > 0);
			
			ulong[] data = new ulong[length];
			ulong[] text;
			
			this.ReadText (cursor, length, data);
			
			ulong code = data[0] & ~ Unicode.Bits.FullCodeMask;
			
			TextConverter.ConvertFromString (simple_text, out text);
			
			for (int i = 0; i < text.Length; i++)
			{
				text[i] |= code;
			}
			
			bool change = false;
			
			if (data.Length == text.Length)
			{
				for (int i = 0; i < text.Length; i++)
				{
					if (data[i] != text[i])
					{
						change = true;
						break;
					}
				}
			}
			else
			{
				change = true;
			}
			
			if (change)
			{
				this.DeleteText (cursor, length);
				this.InsertText (cursor, text);
			}
			
			return change;
		}
		
		internal bool InPlaceReplaceText(ICursor cursor, int length, ulong[] text)
		{
			//	Remplace le texte sans mettre � jour les informations de undo
			//	et de redo. Retourne true si une modification a eu lieu.
			
			//	Le texte de remplacement n'a pas besoin d'avoir la m�me longueur
			//	que le texte qui est remplac� (contrairement � WriteText qui se
			//	contente d'�craser le texte sur place).
			
			int position = this.text.GetCursorPosition (cursor.CursorId);
			
			bool changed = this.InternalReplaceText (position, length, text);
			
			this.text_length -= length;
			this.text_length += text.Length;
			
			this.UpdateTextBreakInformation (position, length);
			
			return changed;
		}
		
		internal bool InPlaceReplaceText(ICursor cursor, int length, string simple_text)
		{
			int position = this.text.GetCursorPosition (cursor.CursorId);
			
			uint[] utf32;
			
			TextConverter.ConvertFromString (simple_text, out utf32);
			
			bool changed = this.InternalReplaceText (position, length, utf32);
			
			this.text_length -= length;
			this.text_length += utf32.Length;
			
			this.UpdateTextBreakInformation (position, length);
			
			return changed;
		}
		
		
		public int ReadText(int position, int length, ulong[] buffer)
		{
			this.text.SetCursorPosition (this.temp_cursor.CursorId, position);
			return this.ReadText (this.temp_cursor, 0, length, buffer);
		}
		
		public int ReadText(ICursor cursor, int length, ulong[] buffer)
		{
			return this.ReadText (cursor, 0, length, buffer);
		}
		
		public int ReadText(ICursor cursor, int offset, int length, ulong[] buffer)
		{
			return this.text.ReadText (cursor.CursorId, offset, length, buffer);
		}
		
		
		public ulong ReadChar(ICursor cursor)
		{
			return this.text.ReadChar (cursor.CursorId);
		}
		
		public ulong ReadChar(ICursor cursor, int offset)
		{
			return this.text.ReadChar (cursor.CursorId, offset);
		}
		
		
		public int WriteText(ICursor cursor, ulong[] new_text)
		{
			return this.WriteText (cursor, 0, new_text);
		}
		
		public int WriteText(ICursor cursor, int offset, ulong[] new_text)
		{
			//	Remplace du texte existant par un nouveau texte; cette op�ration
			//	�crase l'ancien texte, lequel est remplac� caract�re par caract�re.
			
			//	Comparer avec ReplaceText qui peut travailler avec des longueurs
			//	diff�rentes.
			
			int length  = new_text.Length;
			int changes = 0;
			
			Internal.SettingsTable settings   = this.StyleList.InternalSettingsTable;
			ulong[]		           old_text = new ulong[length];
			
			this.text.ReadText (cursor.CursorId, offset, length, old_text);
			
			//	Y a-t-il vraiment des modifications ?
			
			for (int i = 0; i < length; i++)
			{
				if (old_text[i] != new_text[i])
				{
					changes++;
				}
			}
			
			//	S'il n'y a aucune modification dans le texte, il n'y pas non plus
			//	lieu de faire quoi que ce soit :
			
			if (changes == 0)
			{
				return length;
			}
			
			//	Met � jour les compteurs pour le texte de remplacement. Le texte
			//	ancien ne doit pas �tre "d�cr�ment�" pour le moment, car il va
			//	�tre m�moris� dans l'oplet ci-apr�s :
			
			this.IncrementUserCount (new_text, length);
			
			int position = this.text.GetCursorPosition (cursor.CursorId) + offset;
			int written  = this.text.WriteText (cursor.CursorId, offset, length, new_text);
			
			this.UpdateTextBreakInformation (position, length);
			
			this.InternalAddOplet (new TextChangeOplet (this, position, length, old_text));
			
			return written;
		}
		
		
		public int ChangeAllMarkers(ulong marker, bool set)
		{
			//	Cette op�ration n'est pas annulable.
			
			this.text.SetCursorPosition (this.temp_cursor.CursorId, 0);
			
			return this.ChangeMarkers (this.temp_cursor, this.TextLength, marker, set);
		}
		
		public int ChangeMarkers(int position, int length, ulong marker, bool set)
		{
			this.text.SetCursorPosition (this.temp_cursor.CursorId, position);
			return this.ChangeMarkers (this.temp_cursor, length, marker, set);
		}
		
		public int ChangeMarkers(ICursor cursor, int length, ulong marker, bool set)
		{
			//	Modifie les marqueurs associ�s � une tranche de texte; voir aussi
			//	Text.TextContext.Marker pour des marqueurs "standard".
			//
			//	Retourne le nombre de caract�res modifi�s.
			//
			//	Cette op�ration n'est pas annulable.
			
			return this.text.ChangeMarkers (cursor.CursorId, length, marker, set);
		}
		
		
		public void NotifyTextChanged()
		{
			//	Indique que les r�glages internes du texte entier ont chang� sans notre
			//	connaissance. On doit consid�rer le texte complet comme "sale".
			
			this.text_change_mark_start = 0;
			this.text_change_mark_end   = this.TextLength;
			
			this.text.ChangeVersion ();
			
			if (this.suspend_text_changed == 0)
			{
				this.OnTextChanged ();
			}
		}
		
		public void NotifyTextChanged(int start, int length)
		{
			int end = System.Math.Min (start + length, this.TextLength);
			
			this.text_change_mark_start = System.Math.Min (this.text_change_mark_start, start);
			this.text_change_mark_end   = System.Math.Max (this.text_change_mark_end, end);
			
			this.text.ChangeVersion ();
			
			if (this.suspend_text_changed == 0)
			{
				this.OnTextChanged ();
			}
		}
		
		public void SuspendTextChanged()
		{
			this.suspend_text_changed++;
		}
		
		public void ResumeTextChanged()
		{
			System.Diagnostics.Debug.Assert (this.suspend_text_changed > 0);
			
			this.suspend_text_changed--;
			
			if (this.suspend_text_changed == 0)
			{
				if (this.last_text_version != this.text.Version)
				{
					this.OnTextChanged ();
				}
			}
		}
		
		
		public void ClearTextChangeMarkPositions()
		{
			this.text_change_mark_start = this.TextLength;
			this.text_change_mark_end   = 0;
		}
		
		
		public string GetDebugText()
		{
			System.Text.StringBuilder buffer = new System.Text.StringBuilder ();
			this.text.SetCursorPosition (this.temp_cursor.CursorId, 0);
			
			for (int i = 0; i < this.text_length; i++)
			{
				ulong code = this.text[this.temp_cursor.CursorId];
				this.text.MoveCursor (this.temp_cursor.CursorId, 1);
				
				buffer.Append ((char) Unicode.Bits.GetCode (code));
			}
			
			return buffer.ToString ();
		}
		
		public string GetDebugUndo()
		{
			System.Text.StringBuilder buffer = new System.Text.StringBuilder ();
			this.text.SetCursorPosition (this.temp_cursor.CursorId, this.text_length + 1);
			
			for (int i = 0; i < this.undo_length; i++)
			{
				ulong code = this.text[this.temp_cursor.CursorId];
				this.text.MoveCursor (this.temp_cursor.CursorId, 1);
				
				buffer.Append ((char) Unicode.Bits.GetCode (code));
			}
			
			return buffer.ToString ();
		}
		
		public string GetDebugAllStyledText()
		{
			ulong[] buffer = new ulong[this.TextLength];
			this.ReadText (0, buffer.Length, buffer);
			return this.GetDebugStyledText (buffer);
		}
		
		public string GetDebugStyledText(ulong[] text)
		{
			System.Text.StringBuilder buffer = new System.Text.StringBuilder ();
			
			ulong prev = Internal.CharMarker.ExtractCoreAndSettings (text[0]);
			
			int start = 0;
			int count = 1;
			
			for (int i = 1; i < text.Length; i++)
			{
				ulong code = Internal.CharMarker.ExtractCoreAndSettings (text[i]);
				
				if (code == prev)
				{
					count++;
					continue;
				}
				
				this.GenerateDebugStyledTextForRun (text, prev, start, count, buffer);
				
				prev  = code;
				start = i;
				count = 1;
			}
			
			this.GenerateDebugStyledTextForRun (text, prev, start, count, buffer);
			
			return buffer.ToString ();
		}
		
		
		private void GenerateDebugStyledTextForRun(ulong[] text, ulong code, int offset, int length, System.Text.StringBuilder buffer)
		{
			if (length == 0)
			{
				return;
			}
			
			buffer.Append ("[");
			
			for (int i = 0; i < length; i++)
			{
				buffer.Append ((char) Unicode.Bits.GetCode (text[offset+i]));
			}
			
			buffer.Append ("]\n    ");
			
			Internal.SettingsTable settings = this.StyleList.InternalSettingsTable;
			
			Styles.CoreSettings  core  = settings.GetCore (code);
			Styles.LocalSettings local = settings.GetLocalSettings (code);
			Styles.ExtraSettings extra = settings.GetExtraSettings (code);
			
			int n = 0;
			
			if (core != null)
			{
				foreach (Property property in core)
				{
					if (n > 0) buffer.Append (", ");
					buffer.Append ("S=");
					buffer.Append (property.WellKnownType.ToString ());
					n++;
				}
			}
			
			if (local != null)
			{
				foreach (Property property in local)
				{
					if (n > 0) buffer.Append (", ");
					buffer.Append ("L=");
					buffer.Append (property.WellKnownType.ToString ());
					n++;
				}
			}
			
			if (extra != null)
			{
				foreach (Property property in extra)
				{
					if (n > 0) buffer.Append (", ");
					buffer.Append ("E=");
					buffer.Append (property.WellKnownType.ToString ());
					n++;
				}
			}
			
			n = 0;
			
			if (core != null)
			{
				foreach (Property property in core)
				{
					switch (property.WellKnownType)
					{
						case Properties.WellKnownType.Styles:
//-						case Properties.WellKnownType.Properties:
							buffer.Append (n == 0 ? "\n    " : ", ");
							Property.SerializeToText (buffer, property);
							n++;
							break;
					}
				}
			}
			
			buffer.Append ("\n");
		}
		
		
		public void ConvertToStyledText(string simple_text, TextStyle text_style, out ulong[] styled_text)
		{
			//	G�n�re un texte en lui appliquant les propri�t�s qui d�finissent
			//	le style et les r�glages associ�s.
			
			TextStyle[] text_styles = { text_style };
			
			this.ConvertToStyledText (simple_text, text_styles, null, out styled_text);
		}
		
		public void ConvertToStyledText(string simple_text, TextStyle text_style, System.Collections.ICollection properties, out ulong[] styled_text)
		{
			TextStyle[] text_styles = (text_style == null) ? new TextStyle[0] : new TextStyle[] { text_style };
			
			this.ConvertToStyledText (simple_text, text_styles, properties, out styled_text);
		}
		
		public void ConvertToStyledText(string simple_text, System.Collections.ICollection text_styles, System.Collections.ICollection properties, out ulong[] styled_text)
		{
			this.ConvertToStyledText (simple_text, this.FlattenStylesAndProperties (text_styles, properties), out styled_text);
		}
		
		public void ConvertToStyledText(string simple_text, System.Collections.ICollection properties, out ulong[] styled_text)
		{
			uint[] utf32;
			
			TextConverter.ConvertFromString (simple_text, out utf32);
			
			this.ConvertToStyledText (utf32, properties, out styled_text);
		}
		
		public void ConvertToStyledText(uint[] utf32, System.Collections.ICollection properties, out ulong[] styled_text)
		{
			ulong style;
			int   length = utf32.Length;
			
			this.ConvertToStyledText (properties, out style);
			
			styled_text = new ulong[length];
			
			for (int i = 0; i < length; i++)
			{
				styled_text[i] = utf32[i] | style;
			}
		}
		
		public void ConvertToStyledText(System.Collections.ICollection properties, out ulong style)
		{
			//	Trie les propri�t�s selon leur type; certaines vont servir �
			//	d�finir le style, d'autres � d�finir les r�glages locaux et
			//	sp�ciaux :
			
			int length = properties == null ? 0 : properties.Count;
			
			Property[] prop_mixed = new Property[length];
			Property   polymorph  = null;
			
			Styles.CoreSettings  search_core  = new Styles.CoreSettings ();
			Styles.LocalSettings search_local = new Styles.LocalSettings ();
			Styles.ExtraSettings search_extra = new Styles.ExtraSettings ();
			
			Styles.PropertyContainer.Accumulator core_acc  = search_core.StartAccumulation ();
			Styles.PropertyContainer.Accumulator local_acc = search_local.StartAccumulation ();
			Styles.PropertyContainer.Accumulator extra_acc = search_extra.StartAccumulation ();
			
			if (length > 0)
			{
				properties.CopyTo (prop_mixed, 0);
			}
			
			for (int i = 0; i < length; i++)
			{
				switch (prop_mixed[i].PropertyType)
				{
					case Properties.PropertyType.CoreSetting:	core_acc.Accumulate (prop_mixed[i]); break;
					case Properties.PropertyType.LocalSetting:	local_acc.Accumulate (prop_mixed[i]); break;
					case Properties.PropertyType.ExtraSetting:	extra_acc.Accumulate (prop_mixed[i]); break;
					
					case Properties.PropertyType.Polymorph:
						if (polymorph == null)
						{
							polymorph = prop_mixed[i];
						}
						else
						{
							System.Diagnostics.Debug.Assert (polymorph.WellKnownType == prop_mixed[i].WellKnownType);
							
							polymorph = polymorph.GetCombination (prop_mixed[i]);
						}
						break;
					
					default:
						throw new System.ArgumentException ("Invalid property type", "properties");
				}
			}
			
			//	S'il y a des propri�t�s polymorphes, d�cide encore dans quelle
			//	cat�gorie les placer (utilis� pour StylesProperty).
			
			if (polymorph != null)
			{
				if (extra_acc.IsEmpty)
				{
					core_acc.Accumulate (polymorph);
				}
				else
				{
					extra_acc.Accumulate (polymorph);
				}
			}
			
			//	G�n�re le style et les r�glages en fonction des propri�t�s :
			
			core_acc.Done ();
			local_acc.Done ();
			extra_acc.Done ();
			
			style = 0;
			
			//	Attache le style et les r�glages; r�utilise de mani�re interne
			//	un style existant, si possible :
			
			this.StyleList.InternalSettingsTable.Attach (ref style, search_core, search_local, search_extra);
			
			if ((core_acc.RequiresSpecialCodeProcessing) ||
				(local_acc.RequiresSpecialCodeProcessing) ||
				(extra_acc.RequiresSpecialCodeProcessing))
			{
				Unicode.Bits.SetSpecialCodeFlag (ref style, true);
			}
		}
		
		
		public System.Collections.ArrayList FlattenStylesAndProperties(System.Collections.ICollection text_styles, System.Collections.ICollection properties)
		{
			return this.FlattenStylesAndProperties (text_styles, properties, true);
		}
		
		public System.Collections.ArrayList FlattenStylesAndProperties(System.Collections.ICollection text_styles, System.Collections.ICollection properties, bool generate_styles_property)
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList ();
			
			if ((text_styles != null) &&
				(text_styles.Count > 0))
			{
				TextStyle[] flat_styles;
				Property[]  flat_properties;
				
				this.TextContext.GetFlatProperties (text_styles, out flat_styles, out flat_properties);
				
				//	Cr�e une propri�t� StylesProperty qui r�sume les styles dont
				//	les propri�t�s viennent d'�tre mises � plat ci-dessus :
				
				list.AddRange (flat_properties);
				
				if (generate_styles_property)
				{
					list.Add (new Properties.StylesProperty (flat_styles));
				}
			}
			
			if ((properties != null) &&
				(properties.Count > 0))
			{
				//	Les propri�t�s "manuelles" viennent s'ajouter � la fin de la
				//	liste, apr�s les propri�t�s du/des styles.
				
				list.AddRange (properties);
				
				//	Prend note (sous une forme s�rialis�e) de toutes ces propri�t�s
				//	additionnelles. Sans PropertiesProperty, il serait impossible de
				//	reconstituer les propri�t�s "manuelles" dans certains cas.
				
				if (this.TextContext.IsPropertiesPropertyEnabled)
				{
					list.Add (new Properties.PropertiesProperty (properties));
				}
			}
			
			return list;
		}
		
		
		public byte[] Serialize()
		{
			System.IO.MemoryStream stream = new System.IO.MemoryStream ();
			
			this.text.WriteRawText (stream, this.text_length + 1);
			
			return stream.ToArray ();
		}
		
		public void Deserialize(byte[] data)
		{
			//	Commence par remettre les compteurs � z�ro pour ce qui �tait
			//	utilis� pour le texte :
			
			int     length = this.TextLength;
			ulong[] text   = new ulong[length];
			
			this.ReadText (0, length, text);
			this.DecrementUserCount (text, length);
			
			//	D�s�rialise � partir d'un memory stream :
			
			System.IO.MemoryStream stream = new System.IO.MemoryStream (data, 0, data.Length, false);
			
			Internal.CursorTable cursor_table = this.text.CursorTable;
			ICursor[]            cursor_array = cursor_table.GetCursorArray ();
			
			//	Dans un texte fra�chement cr��, il y a un curseur temporaire qui
			//	appartient � TextStory, un curseur normal qui appartient au navi-
			//	gateur, ainsi qu'un curseur temporaire (pour le navigateur) :
			
			System.Diagnostics.Debug.Assert (cursor_array.Length == 3);
			
			foreach (ICursor cursor in cursor_array)
			{
				this.text.RecycleCursor (cursor.CursorId);
			}
			
			//	Le plus simple est de tuer l'ancienne TextTable pour la remplacer
			//	par une toute fra�che et toute propre. Elle n'a aucun curseur :
			
			this.text = new Internal.TextTable ();
			
			this.text.ReadRawText (stream);
			
			stream.Close ();
			stream = null;
			data   = null;
			
			this.text_length = this.text.TextLength - 1;
			this.undo_length = 0;
			
			//	Restitue les curseurs; ils seront plac�s en d�but de document,
			//	ce qui convient parfaitement :
			
			foreach (ICursor cursor in cursor_array)
			{
				this.text.NewCursor (cursor);
			}
			
			//	Met � jour les compteurs d'utilisation pour les "styles" et les
			//	tabulateurs :
			
			length = this.TextLength;
			text   = new ulong[length];
			
			this.ReadText (0, length, text);
			this.IncrementUserCount (text, length);
			
			//	Met � jour les marqueurs dans le texte; supprime toutes traces
			//	li�es � d'anciennes s�lections ou erreurs d'orthographe.
			
			ulong markers = 0;
			
			markers  = this.TextContext.Markers.Selected;
			markers |= this.TextContext.Markers.SpellCheckingError;
			
			this.ChangeAllMarkers (markers, false);
			
			//	Prend note qu'il faudra tout v�rifier au correcteur :
			
			markers  = this.TextContext.Markers.RequiresSpellChecking;
			
			this.ChangeAllMarkers (markers, true);
		}
		
		
		private void IncrementUserCount(ulong[] text, int length)
		{
			Internal.SettingsTable settings = this.StyleList.InternalSettingsTable;
			
			for (int i = 0; i < length; i++)
			{
				ulong code = text[i];
				
				Styles.CoreSettings  core  = settings.GetCore (code);
				Styles.LocalSettings local = settings.GetLocalSettings (code);
				Styles.ExtraSettings extra = settings.GetExtraSettings (code);
				
				if (core  != null) core.IncrementUserCount ();
				if (local != null) local.IncrementUserCount ();
				if (extra != null) extra.IncrementUserCount ();
				
				if ((extra != null) &&
					(extra.UserCount == 1))
				{
					Properties.TabsProperty tabs = extra[Properties.WellKnownType.Tabs] as Properties.TabsProperty;
					
					if (tabs != null)
					{
						foreach (string tag in tabs.TabTags)
						{
							this.context.TabList.IncrementTabUserCount (tag);
						}
					}
				}
				
				if (Unicode.Bits.GetUnicodeCode (code) == Unicode.Code.HorizontalTab)
				{
					Properties.TabProperty tab;
					this.context.GetTab (code, out tab);
					this.context.TabList.IncrementTabUserCount (tab);
				}
			}
		}
		
		private void DecrementUserCount(ulong[] text, int length)
		{
			Internal.SettingsTable settings = this.StyleList.InternalSettingsTable;
			
			for (int i = 0; i < length; i++)
			{
				ulong code = text[i];
				
				Styles.CoreSettings  core  = settings.GetCore (code);
				Styles.LocalSettings local = settings.GetLocalSettings (code);
				Styles.ExtraSettings extra = settings.GetExtraSettings (code);
				
				if (core  != null) core.DecrementUserCount ();
				if (local != null) local.DecrementUserCount ();
				if (extra != null) extra.DecrementUserCount ();
				
				if ((extra != null) &&
					(extra.UserCount == 0))
				{
					Properties.TabsProperty tabs = extra[Properties.WellKnownType.Tabs] as Properties.TabsProperty;
					
					if (tabs != null)
					{
						foreach (string tag in tabs.TabTags)
						{
							this.context.TabList.DecrementTabUserCount (tag);
						}
					}
				}
				
				if (Unicode.Bits.GetUnicodeCode (code) == Unicode.Code.HorizontalTab)
				{
					Properties.TabProperty tab;
					this.context.GetTab (code, out tab);
					this.context.TabList.DecrementTabUserCount (tab);
				}
			}
		}
		
		
		private void InternalAddOplet(Common.Support.IOplet oplet)
		{
			if (this.IsOpletQueueEnabled)
			{
				Common.Support.IOplet[] last_oplets = this.oplet_queue.LastActionOplets;
				bool enable_merge = (this.oplet_queue.LastActionMergeMode != Common.Support.OpletQueue.MergeMode.Disabled) &&
					/**/            (this.oplet_queue.PendingMergeMode != Common.Support.OpletQueue.MergeMode.Disabled);
				
				this.oplet_queue.BeginAction ();
				
				this.NotifyAddingOplet (oplet);
				
				//	V�rifie si quelqu'un a g�n�r� des oplets en r�ponse � notre
				//	�v�nement. Si c'est le cas, on ne peut plus rien fusionner;
				//	ajoute simplement l'oplet � la liste, puis valide l'action :
				
				if (this.oplet_queue.PendingOpletCount > 0)
				{
					this.oplet_queue.Insert (oplet);
					this.NotifyAddedOplet (oplet);
					this.oplet_queue.ValidateAction ();
					this.ApplyAutomaticOpletName ();
					
					return;
				}
				else
				{
					this.oplet_queue.CancelAction ();
				}
				
				if ((last_oplets.Length == 1) &&
					(this.debug_disable_merge == false) &&
					(enable_merge))
				{
					Common.Support.IOplet last = last_oplets[0];
					
					if (this.MergeOplets (last, oplet))
					{
						oplet.Dispose ();
						this.ApplyAutomaticOpletName (true);
						return;
					}
				}
				
				//	TODO: g�rer la fusion d'oplets identiques
				
				using (this.oplet_queue.BeginAction ())
				{
					this.oplet_queue.Insert (oplet);
					this.NotifyAddedOplet (oplet);
					this.oplet_queue.ValidateAction ();
					this.ApplyAutomaticOpletName ();
				}
			}
			else
			{
				oplet.Dispose ();
			}
		}
		
		
		protected Common.Support.IOplet[] FindTextOplets(System.Collections.ICollection oplets)
		{
			//	Trouve tous les oplets qui appartiennent au projet Common.Text
			//	parmi la liste pass�e en entr�e.
			
			System.Collections.ArrayList list = new System.Collections.ArrayList ();
			System.Reflection.Assembly assembly = this.GetType ().Assembly;
			
			foreach (Common.Support.IOplet oplet in oplets)
			{
				if (oplet.GetType ().Assembly == assembly)
				{
					list.Add (oplet);
				}
			}
			
			return (Common.Support.IOplet[]) list.ToArray (typeof (Common.Support.IOplet));
		}
		
		protected bool MergeOplets(Common.Support.IOplet last, Common.Support.IOplet oplet)
		{
			//	Retourne true si l'oplet peut �tre fusionn� avec le pr�c�dent.
			
			if (last.GetType () == oplet.GetType ())
			{
				//	L'oplet qui doit �tre ins�r� est du m�me type que celui qui
				//	a �t� ins�r� auparavant. Peut-�tre peut-on les fusionner ?
				
				if (oplet is CursorMoveOplet)
				{
					//	Un d�placement du curseur suivant un autre --> on ne
					//	conserve que la position de d�part; pour autant que
					//	les deux oplets concernent le m�me curseur.
					
					CursorMoveOplet op_1 = oplet as CursorMoveOplet;
					CursorMoveOplet op_2 = last as CursorMoveOplet;
					
					if (op_1.Cursor == op_2.Cursor)
					{
						return true;
					}
				}
				else if (oplet is TextInsertOplet)
				{
					TextInsertOplet op_1 = oplet as TextInsertOplet;
					TextInsertOplet op_2 = last as TextInsertOplet;
					
					if (op_2.MergeWith (op_1))
					{
						return true;
					}
				}
				else if (oplet is TextDeleteOplet)
				{
					TextDeleteOplet op_1 = oplet as TextDeleteOplet;
					TextDeleteOplet op_2 = last as TextDeleteOplet;
					
					if (op_2.MergeWith (op_1))
					{
						return true;
					}
				}
				else if (oplet is TextChangeOplet)
				{
					TextChangeOplet op_1 = oplet as TextChangeOplet;
					TextChangeOplet op_2 = last as TextChangeOplet;
					
					if (op_2.MergeWith (op_1))
					{
						return true;
					}
				}
				else if (oplet is TextNavigator.DefineSelectionOplet)
				{
					//	Deux s�lections sont toujours combinables; on prend la
					//	premi�re comme r�f�rence.
					
					return true;
				}
			}
			
			return false;
		}
		
		protected void UpdateTextBreakInformation(int position, int length)
		{
			//	Met � jour l'information relative � la coupure des lignes autour
			//	du passage modifi�.
			
			System.Diagnostics.Debug.Assert (position <= this.TextLength);
			
			int area_start = System.Math.Max (0, position - 20);
			int area_end   = System.Math.Min (position + length + 20, this.text_length);
			
			if (area_end > area_start)
			{
				ulong[] text = new ulong[area_end - area_start];
				
				this.text.SetCursorPosition (this.temp_cursor.CursorId, area_start);
				this.text.ReadText (this.temp_cursor.CursorId, area_end - area_start, text);
				
				//	S'il y a des sauts de lignes "forc�s" dans le texte avant et
				//	apr�s le passage modifi�, on recadre la fen�tre :
				
				int from_pos = area_start;
				int to_pos   = area_end;
				
				for (int i = position - 1; i >= area_start; i--)
				{
					if (Unicode.Bits.GetBreakInfo (text[i - area_start]) == Unicode.BreakInfo.Yes)
					{
						from_pos = i + 1;
						break;
					}
				}
				
				for (int i = position + length; i < area_end; i++)
				{
					if (Unicode.Bits.GetBreakInfo (text[i - area_start]) == Unicode.BreakInfo.Yes)
					{
						to_pos = i;
						break;
					}
				}
				
				//	Cherche les fronti�res de mots les plus proches, avant/apr�s le
				//	passage consid�r� :
				
				int word_start = from_pos;
				int word_end   = to_pos;
				
				for (int i = position - 2; i >= from_pos; i--)
				{
					if (Unicode.Bits.GetBreakInfo (text[i - area_start]) == Unicode.BreakInfo.Optional)
					{
						word_start = i + 1;
						break;
					}
				}
				
				for (int i = position + length; i < to_pos; i++)
				{
					if (Unicode.Bits.GetBreakInfo (text[i - area_start]) == Unicode.BreakInfo.Optional)
					{
						word_end = i + 1;
						break;
					}
				}
				
				System.Diagnostics.Debug.Assert (from_pos <= position);
				System.Diagnostics.Debug.Assert (to_pos >= position + length);
				
				System.Diagnostics.Debug.Assert (word_start >= from_pos);
				System.Diagnostics.Debug.Assert (word_end <= to_pos);
				
				//	Demande une analyse du passage consid�r� et recopie les
				//	informations dans le texte lui-m�me :
				
				int text_offset = word_start - area_start;
				int text_length = word_end - word_start;
				
				Unicode.BreakInfo[] breaks = new Unicode.BreakInfo[text_length];
				Unicode.DefaultBreakAnalyzer.GenerateBreaks (text, text_offset, text_length, breaks);
				LanguageEngine.GenerateHyphens (this.context, text, text_offset, text_length, breaks);
				Unicode.Bits.SetBreakInfo (text, text_offset, breaks);
				
				Internal.CharMarker.SetMarkers (this.context.Markers.RequiresSpellChecking, text, text_offset, text_length);
				
				this.text.WriteText (this.temp_cursor.CursorId, area_end - area_start, text);
				
				//	Agrandit la plage dans laquelle il y a eu des modifications signal�es
				//	par des marques telles que RequiresSpellChecking. On s'arrange pour
				//	toujours couvrir des paragraphes complets :
				
				int para_start;
				int para_end;
				
				Internal.Navigator.GetExtendedParagraphPositions (this, word_start, out para_start, out para_end);
				
#if DEBUG
				int para_start_1;
				int para_end_1;
				
				Internal.Navigator.GetParagraphPositions (this, word_start, out para_start_1, out para_end_1);
				
				System.Diagnostics.Debug.Assert (para_start <= para_start_1);
				System.Diagnostics.Debug.Assert (para_end == para_end_1);
#endif
				
				if (word_end > para_end)
				{
					int para_start_2;
					int para_end_2;
					
					Internal.Navigator.GetParagraphPositions (this, word_end, out para_start_2, out para_end_2);
					
					para_end = para_end_2;
				}
				
				if (para_start < this.text_change_mark_start)
				{
					this.text_change_mark_start = para_start;
				}
				if (para_end > this.text_change_mark_end)
				{
					this.text_change_mark_end = para_end;
				}
			}
		}
		
		protected void InternalInsertText(int position, ulong[] text, bool book_keeping)
		{
			if (book_keeping)
			{
				//	Passe en revue tous les caract�res et met � jour les compteurs
				//	d'utilisation pour les styles associ�s :
				
				this.IncrementUserCount (text, text.Length);
			}
			
			this.text.SetCursorPosition (this.temp_cursor.CursorId, position);
			this.text.InsertText (this.temp_cursor.CursorId, text);
		}
		
		protected void InternalDeleteText(int position, int length, out CursorInfo[] infos, bool book_keeping)
		{
			if (book_keeping)
			{
				//	Passe en revue tous les caract�res et met � jour les compteurs
				//	d'utilisation pour les styles associ�s :
				
				ulong[] text = new ulong[length];
				
				this.text.SetCursorPosition (this.temp_cursor.CursorId, position);
				this.text.ReadText (this.temp_cursor.CursorId, length, text);
				
				this.DecrementUserCount (text, length);
			}
			
			this.text.SetCursorPosition (this.temp_cursor.CursorId, position);
			this.text.DeleteText (this.temp_cursor.CursorId, length, out infos);
		}
		
		protected bool InternalReplaceText(int position, int length, ulong[] text)
		{
			//	Remplace du texte sans gestion du undo/redo ni mise � jour des
			//	longueurs respectives de 'text area' et 'undo area', ni gestion
			//	des curseurs.
			
			this.text.SetCursorPosition (this.temp_cursor.CursorId, position);
			
			ulong[] data = new ulong[length];
			int     read = this.text.ReadText (this.temp_cursor.CursorId, length, data);
			
			System.Diagnostics.Debug.Assert (read == length);
			
			CursorInfo[] infos;
			
			this.InternalDeleteText (position, length, out infos, true);
			this.InternalInsertText (position, text, true);
			
			if ((infos != null) &&
				(infos.Length > 0))
			{
				this.InternalRestoreCursorPositions (infos, 0);
			}
			
			if (data.Length != text.Length)
			{
				return true;
			}
			
			for (int i = 0; i < data.Length; i++)
			{
				ulong a = data[i] & ~Unicode.Bits.InfoMask;
				ulong b = text[i] & ~Unicode.Bits.InfoMask;
				
				if (a != b)
				{
					return true;
				}
			}
			
			return false;
		}
		
		protected bool InternalReplaceText(int position, int length, uint[] utf32)
		{
			//	Remplace du texte sans gestion du undo/redo ni mise � jour des
			//	longueurs respectives de 'text area' et 'undo area', ni gestion
			//	des curseurs.
			
			this.text.SetCursorPosition (this.temp_cursor.CursorId, position);
			
			ulong[] text = new ulong[utf32.Length];
			ulong[] data = new ulong[length];
			int     read = this.text.ReadText (this.temp_cursor.CursorId, length, data);
			
			System.Diagnostics.Debug.Assert (read == length);
			
			ulong code = (~ Unicode.Bits.FullCodeMask) & data[0];
			
			for (int i = 0; i < text.Length; i++)
			{
				text[i] = code | utf32[i];
			}
			
			CursorInfo[] infos;
			
			this.InternalDeleteText (position, length, out infos, true);
			this.InternalInsertText (position, text, true);
			
			if ((infos != null) &&
				(infos.Length > 0))
			{
				this.InternalRestoreCursorPositions (infos, 0);
			}
			
			if (data.Length != text.Length)
			{
				return true;
			}
			
			for (int i = 0; i < data.Length; i++)
			{
				ulong a = data[i] & ~Unicode.Bits.InfoMask;
				ulong b = text[i] & ~Unicode.Bits.InfoMask;
				
				if (a != b)
				{
					return true;
				}
			}
			
			return false;
		}
		
		protected void InternalMoveText(int from_pos, int to_pos, int length)
		{
			//	D�place le texte sans gestion du undo/redo ni mise � jour des
			//	longueurs respectives de 'text area' et 'undo area', ni gestion
			//	des curseurs.
			
			//	L'appelant fournit une position de destination qui est valide
			//	seulement apr�s la suppression (temporaire) du texte.
			
			this.text.SetCursorPosition (this.temp_cursor.CursorId, from_pos);
			
			ulong[] data = new ulong[length];
			int     read = this.text.ReadText (this.temp_cursor.CursorId, length, data);
			
			System.Diagnostics.Debug.Assert (read == length);
			
			CursorInfo[] infos;
			
			this.InternalDeleteText (from_pos, length, out infos, false);
			this.InternalInsertText (to_pos, data, false);
			
			if ((infos != null) &&
				(infos.Length > 0))
			{
				//	La liste des curseurs affect�s contient peut-�tre des curseurs
				//	temporaires; on commence par les filtrer, puis on d�place tous
				//	les curseurs restants � la nouvelle position :
				
				infos = this.text.FilterCursors (infos, new CursorInfo.Filter (this.FilterSaveCursors));
				
				this.InternalRestoreCursorPositions (infos, to_pos - from_pos);
			}
		}
		
		protected void InternalReadText(int position, ulong[] buffer)
		{
			//	Lit le texte � la position donn�e.
			
			this.text.SetCursorPosition (this.temp_cursor.CursorId, position);
			
			int length = buffer.Length;
			int read   = this.text.ReadText (this.temp_cursor.CursorId, length, buffer);
			
			System.Diagnostics.Debug.Assert (read == length);
		}
		
		protected void InternalWriteText(int position, ulong[] text)
		{
			//	Ecrit le texte � la position donn�e.
			
			this.text.SetCursorPosition (this.temp_cursor.CursorId, position);
			
			int length  = text.Length;
			int written = this.text.WriteText (this.temp_cursor.CursorId, length, text);
			
			System.Diagnostics.Debug.Assert (written == length);
		}
		
		protected void InternalSaveCursorPositions(int position, int length, out CursorInfo[] infos)
		{
			infos = this.text.FindCursors (position, length, new CursorInfo.Filter (this.FilterSaveCursors));
		}
		
		protected void InternalRestoreCursorPositions(CursorInfo[] infos, int offset)
		{
			if ((infos != null) &&
				(infos.Length > 0))
			{
				for (int i = 0; i < infos.Length; i++)
				{
					this.text.SetCursorPosition (infos[i].CursorId, infos[i].Position + offset);
					this.text.SetCursorDirection (infos[i].CursorId, infos[i].Direction);
				}
			}
		}
		
		
		protected bool FilterSaveCursors(ICursor cursor, int position)
		{
			return (cursor != null) && (cursor.Attachment != CursorAttachment.Temporary);
		}
		
		
		private void SetupTextStory()
		{
			this.text = new Internal.TextTable ();
			
			this.text_length = 0;
			this.undo_length = 0;
			
			this.temp_cursor = new Cursors.TempCursor ();
			
			this.text.NewCursor (this.temp_cursor);
			this.text.InsertText (this.temp_cursor.CursorId, new ulong[] { 0ul });
		}
		
		private void SetupOpletQueue(Common.Support.OpletQueue oplet_queue)
		{
			this.oplet_queue = oplet_queue;
		}
		
		private void SetupContext(TextContext context)
		{
			if (this.context != null)
			{
				this.context.Detach (this);
			}
			
			this.context = context;
			
			if (this.context != null)
			{
				this.context.Attach (this);
			}
		}
		
		
		private void NotifyAddingOplet(Common.Support.IOplet oplet)
		{
			this.OnOpletExecuted (new OpletEventArgs (oplet, Common.Support.OpletEvent.AddingOplet));
		}
		
		private void NotifyAddedOplet(Common.Support.IOplet oplet)
		{
			this.OnOpletExecuted (new OpletEventArgs (oplet, Common.Support.OpletEvent.AddedOplet));
		}
		
		private void NotifyUndoExecuted(BaseOplet oplet)
		{
			this.OnOpletExecuted (new OpletEventArgs (oplet, Common.Support.OpletEvent.UndoExecuted));
		}
		
		private void NotifyRedoExecuted(BaseOplet oplet)
		{
			this.OnOpletExecuted (new OpletEventArgs (oplet, Common.Support.OpletEvent.RedoExecuted));
		}
		
		
		protected virtual void OnOpletExecuted(OpletEventArgs e)
		{
			if (this.OpletExecuted != null)
			{
				this.OpletExecuted (this, e);
			}
		}
		
		protected virtual void OnTextChanged()
		{
			this.last_text_version = this.text.Version;
			
			if (this.TextChanged != null)
			{
				this.TextChanged (this);
			}
		}
		
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SetupContext (null);
			}
		}
		
		
		#region Abstract BaseOplet Class
		internal abstract class BaseOplet : Common.Support.AbstractOplet
		{
			protected BaseOplet(TextStory story)
			{
				this.story = story;
			}
			
			
			
			protected void NotifyUndoExecuted()
			{
				this.story.NotifyUndoExecuted (this);
			}
			
			protected void NotifyRedoExecuted()
			{
				this.story.NotifyRedoExecuted (this);
			}
			
			
			protected readonly TextStory		story;
		}
		#endregion
		
		#region ICursorOplet Interface
		internal interface ICursorOplet
		{
			ICursor			Cursor		{ get; }
			CursorInfo[]	CursorInfos	{ get; }
		}
		#endregion
		
		#region IDisposable Members
		public void Dispose()
		{
			this.Dispose (true);
			System.GC.SuppressFinalize (this);
		}
		#endregion
		
		#region Abstract BaseCursorOplet Class
		internal abstract class BaseCursorOplet : BaseOplet, ICursorOplet
		{
			public BaseCursorOplet(TextStory story, ICursor cursor) : base (story)
			{
				this.cursor = cursor;
			}
			
			
			#region ICursorOplet Members
			public ICursor						Cursor
			{
				get
				{
					return this.cursor;
				}
			}
			
			public virtual CursorInfo[]			CursorInfos
			{
				get
				{
					return null;
				}
			}
			#endregion
			
			protected readonly ICursor			cursor;
		}
		#endregion
		
		#region TextInsertOplet Class
		internal class TextInsertOplet : BaseOplet, ICursorOplet
		{
			public TextInsertOplet(TextStory story, int position, int length) : base (story)
			{
				this.position = position;
				this.length   = length;
				
				this.RememberText ();
			}
			
			public TextInsertOplet(TextStory story, int position, int length, ICursor cursor, int direction) : this (story, position, length)
			{
				this.cursor    = cursor;
				this.direction = direction;
			}
			
			
			public bool MergeWith(TextInsertOplet other)
			{
				if (this.position + this.length == other.position)
				{
					this.length += other.length;
					return true;
				}
				
				return false;
			}
			
			public string ExtractText()
			{
				return this.text;
			}
			
			
			public override Common.Support.IOplet Undo()
			{
				this.story.InternalSaveCursorPositions (this.position, this.length, out this.cursors);
				
				System.Diagnostics.Debug.Assert (this.cursors != null);
				
				int undo_start = this.story.text_length + 1;
				int undo_end   = undo_start + this.story.undo_length;
				
				this.story.InternalMoveText (this.position, undo_start - this.length, this.length);
				
				this.story.text_length -= this.length;
				this.story.undo_length += this.length;
				
				if (this.cursor != null)
				{
					int new_dir = this.story.text.GetCursorDirection (this.cursor.CursorId);
					int old_dir = this.direction;
					
					this.story.text.SetCursorDirection (this.cursor.CursorId, old_dir);
					
					this.direction = new_dir;
					
					System.Diagnostics.Debug.Assert (this.story.text.GetCursorPosition (this.cursor.CursorId) == this.position);
				}
				
				this.story.UpdateTextBreakInformation (this.position, 0);
				this.NotifyUndoExecuted ();
				
				return this;
			}
			
			public override Common.Support.IOplet Redo()
			{
				int undo_start = this.story.text_length + 1;
				int undo_end   = undo_start + this.story.undo_length;
				
				this.story.InternalMoveText (undo_start, this.position, this.length);
				
				this.story.text_length += this.length;
				this.story.undo_length -= this.length;
				
				this.story.UpdateTextBreakInformation (this.position, this.length);
				this.story.InternalRestoreCursorPositions (this.cursors, 0);
				
				if (this.cursor != null)
				{
					int new_dir = this.story.text.GetCursorDirection (this.cursor.CursorId);
					int old_dir = this.direction;
					
					this.story.text.SetCursorDirection (this.cursor.CursorId, old_dir);
					
					this.direction = new_dir;
				}
				
				this.cursors = null;
				this.NotifyRedoExecuted ();
				
				return this;
			}
			
			
			#region ICursorOplet Members
			public ICursor						Cursor
			{
				get
				{
					return this.cursor;
				}
			}
			
			public CursorInfo[]					CursorInfos
			{
				get
				{
					return this.cursors;
				}
			}
			#endregion
			
			public override void Dispose()
			{
				//	Lorsque l'on supprime une information permettant d'annuler
				//	une insertion, il n'y a rien � faire. Par contre, si l'oplet
				//	est dans l'�tat "redoable", il faudra supprimer le texte de
				//	la "undo area".
				
				System.Diagnostics.Debug.Assert (this.length > 0);
				
				if (this.cursors != null)
				{
					int undo_start = this.story.text_length + 1;
					int undo_end   = undo_start + this.story.undo_length;
					
					CursorInfo[] infos;
					this.story.InternalDeleteText (undo_start, this.length, out infos, true);
					
					//	TODO: g�rer la suppression des curseurs...
					//	TODO: g�rer la suppression des styles...
					
					this.story.undo_length -= this.length;
					this.cursors = null;
				}
				
				this.length   = 0;
				
				base.Dispose ();
			}
			
			
			private void RememberText()
			{
				//	G�n�re le texte simple qui repr�sente ce qui a �t� ins�r�.
				
				ulong[] buffer = new ulong[this.length];
				this.story.ReadText (this.position, this.length, buffer);
				
				TextConverter.ConvertToString (buffer, out this.text);
			}
			
			
			private readonly ICursor			cursor;
			
			private int							position;
			private int							direction;
			private int							length;
			
			private CursorInfo[]				cursors;
			private string						text;
		}
		#endregion
		
		#region TextDeleteOplet Class
		internal class TextDeleteOplet : BaseOplet, ICursorOplet
		{
			public TextDeleteOplet(TextStory story, int position, int length, CursorInfo[] cursors) : base (story)
			{
				this.position = position;
				this.length   = length;
				this.cursors  = cursors;
				
				this.RememberText ();
			}
			
			public TextDeleteOplet(TextStory story, int position, int length, CursorInfo[] cursors, ICursor cursor, int direction) : this (story, position, length, cursors)
			{
				this.cursor    = cursor;
				this.direction = direction;
			}
			
			
			public bool MergeWith(TextDeleteOplet other)
			{
				if (this.position == other.position)
				{
					//	Les deux tranches sont d�j� stock�es dans le bon ordre dans
					//	le buffer d'annulation; on peut simplement allonger le texte
					//	� annuler :
					
					this.length += other.length;
					other.length = 0;
					return true;
				}
				
				if (this.position == other.position + other.length)
				{
					//	Le texte doit encore �tre permut� dans le buffer d'annulation
					//	avant de pouvoir allonger le texte � annuler :
					
					int undo_start = this.story.text_length + 1;
					int undo_end   = undo_start + this.story.undo_length;
					
					int pos_2 = undo_end - other.length;
					int pos_1 = pos_2 - this.length;
					
					this.story.InternalMoveText (pos_2, pos_1, other.length);
					
					this.position = other.position;
					this.length  += other.length;
					other.length  = 0;
					return true;
				}
				
				return false;
			}
			
			public string ExtractText()
			{
				return this.text;
			}
			
			
			public override Common.Support.IOplet Undo()
			{
				int undo_start = this.story.text_length + 1;
				int undo_end   = undo_start + this.story.undo_length;
				
				this.story.InternalMoveText (undo_end - this.length, this.position, this.length);
				
				this.story.text_length += this.length;
				this.story.undo_length -= this.length;
				
				this.story.UpdateTextBreakInformation (this.position, this.length);
				this.story.InternalRestoreCursorPositions (this.cursors, 0);
				
				if (this.cursor != null)
				{
					int new_dir = this.story.text.GetCursorDirection (this.cursor.CursorId);
					int old_dir = this.direction;
					
					this.story.text.SetCursorDirection (this.cursor.CursorId, old_dir);
					
					this.direction = new_dir;
				}
				
				this.cursors = null;
				this.NotifyUndoExecuted ();
				
				return this;
			}
			
			public override Common.Support.IOplet Redo()
			{
				this.story.InternalSaveCursorPositions (this.position, this.length, out this.cursors);
				
				System.Diagnostics.Debug.Assert (this.cursors != null);
				
				int undo_start = this.story.text_length + 1;
				int undo_end   = undo_start + this.story.undo_length;
				
				this.story.InternalMoveText (this.position, undo_end - this.length, this.length);
				
				this.story.text_length -= this.length;
				this.story.undo_length += this.length;
				
				if (this.cursor != null)
				{
					int new_dir = this.story.text.GetCursorDirection (this.cursor.CursorId);
					int old_dir = this.direction;
					
					this.story.text.SetCursorDirection (this.cursor.CursorId, old_dir);
					
					this.direction = new_dir;
					
					System.Diagnostics.Debug.Assert (this.story.text.GetCursorPosition (this.cursor.CursorId) == this.position);
				}
				
				this.story.UpdateTextBreakInformation (this.position, 0);
				this.NotifyRedoExecuted ();
				
				return this;
			}
			
			
			#region ICursorOplet Members
			public ICursor						Cursor
			{
				get
				{
					return this.cursor;
				}
			}
			
			public CursorInfo[]					CursorInfos
			{
				get
				{
					return this.cursors;
				}
			}
			#endregion
			
			public override void Dispose()
			{
				//	Lorsque l'on supprime une information permettant de refaire
				//	une destruction, il n'y a rien � faire. Par contre, si l'oplet
				//	est dans l'�tat "undoable", il faudra supprimer le texte de
				//	la "undo area".
				
				if (this.cursors != null)
				{
					if (this.length > 0)
					{
						int undo_start = this.story.text_length + 1;
						int undo_end   = undo_start + this.story.undo_length;
						
						CursorInfo[] infos;
						this.story.InternalDeleteText (undo_end - this.length, this.length, out infos, true);
					}
					
					//	TODO: g�rer la suppression des curseurs...
					//	TODO: g�rer la suppression des styles...
					
					this.story.undo_length -= this.length;
					this.cursors = null;
				}
				
				this.length   = 0;
				
				base.Dispose ();
			}
			
			
			private void RememberText()
			{
				//	G�n�re le texte simple qui repr�sente ce qui a �t� d�truit.
				
				int undo_start = this.story.text_length + 1;
				int undo_end   = undo_start + this.story.undo_length;
				
				ulong[] buffer = new ulong[this.length];
				this.story.ReadText (undo_end - this.length, this.length, buffer);
				
				TextConverter.ConvertToString (buffer, out this.text);
			}
			
			
			private readonly ICursor			cursor;
			
			private int							position;
			private int							direction;
			private int							length;
			private CursorInfo[]				cursors;
			private string						text;
		}
		#endregion
		
		#region TextChangeOplet Class
		internal class TextChangeOplet : BaseOplet
		{
			public TextChangeOplet(TextStory story, int position, int length, ulong[] old_text) : base (story)
			{
				this.position = position;
				this.length   = length;
				this.text     = old_text;
			}
			
			
			public override Common.Support.IOplet Undo()
			{
				ulong[] old_text = new ulong[this.length];
				ulong[] new_text = this.text;
				
				this.story.InternalReadText (this.position, old_text);
				this.story.InternalWriteText (this.position, new_text);
				
				this.text = old_text;
				
				this.story.UpdateTextBreakInformation (this.position, this.length);
				this.NotifyUndoExecuted ();
				
				return this;
			}
			
			public override Common.Support.IOplet Redo()
			{
				ulong[] old_text = new ulong[this.length];
				ulong[] new_text = this.text;
				
				this.story.InternalReadText (this.position, old_text);
				this.story.InternalWriteText (this.position, new_text);
				
				this.text = old_text;
				
				this.story.UpdateTextBreakInformation (this.position, 0);
				this.NotifyRedoExecuted ();
				
				return this;
			}
			
			
			public override void Dispose()
			{
				//	Lorsque l'on supprime une information permettant de refaire
				//	ou d�faire une modification, il suffit de mettre � jour les
				//	compteurs d'utilisation des styles.
				
				System.Diagnostics.Debug.Assert (this.length > 0);
				
				this.story.DecrementUserCount (this.text, this.length);
				
				this.text   = null;
				this.length = 0;
				
				base.Dispose ();
			}
			
			public bool MergeWith(TextChangeOplet other)
			{
				if ((this.position == other.position) &&
					(this.length == other.length))
				{
					return true;
				}
				
				return false;
			}
			
			
			private int							position;
			private int							length;
			private ulong[]						text;
		}
		#endregion
		
		#region CursorMoveOplet Class
		internal class CursorMoveOplet : BaseCursorOplet
		{
			public CursorMoveOplet(TextStory story, ICursor cursor, int position, int direction) : base (story, cursor)
			{
				this.position  = position;
				this.direction = direction;
			}
			
			
			public override Common.Support.IOplet Undo()
			{
				Common.Support.IOplet oplet = this.Swap ();
				this.NotifyUndoExecuted ();
				return oplet;
			}
			
			public override Common.Support.IOplet Redo()
			{
				Common.Support.IOplet oplet = this.Swap ();
				this.NotifyRedoExecuted ();
				return oplet;
			}
			
			
			private Common.Support.IOplet Swap()
			{
				int old_pos = this.position;
				int old_dir = this.direction;
				
				int new_pos = this.story.text.GetCursorPosition (this.cursor.CursorId);
				int new_dir = this.story.text.GetCursorDirection (this.cursor.CursorId);
				
				this.story.text.SetCursorPosition (this.cursor.CursorId, old_pos);
				this.story.text.SetCursorDirection (this.cursor.CursorId, old_dir);
				
				this.position  = new_pos;
				this.direction = new_dir;
				
				return this;
			}
			
			
			private int							position;
			private int							direction;
		}
		#endregion
		
		#region CursorNewRecycleOplet Class
		internal class CursorNewRecycleOplet : BaseCursorOplet
		{
			public CursorNewRecycleOplet(TextStory story, ICursor cursor, int position, int direction) : base (story, cursor)
			{
				this.position  = position;
				this.direction = direction;
			}
			
			
			public override Common.Support.IOplet Undo()
			{
				Common.Support.IOplet oplet = this.Swap ();
				this.NotifyUndoExecuted ();
				return oplet;
			}
			
			public override Common.Support.IOplet Redo()
			{
				Common.Support.IOplet oplet = this.Swap ();
				this.NotifyRedoExecuted ();
				return oplet;
			}
			
			
			private Common.Support.IOplet Swap()
			{
				if (this.position == -1)
				{
					this.position  = this.story.text.GetCursorPosition (this.cursor.CursorId);
					this.direction = this.story.text.GetCursorDirection (this.cursor.CursorId);
					
					this.story.text.RecycleCursor (this.cursor.CursorId);
				}
				else
				{
					this.story.text.NewCursor (this.cursor);
					this.story.text.SetCursorPosition (this.cursor.CursorId, this.position);
					this.story.text.SetCursorDirection (this.cursor.CursorId, this.direction);
					
					this.position  = -1;
					this.direction = 0;
				}
				
				return this;
			}
			
			
			private int							position;
			private int							direction;
		}
		#endregion
		
		public delegate bool CodeCallback(ulong code);
		public delegate void RenamingCallback(string name, ref string output);

		
		public event OpletEventHandler			OpletExecuted;
		public event EventHandler				TextChanged;
		public event RenamingCallback			RenamingOplet;
		
		
		private Internal.TextTable				text;
		private int								text_length;		//	texte dans la zone texte
		private int								undo_length;		//	texte dans la zone undo
		private ICursor							temp_cursor;
		
		private Common.Support.OpletQueue		oplet_queue;
		private int								oplet_queue_disable;
		private TextContext						context;
		
		private bool							debug_disable_oplet;
		private bool							debug_disable_merge;
		
		private int								text_change_mark_start;
		private int								text_change_mark_end;
		
		private int								suspend_text_changed;
		private long							last_text_version = 0;
	}
}
