using System;
using Yupi.Messages.Contracts;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using System.Collections.Generic;
using System.Linq;

namespace Yupi.Messages.Other
{
	public class InfoRetrieveMessageEvent : AbstractHandler
	{
		private IRepository<MessengerMessage> MessengerRepository;
		private IRepository<TargetedOffer> OfferRepository;

		public InfoRetrieveMessageEvent ()
		{
			MessengerRepository = DependencyFactory.Resolve<IRepository<MessengerMessage>> ();
			OfferRepository = DependencyFactory.Resolve<IRepository<TargetedOffer>> ();
		}

		public override void HandleMessage (Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<UserObjectMessageComposer> ().Compose (session, session.Info);
			router.GetComposer<BuildersClubMembershipMessageComposer> ().Compose (
				session, 
				session.Info.BuilderInfo.BuildersExpire,
				session.Info.BuilderInfo.BuildersItemsMax
			);

			// TODO Camera hardcoded (disabled)
			router.GetComposer<SendPerkAllowancesMessageComposer> ().Compose (session, session.Info, false);

			InitMessenger (session, router);

			router.GetComposer<CitizenshipStatusMessageComposer> ().Compose (session, "citizenship");
			router.GetComposer<GameCenterGamesListMessageComposer> ().Compose (session);
			router.GetComposer<AchievementPointsMessageComposer> ().Compose (session, session.Info.Wallet.AchievementPoints);

			router.GetComposer<FigureSetIdsMessageComposer> ().Compose (session);
			router.GetComposer<CatalogPromotionGetCategoriesMessageComposer> ().Compose (session);

			IList<TargetedOffer> offers = OfferRepository.FilterBy (x => x.ExpiresAt > DateTime.Now).ToList ();
			router.GetComposer<TargetedOfferMessageComposer> ().Compose (session, offers);
		}

		private void InitMessenger (Yupi.Model.Domain.Habbo session, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<LoadFriendsCategoriesComposer> ().Compose (session);

			router.GetComposer<LoadFriendsMessageComposer> ().Compose (session, session.Info.Relationships.Relationships);
			router.GetComposer<FriendRequestsMessageComposer> ().Compose (session, session.Info.Relationships.ReceivedRequests);

			var messages = MessengerRepository.FilterBy (x => x.To == session.Info && !x.Read);

			foreach (MessengerMessage message in messages) {
				router.GetComposer<ConsoleChatMessageComposer> ().Compose (session, message);
				message.Read = true;
				MessengerRepository.Save (message);
			}
		}
	}
}

