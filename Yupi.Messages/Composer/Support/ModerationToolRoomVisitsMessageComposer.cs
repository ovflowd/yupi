using System;
using Yupi.Protocol.Buffers;


using System.Linq;

namespace Yupi.Messages.Support
{
	public class ModerationToolRoomVisitsMessageComposer : Yupi.Messages.Contracts.ModerationToolRoomVisitsMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, uint userId)
		{
			// TODO Only works when user is online
			GameClient user = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(userId);

				if (user?.GetHabbo () == null) {
					message.AppendString ("Not online");
					message.AppendInteger (0);
				} else {
					message.AppendString(user.GetHabbo().UserName);
					message.StartArray();
					// TODO Refactor
					foreach (
						RoomData roomData in
						user.GetHabbo()
						.RecentlyVisitedRooms.Select(roomId => Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId))
						.Where(roomData => roomData != null))
					{
						message.AppendInteger(roomData.Id);
						message.AppendString(roomData.Name);

						message.AppendInteger(0); //hour
						message.AppendInteger(0); //min

						message.SaveArray();
					}

					message.EndArray();
				}

				session.Send (message);
			}
		}
	}
}

