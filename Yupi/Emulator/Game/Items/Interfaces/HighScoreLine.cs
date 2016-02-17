namespace Yupi.Game.Items.Interfaces
{
    /// <summary>
    ///     Class HighScoreLine.
    /// </summary>
    internal class HighScoreLine
    {
        /// <summary>
        ///     The score
        /// </summary>
        internal int Score;

        /// <summary>
        ///     The username
        /// </summary>
        internal string Username;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HighScoreLine" /> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="score">The score.</param>
        internal HighScoreLine(string username, int score)
        {
            Username = username;
            Score = score;
        }
    }
}