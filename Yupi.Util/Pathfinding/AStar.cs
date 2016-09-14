using System;
using System.Collections.Generic;
using System.Numerics;
using Priority_Queue;
using System.Collections;

namespace Yupi.Util.Pathfinding
{
	public abstract class AStar<TileType> where TileType : IEquatable<TileType>
	{
		protected class Node : IEquatable<Node>
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
		private Func<TileType, ICollection> GetNeighbours;

		private object Lock;

		public AStar (Func<TileType, bool> isWalkable, 
			Func<TileType, ICollection> getNeighbours)
		{
			Lock = new object ();
			OpenList = new SimplePriorityQueue<Node> ();
			ClosedList = new HashSet<Node> ();
			this.IsWalkable = isWalkable;
			this.GetNeighbours = getNeighbours;
		}

		protected abstract float GetDistance(TileType start, TileType target);
		protected abstract float Cost (TileType prev, TileType next);

		public List<TileType> Find (TileType start, TileType target)
		{
			if (!IsWalkable (target) || !IsWalkable(start)) {
				return null;
			}

			lock (this.Lock) {
				// Swap target & start to receive result in right order
				List<TileType> result = FindImpl (target, start);
				this.OpenList.Clear ();
				this.ClosedList.Clear ();
				return result;
			}
		}

		private List<TileType> FindImpl (TileType start, TileType target)
		{
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

				float tentative_g = currentNode.GScore + Cost (currentNode.Tile, succ.Tile);

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
	}
}