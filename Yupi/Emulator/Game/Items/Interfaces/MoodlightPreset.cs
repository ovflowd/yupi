namespace Yupi.Emulator.Game.Items.Interfaces
{
    /// <summary>
    ///     Class MoodlightPreset.
    /// </summary>
     class MoodlightPreset
    {
        /// <summary>
        ///     The background only
        /// </summary>
         bool BackgroundOnly;

        /// <summary>
        ///     The color code
        /// </summary>
         string ColorCode;

        /// <summary>
        ///     The color intensity
        /// </summary>
         int ColorIntensity;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MoodlightPreset" /> class.
        /// </summary>
        /// <param name="colorCode">The color code.</param>
        /// <param name="colorIntensity">The color intensity.</param>
        /// <param name="backgroundOnly">if set to <c>true</c> [background only].</param>
         MoodlightPreset(string colorCode, int colorIntensity, bool backgroundOnly)
        {
            ColorCode = colorCode;
            ColorIntensity = colorIntensity;
            BackgroundOnly = backgroundOnly;
        }
    }
}