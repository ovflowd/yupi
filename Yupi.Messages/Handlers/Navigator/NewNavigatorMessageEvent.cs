using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using System.Linq;
using Yupi.Model;

namespace Yupi.Messages.Navigator
{
	public class NewNavigatorMessageEvent : AbstractHandler
	{
		private IRepository<NavigatorCategory> NavigatorRepository;

		public NewNavigatorMessageEvent ()
		{
			NavigatorRepository = DependencyFactory.Resolve<IRepository<NavigatorCategory>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<NavigatorMetaDataComposer> ().Compose (session);
			router.GetComposer<NavigatorLiftedRoomsComposer> ().Compose (session);
			router.GetComposer<NavigatorCategoriesComposer> ().Compose (session, NavigatorRepository.All().ToList ());
			router.GetComposer<NavigatorSavedSearchesComposer> ().Compose (session, session.Info.NavigatorLog);
			router.GetComposer<NewNavigatorSizeMessageComposer> ().Compose (session, session.Info.Preferences);
		}
	}
}

