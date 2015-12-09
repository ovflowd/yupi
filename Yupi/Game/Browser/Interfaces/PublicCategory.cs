namespace Yupi.Game.Browser.Interfaces
{
    /// <summary>
    ///     Class FlatCat.
    /// </summary>
    internal class PublicCategory
    {
        /// <summary>
        ///     The caption
        /// </summary>
        internal string Caption;

        /// <summary>
        ///     The identifier
        /// </summary>
        internal int Id;

        /// <summary>
        ///     The minimum rank
        /// </summary>
        internal int MinRank;

        /// <summary>
        ///     The users now
        /// </summary>
        internal int UsersNow;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PublicCategory" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="minRank">The minimum rank.</param>
        internal PublicCategory(int id, string caption, int minRank)
        {
            Id = id;
            Caption = caption;
            MinRank = minRank;
            UsersNow = 0;
        }
    }
}