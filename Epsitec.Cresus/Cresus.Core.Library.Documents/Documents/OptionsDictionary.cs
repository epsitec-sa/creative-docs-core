﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Types;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Documents
{
	/// <summary>
	/// Ce dictionnaire contient toutes les options permettant d'adapter l'impression d'un document.
	/// La clé du dictionnaire est un nom d'option (DocumentOption).
	/// La valeur du dictionnaire est la valeur de l'option. Par exemple "true".
	/// </summary>
	public class OptionsDictionary
	{
		public OptionsDictionary()
		{
			this.dictionary = new Dictionary<DocumentOption, string> ();
		}

		public void Clear()
		{
			this.dictionary.Clear ();
		}

		public void Add(DocumentOption option, string value)
		{
			this.dictionary[option] = value;
		}

		public void Remove(DocumentOption option)
		{
			if (this.dictionary.ContainsKey (option))
			{
				this.dictionary.Remove (option);
			}
		}


		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		public bool ContainsOption(DocumentOption option)
		{
			return this.dictionary.ContainsKey (option);
		}

		public string GetValue(DocumentOption option)
		{
			if (this.dictionary.ContainsKey (option))
			{
				return this.dictionary[option];
			}
			else
			{
				return null;
			}
		}

		public IEnumerable<DocumentOption> Options
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		public IEnumerable<KeyValuePair<DocumentOption, string>> ContentPair
		{
			get
			{
				foreach (var pair in this.dictionary)
				{
					yield return pair;
				}
			}
		}


		public void Merge(OptionsDictionary src)
		{
			if (src != null)
			{
				foreach (var pair in src.ContentPair)
				{
					this.Add (pair.Key, pair.Value);
				}
			}
		}

		public void Extract(OptionsDictionary src)
		{
			if (src != null)
			{
				foreach (var pair in src.ContentPair)
				{
					this.Remove (pair.Key);
				}
			}
		}


		public string GetSerializedData()
		{
			//	Retourne une chaîne qui représente tout le contenu du dictionnaire.
			var list = new List<string> ();

			foreach (var pair in this.dictionary)
			{
				list.Add (DocumentOptions.ToString (pair.Key));
				list.Add (pair.Value);
			}

			return string.Join ("◊", list);
		}

		public void SetSerializedData(string data)
		{
			//	Initialise le dictionnaire à partir d'une chaîne obtenue avec GetSerializedData.
			this.dictionary.Clear ();

			if (!string.IsNullOrEmpty (data))
			{
				var list = data.Split ('◊');

				for (int i = 0; i < list.Length-1; i+=2)
				{
					var option = DocumentOptions.Parse (list[i]);
					var value = list[i+1];

					this.dictionary.Add (option, value);
				}
			}
		}


		public static OptionsDictionary GetDefault()
		{
			//	Retourne toutes les options par défaut.
			var dict = new OptionsDictionary ();
			var all = Verbose.VerboseDocumentOption.GetAll ();

			foreach (var one in all)
			{
				dict.Add (one.Option, one.DefaultValue);
			}

			return dict;
		}


		private readonly Dictionary<DocumentOption, string>		dictionary;
	}
}