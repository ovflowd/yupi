namespace Yupi.Data.Structs
{
    /// <summary>
    /// Struct FurnitureData
    /// </summary>
    public struct FurnitureData
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public int Id;

        /// <summary>
        /// The name
        /// </summary>
        public string Name;

        /// <summary>
        /// The x
        /// </summary>
        public ushort X, Y;

        /// <summary>
        /// The can sit
        /// </summary>
        public bool CanSit, CanWalk;

        /// <summary>
        /// Initializes a new instance of the <see cref="FurnitureData" /> struct.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="canSit">if set to <c>true</c> [can sit].</param>
        /// <param name="canWalk">if set to <c>true</c> [can walk].</param>
        public FurnitureData(int id, string name, ushort x = 0, ushort y = 0, bool canSit = false, bool canWalk = false)
        {
            Id = id;
            Name = name;
            X = x;
            Y = y;
            CanSit = canSit;
            CanWalk = canWalk;
        }
    }
}
