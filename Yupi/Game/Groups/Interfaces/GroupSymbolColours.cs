namespace Yupi.Game.Groups.Interfaces
{
    /// <summary>
    ///     Struct GroupSymbolColours
    /// </summary>
    internal struct GroupSymbolColours
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
        ///     Initializes a new instance of the <see cref="GroupSymbolColours" /> struct.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="colour">The colour.</param>
        internal GroupSymbolColours(int id, string colour)
        {
            Id = id;
            Colour = colour;
        }
    }
}