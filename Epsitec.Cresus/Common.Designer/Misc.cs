using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

namespace Epsitec.Common.Designer
{
	/// <summary>
	/// La classe Misc contient quelques routines g�n�rales.
	/// </summary>
	public class Misc
	{
		static public void AddSpring(Graphics graphics, Point p1, Point p2, double thickness, int loops)
		{
			//	Dessine un ressort horizontal ou vertical.
			double step = Point.Distance(p1, p2)/loops;

			if (p1.Y == p2.Y)  // ressort horizontal ?
			{
				p1.X = System.Math.Min(p1.X, p2.X);
				for (int i=0; i<loops; i++)
				{
					graphics.AddLine(p1.X, p1.Y, p1.X+step*0.25, p1.Y+thickness);
					graphics.AddLine(p1.X+step*0.25, p1.Y+thickness, p1.X+step*0.75, p1.Y-thickness);
					graphics.AddLine(p1.X+step*0.75, p1.Y-thickness, p1.X+step, p1.Y);
					p1.X += step;
				}
			}
			else if (p1.X == p2.X)  // ressort vertical ?
			{
				p1.Y = System.Math.Min(p1.Y, p2.Y);
				for (int i=0; i<loops; i++)
				{
					graphics.AddLine(p1.X, p1.Y, p1.X+thickness, p1.Y+step*0.25);
					graphics.AddLine(p1.X+thickness, p1.Y+step*0.25, p1.X-thickness, p1.Y+step*0.75);
					graphics.AddLine(p1.X-thickness, p1.Y+step*0.75, p1.X, p1.Y+step);
					p1.Y += step;
				}
			}
			else
			{
				throw new System.Exception("This geometry is not implemented.");
			}
		}

		static public void AddBox(Graphics graphics, Point p1, Point p2, double thickness)
		{
			//	Dessine une bo�te horizontale ou verticale.
			if (p1.Y == p2.Y)  // bo�te horizontale ?
			{
				p1.Y -= thickness+1;
				p2.Y += thickness-1;
				p2.X -= 1;
				Misc.AlignForLine(graphics, ref p1);
				Misc.AlignForLine(graphics, ref p2);
				graphics.AddRectangle(new Rectangle(p1, p2));
			}
			else if (p1.X == p2.X)  // bo�te verticale ?
			{
				p1.X -= thickness+1;
				p2.X += thickness-1;
				p2.Y -= 1;
				Misc.AlignForLine(graphics, ref p1);
				Misc.AlignForLine(graphics, ref p2);
				graphics.AddRectangle(new Rectangle(p1, p2));
			}
			else
			{
				throw new System.Exception("This geometry is not implemented.");
			}
		}

		static public void AlignForLine(Graphics graphics, ref Point p)
		{
			//	Aligne un point pour permettre un joli Graphics.AddLine.
			graphics.Align(ref p);
			p.X += 0.5;
			p.Y += 0.5;
		}


		static public int IndexOfString(string[] list, string searched)
		{
			//	Cherche l'index d'une cha�ne dans une liste de cha�nes.
			for (int i=0; i<list.Length; i++)
			{
				if (list[i] == searched)  return i;
			}
			return -1;
		}

		static public string CultureLongName(System.Globalization.CultureInfo culture)
		{
			//	Retourne le nom long d'une culture, par exemple "Italiano (Italian, IT)".
			return string.Format("{0} ({1}, {2})", Misc.CultureName(culture), culture.DisplayName, Misc.CultureShortName(culture));
		}

		static public string CultureShortName(System.Globalization.CultureInfo culture)
		{
			//	Retourne le nom court (2 lettres) d'une culture.
			return Misc.ProperName(culture.IetfLanguageTag);
		}

		static public string CultureName(System.Globalization.CultureInfo culture)
		{
			//	Retourne le nom standard d'une culture.
			return Misc.ProperName(culture.NativeName);
		}


		static public string ProperName(string text)
		{
			//	Retourne le texte avec une majuscule au d�but.
			if (text.Length <= 1)
			{
				return text.ToUpper();
			}
			else
			{
				return string.Concat(text.Substring(0, 1).ToUpper(), text.Substring(1));
			}
		}


		static public string Resume(string text)
		{
			//	Retourne une version r�sum�e � environ 20 caract�res au maximum.
			return Misc.Resume(text, 20);
		}
		
		static public string Resume(string text, int max)
		{
			//	Retourne une version r�sum�e � environ 'max' caract�res au maximum.
			System.Diagnostics.Debug.Assert(max > 2);
			if ( text.Length > max )
			{
				return string.Concat(text.Substring(0, max-2), "...");
			}
			else
			{
				return text;
			}
		}


