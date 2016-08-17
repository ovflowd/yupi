using System;
using Yupi.Model;
using Yupi.Model.Repository;
using Yupi.Model.Domain;


namespace Yupi.Messages.Groups
{
	public class RemoveFavouriteGroupMessageEvent : AbstractHandler
	{
		private IRepository<UserInfo> UserRepository;

		public RemoveFavouriteGroupMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			request.GetUInt32(); // TODO Unused!
			session.Info.FavouriteGroup = null;

			UserRepository.Save (session.Info);

			router.GetComposer<FavouriteGroupMessageComposer> ().Compose (session, session.Info.Id);
			router.GetComposer<ChangeFavouriteGroupMessageComposer> ().Compose (session, null, 0);
		}
	}
}

