// ---------------------------------------------------------------------------------
// <copyright file="WallCoordinate.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
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