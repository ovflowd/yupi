namespace Yupi.Model.Domain
{
    /// <summary>
    ///     Class SongData.
    /// </summary>
    public class SongData
    {
        /// <summary>
        ///     Gets the name of the code.
        /// </summary>
        /// <value>The name of the code.</value>
        public string CodeName { get; private set; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets the artist.
        /// </summary>
        /// <value>The artist.</value>
        public string Artist { get; private set; }

        /// <summary>
        ///     Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public string Data { get; private set; }

        /// <summary>
        ///     Gets the length seconds.
        /// </summary>
        /// <value>The length seconds.</value>
		public double LengthSeconds { 
			get {
				return LengthMiliseconds / 100d;
			} 
		}

        /// <summary>
        ///     The length in miliseconds.
        /// </summary>
        /// <value>The length miliseconds.</value>
		public int LengthMiliseconds { get; private set; }
    }
}