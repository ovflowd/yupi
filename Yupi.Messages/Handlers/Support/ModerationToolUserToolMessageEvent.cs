using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.Support
{
	public class ModerationToolUserToolMessageEvent : AbstractHandler
	{
		private IRepository<UserInfo> UserRepository;
		private IRepository<SupportTicket> SupportRepository;

		public ModerationToolUserToolMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>> ();
			SupportRepository = DependencyFactory.Resolve<IRepository<SupportTicket>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			// TODO Rewrite rights management to prevent usage of strings...
			if (session.Info.HasPermission("fuse_mod"))
			{
				int userId = message.GetInteger();

				UserInfo info = UserRepository.FindBy (userId);
				var tickets = SupportRepository.FilterBy (x => x.Sender == session.Info);


				router.GetComposer<ModerationToolUserToolMessageComposer> ().Compose (session, info);
			}
		}
	}
}

