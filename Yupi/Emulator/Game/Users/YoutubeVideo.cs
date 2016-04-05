using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Users
{
    /// <summary>
    ///     Class YoutubeVideo.
    /// </summary>
     class YoutubeVideo
    {
         string Description;
         string Name;
         string VideoId;

         YoutubeVideo(string videoId, string name, string description)
        {
            VideoId = videoId;
            Name = name;
            Description = description;
        }

         void Serialize(SimpleServerMessageBuffer messageBuffer)
        {
            messageBuffer.AppendString(VideoId);
            messageBuffer.AppendString(Name);
            messageBuffer.AppendString(Description);
        }
    }
}