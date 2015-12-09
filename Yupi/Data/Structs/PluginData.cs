namespace Yupi.Data.Structs
{
    /// <summary>
    /// Description of IPlugin.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets the plugin_name.
        /// </summary>
        /// <value>The plugin_name.</value>
        string PluginName { get; }

        /// <summary>
        /// Gets the plugin_version.
        /// </summary>
        /// <value>The plugin_version.</value>
        string PluginVersion { get; }

        /// <summary>
        /// Gets the plugin_author.
        /// </summary>
        /// <value>The plugin_author.</value>
        string PluginAuthor { get; }

        /// <summary>
        /// Message_voids this instance.
        /// </summary>
        void message_void();

        /// <summary>
        /// Content_voids this instance.
        /// </summary>
        void content_void();

        /// <summary>
        /// Packets_voids this instance.
        /// </summary>
        void packets_void();

        /// <summary>
        /// Habbo_voids this instance.
        /// </summary>
        void habbo_void();
    }
}