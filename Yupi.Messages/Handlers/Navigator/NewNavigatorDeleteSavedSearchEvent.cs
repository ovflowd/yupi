using System;
using Yupi.Util;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;

namespace Yupi.Messages.Navigator
{
	public class NewNavigatorDeleteSavedSearchEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;

		public NewNavigatorDeleteSavedSearchEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int searchId = request.GetInteger();

			session.UserData.Info.NavigatorLog.RemoveAll (x => x.Id == searchId);

			UserRepository.Save (session.UserData.Info);

			router.GetComposer<NavigatorSavedSearchesComposer> ().Compose (session, session.UserData.Info.NavigatorLog);
		}
	}
}

