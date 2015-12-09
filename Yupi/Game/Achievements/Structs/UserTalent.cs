namespace Yupi.Game.Achievements.Structs
{
    /// <summary>
    ///     Class UserTalent.
    /// </summary>
    internal struct UserTalent
    {
        /// <summary>
        ///     The talent identifier
        /// </summary>
        internal int TalentId;

        /// <summary>
        ///     The state
        /// </summary>
        internal int State;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserTalent" /> class.
        /// </summary>
        /// <param name="talentId">The talent identifier.</param>
        /// <param name="state">The state.</param>
        public UserTalent(int talentId, int state)
        {
            TalentId = talentId;
            State = state;
        }
    }
}