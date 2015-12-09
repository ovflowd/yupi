namespace Yupi.Game.Pathfinding
{
    /// <summary>
    ///     Class Rotation.
    /// </summary>
    internal static class Rotation
    {
        /// <summary>
        ///     Calculates the specified x1.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <returns>System.Int32.</returns>
        internal static int Calculate(int x1, int y1, int x2, int y2)
        {
            var result = 0;

            if (x1 > x2 && y1 > y2)
                result = 7;
            else if (x1 < x2 && y1 < y2)
                result = 3;
            else if (x1 > x2 && y1 < y2)
                result = 5;
            else if (x1 < x2 && y1 > y2)
                result = 1;
            else if (x1 > x2)
                result = 6;
            else if (x1 < x2)
                result = 2;
            else if (y1 < y2)
                result = 4;
            else if (y1 > y2)
                result = 0;

            return result;
        }

        /// <summary>
        ///     Calculates the specified x1.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="moonwalk">if set to <c>true</c> [moonwalk].</param>
        /// <returns>System.Int32.</returns>
        internal static int Calculate(int x1, int y1, int x2, int y2, bool moonwalk)
        {
            var num = Calculate(x1, y1, x2, y2);

            return !moonwalk ? num : RotationIverse(num);
        }

        /// <summary>
        ///     Rotations the iverse.
        /// </summary>
        /// <param name="rot">The rot.</param>
        /// <returns>System.Int32.</returns>
        internal static int RotationIverse(int rot)
        {
            if (rot > 3)
                rot -= 4;
            else
                rot += 4;

            return rot;
        }
    }
}