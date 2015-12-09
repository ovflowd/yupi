#region

using System;
using System.Collections.Generic;
using System.Drawing;

#endregion

namespace Yupi.Core.Algorithms.Astar
{
    /// <summary>
    /// Uses about 50 MB for a 1024x1024 grid.
    /// </summary>
    public class AStarSolver<TPathNode> where TPathNode : IPathNode
    {
        #region declares

        private delegate double CalculateHeuristicDelegate(PathNode inStart, PathNode inEnd);

        private CalculateHeuristicDelegate _calculationMethod;
        private static readonly double Sqrt2 = Math.Sqrt(2);
        public double TieBreaker { get; set; }
        private readonly bool _allowDiagonal;
        private PathNode _startNode;
        private PathNode _endNode;
        private bool[,] _mClosedSet;
        private bool[,] _mOpenSet;
        private PriorityQueue<PathNode, double> _mOrderedOpenSet;
        private PathNode[,] _mSearchSpace;
        private int _size;

        public TPathNode SearchSpace { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        #endregion

        #region constructor

        /// <summary>
        /// Creates a new AstarSolver
        /// </summary>
        /// <param name="inGrid">The inut grid</param>
        /// <param name="allowDiagonal">Indication if diagonal is allowed</param>
        /// <param name="calculator">The Calculator method</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public AStarSolver(bool allowDiagonal, AStarHeuristicType calculator, TPathNode inGrid, int width, int height)
        {
            SetHeuristictype(calculator);
            _allowDiagonal = allowDiagonal;
            PrepareMap(inGrid, width, height);
        }

        #endregion

        private void PrepareMap(TPathNode inGrid, int width, int height)
        {
            SearchSpace = inGrid;
            Width = width; //inGrid.GetLength(1);
            Height = height; //inGrid.GetLength(0);
            _size = Width * Height;
            _mSearchSpace = new PathNode[Height, Width];
            _mOrderedOpenSet = new PriorityQueue<PathNode, double>(PathNode.Comparer, Width + Height);
        }

        private void ResetSearchSpace()
        {
            for (var y = 0; y < Height; y++)
                for (var x = 0; x < Width; x++)
                    _mSearchSpace[y, x] = new PathNode(x, y, SearchSpace);
        }

        #region calculation types setting

        /// <summary>
        /// Sets the calculation type
        /// </summary>
        /// <param name="calculator"></param>
        private void SetHeuristictype(AStarHeuristicType calculator)
        {
            switch (calculator)
            {
                case AStarHeuristicType.FastSearch:
                    _calculationMethod = CalculateHeuristicFast;
                    break;

                case AStarHeuristicType.Between:
                    _calculationMethod = CalculateHeuristicBetween;
                    break;

                case AStarHeuristicType.ShortestPath:
                    _calculationMethod = CalculateHeuristicShortestRoute;
                    break;

                case AStarHeuristicType.ExperimentalSearch:
                    _calculationMethod = CalculateHeuristicExperimental;
                    break;
            }
        }

        protected virtual double CalculateHeuristicExperimental(PathNode inStart, PathNode inEnd)
        {
            return CalculateHeuristicFast(inStart, inEnd);
        }

        protected virtual double CalculateHeuristicFast(PathNode inStart, PathNode inEnd)
        {
            double dx1 = inStart.X - _endNode.X;
            double dy1 = inStart.Y - _endNode.Y;
            var cross = Math.Abs(dx1 - dy1);
            return Math.Ceiling(Math.Abs(inStart.X - inEnd.X) + (double)Math.Abs(inStart.Y - inEnd.Y)) + cross;
        }

        protected virtual double CalculateHeuristicBetween(PathNode inStart, PathNode inEnd)
        {
            double dx1 = inStart.X - _endNode.X;
            double dy1 = inStart.Y - _endNode.Y;
            double dx2 = _startNode.X - _endNode.X;
            double dy2 = _startNode.Y - _endNode.Y;
            var cross = Math.Abs(dx1 * dy2 - dx2 * dy1);
            return Math.Ceiling(Math.Abs(inStart.X - inEnd.X) + (double)Math.Abs(inStart.Y - inEnd.Y)) + cross;
        }

        protected virtual double CalculateHeuristicShortestRoute(PathNode inStart, PathNode inEnd)
        {
            return
                Math.Sqrt((inStart.X - inEnd.X) * (inStart.X - inEnd.X) + (inStart.Y - inEnd.Y) * (inStart.Y - inEnd.Y));
        }

        #endregion

        #region neighbour calculation

        /// <summary>
        /// Calculates the neighbour distance
        /// </summary>
        /// <param name="inStart">Start node</param>
        /// <param name="inEnd">End node</param>
        /// <returns></returns>
        protected virtual double NeighborDistance(PathNode inStart, PathNode inEnd)
        {
            var diffX = Math.Abs(inStart.X - inEnd.X);
            var diffY = Math.Abs(inStart.Y - inEnd.Y);

            switch (diffX + diffY)
            {
                case 1:
                    return 1;

                case 2:
                    return Sqrt2;

                default:
                    throw new ApplicationException();
            }
        }

        #endregion

        #region search algo

        /// <summary>
        /// Returns null, if no path is found. Start- and End-Node are included in returned path. The user context
        /// is passed to IsWalkable().
        /// </summary>
        public LinkedList<PathNode> Search(Point inEndNode, Point inStartNode)
        //TPathNode inGrid, int width, int height)
        {
            //prepareMap(inGrid, width, height);
            //if (width < inStartNode.X || height < inStartNode.Y)
            //    return null;
            //if (width < inEndNode.X || height < inEndNode.Y)
            //    return null;
            ResetSearchSpace();
            _mOrderedOpenSet = new PriorityQueue<PathNode, double>(PathNode.Comparer, Width + Height);

            _mClosedSet = new bool[Height, Width];
            _mOpenSet = new bool[Height, Width];

            _startNode = _mSearchSpace[inStartNode.Y, inStartNode.X];
            _endNode = _mSearchSpace[inEndNode.Y, inEndNode.X];

            if (_startNode == _endNode)
                return new LinkedList<PathNode>(new[] { _startNode });
            var neighborNodes = _allowDiagonal ? new PathNode[8] : new PathNode[4];

            TieBreaker = 0;

            _startNode.G = 0;
            _startNode.Optimal = _calculationMethod(_startNode, _endNode);
            TieBreaker = 1d / _startNode.Optimal;
            _startNode.F = _startNode.Optimal;

            _mOrderedOpenSet.Push(_startNode);
            _startNode.ExtraWeight = _size;

            PathNode x;

            while ((x = _mOrderedOpenSet.Pop()) != null)
            {
                if (x == _endNode)
                {
                    var result = ReconstructPath(x);

                    result.AddLast(_endNode);

                    return result;
                }

                _mClosedSet[x.Y, x.X] = true;

                if (_allowDiagonal)
                    StoreNeighborNodesDiagonal(x, neighborNodes);
                else
                    StoreNeighborNodesNoDiagonal(x, neighborNodes);

                foreach (var t in neighborNodes)
                {
                    var y = t;

                    if (y == null)
                        continue;

                    if (y.UserItem.IsBlocked(y.X, y.Y, (_endNode.X == y.X && _endNode.Y == y.Y)))
                        continue;

                    if (_mClosedSet[y.Y, y.X])
                        continue;

                    var trailScore = y.G + 1;
                    var wasAdded = false;

                    bool scoreResultBetter;
                    if (_mOpenSet[y.Y, y.X] == false)
                    {
                        _mOpenSet[y.Y, y.X] = true;
                        scoreResultBetter = true;
                        wasAdded = true;
                    }
                    else if (trailScore < y.G)
                        scoreResultBetter = true;
                    else
                        scoreResultBetter = false;

                    if (!scoreResultBetter)
                        continue;
                    y.Parent = x;

                    if (wasAdded)
                    {
                        y.G = trailScore;
                        y.Optimal = CalculateHeuristicBetween(y, _endNode) + (x.ExtraWeight - 10);
                        y.ExtraWeight = x.ExtraWeight - 10;
                        y.F = y.G + y.Optimal;
                        _mOrderedOpenSet.Push(y);
                    }
                    else
                    {
                        y.G = trailScore;
                        y.Optimal = CalculateHeuristicBetween(y, _endNode) + (x.ExtraWeight - 10);
                        _mOrderedOpenSet.Update(y, y.G + y.Optimal);
                        y.ExtraWeight = x.ExtraWeight - 10;
                    }
                }
            }

            return null;
        }

        #endregion

        #region neighbour storing

        private void StoreNeighborNodesDiagonal(PathNode inAround, PathNode[] inNeighbors)
        {
            var x = inAround.X;
            var y = inAround.Y;

            if ((x > 0) && (y > 0))
                inNeighbors[0] = _mSearchSpace[y - 1, x - 1];
            else
                inNeighbors[0] = null;

            if (y > 0)
                inNeighbors[1] = _mSearchSpace[y - 1, x];
            else
                inNeighbors[1] = null;

            if ((x < Width - 1) && (y > 0))
                inNeighbors[2] = _mSearchSpace[y - 1, x + 1];
            else
                inNeighbors[2] = null;

            if (x > 0)
                inNeighbors[3] = _mSearchSpace[y, x - 1];
            else
                inNeighbors[3] = null;

            if (x < Width - 1)
                inNeighbors[4] = _mSearchSpace[y, x + 1];
            else
                inNeighbors[4] = null;

            if ((x > 0) && (y < Height - 1))
                inNeighbors[5] = _mSearchSpace[y + 1, x - 1];
            else
                inNeighbors[5] = null;

            if (y < Height - 1)
                inNeighbors[6] = _mSearchSpace[y + 1, x];
            else
                inNeighbors[6] = null;

            if ((x < Width - 1) && (y < Height - 1))
                inNeighbors[7] = _mSearchSpace[y + 1, x + 1];
            else
                inNeighbors[7] = null;
        }

        private void StoreNeighborNodesNoDiagonal(PathNode inAround, PathNode[] inNeighbors)
        {
            var x = inAround.X;
            var y = inAround.Y;

            if (y > 0)
                inNeighbors[0] = _mSearchSpace[y - 1, x];
            else
                inNeighbors[0] = null;

            if (x > 0)
                inNeighbors[1] = _mSearchSpace[y, x - 1];
            else
                inNeighbors[1] = null;

            if (x < Width - 1)
                inNeighbors[2] = _mSearchSpace[y, x + 1];
            else
                inNeighbors[2] = null;

            if (y < Height - 1)
                inNeighbors[3] = _mSearchSpace[y + 1, x];
            else
                inNeighbors[3] = null;
        }

        #endregion

        #region reconstructPath

        private static LinkedList<PathNode> ReconstructPath(PathNode currentNode)
        {
            var result = new LinkedList<PathNode>();

            ReconstructPathRecursive(currentNode, result);

            return result;
        }

        private static void ReconstructPathRecursive(PathNode currentNode, LinkedList<PathNode> result)
        {
            var item = currentNode;
            result.AddFirst(item);
            while ((item = item.Parent) != null)
                result.AddFirst(item);
        }

        #endregion

        #region openmap

        //private class OpenCloseMap
        //{
        //    private PathNode[,] m_Map;
        //    public int Width { get; private set; }
        //    public int Height { get; private set; }
        //    public int Count { get; private set; }

        //    public PathNode this[Int32 x, Int32 y]
        //    {
        //        get
        //        {
        //            return m_Map[x, y];
        //        }
        //    }

        //    public PathNode this[PathNode Node]
        //    {
        //        get
        //        {
        //            return m_Map[Node.X, Node.Y];
        //        }

        //    }

        //    public bool IsEmpty
        //    {
        //        get
        //        {
        //            return Count == 0;
        //        }
        //    }

        //    public OpenCloseMap(int inWidth, int inHeight)
        //    {
        //        m_Map = new PathNode[inWidth, inHeight];
        //        Width = inWidth;
        //        Height = inHeight;
        //    }

        //    public void Add(PathNode inValue)
        //    {
        //        PathNode item = m_Map[inValue.X, inValue.Y];
        //        Count++;
        //        m_Map[inValue.X, inValue.Y] = inValue;
        //    }

        //    public bool Contains(PathNode inValue)
        //    {
        //        PathNode item = m_Map[inValue.X, inValue.Y];

        //        if (item == null)
        //            return false;
        //        return true;
        //    }

        //    public void Remove(PathNode inValue)
        //    {
        //        PathNode item = m_Map[inValue.X, inValue.Y];
        //        Count--;
        //        m_Map[inValue.X, inValue.Y] = null;
        //    }
        //}

        #endregion

        #region path node class

        public class PathNode : IPathNode, IComparer<PathNode>, IWeightAddable<double>
        {
            public static readonly PathNode Comparer = new PathNode(0, 0, default(TPathNode));

            public TPathNode UserItem { get; internal set; }
            public double G { get; internal set; }
            public double Optimal { get; internal set; }
            public double F { get; internal set; }

            public PathNode Parent { get; set; }

            public bool IsBlocked(int x, int y, bool lastTile)
            {
                return UserItem.IsBlocked(x, y, lastTile);
            }

            public int X { get; internal set; }
            public int Y { get; internal set; }
            public int ExtraWeight;

            public int Compare(PathNode x, PathNode y)
            {
                if (x.F < y.F)
                    return -1;
                return x.F > y.F ? 1 : 0;
            }

            public PathNode(int inX, int inY, TPathNode inUserContext)
            {
                X = inX;
                Y = inY;
                UserItem = inUserContext;
            }

            public double WeightChange
            {
                get { return F; }
                set { F = value; }
            }

            public bool BeenThere { get; set; }
        }

        #endregion
    }
}