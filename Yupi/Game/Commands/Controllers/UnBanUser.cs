using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Users;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class UnBanUser. This class cannot be inherited.
    /// </summary>
    internal sealed class UnBanUser : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnBanUser" /> class.
        /// </summary>
        public UnBanUser()
        {
            MinRank = 4;
            Description = "Unban a user!";
            Usage = ":unban [USERNAME]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Habbo user = Yupi.GetHabboForName(pms[0]);

            if (user == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            if (user.Rank >= session.GetHabbo().Rank)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_is_higher_rank"));
                return true;
            }
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery("DELETE FROM users_bans WHERE value = @name");
                adapter.AddParameter("name", user.UserName);
                adapter.RunQuery();
                Yupi.GetGame()
                    .GetModerationTool()
                    .LogStaffEntry(session.GetHabbo().UserName, user.UserName, "Unban",
                        $"User has been Unbanned [{pms[0]}]");
                return true;
            }
        }
    }
}