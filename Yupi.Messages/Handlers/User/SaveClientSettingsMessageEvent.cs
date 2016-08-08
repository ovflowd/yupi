using System;
using Yupi.Model.Domain.Components;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.User
{
	public class SaveClientSettingsMessageEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;

		public SaveClientSettingsMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			UserPreferences preferences = session.UserData.Info.Preferences;

			// TODO Validate values
			preferences.Volume1 = message.GetInteger();
			preferences.Volume2 = message.GetInteger();
			preferences.Volume3 = message.GetInteger();
			UserRepository.Save (session.UserData.Info);
		}
	}
}

