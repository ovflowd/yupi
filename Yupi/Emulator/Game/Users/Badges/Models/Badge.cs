namespace Yupi.Emulator.Game.Users.Badges.Models
{
    /// <summary>
    ///     Class Badge.
    /// </summary>
     public class Badge
    {
        /// <summary>
        ///     The code
        /// </summary>
     public string Code;

        /// <summary>
        ///     The slot
        /// </summary>
     public int Slot;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Badge" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="slot">The slot.</param>
     public Badge(string code, int slot)
        {
            Code = code;
            Slot = slot;
        }
    }
}