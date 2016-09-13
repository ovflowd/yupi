using System;
using System.Collections.Generic;
using System.Numerics;
using Priority_Queue;

namespace Yupi.Controller
{
	public class AStar
	{
		private class Node : IEquatable<Node>
		{
			public readonly Vector2 Tile;
			public float GScore;
			public Node Predecessor;

			public Node (float x, float y) : this (new Vector2 (x, y))
			{

			}

			public Node (Vector2 tile)
			{
				this.Tile = tile;
			}

			public bool Equals (Node other)
			{
				return this.Tile == other.Tile;
			}

			public override bool Equals(object obj)
			{
				return Equals(obj as Node);
			}

			public override int GetHashCode()
			{
				return this.Tile.GetHashCode ();
			}
		}

		private readonly SimplePriorityQueue<Node> OpenList;
		private readonly HashSet<Node> ClosedList;

		public AStar ()
		{
			OpenList = new SimplePriorityQueue<Node> ();
			ClosedList = new HashSet<Node> ();
		}

		public List<Vector2> Find (Vector2 start, Vector2 target)
		{
			OpenList.Enqueue (new Node (start), 0);

			Node targetNode = new Node (target);

			while (OpenList.Count > 0) {
				Node currentNode = OpenList.Dequeue ();

				if (currentNode.Tile == target) {
					return Reconstruct(currentNode);
				}

				ClosedList.Add (currentNode);

				expandNode (currentNode, targetNode);
			}

			return null;
		}

		private List<Vector2> Reconstruct(Node current) {
			List<Vector2> path = new List<Vector2> ();


			while (current != null) {
				path.Add (current.Tile);
				current = current.Predecessor;
			}

			return path;
		}

		private Node[] GetSuccessors (Node node)
		{
			return new Node[] {
				new Node (node.Tile.X - 1, node.Tile.Y),
				new Node (node.Tile.X - 1, node.Tile.Y - 1),
				new Node (node.Tile.X - 1, node.Tile.Y + 1),
				new Node (node.Tile.X + 1, node.Tile.Y),
				new Node (node.Tile.X + 1, node.Tile.Y - 1),
				new Node (node.Tile.X + 1, node.Tile.Y + 1),
				new Node (node.Tile.X, node.Tile.Y + 1),
				new Node (node.Tile.X, node.Tile.Y - 1),
			};
		}

		private void expandNode (Node currentNode, Node target)
		{
			foreach (Node succ in GetSuccessors(currentNode)) {
				if (ClosedList.Contains (succ)) {
					continue;
				}

				float tentative_g = currentNode.GScore + cost(currentNode, succ);

				if (OpenList.Contains (succ) && succ.GScore < tentative_g) {
					continue;
				}

				succ.GScore = tentative_g;
				succ.Predecessor = currentNode;

				float fScore = tentative_g + distance(succ, target);

				if (OpenList.Contains (succ)) {
					OpenList.UpdatePriority (succ, fScore);
				} else {
					OpenList.Enqueue (succ, fScore);
				}
			}
		}

		private float cost(Node prev, Node next) {
			return 1;
		}

		private float distance (Node start, Node target)
		{
			return (target.Tile - start.Tile).Length ();
		}
	}
}