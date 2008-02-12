using System.Collections.Generic;

using Epsitec.Common.Drawing;
using Epsitec.Common.Support;
using Epsitec.Common.Support.ResourceAccessors;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;

namespace Epsitec.Common.FormEngine
{
	/// <summary>
	/// Proc�dures de manipulation et d'arrangement de listes et d'arbres.
	/// </summary>
	public class Arrange
	{
		public Arrange(IFormResourceProvider resourceProvider)
		{
			//	Constructeur.
			this.resourceProvider = resourceProvider;
		}


		public void Build(FormDescription baseForm, FormDescription deltaForm, out List<FieldDescription> baseFields, out List<FieldDescription> finalFields, out Druid entityId)
		{
			//	Construit la liste des FieldDescription finale.
			//	S'il s'agit d'un Form delta, cherche tous les Forms qui servent � le d�finir, jusqu'au Form de base initial:
			//	 - baseFields contient la liste de base (la g�n�ration pr�c�dente n-1)
			//   - finalFields contient la liste finale (la derni�re g�n�ration n)
			//	S'il s'agit d'un Form de base:
			//	 - baseFields est nul
			//   - finalFields contient la liste finale
			List<List<FieldDescription>> list = new List<List<FieldDescription>>();
			Druid parentId;

			if (deltaForm != null)
			{
				list.Add(deltaForm.Fields);
			}

			list.Add(baseForm.Fields);
			parentId = baseForm.DeltaBaseFormId;
			entityId = baseForm.EntityId;

			while (parentId.IsValid)
			{
				string xml = this.resourceProvider.GetFormXmlSource(parentId);
				if (string.IsNullOrEmpty(xml))
				{
					break;
				}

				FormDescription form = Serialization.DeserializeForm(xml);
				list.Add(form.Fields);
				parentId = form.DeltaBaseFormId;
				entityId = form.EntityId;
			}

			//	A partir du Form de base initial, fusionne avec tous les Forms delta.
			if (list.Count == 1)
			{
				baseFields = null;
				finalFields = list[0];
			}
			else
			{
				finalFields = list[list.Count-1];
				baseFields = null;
				for (int i=list.Count-2; i>=0; i--)
				{
					baseFields = finalFields;
					finalFields = this.Merge(baseFields, list[i]);
				}
			}

			if (baseFields == null)
			{
				baseFields = new List<FieldDescription>();
			}
		}

#if false
		public void Build(FormDescription form, out List<FieldDescription> baseFields, out List<FieldDescription> finalFields)
		{
			//	Construit la liste des FieldDescription finale.
			//	S'il s'agit d'un Form delta, cherche tous les Forms qui servent � le d�finir, jusqu'au Form de base initial:
			//	 - baseFields contient la liste de base (la g�n�ration pr�c�dente n-1)
			//   - finalFields contient la liste finale (la derni�re g�n�ration n)
			//	S'il s'agit d'un Form de base:
			//	 - baseFields est nul
			//   - finalFields contient la liste finale
			if (form.IsDelta)
			{
				List<FormDescription> baseForms = new List<FormDescription>();
				FormDescription baseForm = form;
				baseForms.Add(baseForm);

				//	Cherche tous les Forms de base, jusqu'� trouver le Form initial qui n'est pas un Form delta.
				//	Par exemple:
				//	- Form1 est un masque de base
				//	- Form2 est un masque delta bas� sur Form1
				//	- Form3 est un masque delta bas� sur Form2
				//	Si on cherche � construire Form3, la liste baseForms contiendra Form3, Form2 et Form1.
				while (baseForm != null && baseForm.IsDelta)
				{
					string xml = this.resourceProvider.GetFormXmlSource(baseForm.DeltaBaseFormId);
					if (string.IsNullOrEmpty(xml))
					{
						baseForm = null;
					}
					else
					{
						baseForm = Serialization.DeserializeForm(xml);
						baseForms.Add(baseForm);
					}
				}

				//	A partir du Form de base initial, fusionne avec tous les Forms delta.
				finalFields = baseForms[baseForms.Count-1].Fields;
				baseFields = null;
				for (int i=baseForms.Count-2; i>=0; i--)
				{
					baseFields = finalFields;
					finalFields = this.Merge(baseFields, baseForms[i].Fields);
				}
			}
			else
			{
				baseFields = null;
				finalFields = form.Fields;
			}
		}
#endif

