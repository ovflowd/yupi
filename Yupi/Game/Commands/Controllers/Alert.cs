using System.Linq;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Alert. This class cannot be inherited.
    /// </summary>
    internal sealed class Alert : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Alert" /> class.
        /// </summary>
        public Alert()
        {
            MinRank = 5;
            Description = "Alerts a User.";
            Usage = ":alert [USERNAME] [MESSAGE]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            var userName = pms[0];
            var msg = string.Join(" ", pms.Skip(1));

            var client = Yupi.GetGame().GetClientManager().GetClientByUserName(userName);
            if (client == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            client.SendNotif(string.Format("{0} \r\r-{1}", msg, session.GetHabbo().UserName));
            return true;
        }
    }
}