﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Core.Entities;

namespace Epsitec.Cresus.Core.Controllers
{
	public class MainViewController : CoreViewController
	{
		public MainViewController(List<AbstractEntity> entities)
			: base ("MainView")
		{
			this.entities = entities;

			this.browserController = new BrowserViewController ("MainBrowser");
			this.dataViewController = new DataViewController ("MainViewer");


			this.browserController.SetContents (this.entities);
			this.browserController.CurrentChanged += sender => this.dataViewController.SelectEntity (this.browserController.ActiveEntity);

			this.CreateTileNodes ();
		}


		public override IEnumerable<CoreController> GetSubControllers()
		{
			yield return this.browserController;
			yield return this.dataViewController;
		}

		public override void CreateUI(Widget container)
		{
			System.Diagnostics.Debug.Assert (this.entities != null);

			this.frame = new FrameBox
			{
				Parent = container,
				Dock = DockStyle.Fill,
				DrawFullFrame = true,
			};

			//	Crée les panneaux gauche et droite séparés par un splitter.
			this.leftPanel = new FrameBox
			{
				Parent = this.frame,
				Name = "LeftPanel",
				Dock = DockStyle.Left,
				Padding = new Margins (5),
				PreferredWidth = 150,
			};

			this.rightPanel = new FrameBox
			{
				Parent = this.frame,
				Name = "RightPanel",
				Dock = DockStyle.Fill,
				Padding = new Margins (5, 0, 5, 5),
			};

			this.splitter = new VSplitter
			{
				Parent = this.frame,
				Dock = DockStyle.Left,
			};

			this.browserController.CreateUI (this.leftPanel);
			this.dataViewController.CreateUI (this.rightPanel);
		}


		private void CreateTiles(Widget embedder)
		{
			embedder.Children.Clear ();

			List<List<TileNode>> list = new List<List<TileNode>> ();
			this.CreateList (list, this.root);

			for (int i = list.Count-1; i >= 0; i--)
			{
				List<TileNode> line = list[i];

				FrameBox frame = new FrameBox (embedder);

				double posx, width;
				MainViewController.ComputeFrameGeometry (list.Count, i, out posx, out width);

				if (i == list.Count-1)
				{
					frame.Anchor = AnchorStyles.All;
					frame.Margins = new Margins (posx, 0, 0, 0);
				}
				else
				{
					frame.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left;
					frame.Margins = new Margins (posx, 0, 0, 0);
					frame.PreferredWidth = width;
				}

				this.CreateTileNodes (line, frame);
			}
		}

		private static void ComputeFrameGeometry(int columnsCount, int columnIndex, out double posx, out double width)
		{
			width = MainViewController.ComputeFrameWidth (columnsCount, columnIndex);
			posx = 0;

			for (int i = 0; i < columnIndex; i++)
			{
				posx += MainViewController.ComputeFrameWidth (columnsCount, i) - 5;
			}
		}

		private static double ComputeFrameWidth(int columnsCount, int columnIndex)
		{
			return 256/(columnsCount-columnIndex);
		}

		private void CreateList(List<List<TileNode>> list, TileNode node)
		{
			List<TileNode> line = new List<TileNode> ();

			foreach (TileNode children in node.TileNodes)
			{
				line.Add (children);
			}

			list.Add (line);

			foreach (TileNode children in node.TileNodes)
			{
				if (children.Selected)
				{
					this.CreateList (list, children);
				}
			}
		}

		private void CreateTileNodes(List<TileNode> nodes, Widget embedder)
		{
			for (int i = 0; i < nodes.Count; i++)
            {
				TileNode children = nodes[i];

				Widgets.SimpleTile tile = new Widgets.SimpleTile (embedder);
				tile.Data = children;
				tile.Dock = DockStyle.Top;
				tile.Margins = new Common.Drawing.Margins (0, 0, 0, (i<nodes.Count-1) ? -1:0);
				tile.IconUri = children.Icon;
				tile.Title = children.Title;
				tile.Content = children.Content;
				tile.SetSelected (children.Selected);
				tile.ArrowLocation = Direction.Right;
				tile.PreferredHeight = tile.ContentHeight;
				tile.Clicked += new EventHandler<MessageEventArgs> (this.HandleTileClicked);
			}
		}

