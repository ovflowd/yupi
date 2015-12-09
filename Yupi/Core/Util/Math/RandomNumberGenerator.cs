using System;

namespace Yupi.Core.Util.Math
{
    /// <summary>
    /// Class RandomNumberGenerator.
    /// </summary>
    internal static class RandomNumberGenerator
    {
        /// <summary>
        /// The global random
        /// </summary>
        private static readonly Random GlobalRandom = new Random();

        /// <summary>
        /// The _local random
        /// </summary>
        [ThreadStatic]
        private static Random _localRandom;

        /// <summary>
        /// Gets the specified minimum.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>System.Int32.</returns>
        public static int Get(int min, int max)
        {
            var random = _localRandom;

            if (random != null)
                return random.Next(min, max);

            int seed;

            lock (GlobalRandom)
                seed = GlobalRandom.Next();

            random = (_localRandom = new Random(seed));

            return random.Next(min, max);
        }
    }
}