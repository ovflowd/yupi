using System;
using Yupi.Net;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class OpenQuestsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<QuestListMessageComposer> ().Compose (session);
		}
	}
}