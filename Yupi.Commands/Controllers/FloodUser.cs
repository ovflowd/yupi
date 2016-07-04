using System;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class FloodUser. This class cannot be inherited.
    /// </summary>
     public sealed class FloodUser : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FloodUser" /> class.
        /// </summary>
        public FloodUser()
        {
            MinRank = 5;
            Description = "Flood user.";
            Usage = ":flood [USERNAME] [TIME]";
            MinParams = 2;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            GameClient client = Yupi.GetGame().GetClientManager().GetClientByUserName(pms[0]);
            if (client == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            if (client.GetHabbo().Rank >= session.GetHabbo().Rank)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_is_higher_rank"));
                return true;
            }
            int time;
            if (!int.TryParse(pms[1], out time))
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("enter_numbers"));
                return true;
            }

            client.GetHabbo().FloodTime = Yupi.GetUnixTimeStamp() + Convert.ToInt32(pms[1]);
			client.Router.GetComposer<FloodFilterMessageComposer> ().Compose (session, Convert.ToInt32(pms[1]));
			return true;
        }
    }
}