using System;
using System.Collections.Generic;

namespace Yupi.Messages.User
{
	public class ChangeUsernameMessageEvent : CheckUsernameMessageEvent
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string newName = message.GetString();
			string oldName = session.GetHabbo().UserName;

			List<string> alternatives;
			NameChangedUpdatesMessageComposer.Status status = Validate (newName, oldName, ref alternatives);

			if (status == NameChangedUpdatesMessageComposer.Status.OK && oldName != newName) {
				using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
					queryReactor.SetQuery (
						"UPDATE users SET username = @newname, last_name_change = @timestamp WHERE id = @userid");
					queryReactor.AddParameter ("newname", newName);
					queryReactor.AddParameter ("timestamp", Yupi.GetUnixTimeStamp () + 43200);
					queryReactor.AddParameter ("userid", session.GetHabbo ().Id);
					queryReactor.RunQuery ();
				}

				session.GetHabbo().LastChange = Yupi.GetUnixTimeStamp() + 43200;
				session.GetHabbo().UserName = text;

				router.GetComposer<UpdateUsernameMessageComposer> ().Compose (session, newName);
				// TODO Refactor
				session.GetHabbo().CurrentRoom.GetRoomUserManager().UpdateUser(userName, text);

				if (session.GetHabbo().CurrentRoom != null)
				{
					router.GetComposer<UserUpdateNameInRoomMessageComposer> ()
						.Compose (session.GetHabbo ().CurrentRoom, session.GetHabbo (), newName);
				}

				// TODO Update room owner 

				// TODO Notify messenger
			}
		}
	}
}

