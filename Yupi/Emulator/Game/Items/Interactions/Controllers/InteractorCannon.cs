using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Timers;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Messages;


namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     public class InteractorCannon : FurniInteractorModel
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

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SuperNotificationMessageComposer"));
            simpleServerMessageBuffer.AppendString("room.kick.cannonball");
            simpleServerMessageBuffer.AppendInteger(2);
            simpleServerMessageBuffer.AppendString("link");
            simpleServerMessageBuffer.AppendString("event:");
            simpleServerMessageBuffer.AppendString("linkTitle");
            simpleServerMessageBuffer.AppendString("ok");

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
                user.GetClient().SendMessage(simpleServerMessageBuffer);
            }

            _mItem.OnCannonActing = false;
        }
    }
}