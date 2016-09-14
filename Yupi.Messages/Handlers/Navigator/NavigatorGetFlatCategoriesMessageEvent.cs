using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using System.Linq;

namespace Yupi.Messages.Navigator
{
	public class NavigatorGetFlatCategoriesMessageEvent : AbstractHandler
	{
		private IRepository<FlatNavigatorCategory> NavigatorRepository;

		public NavigatorGetFlatCategoriesMessageEvent ()
		{
			NavigatorRepository = DependencyFactory.Resolve<IRepository<FlatNavigatorCategory>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<FlatCategoriesMessageComposer> ().Compose (session, NavigatorRepository.All().ToList(), session.Info.Rank);
		}
	}
}

