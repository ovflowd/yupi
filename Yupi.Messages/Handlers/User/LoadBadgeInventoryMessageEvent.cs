using System;


namespace Yupi.Messages.User
{
	public class LoadBadgeInventoryMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<LoadBadgesWidgetMessageComposer>().Compose(session, session.UserData.Info.Badges);
		}
	}
}

