using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;


namespace Yupi.Messages.User
{
	public class SetInvitationsPreferenceMessageEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;

		public SetInvitationsPreferenceMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			session.UserData.Info.Preferences.IgnoreRoomInvite = message.GetBool();
			UserRepository.Save (session.UserData.Info);
		}
	}
}

