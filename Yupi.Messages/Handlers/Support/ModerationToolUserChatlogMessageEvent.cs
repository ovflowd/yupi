using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;

namespace Yupi.Messages.Support
{
	public class ModerationToolUserChatlogMessageEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;

		public ModerationToolUserChatlogMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.UserData.Info.HasPermission("fuse_chatlogs"))
				return;

			int userId = message.GetInteger ();

			UserInfo info = UserRepository.FindBy (userId);

			if (info != null) {
				router.GetComposer<ModerationToolUserChatlogMessageComposer> ().Compose (session, info);
			}
		}
	}
}

