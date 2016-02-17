using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Enable. This class cannot be inherited.
    /// </summary>
    internal sealed class Enable : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Enable" /> class.
        /// </summary>
        public Enable()
        {
            MinRank = -3;
            Description = "Enable/disable effect";
            Usage = ":enable";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            RoomUser user =
                session.GetHabbo()
                    .CurrentRoom.GetRoomUserManager()
                    .GetRoomUserByHabbo(session.GetHabbo().UserName);
            if (user.RidingHorse) return true;
            if (user.IsLyingDown) return true;
            ushort effect;

            if (!ushort.TryParse(pms[0], out effect)) return true;
            if (effect == 102 && !session.GetHabbo().HasFuse("fuse_mod")) return true;
            if (effect == 140 && !(session.GetHabbo().Vip || session.GetHabbo().HasFuse("fuse_vip_commands")))
                return true;
            if (effect == 178 && !session.GetHabbo().HasFuse("fuse_mod")) return true; // TODO: Need ambassadors

            session.GetHabbo()
                .GetAvatarEffectsInventoryComponent()
                .ActivateCustomEffect(effect);

            return true;
        }
    }
}