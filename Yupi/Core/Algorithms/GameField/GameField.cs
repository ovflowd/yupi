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
using System.Linq;
using Yupi.Core.Algorithms.Astar;
using Yupi.Core.Algorithms.Astar.Enums;
using Yupi.Core.Algorithms.Astar.Interfaces;
using Yupi.Core.Algorithms.GameField.Algorithm;

namespace Yupi.Core.Algorithms.GameField
{
    public class GameField : IPathNode
    {
        private readonly AStarSolver<GameField> _astarSolver;

        private readonly bool _diagonal;
        private readonly Queue<GametileUpdate> _newEntries = new Queue<GametileUpdate>();
        private byte[,] _currentField;

        private GametileUpdate _currentlyChecking;

        public GameField(byte[,] theArray, bool diagonalAllowed)
        {
            _currentField = theArray;
            _diagonal = diagonalAllowed;
            _astarSolver = new AStarSolver<GameField>(diagonalAllowed, AStarHeuristicType.ExperimentalSearch, this,
                theArray.GetUpperBound(1) + 1, theArray.GetUpperBound(0) + 1);
        }

        public bool this[int y, int x]
            => y >= 0 && x >= 0 && y <= _currentField.GetUpperBound(0) && x <= _currentField.GetUpperBound(1);

        public bool IsBlocked(int x, int y, bool lastTile)
            => (_currentlyChecking.X == x && _currentlyChecking.Y == y) || GetValue(x, y) != _currentlyChecking.Value;

        public void UpdateLocation(int x, int y, byte value)
        {
            _newEntries.Enqueue(new GametileUpdate(x, y, value));
        }

        public List<PointField> DoUpdate(bool oneloop = false)
        {
            List<PointField> list = new List<PointField>();

            while (_newEntries.Count > 0)
            {
                _currentlyChecking = _newEntries.Dequeue();

                List<Point> connectedItems = GetConnectedItems(_currentlyChecking);

                if (connectedItems.Count > 1)
                {
                    IEnumerable<LinkedList<AStarSolver<GameField>.XPathNode>> list2 = HandleListOfConnectedPoints(connectedItems, _currentlyChecking);

                    list.AddRange(list2.Where(current => current.Count >= 4)
                        .Select(FindClosed)
                        .Where(pointField => true));
                }

                _currentField[_currentlyChecking.Y, _currentlyChecking.X] = _currentlyChecking.Value;
            }

            return list;
        }

        public byte GetValue(int x, int y) => this[y, x] ? _currentField[y, x] : (byte) 0;

        public byte GetValue(Point p) => this[p.Y, p.X] ? _currentField[p.Y, p.X] : (byte) 0;

        public void Destroy()
        {
            _currentField = null;
        }

        private PointField FindClosed(LinkedList<AStarSolver<GameField>.XPathNode> nodeList)
        {
            PointField pointField = new PointField(_currentlyChecking.Value);
            int num = 2147483647;
            int num2 = -2147483648;
            int num3 = 2147483647;
            int num4 = -2147483648;

            foreach (AStarSolver<GameField>.XPathNode current in nodeList)
            {
                if (current.X < num)
                    num = current.X;

                if (current.X > num2)
                    num2 = current.X;

                if (current.Y < num3)
                    num3 = current.Y;

                if (current.Y > num4)
                    num4 = current.Y;
            }

            int x = (int) Math.Ceiling((num2 - num)/2f) + num;
            int y = (int) Math.Ceiling((num4 - num3)/2f) + num3;

            List<Point> list = new List<Point>();
            List<Point> list2 = new List<Point> {new Point(_currentlyChecking.X, _currentlyChecking.Y)};

            list.Add(new Point(x, y));

            while (list.Any())
            {
                Point point = list[0];
                int x2 = point.X;
                int y2 = point.Y;

                if (x2 < num)
                    return null;

                if (x2 > num2)
                    return null;

                if (y2 < num3)
                    return null;

                if (y2 > num4)
                    return null;

                if (this[y2 - 1, x2] && _currentField[y2 - 1, x2] == 0)
                {
                    Point item = new Point(x2, y2 - 1);

                    if (!list.Contains(item) && !list2.Contains(item))
                        list.Add(item);
                }

                if (this[y2 + 1, x2] && _currentField[y2 + 1, x2] == 0)
                {
                    Point item = new Point(x2, y2 + 1);

                    if (!list.Contains(item) && !list2.Contains(item))
                        list.Add(item);
                }

                if (this[y2, x2 - 1] && _currentField[y2, x2 - 1] == 0)
                {
                    Point item = new Point(x2 - 1, y2);

                    if (!list.Contains(item) && !list2.Contains(item))
                        list.Add(item);
                }

                if (this[y2, x2 + 1] && _currentField[y2, x2 + 1] == 0)
                {
                    Point item = new Point(x2 + 1, y2);

                    if (!list.Contains(item) && !list2.Contains(item))
                        list.Add(item);
                }

                if (GetValue(point) == 0)
                    pointField.Add(point);

                list2.Add(point);
                list.RemoveAt(0);
            }

            return pointField;
        }

        private IEnumerable<LinkedList<AStarSolver<GameField>.XPathNode>> HandleListOfConnectedPoints(
            List<Point> pointList, GametileUpdate update)
        {
            List<LinkedList<AStarSolver<GameField>.XPathNode>> list = new List<LinkedList<AStarSolver<GameField>.XPathNode>>();
            int num = 0;

            foreach (Point current in pointList)
            {
                num++;

                if (num == pointList.Count/2 + 1)
                    return list;

                list.AddRange(
                    pointList.Where(current2 => !(current == current2))
                        .Select(current2 => _astarSolver.Search(current2, current))
                        .Where(linkedList => linkedList != null));
            }

            return list;
        }

        private List<Point> GetConnectedItems(GametileUpdate update)
        {
            List<Point> list = new List<Point>();
            int x = update.X;
            int y = update.Y;

            if (_diagonal)
            {
                if (this[y - 1, x - 1] && _currentField[y - 1, x - 1] == update.Value)
                    list.Add(new Point(x - 1, y - 1));

                if (this[y - 1, x + 1] && _currentField[y - 1, x + 1] == update.Value)
                    list.Add(new Point(x + 1, y - 1));

                if (this[y + 1, x - 1] && _currentField[y + 1, x - 1] == update.Value)
                    list.Add(new Point(x - 1, y + 1));

                if (this[y + 1, x + 1] && _currentField[y + 1, x + 1] == update.Value)
                    list.Add(new Point(x + 1, y + 1));
            }

            if (this[y - 1, x] && _currentField[y - 1, x] == update.Value)
                list.Add(new Point(x, y - 1));

            if (this[y + 1, x] && _currentField[y + 1, x] == update.Value)
                list.Add(new Point(x, y + 1));

            if (this[y, x - 1] && _currentField[y, x - 1] == update.Value)
                list.Add(new Point(x - 1, y));

            if (this[y, x + 1] && _currentField[y, x + 1] == update.Value)
                list.Add(new Point(x + 1, y));

            return list;
        }
    }
}