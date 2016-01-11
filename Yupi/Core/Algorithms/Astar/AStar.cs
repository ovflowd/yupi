/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using Yupi.Core.Algorithms.Astar.Enums;
using Yupi.Core.Algorithms.Astar.Interfaces;

namespace Yupi.Core.Algorithms.Astar
{
    /// <summary>
    ///     Uses about 50 MB for a 1024x1024 grid.
    /// </summary>
    public class AStarSolver<TPathNode> where TPathNode : IPathNode
    {
        private readonly bool _allowDiagonal;

        private CalculateHeuristicDelegate _calculationMethod;
        private XPathNode _endNode;
        private bool[,] _mClosedSet;
        private bool[,] _mOpenSet;
        private PriorityQueue<XPathNode, double> _mOrderedOpenSet;
        private XPathNode[,] _mSearchSpace;
        private int _size;
        private XPathNode _startNode;

        /// <summary>
        ///     Creates a new AstarSolver
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

        public double TieBreaker { get; set; }

        public TPathNode SearchSpace { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        private void PrepareMap(TPathNode inGrid, int width, int height)
        {
            SearchSpace = inGrid;

            Width = width;
            Height = height;
            _size = Width*Height;

            _mSearchSpace = new XPathNode[Height, Width];

            _mOrderedOpenSet = new PriorityQueue<XPathNode, double>(PathNode.Comparer, Width + Height);
        }

        private void ResetSearchSpace()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    _mSearchSpace[y, x] = new XPathNode(x, y, SearchSpace);
        }

        /// <summary>
        ///     Sets the calculation type
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

        protected virtual double CalculateHeuristicExperimental(XPathNode inStart, XPathNode inEnd)
        {
            return CalculateHeuristicFast(inStart, inEnd);
        }

        protected virtual double CalculateHeuristicFast(XPathNode inStart, XPathNode inEnd)
        {
            double dx1 = inStart.X - _endNode.X;
            double dy1 = inStart.Y - _endNode.Y;
            double cross = Math.Abs(dx1 - dy1);
            return Math.Ceiling(Math.Abs(inStart.X - inEnd.X) + (double) Math.Abs(inStart.Y - inEnd.Y)) + cross;
        }

        protected virtual double CalculateHeuristicBetween(XPathNode inStart, XPathNode inEnd)
        {
            double dx1 = inStart.X - _endNode.X;
            double dy1 = inStart.Y - _endNode.Y;
            double dx2 = _startNode.X - _endNode.X;
            double dy2 = _startNode.Y - _endNode.Y;
            double cross = Math.Abs(dx1*dy2 - dx2*dy1);
            return Math.Ceiling(Math.Abs(inStart.X - inEnd.X) + (double) Math.Abs(inStart.Y - inEnd.Y)) + cross;
        }

        protected virtual double CalculateHeuristicShortestRoute(XPathNode inStart, XPathNode inEnd)
        {
            return
                Math.Sqrt((inStart.X - inEnd.X)*(inStart.X - inEnd.X) + (inStart.Y - inEnd.Y)*(inStart.Y - inEnd.Y));
        }

        /// <summary>
        ///     Calculates the neighbour distance
        /// </summary>
        /// <param name="inStart">Start node</param>
        /// <param name="inEnd">End node</param>
        /// <returns></returns>
        protected virtual double NeighborDistance(XPathNode inStart, XPathNode inEnd)
        {
            int diffX = Math.Abs(inStart.X - inEnd.X);
            int diffY = Math.Abs(inStart.Y - inEnd.Y);

            switch (diffX + diffY)
            {
                case 1:
                    return 1;
                case 2:
                    return Math.Sqrt(2);
                default:
                    throw new ApplicationException();
            }
        }

        /// <summary>
        ///     Returns null, if no path is found. Start- and End-Node are included in returned path. The user context
        ///     is passed to IsWalkable().
        /// </summary>
        public LinkedList<XPathNode> Search(Point inEndNode, Point inStartNode)
        {
            ResetSearchSpace();
            _mOrderedOpenSet = new PriorityQueue<XPathNode, double>(PathNode.Comparer, Width + Height);

            _mClosedSet = new bool[Height, Width];
            _mOpenSet = new bool[Height, Width];

            _startNode = _mSearchSpace[inStartNode.Y, inStartNode.X];
            _endNode = _mSearchSpace[inEndNode.Y, inEndNode.X];

            if (_startNode == _endNode)
                return new LinkedList<XPathNode>(new[] {_startNode});

            XPathNode[] neighborNodes = _allowDiagonal ? new XPathNode[8] : new XPathNode[4];

            TieBreaker = 0;

            _startNode.G = 0;
            _startNode.Optimal = _calculationMethod(_startNode, _endNode);
            TieBreaker = 1d/_startNode.Optimal;
            _startNode.F = _startNode.Optimal;

            _mOrderedOpenSet.Push(_startNode);
            _startNode.ExtraWeight = _size;

            XPathNode x;

            while ((x = _mOrderedOpenSet.Pop()) != null)
            {
                if (x == _endNode)
                {
                    LinkedList<XPathNode> result = ReconstructPath(x);

                    result.AddLast(_endNode);

                    return result;
                }

                _mClosedSet[x.Y, x.X] = true;

                if (_allowDiagonal)
                    StoreNeighborNodesDiagonal(x, neighborNodes);
                else
                    StoreNeighborNodesNoDiagonal(x, neighborNodes);

                foreach (XPathNode t in neighborNodes)
                {
                    XPathNode y = t;

                    if (y == null)
                        continue;

                    if (y.UserItem.IsBlocked(y.X, y.Y, _endNode.X == y.X && _endNode.Y == y.Y))
                        continue;

                    if (_mClosedSet[y.Y, y.X])
                        continue;

                    double trailScore = y.G + 1;
                    bool wasAdded = false;

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

        private void StoreNeighborNodesDiagonal(XPathNode inAround, XPathNode[] inNeighbors)
        {
            int x = inAround.X;
            int y = inAround.Y;

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

        private void StoreNeighborNodesNoDiagonal(XPathNode inAround, XPathNode[] inNeighbors)
        {
            int x = inAround.X;
            int y = inAround.Y;

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

        private static LinkedList<XPathNode> ReconstructPath(XPathNode currentNode)
        {
            LinkedList<XPathNode> result = new LinkedList<XPathNode>();

            ReconstructPathRecursive(currentNode, result);

            return result;
        }

        private static void ReconstructPathRecursive(PathNode currentNode, LinkedList<XPathNode> result)
        {
            PathNode item = currentNode;

            result.AddFirst((XPathNode) item);

            while ((item = item.Parent) != null)
                result.AddFirst((XPathNode) item);
        }

        private delegate double CalculateHeuristicDelegate(XPathNode inStart, XPathNode inEnd);

        public class XPathNode : PathNode
        {
            public XPathNode(int inX, int inY, IPathNode inUserContext) : base(inX, inY, inUserContext)
            {
                X = inX;
                Y = inY;
                UserItem = inUserContext;
            }
        }
    }
}