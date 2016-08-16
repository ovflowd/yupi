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

		public override void Compose (Yupi.Protocol.ISender session, UserInfo user, UserInfo requester)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (user.Id);
				message.AppendString (user.UserName);
				message.AppendString (user.Look);
				message.AppendString (user.Motto);
				message.AppendString (user.CreateDate.ToString ("dd/MM/yyyy"));
				message.AppendInteger (user.Wallet.AchievementPoints);
				message.AppendInteger (user.Relationships.Relationships.Count);
				message.AppendBool (user.Relationships.IsFriendsWith (requester));
				message.AppendBool (requester.Relationships.HasSentRequestTo (user));
				message.AppendBool (Manager.IsOnline (user));
			
				message.AppendInteger (user.UserGroups.Count);

				foreach (Group group in user.UserGroups) {
					message.AppendInteger (group.Id);
					message.AppendString (group.Name);
					message.AppendString (group.Badge);
					message.AppendString (group.Colour1.Colour.ToString());
					message.AppendString (group.Colour2.Colour.ToString());
					message.AppendBool (group == user.FavouriteGroup);
					message.AppendInteger (-1);
					message.AppendBool (group.Forum != null);
				}

				message.AppendInteger ((int)(DateTime.Now - user.LastOnline).TotalSeconds);
				message.AppendBool (true);

				session.Send (message);
			}
		}
	}
}

