using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Controller;
using Yupi.Util;


namespace Yupi.Messages.Messenger
{
	public class AcceptFriendMessageEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;
		private AchievementManager AchievementManager;
		private ClientManager ClientManager;

		public AcceptFriendMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>>();
			AchievementManager = DependencyFactory.Resolve<AchievementManager>();
			ClientManager = DependencyFactory.Resolve<ClientManager>();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int count = request.GetInteger();
			for (int i = 0; i < count; i++)
			{
				int userId = request.GetInteger();

				UserInfo friend = UserRepository.FindBy (userId);

				if (friend != null && friend.Relationships.HasSentRequestTo (session.UserData.Info)) {
					Relationship friendRelation = friend.Relationships.Add (session.UserData.Info);
					Relationship userRelation = session.UserData.Info.Relationships.Add (friend);

					friend.Relationships.SentRequests.RemoveAll (x => x.To == session.UserData.Info);

					AchievementManager.ProgressUserAchievement(session.UserData, "ACH_FriendListSize", 1);

					session.Router.GetComposer<FriendUpdateMessageComposer> ().Compose (session, userRelation);

					var friendSession = ClientManager.GetByInfo (friend);

					if (friendSession != null) {
						friendSession.Router.GetComposer<FriendUpdateMessageComposer> ()
							.Compose (friendSession, friendRelation);
					}

					UserRepository.Save (friend);
					UserRepository.Save (session.UserData.Info);
				}
			}
		}
	}
}

