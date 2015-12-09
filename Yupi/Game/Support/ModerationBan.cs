namespace Yupi.Game.Support
{
    /// <summary>
    ///     Struct ModerationBan
    /// </summary>
    internal struct ModerationBan
    {
        /// <summary>
        ///     The type
        /// </summary>
        internal ModerationBanType Type;

        /// <summary>
        ///     The variable
        /// </summary>
        internal string Variable;

        /// <summary>
        ///     The reason message
        /// </summary>
        internal string ReasonMessage;

        /// <summary>
        ///     The expire
        /// </summary>
        internal double Expire;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ModerationBan" /> struct.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="variable">The variable.</param>
        /// <param name="reasonMessage">The reason message.</param>
        /// <param name="expire">The expire.</param>
        internal ModerationBan(ModerationBanType type, string variable, string reasonMessage, double expire)
        {
            Type = type;
            Variable = variable;
            ReasonMessage = reasonMessage;
            Expire = expire;
        }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="ModerationBan" /> is expired.
        /// </summary>
        /// <value><c>true</c> if expired; otherwise, <c>false</c>.</value>
        internal bool Expired => Yupi.GetUnixTimeStamp() >= Expire;
    }
}