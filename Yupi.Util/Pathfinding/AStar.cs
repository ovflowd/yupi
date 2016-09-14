using System;
using System.Collections;
using System.Collections.Generic;

namespace Yupi.Util.Pathfinding
{
    public abstract class AStar<TileType> where TileType : IEquatable<TileType>
    {
        private readonly HashSet<Node> ClosedList;

        private readonly SimplePriorityQueue<Node> OpenList;
        private readonly Func<TileType, ICollection> GetNeighbours;

        private readonly Func<TileType, bool> IsWalkable;

        private readonly object Lock;

        public AStar(Func<TileType, bool> isWalkable,
            Func<TileType, ICollection> getNeighbours)
        {
            Lock = new object();
            OpenList = new SimplePriorityQueue<Node>();
            ClosedList = new HashSet<Node>();
            IsWalkable = isWalkable;
            GetNeighbours = getNeighbours;
        }

        protected abstract float GetDistance(TileType start, TileType target);
        protected abstract float Cost(TileType prev, TileType next);

        public List<TileType> Find(TileType start, TileType target)
        {
            if (!IsWalkable(target) || !IsWalkable(start)) return null;

            lock (Lock)
            {
                // Swap target & start to receive result in right order
                var result = FindImpl(target, start);
                OpenList.Clear();
                ClosedList.Clear();
                return result;
            }
        }

        private List<TileType> FindImpl(TileType start, TileType target)
        {
            OpenList.Enqueue(new Node(start), 0);

            var targetNode = new Node(target);

            while (OpenList.Count > 0)
            {
                Node currentNode = OpenList.Dequeue();

                if (currentNode.Tile.Equals(target)) return Reconstruct(currentNode);

                ClosedList.Add(currentNode);

                expandNode(currentNode, targetNode);
            }
            return null;
        }

        private List<TileType> Reconstruct(Node current)
        {
            var path = new List<TileType>();


            while (current != null)
            {
                path.Add(current.Tile);
                current = current.Predecessor;
            }

            return path;
        }

        private void expandNode(Node currentNode, Node target)
        {
            foreach (TileType succTile in GetNeighbours(currentNode.Tile))
            {
                var succ = new Node(succTile);

                if (ClosedList.Contains(succ)) continue;

                if (!IsWalkable(succ.Tile)) continue;

                var tentative_g = currentNode.GScore + Cost(currentNode.Tile, succ.Tile);

                if (OpenList.Contains(succ) && (succ.GScore < tentative_g)) continue;

                succ.GScore = tentative_g;
                succ.Predecessor = currentNode;

                var fScore = tentative_g + GetDistance(succ.Tile, target.Tile);

                if (OpenList.Contains(succ)) OpenList.UpdatePriority(succ, fScore);
                else OpenList.Enqueue(succ, fScore);
            }
        }

        protected class Node : IEquatable<Node>
        {
            public readonly TileType Tile;
            public float GScore;
            public Node Predecessor;

            public Node(TileType tile)
            {
                Tile = tile;
            }

            public bool Equals(Node other)
            {
                return Tile.Equals(other.Tile);
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Node);
            }

            public override int GetHashCode()
            {
                return Tile.GetHashCode();
            }
        }
    }
}