using System;
using System.Collections.Generic;

namespace Yupi.Messages.User
{
	public class ChangeUsernameMessageEvent : CheckUsernameMessageEvent
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string newName = message.GetString();

			List<string> alternatives;
			NameChangedUpdatesMessageComposer.Status status = Validate (newName, session.UserData.Info.UserName, out alternatives);

			if (status == NameChangedUpdatesMessageComposer.Status.OK) {
				session.UserData.Info.UserName = newName;

				router.GetComposer<UpdateUsernameMessageComposer> ().Compose (session, newName);
				// TODO Refactor

				UserRepository.Save (session.UserData.Info);

				if (session.UserData.Room != null)
				{
					router.GetComposer<UserUpdateNameInRoomMessageComposer> ()
						.Compose (session.UserData.Room, session.UserData);
				}

				// TODO Update room owner 

				// TODO Notify messenger
			}
		}
	}
}

