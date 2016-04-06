namespace Yupi.Emulator.Game.Items.Interfaces
{
    /// <summary>
    ///     Class HighScoreLine.
    /// </summary>
     public class HighScoreLine
    {
        /// <summary>
        ///     The score
        /// </summary>
     public int Score;

        /// <summary>
        ///     The username
        /// </summary>
     public string Username;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HighScoreLine" /> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="score">The score.</param>
     public HighScoreLine(string username, int score)
        {
            Username = username;
            Score = score;
        }
    }
}