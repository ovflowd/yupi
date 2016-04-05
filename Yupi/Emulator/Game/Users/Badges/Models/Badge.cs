namespace Yupi.Emulator.Game.Users.Badges.Models
{
    /// <summary>
    ///     Class Badge.
    /// </summary>
     class Badge
    {
        /// <summary>
        ///     The code
        /// </summary>
         string Code;

        /// <summary>
        ///     The slot
        /// </summary>
         int Slot;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Badge" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="slot">The slot.</param>
         Badge(string code, int slot)
        {
            Code = code;
            Slot = slot;
        }
    }
}