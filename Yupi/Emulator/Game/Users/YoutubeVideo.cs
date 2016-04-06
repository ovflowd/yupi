using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Users
{
    /// <summary>
    ///     Class YoutubeVideo.
    /// </summary>
     public class YoutubeVideo
    {
     public string Description;
     public string Name;
     public string VideoId;

     public YoutubeVideo(string videoId, string name, string description)
        {
            VideoId = videoId;
            Name = name;
            Description = description;
        }

     public void Serialize(SimpleServerMessageBuffer messageBuffer)
        {
            messageBuffer.AppendString(VideoId);
            messageBuffer.AppendString(Name);
            messageBuffer.AppendString(Description);
        }
    }
}