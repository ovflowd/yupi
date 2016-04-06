namespace Yupi.Emulator.Game.Support
{
    /// <summary>
    ///     Class ModerationTemplate.
    /// </summary>
     public class ModerationTemplate
    {
        /// <summary>
        ///     The avatar ban
        /// </summary>
     public bool AvatarBan;

        /// <summary>
        ///     The ban hours
        /// </summary>
     public short BanHours;

        /// <summary>
        ///     The ban message
        /// </summary>
     public string BanMessage;

        /// <summary>
        ///     The caption
        /// </summary>
     public string Caption;

        /// <summary>
        ///     The category
        /// </summary>
     public short Category;

        /// <summary>
        ///     The c name
        /// </summary>
     public string CName;

        /// <summary>
        ///     The identifier
        /// </summary>
     public uint Id;

        /// <summary>
        ///     The mute
        /// </summary>
     public bool Mute;

        /// <summary>
        ///     The trade lock
        /// </summary>
     public bool TradeLock;

        /// <summary>
        ///     The warning message
        /// </summary>
     public string WarningMessage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ModerationTemplate" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="category">The category.</param>
        /// <param name="cName">Name of the c.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="warningMessage">The warning message.</param>
        /// <param name="banMessage">The ban message.</param>
        /// <param name="banHours">The ban hours.</param>
        /// <param name="avatarBan">if set to <c>true</c> [avatar ban].</param>
        /// <param name="mute">if set to <c>true</c> [mute].</param>
        /// <param name="tradeLock">if set to <c>true</c> [trade lock].</param>
     public ModerationTemplate(uint id, short category, string cName, string caption, string warningMessage,
            string banMessage, short banHours, bool avatarBan, bool mute, bool tradeLock)
        {
            Id = id;
            Category = category;
            CName = cName;
            Caption = caption;
            WarningMessage = warningMessage;
            BanMessage = banMessage;
            BanHours = banHours;
            AvatarBan = avatarBan;
            Mute = mute;
            TradeLock = tradeLock;
        }
    }
}