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
using Yupi.Core.Algorithms.Astar.Interfaces;

namespace Yupi.Core.Algorithms.Astar
{
    public class PathNode : IPathNode, IComparer<PathNode>, IWeightAddable<double>
    {
        public static readonly PathNode Comparer = new PathNode(0, 0, default(IPathNode));
        public int ExtraWeight;

        public PathNode(int inX, int inY, IPathNode inUserContext)
        {
            X = inX;
            Y = inY;
            UserItem = inUserContext;
        }

        public IPathNode UserItem { get; internal set; }
        public double G { get; internal set; }
        public double Optimal { get; internal set; }
        public double F { get; internal set; }

        public PathNode Parent { get; set; }

        public int X { get; internal set; }
        public int Y { get; internal set; }

        public bool BeenThere { get; set; }

        public int Compare(PathNode x, PathNode y)
        {
            if (x.F < y.F)
                return -1;

            return x.F > y.F ? 1 : 0;
        }

        public bool IsBlocked(int x, int y, bool lastTile) => UserItem.IsBlocked(x, y, lastTile);

        public double WeightChange
        {
            get { return F; }
            set { F = value; }
        }
    }
}