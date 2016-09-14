// ---------------------------------------------------------------------------------
// <copyright file="Pathfinder.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Util.Pathfinding
{
    using System;
    using System.Collections;
    using System.Numerics;

    using Yupi.Util.Pathfinding;

    public class Pathfinder : AStar<Vector2>
    {
        #region Fields

        private const float DiagonalCost = 1.1f;
        private const float StraightCost = 1;

        #endregion Fields

        #region Constructors

        public Pathfinder(Func<Vector2, bool> isWalkable, Func<Vector2, ICollection> getNeighbours)
            : base(isWalkable, getNeighbours)
        {
        }

        #endregion Constructors

        #region Methods

        protected override float Cost(Vector2 prev, Vector2 next)
        {
            if (prev.X == next.X || prev.Y == next.Y)
            {
                return StraightCost;
            }
            else
            {
                return DiagonalCost;
            }
        }

        protected override float GetDistance(Vector2 start, Vector2 goal)
        {
            Vector2 diff = Vector2.Abs(start - goal);

            float h_diagonal = Math.Min(diff.X, diff.Y);
            float h_straight = diff.X + diff.Y;

            return DiagonalCost*h_diagonal + StraightCost*(h_straight - 2*h_diagonal);
        }

        #endregion Methods
    }
}