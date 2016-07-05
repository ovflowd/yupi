using System;
using Yupi.Messages.User;
using Yupi.Messages.GameCenter;
using Yupi.Messages.Catalog;

namespace Yupi.Messages.Other
{
	public class InfoRetrieveMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<UserObjectMessageComposer> ().Compose (session, session.GetHabbo ());
			router.GetComposer<BuildersClubMembershipMessageComposer> ().Compose (
				session, 
				session.GetHabbo ().BuildersExpire,
				session.GetHabbo ().BuildersItemsMax
			);
			router.GetComposer<SendPerkAllowancesMessageComposer> ().Compose (session);

			session.GetHabbo().InitMessenger();

			// TODO Must this really be sent here?
			router.GetComposer<CitizenshipStatusMessageComposer> ().Compose (session, "citizenship");
			router.GetComposer<GameCenterGamesListMessageComposer> ().Compose (session);
			router.GetComposer<AchievementPointsMessageComposer> ().Compose (session, session.GetHabbo ().AchievementPoints);
			router.GetComposer<FigureSetIdsMessageComposer> ().Compose (session);

			router.GetComposer<CatalogPromotionGetCategoriesMessageComposer> ().Compose (session);

			if (Yupi.GetGame().GetTargetedOfferManager().CurrentOffer != null)
			{
				router.GetComposer<TargetedOfferMessageComposer> ().Compose (
					session, Yupi.GetGame ().GetTargetedOfferManager ().CurrentOffer);
			}
		}
	}
}