		private void CreateTileNodes()
		{
			this.root = new TileNode (null, "", "Racine", "");

			TileNode client1 = new TileNode (this.root, "Data.Person", "Personne", "Daniel Roux");
			TileNode client2 = new TileNode (this.root, "Data.Person", "Employeur", "EPSITEC SA");

			this.root.TileNodes.Add (client1);
			this.root.TileNodes.Add (client2);

			//	Client1
			TileNode person1 = new TileNode (client1, "Data.Person", "Personne", "Daniel Roux<br/>Crésentine 33<br/>1023 Crissier");
			TileNode adress1 = new TileNode (client1, "Data.Mail", "Adresses", "Privé: Crésentine 33<br/>Prof: Epsitec SA");
			TileNode tel1 = new TileNode (client1, "Data.Telecom", "Téléphones", "Privé: 021 671 05 91<br/>Prof: 021 671 05 92<br/>Fax: 021 671 05 95");
			TileNode mail1 = new TileNode (client1, "Data.Uri", "Mails", "Prof: roux@epsitec.ch");

			client1.TileNodes.Add (person1);
			client1.TileNodes.Add (adress1);
			client1.TileNodes.Add (tel1);
			client1.TileNodes.Add (mail1);

			TileNode adress11 = new TileNode (adress1, "1", "Adresse (privé)", "Crésentine 33<br/>1023 Crisser<br/>Suisse");
			TileNode adress12 = new TileNode (adress1, "2", "Adresse (prof)", "Epsitec SA<br/>Ch. du Fontenay 3<br/>1400 Yverdon-les-Bains<br/>Suisse");
			TileNode tel11 = new TileNode (tel1, "1", "Téléphone (privé)", "021 671 05 91");
			TileNode tel12 = new TileNode (tel1, "2", "Téléphone (prof)", "021 671 05 92");
			TileNode tel13 = new TileNode (tel1, "3", "Téléphone (fax)", "021 671 05 95");
			TileNode mail11 = new TileNode (mail1, "1", "Mail (prof)", "roux@epsitec.ch");

			adress1.TileNodes.Add (adress11);
			adress1.TileNodes.Add (adress12);

			tel1.TileNodes.Add (tel11);
			tel1.TileNodes.Add (tel12);
			tel1.TileNodes.Add (tel13);

			mail1.TileNodes.Add (mail11);

			//	Client2
			TileNode person2 = new TileNode (client2, "Data.Person", "Employeur", "Pierre Arnaud<br/>Ch. du Fontenay 3<br/>1400 Yverdon-les-Bains");
			TileNode adress2 = new TileNode (client2, "Data.Mail", "Adresses", "Privé: Ch. du Fontenay 3<br/>Prof: Epsitec SA");
			TileNode tel2 = new TileNode (client2, "Data.Telecom", "Téléphones", "Privé: 024 425 08 09<br/>Natel: 079 367 45 97<br/>Prof: 0848 27 37 87");
			TileNode mail2 = new TileNode (client2, "Data.Uri", "Mails", "Privé: parnaud@hotmail.com<br/>Prof: arnaud@epsitec.ch<br/>Prof: opac@opac.com<br/>Prof: bonjour@opac.com");
			TileNode rem2 = new TileNode (client2, "R", "Remarque", "La Ligne Crésus propose des logiciels puissants,<br/>flexibles et surtout conviviaux et faciles d'emploi.<br/>Ces produits sont parfaitement adaptés au marché suisse;<br/>ils existent aussi en allemand sous le nom de Krösus.<br/><br/>Depuis 1994, EPSITEC SA a étendu ses activités au monde PC.<br/>Le logiciel Crésus Comptabilité, développé par Daniel Roux sur Smaky,<br/>a été transporté d'abord sous Windows 3.<br/>Il s'est rapidement imposé en Suisse romande.");

			client2.TileNodes.Add (person2);
			client2.TileNodes.Add (adress2);
			client2.TileNodes.Add (tel2);
			client2.TileNodes.Add (mail2);
			client2.TileNodes.Add (rem2);

			TileNode adress21 = new TileNode (adress2, "1", "Adresse (privé)", "Ch. du Fontenay 6<br/>1400 Yverdon-les-Bains<br/>Suisse");
			TileNode adress22 = new TileNode (adress2, "2", "Adresse (prof)", "Epsitec SA<br/>Ch. du Fontenay 3<br/>1400 Yverdon-les-Bains<br/>Suisse");
			TileNode tel21 = new TileNode (tel2, "1", "Téléphone (privé)", "024 425 08 09");
			TileNode tel22 = new TileNode (tel2, "2", "Téléphone (natel)", "079 367 45 97");
			TileNode tel23 = new TileNode (tel2, "3", "Téléphone (prof)", "0848 27 37 87");
			TileNode mail21 = new TileNode (mail2, "1", "Mail (privé)", "parnaud@hotmail.com");
			TileNode mail22 = new TileNode (mail2, "2", "Mail (prof)", "arnaud@epsitec.ch");
			TileNode mail23 = new TileNode (mail2, "3", "Mail (prof)", "opac@opac.com");
			TileNode mail24 = new TileNode (mail2, "4", "Mail (prof)", "bonjour@opac.com");

			adress2.TileNodes.Add (adress21);
			adress2.TileNodes.Add (adress22);

			tel2.TileNodes.Add (tel21);
			tel2.TileNodes.Add (tel22);
			tel2.TileNodes.Add (tel23);

			mail2.TileNodes.Add (mail21);
			mail2.TileNodes.Add (mail22);
			mail2.TileNodes.Add (mail23);
			mail2.TileNodes.Add (mail24);
		}

