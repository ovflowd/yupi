using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Timers;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorCannon : FurniInteractorModel
    {
        private HashSet<Point> _mCoords;
        private RoomItem _mItem;

        public override void OnPlace(GameClient session, RoomItem item)
        {
            item.ExtraData = "0";
        }

        public override void OnRemove(GameClient session, RoomItem item)
        {
            item.ExtraData = "0";
        }

        public override void OnWiredTrigger(RoomItem item)
        {
            if (item.OnCannonActing)
                return;

            item.OnCannonActing = true;

            HashSet<Point> coords = new HashSet<Point>();

            int itemX = item.X;
            int itemY = item.Y;

            switch (item.Rot)
            {
                case 0: // TESTEADO OK
                    int startingcoordX = itemX - 1;

                    for (int i = startingcoordX; i > 0; i--)
                        coords.Add(new Point(i, itemY));

                    break;

                case 4: // TESTEADO OK
                    int startingcoordX2 = itemX + 2;
                    int mapsizeX = item.GetRoom().GetGameMap().Model.MapSizeX;

                    for (int i = startingcoordX2; i < mapsizeX; i++)
                        coords.Add(new Point(i, itemY));

                    break;

                case 2: // TESTEADO OK
                    int startingcoordY = itemY - 1;

                    for (int i = startingcoordY; i > 0; i--)
                        coords.Add(new Point(itemX, i));

                    break;

                case 6: // OK!
                    int startingcoordY2 = itemY + 2;
                    int mapsizeY = item.GetRoom().GetGameMap().Model.MapSizeY;

                    for (int i = startingcoordY2; i < mapsizeY; i++)
                        coords.Add(new Point(itemX, i));

                    break;
            }

            item.ExtraData = item.ExtraData == "0" ? "1" : "0";
            item.UpdateState();

            _mItem = item;
            _mCoords = coords;

            Timer explodeTimer = new Timer(1350);
            explodeTimer.Elapsed += ExplodeAndKick;
            explodeTimer.Enabled = true;
        }

        private void ExplodeAndKick(object source, ElapsedEventArgs e)
        {
            Timer timer = (Timer) source;
            timer.Stop();

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
            serverMessage.AppendString("room.kick.cannonball");
            serverMessage.AppendInteger(2);
            serverMessage.AppendString("link");
            serverMessage.AppendString("event:");
            serverMessage.AppendString("linkTitle");
            serverMessage.AppendString("ok");

            Room room = _mItem.GetRoom();

            HashSet<RoomUser> toRemove = new HashSet<RoomUser>();

            foreach (
                RoomUser user in
                    _mCoords.SelectMany(
                        coord =>
                            room.GetGameMap()
                                .GetRoomUsers(coord)
                                .Where(
                                    user =>
                                        user != null && !user.IsBot && !user.IsPet &&
                                        user.GetUserName() != room.RoomData.Owner)))
            {
                user.GetClient().GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(4, false);
                toRemove.Add(user);
            }

            foreach (RoomUser user in toRemove)
            {
                room.GetRoomUserManager().RemoveUserFromRoom(user.GetClient(), true, false);
                user.GetClient().SendMessage(serverMessage);
            }

            _mItem.OnCannonActing = false;
        }
    }
}