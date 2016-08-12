using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Controller;

namespace Yupi.Messages.Messenger
{
	public class RequestFriendMessageEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;
		private ClientManager ClientManager;

		public RequestFriendMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>>();
			ClientManager = DependencyFactory.Resolve<ClientManager>();
		}

		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string friendName =	request.GetString ();

			UserInfo friend = UserRepository.FindBy (x => x.UserName == friendName);

			if (friend != null) {

				if (friend.HasFriendRequestsDisabled) {
					router.GetComposer<NotAcceptingRequestsMessageComposer> ().Compose (session);
				} else {

					if (!session.UserData.Info.Relationships.HasSentRequestTo (friend)) {
						var friendRequest = new FriendRequest(session.UserData.Info, friend);

						session.UserData.Info.Relationships.SentRequests.Add (friendRequest);

						UserRepository.Save (session.UserData.Info);

						var friendSession = ClientManager.GetByInfo (friend);

						if (friendSession != null) {
							friendSession.Router.GetComposer<ConsoleSendFriendRequestMessageComposer> ().Compose (friendSession, friendRequest);
						}
					} 
				}
			}

		}
	}
}

