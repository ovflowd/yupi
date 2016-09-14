// ---------------------------------------------------------------------------------
// <copyright file="AStar.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Util.Pathfinding
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Numerics;

    using Priority_Queue;

    public abstract class AStar<TileType>
        where TileType : IEquatable<TileType>
    {
        #region Fields

        private readonly HashSet<Node> ClosedList;
        private readonly SimplePriorityQueue<Node> OpenList;

        private Func<TileType, ICollection> GetNeighbours;
        private Func<TileType, bool> IsWalkable;
        private object Lock;

        #endregion Fields

        #region Constructors

        public AStar(Func<TileType, bool> isWalkable,
            Func<TileType, ICollection> getNeighbours)
        {
            Lock = new object();
            OpenList = new SimplePriorityQueue<Node>();
            ClosedList = new HashSet<Node>();
            this.IsWalkable = isWalkable;
            this.GetNeighbours = getNeighbours;
        }

        #endregion Constructors

        #region Methods

        public List<TileType> Find(TileType start, TileType target)
        {
            if (!IsWalkable(target) || !IsWalkable(start))
            {
                return null;
            }

            lock (this.Lock)
            {
                // Swap target & start to receive result in right order
                List<TileType> result = FindImpl(target, start);
                this.OpenList.Clear();
                this.ClosedList.Clear();
                return result;
            }
        }

        protected abstract float Cost(TileType prev, TileType next);

        protected abstract float GetDistance(TileType start, TileType target);

        private void expandNode(Node currentNode, Node target)
        {
            foreach (TileType succTile in GetNeighbours(currentNode.Tile))
            {
                Node succ = new Node(succTile);

                if (ClosedList.Contains(succ))
                {
                    continue;
                }

                if (!IsWalkable(succ.Tile))
                {
                    continue;
                }

                float tentative_g = currentNode.GScore + Cost(currentNode.Tile, succ.Tile);

                if (OpenList.Contains(succ) && succ.GScore < tentative_g)
                {
                    continue;
                }

                succ.GScore = tentative_g;
                succ.Predecessor = currentNode;

                float fScore = tentative_g + GetDistance(succ.Tile, target.Tile);

                if (OpenList.Contains(succ))
                {
                    OpenList.UpdatePriority(succ, fScore);
                }
                else
                {
                    OpenList.Enqueue(succ, fScore);
                }
            }
        }

        private List<TileType> FindImpl(TileType start, TileType target)
        {
            OpenList.Enqueue(new Node(start), 0);

            Node targetNode = new Node(target);

            while (OpenList.Count > 0)
            {
                Node currentNode = OpenList.Dequeue();

                if (currentNode.Tile.Equals(target))
                {
                    return Reconstruct(currentNode);
                }

                ClosedList.Add(currentNode);

                expandNode(currentNode, targetNode);
            }
            return null;
        }

        private List<TileType> Reconstruct(Node current)
        {
            List<TileType> path = new List<TileType>();

            while (current != null)
            {
                path.Add(current.Tile);
                current = current.Predecessor;
            }

            return path;
        }

        #endregion Methods

        #region Nested Types

        protected class Node : IEquatable<Node>
        {
            #region Fields

            public readonly TileType Tile;

            public float GScore;
            public Node Predecessor;

            #endregion Fields

            #region Constructors

            public Node(TileType tile)
            {
                this.Tile = tile;
            }

            #endregion Constructors

            #region Methods

            public bool Equals(Node other)
            {
                return this.Tile.Equals(other.Tile);
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Node);
            }

            public override int GetHashCode()
            {
                return this.Tile.GetHashCode();
            }

            #endregion Methods
        }

        #endregion Nested Types
    }
}