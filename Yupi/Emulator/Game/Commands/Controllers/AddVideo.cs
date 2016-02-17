using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class SetVideos. This class cannot be inherited.
    /// </summary>
    internal sealed class AddVideo : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SetVideos" /> class.
        /// </summary>
        public AddVideo()
        {
            MinRank = -1;
            Description = "Add Youtube Video";
            Usage = ":setvideo [YOUTUBE VIDEO URL]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            session.GetHabbo().GetYoutubeManager().AddUserVideo(session, pms[0]);
            return true;
        }
    }
}