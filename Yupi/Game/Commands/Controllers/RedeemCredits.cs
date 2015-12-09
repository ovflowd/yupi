using System;
using Yupi.Core.Io;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RedeemCredits.
    /// </summary>
    internal sealed class RedeemCredits : Command
    {
        public RedeemCredits()
        {
            MinRank = 1;
            Description = "Redeems all Goldbars in your inventory to Credits.";
            Usage = ":redeemcredits";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            try
            {
                session.GetHabbo().GetInventoryComponent().Redeemcredits(session);
                session.SendNotif(Yupi.GetLanguage().GetVar("command_redeem_credits"));
            }
            catch (Exception e)
            {
                Writer.LogException(e.ToString());
                session.SendNotif(Yupi.GetLanguage().GetVar("command_redeem_credits"));
            }
            return true;
        }
    }
}