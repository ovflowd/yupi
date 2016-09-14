using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;


namespace Yupi.Messages.User
{
	public class SetInvitationsPreferenceMessageEvent : AbstractHandler
	{
		private IRepository<UserInfo> UserRepository;

		public SetInvitationsPreferenceMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			session.Info.Preferences.IgnoreRoomInvite = message.GetBool();
			UserRepository.Save (session.Info);
		}
	}
}

