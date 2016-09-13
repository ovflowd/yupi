using System;
using System.Collections.Generic;
using System.Numerics;
using Priority_Queue;

namespace Yupi.Controller
{
	public class AStar<TileType> where TileType : IEquatable<TileType>
	{
		private class Node : IEquatable<Node>
		{
			public readonly TileType Tile;
			public float GScore;
			public Node Predecessor;

			public Node (TileType tile)
			{
				this.Tile = tile;
			}

			public bool Equals (Node other)
			{
				return this.Tile.Equals (other.Tile);
			}

			public override bool Equals (object obj)
			{
				return Equals (obj as Node);
			}

			public override int GetHashCode ()
			{
				return this.Tile.GetHashCode ();
			}
		}

		private readonly SimplePriorityQueue<Node> OpenList;
		private readonly HashSet<Node> ClosedList;

		private Func<TileType, bool> IsWalkable;
		private Func<TileType, TileType[]> GetNeighbours;
		private Func<TileType, TileType, float> GetDistance;

		private object Lock;

		public AStar (Func<TileType, bool> isWalkable, 
		              Func<TileType, TileType[]> getNeighbours, 
		              Func<TileType, TileType, float> getDistance)
		{
			Lock = new object ();
			OpenList = new SimplePriorityQueue<Node> ();
			ClosedList = new HashSet<Node> ();
			this.IsWalkable = isWalkable;
			this.GetNeighbours = getNeighbours;
			this.GetDistance = getDistance;
		}

		public List<TileType> Find (TileType start, TileType target)
		{
			lock (this.Lock) {
				List<TileType> result = FindImpl (start, target);
				this.OpenList.Clear ();
				this.ClosedList.Clear ();
				return result;
			}
		}

		private List<TileType> FindImpl (TileType start, TileType target)
		{
			if (!IsWalkable (target)) {
				return null;
			}

			OpenList.Enqueue (new Node (start), 0);

			Node targetNode = new Node (target);

			while (OpenList.Count > 0) {
				Node currentNode = OpenList.Dequeue ();

				if (currentNode.Tile.Equals (target)) {
					return Reconstruct (currentNode);
				}

				ClosedList.Add (currentNode);

				expandNode (currentNode, targetNode);
			}
			return null;
		}

		private List<TileType> Reconstruct (Node current)
		{
			List<TileType> path = new List<TileType> ();


			while (current != null) {
				path.Add (current.Tile);
				current = current.Predecessor;
			}

			return path;
		}

		private void expandNode (Node currentNode, Node target)
		{
			foreach (TileType succTile in GetNeighbours(currentNode.Tile)) {

				Node succ = new Node (succTile);

				if (ClosedList.Contains (succ)) {
					continue;
				}

				if (!IsWalkable (succ.Tile)) {
					continue;
				}

				float tentative_g = currentNode.GScore + cost (currentNode, succ);

				if (OpenList.Contains (succ) && succ.GScore < tentative_g) {
					continue;
				}

				succ.GScore = tentative_g;
				succ.Predecessor = currentNode;

				float fScore = tentative_g + GetDistance (succ.Tile, target.Tile);

				if (OpenList.Contains (succ)) {
					OpenList.UpdatePriority (succ, fScore);
				} else {
					OpenList.Enqueue (succ, fScore);
				}
			}
		}

		private float cost (Node prev, Node next)
		{
			return 1;
		}
	}
}