using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class DisconnectUser. This class cannot be inherited.
    /// </summary>
    internal sealed class DisconnectUser : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DisconnectUser" /> class.
        /// </summary>
        public DisconnectUser()
        {
            MinRank = 7;
            Description = "dc user.";
            Usage = ":dc [username]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            GameClient user = Yupi.GetGame().GetClientManager().GetClientByUserName(pms[0]);
            if (user == null || user.GetHabbo() == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            if (user.GetHabbo().Rank >= session.GetHabbo().Rank)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_is_higher_rank"));
                return true;
            }
            try
            {
                user.GetConnection().Dispose();
                Yupi.GetGame()
                    .GetModerationTool()
                    .LogStaffEntry(session.GetHabbo().UserName, user.GetHabbo().UserName, "dc",
                        $"Disconnect User[{pms[1]}]");
            }
            catch
            {
            }

            return true;
        }
    }
}