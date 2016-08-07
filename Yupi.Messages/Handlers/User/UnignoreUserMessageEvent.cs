using System;


namespace Yupi.Messages.User
{
	public class UnignoreUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string text = request.GetString();
			Yupi.Model.Domain.Habbo habbo = Yupi.GetGame().GetClientManager().GetClientByUserName(text).GetHabbo();

			if (habbo == null)
				return;

			//if (!session.GetHabbo().MutedUsers.Contains(habbo.Info.Id)) - Keeping Original incase I've done this wrong - Zak
            if(!session.UserData.Info.MutedUsers.Contains(habbo.Info))
				return;

            // Not sure if I've done this right, please confirm - Zak
            session.UserData.Info.MutedUsers.Remove(habbo.Info); 
			
            //session.GetHabbo().MutedUsers.Remove(habbo.Info.Id);

			router.GetComposer<UpdateIgnoreStatusMessageComposer> ().Compose (session, UpdateIgnoreStatusMessageComposer.State.LISTEN, username);
		}
	}
}

