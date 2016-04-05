using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class MassCredits. This class cannot be inherited.
    /// </summary>
     sealed class MassCredits : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MassCredits" /> class.
        /// </summary>
        public MassCredits()
        {
            MinRank = 8;
            Description = "Gives all the users online credits.";
            Usage = ":masscredits [AMOUNT]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            uint amount;

            if (!uint.TryParse(pms[0], out amount))
            {
                session.SendNotif(Yupi.GetLanguage().GetVar("enter_numbers"));

                return true;
            }

            foreach (GameClient client in Yupi.GetGame().GetClientManager().Clients.Values)
            {
                if (client?.GetHabbo() == null)
                    continue;

                client.GetHabbo().Credits += amount;

                client.GetHabbo().UpdateCreditsBalance();

                client.SendNotif(Yupi.GetLanguage().GetVar("command_mass_credits_one_give") + amount + Yupi.GetLanguage().GetVar("command_mass_credits_two_give"));
            }

            Yupi.GetGame().GetModerationTool().LogStaffEntry(session.GetHabbo().UserName, string.Empty, "Credits", string.Concat("RoomCredits in room [", session.GetHabbo().CurrentRoom.RoomId, "] with amount [", pms[0], "]"));


            return true;
        }
    }
}