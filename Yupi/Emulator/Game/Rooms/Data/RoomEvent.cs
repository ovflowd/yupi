namespace Yupi.Game.Rooms.Data
{
    /// <summary>
    ///     Class RoomEvent.
    /// </summary>
    internal class RoomEvent
    {
        /// <summary>
        ///     The category
        /// </summary>
        internal int Category;

        /// <summary>
        ///     The description
        /// </summary>
        internal string Description;

        /// <summary>
        ///     The name
        /// </summary>
        internal string Name;

        /// <summary>
        ///     The room identifier
        /// </summary>
        internal uint RoomId;

        /// <summary>
        ///     The time
        /// </summary>
        internal int Time;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomEvent" /> class.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="time">The time.</param>
        /// <param name="category">The category.</param>
        internal RoomEvent(uint roomId, string name, string description, int time = 0, int category = 1)
        {
            RoomId = roomId;
            Name = name;
            Description = description;
            Time = time == 0 ? Yupi.GetUnixTimeStamp() + 7200 : time;

            Category = category;
        }

        /// <summary>
        ///     Gets a value indicating whether this instance has expired.
        /// </summary>
        /// <value><c>true</c> if this instance has expired; otherwise, <c>false</c>.</value>
        internal bool HasExpired => Yupi.GetUnixTimeStamp() > Time;
    }
}