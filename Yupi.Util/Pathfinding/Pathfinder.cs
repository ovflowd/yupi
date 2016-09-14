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