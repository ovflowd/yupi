using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Users
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

        internal void Serialize(SimpleServerMessageBuffer messageBuffer)
        {
            messageBuffer.AppendString(VideoId);
            messageBuffer.AppendString(Name);
            messageBuffer.AppendString(Description);
        }
    }
}