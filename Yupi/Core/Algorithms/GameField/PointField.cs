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

using System.Collections.Generic;
using System.Drawing;

namespace Yupi.Core.Algorithms.GameField
{
    public class PointField
    {
        private static readonly Point BadPoint = new Point(-1, -1);
        private readonly List<Point> _pointList;
        private Point _mostDown = BadPoint;
        private Point _mostLeft = BadPoint;
        private Point _mostRight = BadPoint;
        private Point _mostTop = BadPoint;

        public PointField(byte forValue)
        {
            _pointList = new List<Point>();

            ForValue = forValue;
        }

        public byte ForValue { get; private set; }

        public List<Point> GetPoints() => _pointList;

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