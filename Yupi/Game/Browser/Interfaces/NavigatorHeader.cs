namespace Yupi.Game.Browser.Interfaces
{
    /// <summary>
    ///     Struct NavigatorHeader
    /// </summary>
    internal struct NavigatorHeader
    {
        /// <summary>
        ///     The room identifier
        /// </summary>
        internal uint RoomId;

        /// <summary>
        ///     The caption
        /// </summary>
        internal string Caption;

        /// <summary>
        ///     The image
        /// </summary>
        internal string Image;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavigatorHeader" /> struct.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="image">The image.</param>
        internal NavigatorHeader(uint roomId, string caption, string image)
        {
            RoomId = roomId;
            Caption = caption;
            Image = image;
        }
    }
}