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
		private Repository<MessengerMessage> MessengerRepository;
		private Repository<TargetedOffer> OfferRepository;

		public InfoRetrieveMessageEvent ()
		{
			MessengerRepository = DependencyFactory.Resolve<Repository<MessengerMessage>> ();
			OfferRepository = DependencyFactory.Resolve<Repository<TargetedOffer>> ();
		}

		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<UserObjectMessageComposer> ().Compose (session, session.UserData.Info);
			router.GetComposer<BuildersClubMembershipMessageComposer> ().Compose (
				session, 
				session.UserData.Info.BuilderInfo.BuildersExpire,
				session.UserData.Info.BuilderInfo.BuildersItemsMax
			);

			// TODO Camera hardcoded (disabled)
			router.GetComposer<SendPerkAllowancesMessageComposer> ().Compose (session, session.UserData.Info, false);

			InitMessenger (session, router);

			// TODO Must this really be sent here?
			router.GetComposer<CitizenshipStatusMessageComposer> ().Compose (session, "citizenship");
			router.GetComposer<GameCenterGamesListMessageComposer> ().Compose (session);
			router.GetComposer<AchievementPointsMessageComposer> ().Compose (session, session.UserData.Info.Wallet.AchievementPoints);
			router.GetComposer<FigureSetIdsMessageComposer> ().Compose (session);

			router.GetComposer<CatalogPromotionGetCategoriesMessageComposer> ().Compose (session);

			IList<TargetedOffer> offers = OfferRepository.FilterBy (x => x.ExpiresAt > DateTime.Now).ToList ();
			router.GetComposer<TargetedOfferMessageComposer> ().Compose (session, offers);
		}

		private void InitMessenger (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<LoadFriendsCategoriesComposer> ().Compose (session);

			router.GetComposer<LoadFriendsMessageComposer> ().Compose (session, session.UserData.Info.Relationships.Relationships);
			router.GetComposer<FriendRequestsMessageComposer> ().Compose (session, session.UserData.Info.Relationships.ReceivedRequests);

			var messages = MessengerRepository.FilterBy (x => x.To == session.UserData.Info && !x.Read);

			foreach (MessengerMessage message in messages) {
				router.GetComposer<ConsoleChatMessageComposer> ().Compose (session, message);
				message.Read = true;
				MessengerRepository.Save (message);
			}
		}
	}
}

