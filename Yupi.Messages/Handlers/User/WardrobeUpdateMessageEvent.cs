using System;


namespace Yupi.Messages.User
{
	public class WardrobeUpdateMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			uint slot = message.GetUInt32();
			string look = message.GetString();
			string gender = message.GetString();

			look = Yupi.FilterFigure(look);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery("SELECT null FROM users_wardrobe WHERE user_id = @user AND slot_id = @slot");
				queryReactor.AddParameter("user", session.GetHabbo().Id);
				queryReactor.AddParameter("slot", slot);
				queryReactor.AddParameter("look", look);
				queryReactor.AddParameter("gender", gender);

				if (queryReactor.GetRow() != null)
				{
					queryReactor.SetQuery("UPDATE users_wardrobe SET look = @look, gender = @gender WHERE user_id = @user AND slot_id = @slot");
				}
				else
				{
					queryReactor.SetQuery(
						"INSERT INTO users_wardrobe (user_id,slot_id,look,gender) VALUES (@user,@slot,@look,@gender)");
				}

				queryReactor.AddParameter("user", session.GetHabbo().Id);
				queryReactor.AddParameter("slot", slot);
				queryReactor.AddParameter("look", look);
				queryReactor.AddParameter("gender", gender);
				queryReactor.RunQuery();
			}
		}
	}
}

