namespace Yupi.Model.Domain
{
    /// <summary>
    ///     Class SongData.
    /// </summary>
    public class SongData
    {
        public virtual int Id { get; protected set; }

        /// <summary>
        ///     Gets the name of the code.
        /// </summary>
        /// <value>The name of the code.</value>
        public virtual string CodeName { get; protected set; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; protected set; }

        /// <summary>
        ///     Gets the artist.
        /// </summary>
        /// <value>The artist.</value>
        public virtual string Artist { get; protected set; }

        /// <summary>
        ///     Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public virtual string Data { get; protected set; }

        [Ignore]
        public virtual double LengthSeconds
        {
            get { return LengthMiliseconds/100d; }
        }

        /// <summary>
        ///     The length in miliseconds.
        /// </summary>
        /// <value>The length miliseconds.</value>
        public virtual int LengthMiliseconds { get; protected set; }
    }
}