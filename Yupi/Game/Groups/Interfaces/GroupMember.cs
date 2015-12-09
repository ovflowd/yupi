namespace Yupi.Game.Groups.Interfaces
{
    /// <summary>
    ///     Class GroupUser.
    /// </summary>
    internal class GroupMember
    {
        /// <summary>
        ///     The date of join on group
        /// </summary>
        internal int DateJoin;

        /// <summary>
        ///     The group identifier
        /// </summary>
        internal int GroupId;

        /// <summary>
        ///     The identifier
        /// </summary>
        internal uint Id;

        /// <summary>
        ///     The look
        /// </summary>
        internal string Look;

        /// <summary>
        ///     The name
        /// </summary>
        internal string Name;

        /// <summary>
        ///     The rank
        /// </summary>
        internal int Rank;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GroupMember" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="look"></param>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="rank">The rank.</param>
        /// <param name="name"></param>
        /// <param name="dateJoin"></param>
        internal GroupMember(uint id, string name, string look, int groupId, int rank, int dateJoin)
        {
            Id = id;
            Name = name;
            Look = look;
            GroupId = groupId;
            Rank = rank;
            DateJoin = dateJoin;
        }
    }
}