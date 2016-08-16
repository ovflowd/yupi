using System;
using Yupi.Model;
using Yupi.Model.Repository;
using Yupi.Model.Domain;


namespace Yupi.Messages.Groups
{
	public class RemoveFavouriteGroupMessageEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;

		public RemoveFavouriteGroupMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			request.GetUInt32(); // TODO Unused!
			session.UserData.Info.FavouriteGroup = null;

			UserRepository.Save (session.UserData.Info);

			router.GetComposer<FavouriteGroupMessageComposer> ().Compose (session, session.UserData.Info.Id);
			router.GetComposer<ChangeFavouriteGroupMessageComposer> ().Compose (session, null, 0);
		}
	}
}

