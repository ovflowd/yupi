namespace Yupi.Game.Groups.Interfaces
{
    /// <summary>
    ///     Struct GroupBackGroundColours
    /// </summary>
    internal struct GroupBackGroundColours
    {
        /// <summary>
        ///     The identifier
        /// </summary>
        internal int Id;

        /// <summary>
        ///     The colour
        /// </summary>
        internal string Colour;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GroupBackGroundColours" /> struct.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="colour">The colour.</param>
        internal GroupBackGroundColours(int id, string colour)
        {
            Id = id;
            Colour = colour;
        }
    }
}