		static public bool IsValidLabel(ref string label)
		{
			//	V�rifie si un nom de label est correct.
			label = Searcher.RemoveAccent(label);

			string[] list = label.Split('.');
			System.Text.StringBuilder builder = new System.Text.StringBuilder(label.Length);
			for (int i=0; i<list.Length; i++)
			{
				if (!IsValidName(ref list[i]))  return false;

				if (i > 0)  builder.Append('.');
				builder.Append(list[i]);
			}

			label = builder.ToString();
			return true;
		}

		static protected bool IsValidName(ref string name)
		{
			//	V�rifie si un nom commence par une lettre puis est suivi de lettres ou de chiffres.
			//	Le nom retourn� commence par une majuscule suivie de minuscules.
			if (name.Length == 0)
			{
				return false;
			}

			System.Text.StringBuilder builder = new System.Text.StringBuilder(name.Length);
			bool first = true;
			foreach (char c in name)
			{
				if (first)
				{
					if (c >= 'A' && c <= 'Z')
					{
						builder.Append(c);
						first = false;
					}
					else if (c >= 'a' && c <= 'z')
					{
						builder.Append(System.Char.ToUpper(c));
						first = false;
					}
					else
					{
						return false;
					}
				}
				else
				{
					if (c >= '0' && c <= '9')
					{
						builder.Append(c);
					}
					else if (c >= 'A' && c <= 'Z')
					{
						builder.Append(c);
					}
					else if (c >= 'a' && c <= 'z')
					{
						builder.Append(c);
					}
					else
					{
						return false;
					}
				}
			}

			name = builder.ToString();
			return true;
		}


		static public void ComboMenuAdd(TextFieldCombo combo, string text)
		{
			//	Ajoute un texte dans le combo-menu.
			if ( combo.Items.Contains(text) )
			{
				combo.Items.Remove(text);
			}
			combo.Items.Insert(0, text);
		}


		static public string GetUnicodeName(int code)
		{
			//	Retourne le nom d'un caract�re Unicode.
			if ( code == 0 )  return "";

			string text = TextBreak.GetUnicodeName(code);

			bool minus = false;
			for( int i=0 ; i<text.Length ; i++ )
			{
				if ( text[i] >= 'a' && text[i] <= 'z' )
				{
					minus = true;  // contient une lettre minuscule
					break;
				}
			}

			if ( !minus )  // aucune minuscule dans le texte ?
			{
				//	Premi�re lettre en majuscule, le reste en minuscules.
				text = string.Format("{0}{1}", text.Substring(0, 1).ToUpper(), text.Substring(1, text.Length-1).ToLower());
			}

			return text;
		}

		
		static public string Bold(string text)
		{
			//	Retourne le texte en gras.
			return string.Format("<b>{0}</b>", text);
		}

		static public string Italic(string text)
		{
			//	Retourne le texte en italique.
			return string.Format("<i>{0}</i>", text);
		}


		static public string Image(string icon)
		{
			//	Retourne le texte pour mettre une image dans un texte.
			return string.Format(@"<img src=""{0}""/>", Misc.Icon(icon));
		}

		static public string Image(string icon, double verticalOffset)
		{
			//	Retourne le texte pour mettre une image dans un texte.
			return string.Format(@"<img src=""{0}"" voff=""{1}""/>", Misc.Icon(icon), verticalOffset.ToString(System.Globalization.CultureInfo.InvariantCulture));
		}

		static public string ImageDyn(string name, string parameter)
		{
			//	Retourne le texte pour mettre une image dynamique dans un texte.
			return string.Format(@"<img src=""{0}""/>", Misc.IconDyn(name, parameter));
		}

		static public string ImageDyn(string name, string parameter, double verticalOffset)
		{
			//	Retourne le texte pour mettre une image dynamique dans un texte.
			return string.Format(@"<img src=""{0}"" voff=""{1}""/>", Misc.IconDyn(name, parameter), verticalOffset.ToString(System.Globalization.CultureInfo.InvariantCulture));
		}

		static public Size IconPreferredSize(string iconSize)
		{
			//	Retourne la taille pr�f�r�e pour une ic�ne. Si la taille r�elle de l'ic�ne n'est
			//	pas exactement identique, ce n'est pas important. Drawing.Canvas cherche au mieux.
			if ( iconSize == "Small" )  return new Size(14, 14);
			if ( iconSize == "Large" )  return new Size(31, 31);
			return new Size(20, 20);
		}

