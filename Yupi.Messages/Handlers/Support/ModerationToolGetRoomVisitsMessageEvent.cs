using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;

namespace Yupi.Messages.Support
{
	public class ModerationToolGetRoomVisitsMessageEvent : AbstractHandler
	{
		private IRepository<UserInfo> UserRepository;

		public ModerationToolGetRoomVisitsMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (session.Info.HasPermission("fuse_mod"))
			{
				int userId = message.GetInteger();

				UserInfo info = UserRepository.FindBy (userId);

				if (info != null) {
					router.GetComposer<ModerationToolRoomVisitsMessageComposer> ().Compose (session, info);
				}

			}
		}
	}
}

