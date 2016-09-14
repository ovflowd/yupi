using System.Collections.Generic;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;
using Yupi.Util;

namespace Yupi.Messages.Messenger
{
    public class ConsoleSearchFriendMessageComposer : Contracts.ConsoleSearchFriendMessageComposer
    {
        private readonly ClientManager ClientManager;

        public ConsoleSearchFriendMessageComposer()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void Compose(ISender session, List<UserInfo> foundFriends, List<UserInfo> foundUsers)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(foundFriends.Count);

                foreach (var user in foundFriends) Serialize(message, user);

                message.AppendInteger(foundUsers.Count);

                foreach (var user in foundUsers) Serialize(message, user);

                session.Send(message);
            }
        }

        private void Serialize(ServerMessage reply, UserInfo user)
        {
            reply.AppendInteger(user.Id);
            reply.AppendString(user.Name);
            reply.AppendString(user.Motto);
            reply.AppendBool(ClientManager.IsOnline(user));
            reply.AppendBool(false); // TODO Hardcoded
            reply.AppendString(string.Empty);
            reply.AppendInteger(0);
            reply.AppendString(user.Look);
            reply.AppendString(user.LastOnline.ToUnix().ToString());
        }
    }
}