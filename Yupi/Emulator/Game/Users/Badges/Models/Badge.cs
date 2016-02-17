namespace Yupi.Game.Users.Badges.Models
{
    /// <summary>
    ///     Class Badge.
    /// </summary>
    internal class Badge
    {
        /// <summary>
        ///     The code
        /// </summary>
        internal string Code;

        /// <summary>
        ///     The slot
        /// </summary>
        internal int Slot;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Badge" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="slot">The slot.</param>
        internal Badge(string code, int slot)
        {
            Code = code;
            Slot = slot;
        }
    }
}