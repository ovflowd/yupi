using System;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Users.Relationships;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.User
{
	public class SetRelationshipMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			uint targetId = message.GetUInt32();
			uint type = message.GetUInt32();

			// TODO Verify targetId !

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				// TODO Refactor relationships
				queryReactor.SetQuery ("SELECT id FROM users_relationships WHERE user_id=@user AND target=@target LIMIT 1");
				queryReactor.AddParameter ("user", session.GetHabbo ().Id);
				queryReactor.AddParameter ("target", targetId);

				int id = queryReactor.GetInteger ();

				if (id == 0) {
					queryReactor.SetQuery ("INSERT INTO users_relationships (user_id, target, type) VALUES (@id, @target, @type)");
				} else {
					queryReactor.SetQuery("UPDATE users_relationships SET user_id = @user, target = @target, type = @type WHERE id = @id");
					queryReactor.AddParameter ("id", id);
				}

				queryReactor.AddParameter("user", session.GetHabbo().Id);
				queryReactor.AddParameter("target", targetId);
				queryReactor.AddParameter("type", type);

				if (id == 0) {
					queryReactor.RunQuery ();
					session.GetHabbo ().Relationships [id].UserId = targetId;
					session.GetHabbo ().Relationships [id].Type = type;
				} else {
					// TODO long vs int
					id = (int)queryReactor.InsertQuery ();
					session.GetHabbo().Relationships.Add(id, new Relationship(id, (int)targetId, (int)type));
				}
			}


			GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(targetId);

			if (clientByUserId != null)
				session.GetHabbo().GetMessenger().UpdateFriend(targetId, clientByUserId, true);
		}
	}
}

