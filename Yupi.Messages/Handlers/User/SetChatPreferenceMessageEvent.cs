using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;


namespace Yupi.Messages.User
{
	public class SetChatPreferenceMessageEvent : AbstractHandler
	{
		private IRepository<UserInfo> UserRepository;

		public SetChatPreferenceMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			session.Info.Preferences.PreferOldChat = message.GetBool();
			UserRepository.Save (session.Info);
		}
	}
}

