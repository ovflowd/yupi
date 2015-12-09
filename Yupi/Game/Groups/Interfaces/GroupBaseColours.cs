namespace Yupi.Game.Groups.Interfaces
{
    /// <summary>
    ///     Struct GroupBaseColours
    /// </summary>
    internal struct GroupBaseColours
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
        ///     Initializes a new instance of the <see cref="GroupBaseColours" /> struct.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="colour">The colour.</param>
        internal GroupBaseColours(int id, string colour)
        {
            Id = id;
            Colour = colour;
        }
    }
}