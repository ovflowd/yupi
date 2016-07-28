using System;


namespace Yupi.Messages.Support
{
	public class ModerationToolUserToolMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			// TODO Rewrite rights management to prevent usage of strings...
			if (session.GetHabbo().HasFuse("fuse_mod"))
			{
				uint userId = message.GetUInt32();
				router.GetComposer<ModerationToolUserToolMessageComposer> ().Compose (session, userId);
			}
		}
	}
}

