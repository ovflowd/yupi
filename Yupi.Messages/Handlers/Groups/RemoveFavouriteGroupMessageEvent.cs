using System;


namespace Yupi.Messages.Groups
{
	public class RemoveFavouriteGroupMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			request.GetUInt32();
			session.GetHabbo().FavouriteGroup = 0;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery ("UPDATE users_stats SET favourite_group = 0 WHERE id = @user_id");
				queryReactor.AddParameter("user_id", session.GetHabbo ().Id);
				queryReactor.RunQuery ();
			}

			router.GetComposer<FavouriteGroupMessageComposer> ().Compose (session, session.GetHabbo ().Id);


			router.GetComposer<ChangeFavouriteGroupMessageComposer> ().Compose (session, null, 0);
		}
	}
}

