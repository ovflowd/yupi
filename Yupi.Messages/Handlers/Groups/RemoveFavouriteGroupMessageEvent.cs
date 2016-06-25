﻿using System;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Groups
{
	public class RemoveFavouriteGroupMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
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
