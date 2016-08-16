using System;


namespace Yupi.Messages.User
{
	public class UnignoreUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string text = request.GetString();
			throw new NotImplementedException ();
			/*
			Yupi.Model.Domain.Habbo habbo = Yupi.GetGame().GetClientManager().GetClientByUserName(text).GetHabbo();

			if (habbo == null)
				return;

            if(!session.UserData.Info.MutedUsers.Contains(habbo.Info))
				return;

            session.UserData.Info.MutedUsers.Remove(habbo.Info); 
			// TODO Save
			router.GetComposer<UpdateIgnoreStatusMessageComposer> ().Compose (session, UpdateIgnoreStatusMessageComposer.State.LISTEN, username);
			*/
		}
	}
}