		public List<FieldDescription> Merge(List<FieldDescription> baseList, List<FieldDescription> deltaList)
		{
			//	Retourne la liste finale fusionn�e.
			List<FieldDescription> finalList = new List<FieldDescription>();

			//	G�n�re la liste fusionn�e de tous les champs. Les champs cach�s sont quand m�me dans la liste,
			//	mais avec la propri�t� DeltaHidden = true.
			if (baseList != null)
			{
				foreach (FieldDescription field in baseList)
				{
					FieldDescription copy = new FieldDescription(field);

					if (deltaList != null)
					{
						int index = Arrange.IndexOfGuid(deltaList, field.Guid);
						if (index != -1 && deltaList[index].DeltaHidden)
						{
							copy.DeltaHidden = true;  // champ � cacher
						}
					}
					finalList.Add(copy);
				}
			}

			if (deltaList != null)
			{
				foreach (FieldDescription field in deltaList)
				{
					//	On consid�re qu'un champ provenant de la liste delta a des liens corrects, tant que
					//	l'on n'a pas effectivement �chou� de le lier aux champs de la liste finale.
					field.DeltaBrokenAttach = false;

					if (field.DeltaShowed)  // champ � montrer (pour inverser un DeltaHidden) ?
					{
						int src = Arrange.IndexOfGuid(finalList, field.Guid);  // cherche le champ � d�placer
						if (src != -1)
						{
							finalList[src].DeltaHidden = false;
							finalList[src].DeltaShowed = true;
						}
					}

					if (field.DeltaMoved)  // champ � d�placer ?
					{
						int src = Arrange.IndexOfGuid(finalList, field.Guid);  // cherche le champ � d�placer
						if (src != -1)
						{
							//	field.DeltaAttachGuid vaut System.Guid.Empty lorsqu'il faut d�placer l'�l�ment en t�te
							//	de liste.
							int dst = -1;  // position pour mettre en-t�te de liste
							if (field.DeltaAttachGuid != System.Guid.Empty)
							{
								dst = Arrange.IndexOfGuid(finalList, field.DeltaAttachGuid);  // cherche o� le d�placer
								if (dst == -1)  // l'�l�ment d'attache n'existe plus ?
								{
									field.DeltaBrokenAttach = true;
									continue;  // on laisse le champ ici
								}
							}

							FieldDescription temp = finalList[src];
							finalList.RemoveAt(src);

							dst = Arrange.IndexOfGuid(finalList, field.DeltaAttachGuid);  // recalcule le "o�" apr�s suppression
							finalList.Insert(dst+1, temp);  // remet l'�l�ment apr�s dst

							temp.DeltaMoved = true;
						}
					}

					if (field.DeltaInserted)  // champ � ins�rer ?
					{
						//	field.DeltaAttachGuid vaut System.Guid.Empty lorsqu'il faut d�placer l'�l�ment en t�te
						//	de liste.
						int dst = -1;  // position pour mettre en-t�te de liste
						if (field.DeltaAttachGuid != System.Guid.Empty)
						{
							dst = Arrange.IndexOfGuid(finalList, field.DeltaAttachGuid);  // cherche o� le d�placer
							if (dst == -1)  // l'�l�ment d'attache n'existe plus ?
							{
								dst = finalList.Count-1;  // on ins�re le champ � la fin
								field.DeltaBrokenAttach = true;
							}
						}

						FieldDescription copy = new FieldDescription(field);
						copy.DeltaInserted = true;
						finalList.Insert(dst+1, copy);  // ins�re l'�l�ment apr�s dst
					}

					if (field.DeltaModified)  // champ � modifier ?
					{
						int index = Arrange.IndexOfGuid(finalList, field.Guid);
						if (index != -1)
						{
							finalList.RemoveAt(index);  // supprime le champ original

							FieldDescription copy = new FieldDescription(field);
							copy.DeltaModified = true;
							finalList.Insert(index, copy);  // et remplace-le par le champ modifi�
						}
					}
				}
			}
			
			return finalList;
		}


		public string Check(List<FieldDescription> list)
		{
			//	V�rifie une liste. Retourne null si tout est ok, ou un message d'erreur.
			//	Les sous-masques (SubForm) n'ont pas encore �t� d�velopp�s.
			int level = 0;

			foreach (FieldDescription field in list)
			{
				if (field.Type == FieldDescription.FieldType.Node)
				{
					return "La liste ne doit pas contenir de Node";
				}

				if (field.Type == FieldDescription.FieldType.BoxBegin)
				{
					level++;
				}

				if (field.Type == FieldDescription.FieldType.BoxEnd)
				{
					level--;
					if (level < 0)
					{
						return "Il manque un d�but de bo�te";
					}
				}
			}

			if (level > 0)
			{
				return "Il manque une fin de bo�te";
			}

			return null;
		}


