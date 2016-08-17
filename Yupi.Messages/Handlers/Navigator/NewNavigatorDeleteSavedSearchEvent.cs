using System;
using Yupi.Util;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;

namespace Yupi.Messages.Navigator
{
	public class NewNavigatorDeleteSavedSearchEvent : AbstractHandler
	{
		private IRepository<UserInfo> UserRepository;

		public NewNavigatorDeleteSavedSearchEvent ()
		{
			UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int searchId = request.GetInteger();

			session.Info.NavigatorLog.RemoveAll (x => x.Id == searchId);

			UserRepository.Save (session.Info);

			router.GetComposer<NavigatorSavedSearchesComposer> ().Compose (session, session.Info.NavigatorLog);
		}
	}
}

