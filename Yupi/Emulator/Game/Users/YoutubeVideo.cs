using Yupi.Messages;

namespace Yupi.Game.Users
{
    /// <summary>
    ///     Class YoutubeVideo.
    /// </summary>
    internal class YoutubeVideo
    {
        internal string Description;
        internal string Name;
        internal string VideoId;

        internal YoutubeVideo(string videoId, string name, string description)
        {
            VideoId = videoId;
            Name = name;
            Description = description;
        }

        internal void Serialize(ServerMessage message)
        {
            message.AppendString(VideoId);
            message.AppendString(Name);
            message.AppendString(Description);
        }
    }
}