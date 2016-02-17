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

namespace Yupi.Game.Pathfinding.Vectors
{
    /// <summary>
    ///     Class Vector2D.
    /// </summary>
    internal class Vector2D
    {
        /// <summary>
        ///     The zero
        /// </summary>
        public static Vector2D Zero = new Vector2D(0, 0);

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2D" /> class.
        /// </summary>
        public Vector2D()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2D" /> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///     Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        public int X { get; }

        /// <summary>
        ///     Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        public int Y { get; }

        /// <summary>
        ///     Gets the distance squared.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>System.Int32.</returns>
        public int GetDistanceSquared(Vector2D point)
        {
            int num = X - point.X;
            int num2 = Y - point.Y;

            return num*num + num2*num2;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            Vector2D vector2D = obj as Vector2D;

            return vector2D?.X == X && vector2D.Y == Y;
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() => $"{X} {Y}".GetHashCode();

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => $"{X}, {Y}";

        /// <summary>
        ///     Implements the +.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector2D operator +(Vector2D one, Vector2D two) => new Vector2D(one.X + two.X, one.Y + two.Y);

        /// <summary>
        ///     Implements the -.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="two">The two.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector2D operator -(Vector2D one, Vector2D two) => new Vector2D(one.X - two.X, one.Y - two.Y);

        public static implicit operator List<object>(Vector2D v)
        {
            throw new NotImplementedException();
        }
    }
}