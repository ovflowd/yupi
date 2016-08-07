using System;


namespace Yupi.Messages.User
{
	public class IgnoreUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string username = request.GetString();
			// TODO Really?! By username?! Who the hell thought that would be a good idea? S.u.l.a.k.e ...?
			Habbo habbo = Yupi.GetGame().GetClientManager().GetClientByUserName(username).GetHabbo();

			if (habbo == null)
				return;
			// TODO Rename mute to ignore!
			if (session.GetHabbo().MutedUsers.Contains(habbo.Id) || habbo.Rank > 4u)
				return;

			session.GetHabbo().MutedUsers.Add(habbo.Id);
			router.GetComposer<UpdateIgnoreStatusMessageComposer> ().Compose (session, UpdateIgnoreStatusMessageComposer.State.IGNORE, username);
		}
	}
}

