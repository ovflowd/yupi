using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model;

namespace Yupi.Messages.User
{
	public class UserProfileMessageComposer : Yupi.Messages.Contracts.UserProfileMessageComposer
	{
		private ClientManager Manager;

		public UserProfileMessageComposer ()
		{
			Manager = DependencyFactory.Resolve<ClientManager> ();
		}

		public override void Compose (Yupi.Protocol.ISender session, UserInfo habbo, UserInfo requester)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (habbo.Id);
				message.AppendString (habbo.UserName);
				message.AppendString (habbo.Look);
				message.AppendString (habbo.Motto);
				message.AppendString (habbo.CreateDate.ToString ("dd/MM/yyyy"));
				message.AppendInteger (habbo.Wallet.AchievementPoints);
				message.AppendInteger (habbo.Relationships.Relationships.Count);
				message.AppendBool (habbo.Relationships.IsFriendsWith (requester));
				message.AppendBool (requester.Relationships.SentRequests.Contains (habbo));
				message.AppendBool (Manager.IsOnline (habbo));
			
				message.AppendInteger (habbo.UserGroups.Count);

				foreach (Group group in habbo.UserGroups) {
					message.AppendInteger (group.Id);
					message.AppendString (group.Name);
					message.AppendString (group.Badge);
					message.AppendString (group.Colour1.Colour);
					message.AppendString (group.Colour2.Colour);
					message.AppendBool (group == habbo.FavouriteGroup);
					message.AppendInteger (-1);
					message.AppendBool (group.Forum != null);
				}

				message.AppendInteger ((int)(DateTime.Now - habbo.LastOnline).TotalSeconds);
				message.AppendBool (true);

				session.Send (message);
			}
		}
	}
}

