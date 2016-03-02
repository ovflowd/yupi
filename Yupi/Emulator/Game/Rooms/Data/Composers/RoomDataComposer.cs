/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System;
using System.Linq;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Rooms.Data.Composers
{
    class RoomDataComposer
    {
        internal static SimpleServerMessageBuffer Compose(SimpleServerMessageBuffer roomDataMessage, GameClient session, Room room, bool isNotReload, bool? sendRoom = false, bool show = true)
        {
            roomDataMessage.Init(PacketLibraryManager.SendRequest("RoomDataMessageComposer"));

            roomDataMessage.AppendBool(show);

            Serialize(roomDataMessage, room, true, !isNotReload);

            roomDataMessage.AppendBool(isNotReload);
            roomDataMessage.AppendBool(Yupi.GetGame().GetNavigator() != null && Yupi.GetGame().GetNavigator().GetPublicRoom(room.RoomData.Id) != null);
            roomDataMessage.AppendBool(!isNotReload || session.GetHabbo().HasFuse("fuse_mod"));
            roomDataMessage.AppendBool(room.RoomMuted);
            roomDataMessage.AppendInteger(room.RoomData.WhoCanMute);
            roomDataMessage.AppendInteger(room.RoomData.WhoCanKick);
            roomDataMessage.AppendInteger(room.RoomData.WhoCanBan);
            roomDataMessage.AppendBool(room.CheckRights(session, true));
            roomDataMessage.AppendInteger(room.RoomData.ChatType);
            roomDataMessage.AppendInteger(room.RoomData.ChatBalloon);
            roomDataMessage.AppendInteger(room.RoomData.ChatSpeed);
            roomDataMessage.AppendInteger(room.RoomData.ChatMaxDistance);
            roomDataMessage.AppendInteger(room.RoomData.ChatFloodProtection);

            return roomDataMessage;
        }

        internal static SimpleServerMessageBuffer Serialize(SimpleServerMessageBuffer messageBuffer, Room room, bool showEvents = false, bool enterRoom = false)
        {
            messageBuffer.AppendInteger(room.RoomData.Id);
            messageBuffer.AppendString(room.RoomData.Name);
            messageBuffer.AppendInteger(room.RoomData.OwnerId);
            messageBuffer.AppendString(room.RoomData.Owner);
            messageBuffer.AppendInteger(room.RoomData.State);
            messageBuffer.AppendInteger(room.RoomData.UsersNow);
            messageBuffer.AppendInteger(room.RoomData.UsersMax);
            messageBuffer.AppendString(room.RoomData.Description);
            messageBuffer.AppendInteger(room.RoomData.TradeState);
            messageBuffer.AppendInteger(room.RoomData.Score);
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(room.RoomData.Category > 0 ? room.RoomData.Category : 0);
            messageBuffer.AppendInteger(room.RoomData.TagCount);

            foreach (string current in room.RoomData.Tags.Where(current => current != null))
                messageBuffer.AppendString(current);

            string imageData = null;

            int enumType = enterRoom ? 32 : 0;

            PublicItem publicItem = Yupi.GetGame()?.GetNavigator()?.GetPublicRoom(room.RoomData.Id);

            if (!string.IsNullOrEmpty(publicItem?.Image))
            {
                imageData = publicItem.Image;

                enumType += 1;
            }

            if (room.RoomData.Group != null)
                enumType += 2;

            if (showEvents && room.RoomData.Event != null)
                enumType += 4;

            if (room.RoomData.Type == "private")
                enumType += 8;

            if (room.RoomData.AllowPets)
                enumType += 16;

            messageBuffer.AppendInteger(enumType);

            if (imageData != null)
                messageBuffer.AppendString(imageData);

            if (room.RoomData.Group != null)
            {
                messageBuffer.AppendInteger(room.RoomData.Group.Id);
                messageBuffer.AppendString(room.RoomData.Group.Name);
                messageBuffer.AppendString(room.RoomData.Group.Badge);
            }

            if (showEvents && room.RoomData.Event != null)
            {
                messageBuffer.AppendString(room.RoomData.Event.Name);
                messageBuffer.AppendString(room.RoomData.Event.Description);
                messageBuffer.AppendInteger((int)Math.Floor((room.RoomData.Event.Time - Yupi.GetUnixTimeStamp()) / 60.0));
            }

            return messageBuffer;
        }
    }
}
