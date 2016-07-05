using System;
using System.Collections.Generic;


namespace Yupi.Messages.Messenger
{
	public class ConsoleInviteFriendsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int count = request.GetInteger();

			List<uint> users = new List<uint>(count);

			for (int i = 0; i < count; i++) {
				users.Add (request.GetUInt32 ());
			}

			string content = request.GetString();

			foreach (uint userId in users) {
				if (!session.GetHabbo ().GetMessenger ().FriendshipExists (userId)) {
					continue;
				}

				GameClient client = Yupi.GetGame ().GetClientManager ().GetClientByUserId (userId);

				if (client == null) {
					continue;
				}
			
				router.GetComposer<ConsoleInvitationMessageComposer> ().Compose (client, session.GetHabbo ().Id, content);
			}
		}
	}
}