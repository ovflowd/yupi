using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class RoomDataMessageComposer : Yupi.Messages.Contracts.RoomDataMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, RoomData room, bool show, bool isNotReload)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(show);
				message.AppendBool(isNotReload);
				/*
				message.AppendBool(Yupi.GetGame().GetNavigator() != null && Yupi.GetGame().GetNavigator().GetPublicRoom(room.RoomData.Id) != null);
				message.AppendBool(!isNotReload || session.UserData.Info.HasPermission("fuse_mod"));
				message.AppendBool(room.IsMuted);
				message.AppendInteger(room.WhoCanMute);
				message.AppendInteger(room.WhoCanKick);
				message.AppendInteger(room.WhoCanBan);
				message.AppendBool(room.CheckRights(session, true));
				*/
				throw new NotImplementedException ();
				message.AppendInteger(room.ChatType);
				message.AppendInteger(room.ChatBalloon);
				message.AppendInteger(room.ChatSpeed);
				message.AppendInteger(room.ChatMaxDistance);
				message.AppendInteger(room.ChatFloodProtection);
				session.Send (message);
			}
		}
	}
}

