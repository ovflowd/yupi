#region

using System.Collections.Generic;
using System.Drawing;

#endregion

namespace Yupi.Core.Algorithms.GameField
{
    public class PointField
    {
        private static readonly Point BadPoint = new Point(-1, -1);
        private readonly List<Point> _pointList;
        private Point _mostLeft = BadPoint;
        private Point _mostTop = BadPoint;
        private Point _mostRight = BadPoint;
        private Point _mostDown = BadPoint;

        public PointField(byte forValue)
        {
            _pointList = new List<Point>();
            ForValue = forValue;
        }

        public byte ForValue { get; private set; }

        public List<Point> GetPoints()
        {
            return _pointList;
        }

        public void Add(Point p)
        {
            if (_mostLeft == BadPoint)
                _mostLeft = p;
            if (_mostRight == BadPoint)
                _mostRight = p;
            if (_mostTop == BadPoint)
                _mostTop = p;
            if (_mostDown == BadPoint)
                _mostDown = p;
            if (p.X < _mostLeft.X)
                _mostLeft = p;
            if (p.X > _mostRight.X)
                _mostRight = p;
            if (p.Y > _mostTop.Y)
                _mostTop = p;
            if (p.Y < _mostDown.Y)
                _mostDown = p;
            _pointList.Add(p);
        }
    }
}