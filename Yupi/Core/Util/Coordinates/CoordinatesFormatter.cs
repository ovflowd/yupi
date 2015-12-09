using System.Drawing;

namespace Yupi.Core.Util.Coordinates
{
    /// <summary>
    /// Class CoordinatesFormatter.
    /// </summary>
    internal class CoordinatesFormatter
    {
        /// <summary>
        /// Points to int.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>System.Int32.</returns>
        internal static int PointToInt(Point p) => CombineXyCoord(p.X, p.Y);

        /// <summary>
        /// Combines the xy coord.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>System.Int32.</returns>
        internal static int CombineXyCoord(int a, int b) => (a << 16) | b;
    }
}