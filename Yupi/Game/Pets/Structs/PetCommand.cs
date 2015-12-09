namespace Yupi.Game.Pets.Structs
{
    /// <summary>
    ///     Struct PetCommand
    /// </summary>
    internal struct PetCommand
    {
        /// <summary>
        ///     The command identifier
        /// </summary>
        internal readonly int CommandId;

        /// <summary>
        ///     The command input
        /// </summary>
        internal readonly string CommandInput;

        /// <summary>
        ///     The minimum rank
        /// </summary>
        internal readonly int MinRank;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PetCommand" /> struct.
        /// </summary>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="commandInput">The command input.</param>
        /// <param name="minRank">The minimum rank.</param>
        public PetCommand(int commandId, string commandInput, int minRank = 0)
        {
            CommandId = commandId;
            CommandInput = commandInput;
            MinRank = minRank;
        }
    }
}