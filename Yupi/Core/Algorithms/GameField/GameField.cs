#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Yupi.Core.Algorithms.Astar;
using Yupi.Core.Algorithms.GameField.Algorithm;

#endregion

namespace Yupi.Core.Algorithms.GameField
{
    public class GameField : IPathNode
    {
        private readonly AStarSolver<GameField> _astarSolver;
        private readonly Queue<GametileUpdate> _newEntries = new Queue<GametileUpdate>();
        private readonly bool _diagonal;
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
        {
            get
            {
                return y >= 0 && x >= 0 && y <= _currentField.GetUpperBound(0) && x <= _currentField.GetUpperBound(1);
            }
        }

        public void UpdateLocation(int x, int y, byte value)
        {
            _newEntries.Enqueue(new GametileUpdate(x, y, value));
        }

        public List<PointField> DoUpdate(bool oneloop = false)
        {
            var list = new List<PointField>();
            while (_newEntries.Count > 0)
            {
                _currentlyChecking = _newEntries.Dequeue();
                var connectedItems = GetConnectedItems(_currentlyChecking);
                if (connectedItems.Count > 1)
                {
                    var list2 = HandleListOfConnectedPoints(connectedItems, _currentlyChecking);
                    list.AddRange(list2.Where(current => current.Count >= 4)
                        .Select(FindClosed)
                        .Where(pointField => true));
                }
                _currentField[_currentlyChecking.Y, _currentlyChecking.X] = _currentlyChecking.Value;
            }
            return list;
        }

        public byte GetValue(int x, int y)
        {
            return this[y, x] ? _currentField[y, x] : (byte)0;
        }

        public byte GetValue(Point p)
        {
            return this[p.Y, p.X] ? _currentField[p.Y, p.X] : (byte)0;
        }

        public bool IsBlocked(int x, int y, bool lastTile)
        {
            return (_currentlyChecking.X == x && _currentlyChecking.Y == y) || GetValue(x, y) != _currentlyChecking.Value;
        }

        public void Destroy()
        {
            _currentField = null;
        }

        private PointField FindClosed(LinkedList<AStarSolver<GameField>.PathNode> nodeList)
        {
            var pointField = new PointField(_currentlyChecking.Value);
            var num = 2147483647;
            var num2 = -2147483648;
            var num3 = 2147483647;
            var num4 = -2147483648;
            foreach (var current in nodeList)
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

            {
                var x = (int)Math.Ceiling((num2 - num) / 2f) + num;
                var y = (int)Math.Ceiling((num4 - num3) / 2f) + num3;
                var list = new List<Point>();
                var list2 = new List<Point> { new Point(_currentlyChecking.X, _currentlyChecking.Y) };
                list.Add(new Point(x, y));
                while (list.Any())
                {
                    var point = list[0];
                    var x2 = point.X;
                    var y2 = point.Y;
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
                        var item = new Point(x2, y2 - 1);
                        if (!list.Contains(item) && !list2.Contains(item))
                            list.Add(item);
                    }
                    if (this[y2 + 1, x2] && _currentField[y2 + 1, x2] == 0)
                    {
                        var item = new Point(x2, y2 + 1);
                        if (!list.Contains(item) && !list2.Contains(item))
                            list.Add(item);
                    }
                    if (this[y2, x2 - 1] && _currentField[y2, x2 - 1] == 0)
                    {
                        var item = new Point(x2 - 1, y2);
                        if (!list.Contains(item) && !list2.Contains(item))
                            list.Add(item);
                    }
                    if (this[y2, x2 + 1] && _currentField[y2, x2 + 1] == 0)
                    {
                        var item = new Point(x2 + 1, y2);
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
        }

        private IEnumerable<LinkedList<AStarSolver<GameField>.PathNode>> HandleListOfConnectedPoints(
            List<Point> pointList,
            GametileUpdate update)
        {
            var list = new List<LinkedList<AStarSolver<GameField>.PathNode>>();
            var num = 0;

            {
                foreach (var current in pointList)
                {
                    num++;
                    if (num == pointList.Count / 2 + 1)
                        return list;
                    list.AddRange(
                        pointList.Where(current2 => !(current == current2))
                            .Select(current2 => _astarSolver.Search(current2, current))
                            .Where(linkedList => linkedList != null));
                }
                return list;
            }
        }

        private List<Point> GetConnectedItems(GametileUpdate update)
        {
            var list = new List<Point>();
            var x = update.X;
            var y = update.Y;

            {
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
}