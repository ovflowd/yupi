namespace Yupi.Game.Users.Relationships
{
    /// <summary>
    ///     Class Relationship.
    /// </summary>
    internal class Relationship
    {
        /// <summary>
        ///     The identifier
        /// </summary>
        public int Id;

        /// <summary>
        ///     The type
        /// </summary>
        public int Type;

        /// <summary>
        ///     The user identifier
        /// </summary>
        public int UserId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Relationship" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <param name="type">The type.</param>
        public Relationship(int id, int user, int type)
        {
            Id = id;
            UserId = user;
            Type = type;
        }
    }
}