		private static void SelectNode(TileNode node)
		{
			if (node.Selected)
			{
				MainViewController.DeselectNode (node);
			}
			else
			{
				if (node.Parent != null)
				{
					foreach (TileNode brother in node.Parent.TileNodes)
					{
						MainViewController.DeselectNode (brother);
					}
				}

				if (node.TileNodes.Count != 0)
				{
					node.Selected = true;
				}
			}
		}

		private static void DeselectNode(TileNode node)
		{
			node.Selected = false;

			foreach (TileNode children in node.TileNodes)
			{
				MainViewController.DeselectNode (children);
			}
		}


		private class TileNode
		{
			public TileNode(TileNode parent, string icon, string title, string content)
			{
				this.tileNodes = new List<TileNode> ();
				this.Icon = icon;
				this.Parent = parent;
				this.Title = title;
				this.Content = content;
			}

			public TileNode Parent
			{
				get;
				set;
			}

			public string Icon
			{
				get;
				set;
			}

			public string Title
			{
				get;
				set;
			}

			public string Content
			{
				get;
				set;
			}

			public bool Selected
			{
				get;
				set;
			}

			public List<TileNode> TileNodes
			{
				get
				{
					return this.tileNodes;
				}
			}

			private List<TileNode> tileNodes;
		}



		private void HandleTileClicked(object sender, MessageEventArgs e)
		{
			Widgets.SimpleTile tile = sender as Widgets.SimpleTile;
			TileNode node = tile.Data as TileNode;

			MainViewController.SelectNode (node);
			this.CreateTiles (this.rightPanel);
		}



		private readonly BrowserViewController browserController;
		private readonly DataViewController dataViewController;

		private List<AbstractEntity> entities;

		private FrameBox frame;

		private FrameBox leftPanel;
		private VSplitter splitter;
		private FrameBox rightPanel;

		private TileNode root;
	}
}
