using System;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using System.Data;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class LoadWardrobeMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session)
		{
			// TODO Query should really not be within composer!!!
			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery(
					"SELECT slot_id, look, gender FROM users_wardrobe WHERE user_id = @user");
				queryReactor.AddParameter("user", session.GetHabbo().Id);

				DataTable table = queryReactor.GetTable();

				using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
					message.AppendInteger (table.Rows.Count);
					foreach (DataRow dataRow in table.Rows) {
						message.AppendInteger (Convert.ToUInt32 (dataRow ["slot_id"]));
						message.AppendString ((string)dataRow ["look"]);
						message.AppendString (dataRow ["gender"].ToString ().ToUpper ());
					}

					session.Send (message);
				}
			}
		}
	}
}

