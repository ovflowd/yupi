using System;

using System.Data;

using Yupi.Protocol.Buffers;
using System.Linq;


namespace Yupi.Messages.Rooms
{
	public class LoadRoomRightsListMessageComposer : AbstractComposer<Room>
	{
		public override void Compose (Yupi.Protocol.ISender session, Room room)
		{
			// TODO Really? Query the DB each time?!
			DataTable table;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery("SELECT user_id FROM rooms_rights WHERE room_id = " + room.RoomId);

				table = queryReactor.GetTable();
			}

			int rowCount = table != null && table.Rows.Count > 0 ? table.Rows.Count : 0;

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(room.RoomData.Id);
				message.AppendInteger(rowCount);

				if (table != null && rowCount > 0)
				{
					foreach (Habbo habboForId in table.Rows.Cast<DataRow>().Select(dataRow => Yupi.GetHabboById((uint)dataRow["user_id"])).Where(habboForId => habboForId != null))
					{
						message.AppendInteger(habboForId.Id);
						message.AppendString(habboForId.UserName);
					}
				}
				session.Send (message);
			}
		}
	}
}

