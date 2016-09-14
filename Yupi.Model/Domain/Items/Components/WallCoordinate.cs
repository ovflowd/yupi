#region Header

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

#endregion Header

namespace Yupi.Model.Domain.Components
{
    using System.Text.RegularExpressions;

    public class WallCoordinate
    {
        #region Fields

        protected int LengthX;
        protected int LengthY;
        protected WallSide Side;
        protected int WidthX;
        protected int WidthY;

        #endregion Fields

        #region Methods

        public static void TryParse(string wallPosition, out WallCoordinate coord)
        {
            coord = new WallCoordinate();
            // TODO Use regex?
            string[] posD = wallPosition.Split(' ');

            coord.Side = posD[2] == "l" ? WallSide.Left : WallSide.Right;

            string[] widD = posD[0].Substring(3).Split(',');

            int.TryParse(widD[0], out coord.WidthX);
            int.TryParse(widD[1], out coord.WidthY);

            string[] lenD = posD[1].Substring(2).Split(',');

            int.TryParse(lenD[0], out coord.LengthX);
            int.TryParse(lenD[1], out coord.LengthY);
        }

        public override string ToString()
        {
            return ":w=" + WidthX + "," + WidthY + " " + "l=" + LengthX + "," + LengthY + " " + (char) Side;
        }

        #endregion Methods
    }
}