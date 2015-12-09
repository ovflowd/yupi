using System;

namespace Yupi.Game.Pathfinding
{
    /// <summary>
    ///     Class PathFinderNode.
    /// </summary>
    internal class PathFinderNode : IComparable<PathFinderNode>
    {
        /// <summary>
        ///     The position
        /// </summary>
        public readonly Vector2D Position;

        /// <summary>
        ///     The cost
        /// </summary>
        public int Cost = 2147483647;

        /// <summary>
        ///     The in closed
        /// </summary>
        public bool InClosed;

        /// <summary>
        ///     The in open
        /// </summary>
        public bool InOpen;

        /// <summary>
        ///     The next
        /// </summary>
        public PathFinderNode Next;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PathFinderNode" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public PathFinderNode(Vector2D position)
        {
            Position = position;
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        public int CompareTo(PathFinderNode other) => Cost.CompareTo(other.Cost);

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj) => obj is PathFinderNode && ((PathFinderNode) obj).Position.Equals(Position);

        /// <summary>
        ///     Equalses the specified bread crumb.
        /// </summary>
        /// <param name="breadCrumb">The bread crumb.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(PathFinderNode breadCrumb) => breadCrumb.Position.Equals(Position);

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() => Position.GetHashCode();
    }
}