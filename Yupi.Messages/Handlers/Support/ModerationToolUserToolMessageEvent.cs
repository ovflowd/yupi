using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.Support
{
	public class ModerationToolUserToolMessageEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;
		private Repository<SupportTicket> SupportRepository;

		public ModerationToolUserToolMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
			SupportRepository = DependencyFactory.Resolve<Repository<SupportTicket>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			// TODO Rewrite rights management to prevent usage of strings...
			if (session.UserData.Info.HasPermission("fuse_mod"))
			{
				int userId = message.GetInteger();

				UserInfo info = UserRepository.FindBy (userId);
				var tickets = SupportRepository.FilterBy (x => x.Sender == session.UserData.Info);


				router.GetComposer<ModerationToolUserToolMessageComposer> ().Compose (session, info);
			}
		}
	}
}