		public List<FieldDescription> Organize(List<FieldDescription> fields)
		{
			//	Arrange une liste.
			//	Les sous-masques (SubForm) se comportent comme un d�but de groupe (BoxBegin),
			//	car ils ont d�j� �t� d�velopp�s en SubForm-Field-Field-BoxEnd.
			List<FieldDescription> list = new List<FieldDescription>();

			//	Copie la liste en rempla�ant les Glue successifs par un suel.
			bool isGlue = false;
			foreach (FieldDescription field in fields)
			{
				if (field.Type == FieldDescription.FieldType.Glue)
				{
					if (isGlue)
					{
						continue;
					}

					isGlue = true;
				}
				else
				{
					isGlue = false;
				}

				list.Add(field);
			}

			//	Si un s�parateur est dans une 'ligne', d�place-le au d�but de la ligne.
			//	Par exemple:
			//	'Field-Glue-Field-Glue-Sep-Field' -> 'Sep-Field-Glue-Field-Glue-Field'
			//	'Field-Glue-Field-Sep-Glue-Field' -> 'Sep-Field-Glue-Field-Glue-Field'
			for (int i=0; i<list.Count; i++)
			{
				if (list[i].Type == FieldDescription.FieldType.Line ||
					list[i].Type == FieldDescription.FieldType.Title)  // s�parateur ?
				{
					int j = i;
					bool move;
					do
					{
						move = false;

						if (j > 0 && list[j-1].Type == FieldDescription.FieldType.Glue)  // glue avant ?
						{
							FieldDescription sep = list[j];
							list.RemoveAt(j);
							list.Insert(j-1, sep);  // remplace 'Glue-Sep' par 'Sep-Glue'
							j--;
							move = true;
						}

						if (j > 0 && j < list.Count-1 && list[j+1].Type == FieldDescription.FieldType.Glue)  // glue apr�s ?
						{
							FieldDescription sep = list[j];
							list.RemoveAt(j);
							list.Insert(j-1, sep);  // remplace 'Xxx-Sep-Glue' par 'Sep-Xxx-Glue'
							j--;
							move = true;
						}
					}
					while (move);  // recommence tant qu'on a pu d�placer
				}
			}

			return list;
		}


		public List<FieldDescription> DevelopSubForm(List<FieldDescription> list)
		{
			//	Retourne une liste d�velopp�e qui ne contient plus de sous-masque.
			//	Un sous-masque (SubForm) se comporte alors comme un d�but de groupe (BoxBegin).
			//	Un BoxEnd correspond � chaque SubForm.
			List<FieldDescription> dst = new List<FieldDescription>();

			this.DevelopSubForm(dst, list, null, null);

			return dst;
		}

		private void DevelopSubForm(List<FieldDescription> dst, List<FieldDescription> fields, FieldDescription source, string prefix)
		{
			foreach (FieldDescription field in fields)
			{
				if (field.Type == FieldDescription.FieldType.SubForm)
				{
					if (field.SubFormId.IsEmpty)  // aucun Form choisi ?
					{
						continue;
					}

					//	Cherche le sous-masque
					FormDescription subForm = null;
					string xml = this.resourceProvider.GetFormXmlSource(field.SubFormId);
					if (!string.IsNullOrEmpty(xml))
					{
						subForm = Serialization.DeserializeForm(xml);
					}

					if (subForm != null)
					{
						dst.Add(field);  // met le SubForm, qui se comportera comme un BoxBegin

						string p = string.Concat(prefix, field.GetPath(null), ".");
						this.DevelopSubForm(dst, subForm.Fields, field, p);  // met les champs du sous-masque dans la bo�te

						FieldDescription boxEnd = new FieldDescription(FieldDescription.FieldType.BoxEnd);
						dst.Add(boxEnd);  // met le BoxEnd pour terminer la bo�te SubForm
					}
				}
				else
				{
					if (prefix == null || (field.Type != FieldDescription.FieldType.Field && field.Type != FieldDescription.FieldType.SubForm))
					{
						dst.Add(field);
					}
					else
					{
						FieldDescription copy = new FieldDescription(field);
						copy.SetFields(prefix+field.GetPath(null));
						copy.Source = source;
						dst.Add(copy);
					}
				}
			}
		}


		public List<FieldDescription> Develop(List<FieldDescription> fields)
		{
			//	Retourne une liste d�velopp�e qui ne contient plus de noeuds.
			List<FieldDescription> dst = new List<FieldDescription>();

			this.Develop(dst, fields);
			
			return dst;
		}

		private void Develop(List<FieldDescription> dst, List<FieldDescription> fields)
		{
			foreach (FieldDescription field in fields)
			{
				if (field.Type == FieldDescription.FieldType.Node)
				{
					this.Develop(dst, field.NodeDescription);
				}
				else
				{
					dst.Add(field);
				}
			}
		}


		static public int IndexOfGuid(List<FieldDescription> list, System.Guid guid)
		{
			//	Retourne l'index de l'�l�ment utilisant un Guid donn�.
			//	Retourne -1 s'il n'en existe aucun.
			for (int i=0; i<list.Count; i++)
			{
				if (list[i].Guid == guid)
				{
					return i;
				}
			}

			return -1;
		}


		private readonly IFormResourceProvider resourceProvider;
	}
}
