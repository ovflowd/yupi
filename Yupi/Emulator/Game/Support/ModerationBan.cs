namespace Yupi.Emulator.Game.Support
{
    /// <summary>
    ///     Struct ModerationBan
    /// </summary>
     struct ModerationBan
    {
        /// <summary>
        ///     The type
        /// </summary>
         ModerationBanType Type;

        /// <summary>
        ///     The variable
        /// </summary>
         string Variable;

        /// <summary>
        ///     The reason message
        /// </summary>
         string ReasonMessage;

        /// <summary>
        ///     The expire
        /// </summary>
         double Expire;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ModerationBan" /> struct.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="variable">The variable.</param>
        /// <param name="reasonMessage">The reason message.</param>
        /// <param name="expire">The expire.</param>
         ModerationBan(ModerationBanType type, string variable, string reasonMessage, double expire)
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
         bool Expired => Yupi.GetUnixTimeStamp() >= Expire;
    }
}