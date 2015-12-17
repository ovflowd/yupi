using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Pathfinding;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorPuzzleBox : FurniInteractorModel
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

                ServerMessage serverMessage = new ServerMessage();

                serverMessage.Init(LibraryParser.OutgoingRequest("ItemAnimationMessageComposer"));

                serverMessage.AppendInteger(item.X);
                serverMessage.AppendInteger(item.Y);
                serverMessage.AppendInteger(point.X);
                serverMessage.AppendInteger(point.Y);
                serverMessage.AppendInteger(1);
                serverMessage.AppendInteger(item.Id);
                serverMessage.AppendString(item.Z.ToString(Yupi.CultureInfo));
                serverMessage.AppendString(num.ToString(Yupi.CultureInfo));
                serverMessage.AppendInteger(0);

                room.SendMessage(serverMessage);

                item.GetRoom()
                    .GetRoomItemHandler()
                    .SetFloorItem(roomUserByHabbo.GetClient(), item, point.X, point.Y, item.Rot, false, false, false);
            }
        }
    }
}