namespace Yupi.Core.Security.BlackWords.Structs
{
    /// <summary>
    /// Struct BlackWordTypeSettings
    /// </summary>
    internal struct BlackWordTypeSettings
    {
        /// <summary>
        /// The filter
        /// </summary>
        public string Filter, Alert, ImageAlert;

        /// <summary>
        /// The maximum advices
        /// </summary>
        public uint MaxAdvices;

        /// <summary>
        /// The automatic ban
        /// </summary>
        public bool AutoBan, ShowMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackWordTypeSettings"/> struct.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="alert">The alert.</param>
        /// <param name="maxAdvices">The maximum advices.</param>
        /// <param name="imageAlert">The image alert.</param>
        /// <param name="autoBan">if set to <c>true</c> [automatic ban].</param>
        /// <param name="showMessage"></param>
        public BlackWordTypeSettings(string filter, string alert, uint maxAdvices, string imageAlert, bool autoBan, bool showMessage)
        {
            Filter = filter;
            Alert = alert;
            MaxAdvices = maxAdvices;
            ImageAlert = imageAlert;
            AutoBan = autoBan;
            ShowMessage = showMessage;
        }
    }
}