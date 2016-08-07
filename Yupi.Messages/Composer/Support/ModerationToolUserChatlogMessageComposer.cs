using System;

using System.Data;
using Yupi.Protocol.Buffers;



namespace Yupi.Messages.Support
{
	public class ModerationToolUserChatlogMessageComposer : Yupi.Messages.Contracts.ModerationToolUserChatlogMessageComposer
	{
		// TODO Refactor
		public override void Compose ( Yupi.Protocol.ISender session, uint userId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (userId);
				message.AppendString (Yupi.GetGame ().GetClientManager ().GetUserNameByUserId (userId));

				DataRowCollection recentRooms = GetRecentRooms (userId);

				message.AppendInteger(recentRooms.Count);

				foreach (DataRow room in recentRooms) {
					uint roomId = (uint)room ["room_id"];
					RoomData roomData = Yupi.GetGame ().GetRoomManager ().GenerateRoomData (roomId);

					message.AppendByte(1);
					message.AppendShort(2);
					message.AppendString("roomName");
					message.AppendByte(2);
					message.AppendString(roomData == null ? "This room was deleted" : roomData.Name);
					message.AppendString("roomId");
					message.AppendByte(1);
					message.AppendInteger(roomId);

					DataRowCollection chatlog = GetChatlog (userId, roomId);

					message.AppendShort(chatlog.Count);

					foreach(DataRow chat in chatlog) {
						Habbo habboForId = Yupi.GetHabboById((uint) chat["user_id"]);
						Yupi.UnixToDateTime((double) chat["timestamp"]);

						if (habboForId == null)
							return null;

						message.AppendInteger(
							(int) (Yupi.GetUnixTimeStamp() - (double) chat["timestamp"]));

						message.AppendInteger(habboForId.Id);
						message.AppendString(habboForId.UserName);
						message.AppendString(chat["message"].ToString());
						message.AppendBool(false);
					}

					message.AppendByte(1);
					message.AppendShort(0);
					message.AppendShort(0);
				}

				session.Send (message);
			}
		}

		private DataRowCollection GetChatlog(uint userId, uint roomId) {
			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery (
					"SELECT user_id,timestamp,message FROM users_chatlogs WHERE room_id = @room AND user_id = @user ORDER BY timestamp DESC LIMIT 30");
				queryReactor.AddParameter ("room", roomId);
				queryReactor.AddParameter ("user", userId);
				return queryReactor.GetTable ().Rows;
			}
		}

		private DataRowCollection GetRecentRooms(uint userId) {
			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery (
					$"SELECT DISTINCT room_id FROM users_chatlogs WHERE user_id = '{userId}' ORDER BY timestamp DESC LIMIT 4");
				return queryReactor.GetTable().Rows;
			}



		}
	}
}

