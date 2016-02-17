namespace Yupi.Game.SoundMachine.Songs
{
    /// <summary>
    ///     Class SongData.
    /// </summary>
    internal class SongData
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SongData" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="codeName">Name of the code.</param>
        /// <param name="name">The name.</param>
        /// <param name="artist">The artist.</param>
        /// <param name="data">The data.</param>
        /// <param name="length">The length.</param>
        public SongData(uint id, string codeName, string name, string artist, string data, double length)
        {
            Id = id;
            CodeName = codeName;
            Name = name;
            Artist = artist;
            Data = data;
            LengthSeconds = length;
        }

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public uint Id { get; private set; }

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
        public double LengthSeconds { get; }

        /// <summary>
        ///     Gets the length miliseconds.
        /// </summary>
        /// <value>The length miliseconds.</value>
        public int LengthMiliseconds => (int) (LengthSeconds*1000.0);
    }
}