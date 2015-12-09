namespace Yupi.Game.Browser.Interfaces
{
    /// <summary>
    ///     Class PromoCat.
    /// </summary>
    internal class PromoCategory
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
        ///     The visible
        /// </summary>
        internal bool Visible;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PromoCategory" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="minRank">The minimum rank.</param>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        internal PromoCategory(int id, string caption, int minRank, bool visible)
        {
            Id = id;
            Caption = caption;
            MinRank = minRank;
            Visible = visible;
        }
    }
}