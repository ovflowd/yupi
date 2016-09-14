namespace Yupi.Messages.Messenger
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class LoadFriendsMessageComposer : Yupi.Messages.Contracts.LoadFriendsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<Relationship> friends)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendInteger(0);
                message.AppendInteger(friends.Count);

                foreach (Relationship relationship in friends)
                {
                    message.AppendInteger(relationship.Friend.Id);
                    message.AppendString(relationship.Friend.Name);
                    /*
                    message.AppendInteger(relationship.Friend.IsOnline);
                    message.AppendBool(!relationship.Friend.AppearOffline && relationship.Friend.IsOnline);
                    message.AppendBool(!relationship.Friend.HideInRoom && relationship.Friend.InRoom);
                    */
                    throw new NotImplementedException();
                    message.AppendString(relationship.Friend.Look);
                    message.AppendInteger(0);
                    message.AppendString(relationship.Friend.Motto);
                    message.AppendString(string.Empty);
                    message.AppendString(string.Empty);
                    message.AppendBool(true);
                    message.AppendBool(false);
                    message.AppendBool(false);
                    message.AppendShort((short) relationship.Type);
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}