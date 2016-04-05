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
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Rooms.Data.Composers
{
     class RoomDataComposer
    {
         static SimpleServerMessageBuffer Compose(SimpleServerMessageBuffer roomDataMessage, GameClient session, Room room, RoomData data, bool isNotReload, bool? sendRoom = false, bool show = true)
        {
            roomDataMessage.Init(PacketLibraryManager.OutgoingHandler("RoomDataMessageComposer"));

            roomDataMessage.AppendBool(show);

            Serialize(roomDataMessage, data, true, !isNotReload);

            roomDataMessage.AppendBool(isNotReload);
            roomDataMessage.AppendBool(Yupi.GetGame().GetNavigator() != null && Yupi.GetGame().GetNavigator().GetPublicRoom(data.Id) != null);
            roomDataMessage.AppendBool(!isNotReload || session.GetHabbo().HasFuse("fuse_mod"));
            roomDataMessage.AppendBool(room?.RoomMuted == true);
            roomDataMessage.AppendInteger(data.WhoCanMute);
            roomDataMessage.AppendInteger(data.WhoCanKick);
            roomDataMessage.AppendInteger(data.WhoCanBan);
            roomDataMessage.AppendBool(room?.CheckRights(session, true) == true);
            roomDataMessage.AppendInteger(data.ChatType);
            roomDataMessage.AppendInteger(data.ChatBalloon);
            roomDataMessage.AppendInteger(data.ChatSpeed);
            roomDataMessage.AppendInteger(data.ChatMaxDistance);
            roomDataMessage.AppendInteger(data.ChatFloodProtection);

            return roomDataMessage;
        }

         static void Serialize(SimpleServerMessageBuffer messageBuffer, RoomData data, bool showEvents = false, bool enterRoom = false)
        {
            messageBuffer.AppendInteger(data.Id);
            messageBuffer.AppendString(data.Name);
            messageBuffer.AppendInteger(data.OwnerId);
            messageBuffer.AppendString(data.Owner);
            messageBuffer.AppendInteger(data.State);
            messageBuffer.AppendInteger(data.UsersNow);
            messageBuffer.AppendInteger(data.UsersMax);
            messageBuffer.AppendString(data.Description);
            messageBuffer.AppendInteger(data.TradeState);
            messageBuffer.AppendInteger(data.Score);
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(data.Category > 0 ? data.Category : 0);
            messageBuffer.AppendInteger(data.TagCount);

            foreach (string current in data.Tags.Where(current => current != null))
                messageBuffer.AppendString(current);

            string imageData = null;

            int enumType = enterRoom ? 32 : 0;

            PublicItem publicItem = Yupi.GetGame()?.GetNavigator()?.GetPublicRoom(data.Id);

            if (!string.IsNullOrEmpty(publicItem?.Image))
            {
                imageData = publicItem.Image;

                enumType += 1;
            }

            if (data.Group != null)
                enumType += 2;

            if (showEvents && data.Event != null)
                enumType += 4;

            if (data.Type == "private")
                enumType += 8;

            if (data.AllowPets)
                enumType += 16;

            messageBuffer.AppendInteger(enumType);

            if (imageData != null)
                messageBuffer.AppendString(imageData);

            if (data.Group != null)
            {
                messageBuffer.AppendInteger(data.Group.Id);
                messageBuffer.AppendString(data.Group.Name);
                messageBuffer.AppendString(data.Group.Badge);
            }

            if (showEvents && data.Event != null)
            {
                messageBuffer.AppendString(data.Event.Name);
                messageBuffer.AppendString(data.Event.Description);
                messageBuffer.AppendInteger((int)Math.Floor((data.Event.Time - Yupi.GetUnixTimeStamp()) / 60.0));
            }
        }
    }
}
