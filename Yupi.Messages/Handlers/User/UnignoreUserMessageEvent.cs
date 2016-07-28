using System;


namespace Yupi.Messages.User
{
	public class UnignoreUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string text = request.GetString();
			Habbo habbo = Yupi.GetGame().GetClientManager().GetClientByUserName(text).GetHabbo();

			if (habbo == null)
				return;

			if (!session.GetHabbo().MutedUsers.Contains(habbo.Id))
				return;

			session.GetHabbo().MutedUsers.Remove(habbo.Id);

			router.GetComposer<UpdateIgnoreStatusMessageComposer> ().Compose (session, UpdateIgnoreStatusMessageComposer.State.LISTEN, username);
		}
	}
}

