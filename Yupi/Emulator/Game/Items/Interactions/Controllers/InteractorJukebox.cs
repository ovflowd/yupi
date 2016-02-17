using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorJukebox : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (!hasRights)
                return;

            if (item.ExtraData == "1")
            {
                item.GetRoom().GetRoomMusicController().Stop();
                item.ExtraData = "0";
            }
            else
            {
                item.GetRoom().GetRoomMusicController().Start();
                item.ExtraData = "1";
            }

            item.UpdateState();
        }

        public override void OnWiredTrigger(RoomItem item)
        {
            if (item.ExtraData == "1")
            {
                item.GetRoom().GetRoomMusicController().Stop();
                item.ExtraData = "0";
            }
            else
            {
                item.GetRoom().GetRoomMusicController().Start();
                item.ExtraData = "1";
            }

            item.UpdateState();
        }
    }
}