		static public string Icon(string icon)
		{
			//	Retourne le nom complet d'une ic�ne.
			return string.Format("manifest:Epsitec.Common.Designer.Images.{0}.icon", icon);
		}

		static public string IconDyn(string name, string parameter)
		{
			//	Retourne le nom complet d'une ic�ne dynamique.
			return string.Format("dyn:{0}/{1}", name, parameter);
		}


		static public string GetShortCut(Command command)
		{
			//	Retourne le nom des touches associ�es � une commande.
			if (command == null || command.HasShortcuts == false)
				return null;

			return command.PreferredShortcut.ToString();
		}

		static public string GetTextWithShortcut(Command command)
		{
			//	Donne le nom d'une commande, avec le raccourci clavier �ventuel entre parenth�ses.
			string shortcut = Misc.GetShortCut(command);

			if (shortcut == null)
			{
				return command.LongCaption;
			}
			else
			{
				return string.Format("{0} ({1})", command.LongCaption, shortcut);
			}
		}

		
		static public string FullName(string filename, bool dirtySerialize)
		{
			//	Donne le nom complet du fichier.
			//	Si le nom n'existe pas, donne "sans titre".
			//	Si le fichier doit �tre s�rialis�, donne le nom en gras.
			string name = "";
			if ( dirtySerialize )  name += "<b>";

			if ( filename == "" )
			{
				name += Res.Strings.Misc.NoTitle;
			}
			else
			{
				name += TextLayout.ConvertToTaggedText(filename);
			}

			if ( dirtySerialize )  name += "</b>";
			return name;
		}

		static public string ExtractName(string filename, bool dirtySerialize)
		{
			//	Extrait le nom de fichier, en ignorant les noms de dossiers et l'extension.
			//	Si le nom n'existe pas, donne "sans titre".
			//	Si le fichier doit �tre s�rialis�, donne le nom en gras.
			string name = "";
			if ( dirtySerialize )  name += "<b>";

			if ( filename == "" )
			{
				name += Res.Strings.Misc.NoTitle;
			}
			else
			{
				name += ExtractName(filename);
			}

			if ( dirtySerialize )  name += "</b>";
			return name;
		}

		static public string ExtractName(string filename)
		{
			//	Extrait le nom de fichier, en ignorant les noms de dossiers et l'extension.
			//	"c:\rep\abc.txt" devient "abc".
			return TextLayout.ConvertToTaggedText(System.IO.Path.GetFileNameWithoutExtension(filename));
		}

		static public bool IsExtension(string filename, string ext)
		{
			//	Indique si un fichier utilise une extension donn�e.
			return filename.ToLower().EndsWith(ext);
		}

		static public string CopyName(string name)
		{
			//	Retourne la copie d'un nom.
			//	"Bidon"              ->  "Copie de Bidon"
			//	"Copie de Bidon"     ->  "Copie (2) de Bidon"
			//	"Copie (2) de Bidon" ->  "Copie (3) de Bidon"
			return Misc.CopyName(name, Res.Strings.Misc.Copy, Res.Strings.Misc.CopyOf);
		}

		static public string CopyName(string name, string copy, string of)
		{
			//	Retourne la copie d'un nom.
			//	copy = "Copie" ou "Copy"
			//	of = "de" ou "of"
			if ( name == "" )
			{
				return copy;
			}

			if ( name.StartsWith(string.Concat(copy, " ", of, " ")) )
			{
				return string.Concat(copy, " (2) ", of, " ", name.Substring(copy.Length+of.Length+2));
			}

			if ( name.StartsWith(string.Concat(copy, " (")) )
			{
				int num = 0;
				int i = copy.Length+2;
				while ( name[i] >= '0' && name[i] <= '9' )
				{
					num *= 10;
					num += name[i++]-'0';
				}
				num ++;
				return string.Concat(copy, " (", num.ToString(), name.Substring(i));
			}

			return string.Concat(copy, " ", of, " ", name);
		}

		static public void Swap(ref bool a, ref bool b)
		{
			//	Permute deux variables.
			bool t = a;
			a = b;
			b = t;
		}

		static public void Swap(ref int a, ref int b)
		{
			int t = a;
			a = b;
			b = t;
		}

		static public void Swap(ref double a, ref double b)
		{
			double t = a;
			a = b;
			b = t;
		}

		static public void Swap(ref Point a, ref Point b)
		{
			Point t = a;
			a = b;
			b = t;
		}

		static public void Swap(ref Size a, ref Size b)
		{
			Size t = a;
			a = b;
			b = t;
		}
	}
}
