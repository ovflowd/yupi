using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Pathfinding;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Messages;


namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     public class InteractorPuzzleBox : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (session == null)
                return;

            RoomUser roomUserByHabbo = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

            if (roomUserByHabbo == null)
                return;

            if (PathFinder.GetDistance(roomUserByHabbo.X, roomUserByHabbo.Y, item.X, item.Y) > 1)
                roomUserByHabbo.MoveTo(item.X + 1, item.Y);

            if (Math.Abs(roomUserByHabbo.X - item.X) < 2 && Math.Abs(roomUserByHabbo.Y - item.Y) < 2)
            {
                roomUserByHabbo.SetRot(
                    PathFinder.CalculateRotation(roomUserByHabbo.X, roomUserByHabbo.Y, item.X, item.Y), false);

                Room room = item.GetRoom();
                Point point = new Point(0, 0);

                switch (roomUserByHabbo.RotBody)
                {
                    case 4:
                        point = new Point(item.X, item.Y + 1);
                        break;

                    case 0:
                        point = new Point(item.X, item.Y - 1);
                        break;

                    case 6:
                        point = new Point(item.X - 1, item.Y);
                        break;

                    default:
                        if (roomUserByHabbo.RotBody != 2)
                            return;

                        point = new Point(item.X + 1, item.Y);
                        break;
                }

                if (!room.GetGameMap().ValidTile2(point.X, point.Y))
                    return;

                List<RoomItem> coordinatedItems = room.GetGameMap().GetCoordinatedItems(point);

                if (coordinatedItems.Any(i => !i.GetBaseItem().Stackable))
                    return;

                double num = item.GetRoom().GetGameMap().SqAbsoluteHeight(point.X, point.Y);

				room.Router.GetComposer<ItemAnimationMessageComposer> ().Compose (room, 
					new Tuple<Point, double> (new Point (item.X, item.Y), item.Z),
					new Tuple<Point, double> (point, num), 0, item.Id, 
					ItemAnimationMessageComposer.Type.Item);

                item.GetRoom()
                    .GetRoomItemHandler()
                    .SetFloorItem(roomUserByHabbo.GetClient(), item, point.X, point.Y, item.Rot, false, false, false);
            }
        }
    }
}