﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Assets.Server.SimpleEngine
{
	/// <summary>
	/// Gère l'accès "en arbre" à des données quelconques en provenance de inputNodes.
	/// </summary>
	public class TreeObjectsNodesGetter : AbstractNodesGetter<TreeNode>
	{
		public TreeObjectsNodesGetter(AbstractNodesGetter<LevelNode> inputNodes)
		{
			this.inputNodes = inputNodes;

			this.nodes       = new List<TreeNode> ();
			this.nodeIndexes = new List<int> ();
		}


		public override int Count
		{
			get
			{
				return this.nodeIndexes.Count;
			}
		}

		public override TreeNode this[int index]
		{
			get
			{
				return this.nodes[this.nodeIndexes[index]];
			}
		}


		public bool IsAllCompacted
		{
			get
			{
				return !this.nodes.Where (x => x.Type == NodeType.Expanded).Any ();
			}
		}

		public bool IsAllExpanded
		{
			get
			{
				return !this.nodes.Where (x => x.Type == NodeType.Compacted).Any ();
			}
		}


		public int SearchBestIndex(Guid value)
		{
			//	Retourne l'index ayant un Guid donné. Si la ligne correspondante
			//	est cachée, on est assez malin pour retourner la prochaine ligne
			//	visible, vers le haut.
			int index = -1;

			if (!value.IsEmpty)
			{
				var i = this.nodes.FindIndex (x => x.Guid == value);
				if (i != -1)
				{
					index = this.nodeIndexes.Where (x => x <= i).Count () - 1;
				}
			}

			return index;
		}


		public int VisibleToAll(int index)
		{
			if (index >= 0 && index < this.nodeIndexes.Count)
			{
				return this.nodeIndexes[index];
			}
			else
			{
				return -1;
			}
		}

		public int AllToVisible(int index)
		{
			return this.nodeIndexes.IndexOf (index);
		}


		public void CompactOrExpand(int index)
		{
			//	Etend ou compacte une ligne (inverse son mode actuel).
			int i = this.nodeIndexes[index];
			var node = this.nodes[i];

			if (node.Type == NodeType.Compacted)
			{
				this.nodes[i] = new TreeNode (node.Guid, node.Level, NodeType.Expanded);
			}
			else if (node.Type == NodeType.Expanded)
			{
				this.nodes[i] = new TreeNode (node.Guid, node.Level, NodeType.Compacted);
			}

			this.UpdateNodeIndexes ();
		}

		public void CompactAll()
		{
			//	Compacte toutes les lignes.
			for (int i=0; i<this.nodes.Count; i++)
			{
				var node = this.nodes[i];

				if (node.Type == NodeType.Expanded)
				{
					this.nodes[i] = new TreeNode (node.Guid, node.Level, NodeType.Compacted);
				}
			}

			this.UpdateNodeIndexes ();
		}

		public void ExpandAll()
		{
			//	Etend toutes les lignes.
			for (int i=0; i<this.nodes.Count; i++)
			{
				var node = this.nodes[i];

				if (node.Type == NodeType.Compacted)
				{
					this.nodes[i] = new TreeNode (node.Guid, node.Level, NodeType.Expanded);
				}
			}

			this.UpdateNodeIndexes ();
		}
	
		
		public void UpdateData()
		{
			//	Met à jour toutes les données en mode étendu.
			this.nodes.Clear ();

			int count = this.inputNodes.Count;
			for (int i=0; i<count; i++)
			{
				var currentNode = this.inputNodes[i];

				//	Par défaut, on considére que la ligne ne peut être ni étendue
				//	ni compactée.
				var type = NodeType.Final;

				if (i < count-2)
				{
					var nextNode = this.inputNodes[i+1];

					//	Si le noeud suivant a un niveau plus élevé, il s'agit d'une
					//	ligne pouvant être étendue ou compactée.
					if (nextNode.Level > currentNode.Level)
					{
						type = NodeType.Expanded;
					}
				}

				var node = new TreeNode (currentNode.Guid, currentNode.Level, type);
				this.nodes.Add (node);
			}

			this.UpdateNodeIndexes ();
		}


		private void UpdateNodeIndexes()
		{
			//	Met à jour l'accès aux noeuds (nodeIndexes) en sautant les
			//	noeuds cachés.
			this.nodeIndexes.Clear ();

			bool skip = false;
			int skipLevel = 0;

			for (int i=0; i<this.nodes.Count; i++)
			{
				var node = this.nodes[i];

				if (skip)
				{
					if (node.Level <= skipLevel)
					{
						skip = false;
					}
					else
					{
						continue;
					}
				}

				if (node.Type == NodeType.Compacted)
				{
					skip = true;
					skipLevel = node.Level;
				}

				this.nodeIndexes.Add (i);
			}
		}


		private readonly AbstractNodesGetter<LevelNode>	inputNodes;
		private readonly List<TreeNode>					nodes;
		private readonly List<int>						nodeIndexes;
	}
}
