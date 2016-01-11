using System;
using System.Threading;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Pathfinding;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorFxBox : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (!hasRights)
                return;

            RoomUser user = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

            if (user == null)
                return;

            Room room = session.GetHabbo().CurrentRoom;

            if (room == null)
                return;

            int effectId = Convert.ToInt32(item.GetBaseItem().Name.Replace("fxbox_fx", ""));

            try
            {
                while (PathFinder.GetDistance(user.X, user.Y, item.X, item.Y) > 1)
                {
                    if (user.RotBody == 0)
                        user.MoveTo(item.X, item.Y + 1);
                    else if (user.RotBody == 2)
                        user.MoveTo(item.X - 1, item.Y);
                    else if (user.RotBody == 4)
                        user.MoveTo(item.X, item.Y - 1);
                    else if (user.RotBody == 6)
                        user.MoveTo(item.X + 1, item.Y);
                    else
                        user.MoveTo(item.X, item.Y + 1); // Diagonal user...
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (PathFinder.GetDistance(user.X, user.Y, item.X, item.Y) == 1)
                {
                    session.GetHabbo().GetAvatarEffectsInventoryComponent().AddNewEffect(effectId, -1, 0);
                    session.GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(effectId);

                    Thread.Sleep(500); //Wait 0.5 second until remove furniture. (Delay)

                    room.GetRoomItemHandler().RemoveFurniture(session, item.Id, false);

                    using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                        commitableQueryReactor.RunFastQuery("DELETE FROM items_rooms WHERE id = " + item.Id);
                }
            }
        }
    }
}