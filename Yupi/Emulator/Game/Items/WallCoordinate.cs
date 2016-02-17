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

using Yupi.Core.Io;

namespace Yupi.Game.Items
{
    /// <summary>
    ///     Class WallCoordinate.
    /// </summary>
    internal class WallCoordinate
    {
        /// <summary>
        ///     The _length x
        /// </summary>
        private readonly int _lengthX;

        /// <summary>
        ///     The _length y
        /// </summary>
        private readonly int _lengthY;

        /// <summary>
        ///     The _side
        /// </summary>
        private readonly char _side;

        /// <summary>
        ///     The _width x
        /// </summary>
        private readonly int _widthX;

        /// <summary>
        ///     The _width y
        /// </summary>
        private readonly int _widthY;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WallCoordinate" /> class.
        /// </summary>
        /// <param name="wallPosition">The wall position.</param>
        public WallCoordinate(string wallPosition)
        {
            string[] posD = wallPosition.Split(' ');

            _side = posD[2] == "l" ? 'l' : 'r';

            string[] widD = posD[0].Substring(3).Split(',');

            _widthX = ServerUserChatTextHandler.Parse(widD[0]);
            _widthY = ServerUserChatTextHandler.Parse(widD[1]);

            string[] lenD = posD[1].Substring(2).Split(',');

            _lengthX = ServerUserChatTextHandler.Parse(lenD[0]);
            _lengthY = ServerUserChatTextHandler.Parse(lenD[1]);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WallCoordinate" /> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="n">The n.</param>
        public WallCoordinate(double x, double y, sbyte n)
        {
            ServerUserChatTextHandler.Split(x, out _widthX, out _widthY);
            ServerUserChatTextHandler.Split(y, out _lengthX, out _lengthY);

            _side = n == 7 ? 'r' : 'l';
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
            => ":w=" + _widthX + "," + _widthY + " " + "l=" + _lengthX + "," + _lengthY + " " + _side;

        /// <summary>
        ///     Generates the database shit.
        /// </summary>
        /// <returns>System.String.</returns>
        internal string GenerateDbShit()
            =>
                "x: " + ServerUserChatTextHandler.Combine(_widthX, _widthY) + " y: " +
                ServerUserChatTextHandler.Combine(_lengthX, _lengthY);

        /// <summary>
        ///     Gets the x value.
        /// </summary>
        /// <returns>System.Double.</returns>
        internal double GetXValue() => ServerUserChatTextHandler.Combine(_widthX, _widthY);

        /// <summary>
        ///     Gets the y value.
        /// </summary>
        /// <returns>System.Double.</returns>
        internal double GetYValue() => ServerUserChatTextHandler.Combine(_lengthX, _lengthY);

        /// <summary>
        ///     ns this instance.
        /// </summary>
        /// <returns>System.Int32.</returns>
        internal int N() => _side == 'l' ? 8 : 7;
    }
}