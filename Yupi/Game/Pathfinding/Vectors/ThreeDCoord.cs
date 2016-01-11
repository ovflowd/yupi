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

namespace Yupi.Game.Pathfinding.Vectors
{
    /// <summary>
    ///     Struct ThreeDCoord
    /// </summary>
    internal struct ThreeDCoord : IEquatable<ThreeDCoord>
    {
        /// <summary>
        ///     The x
        /// </summary>
        internal readonly int X;

        /// <summary>
        ///     The y
        /// </summary>
        internal readonly int Y;

        /// <summary>
        ///     The z
        /// </summary>
        internal readonly int Z;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ThreeDCoord" /> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        internal ThreeDCoord(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        ///     Equalses the specified compared coord.
        /// </summary>
        /// <param name="comparedCoord">The compared coord.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(ThreeDCoord comparedCoord)
            => X == comparedCoord.X && Y == comparedCoord.Y && Z == comparedCoord.Z;

        /// <summary>
        ///     Implements the ==.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ThreeDCoord a, ThreeDCoord b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;

        /// <summary>
        ///     Implements the !=.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ThreeDCoord a, ThreeDCoord b) => !(a == b);

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() => X ^ Y ^ Z;

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj) => obj != null && base.GetHashCode().Equals(obj.GetHashCode());
    }
}