namespace Yupi.Messages.Messenger
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class FriendUpdateMessageComposer : Yupi.Messages.Contracts.FriendUpdateMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Relationship relationship)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0);
                message.AppendInteger(1); // TODO Refactor!

                if (relationship.Deleted)
                {
                    message.AppendInteger(-1); // DELETED
                    message.AppendInteger(relationship.Friend.Id);
                }
                else
                {
                    message.AppendInteger(0);
                    message.AppendInteger(relationship.Friend.Id);
                    message.AppendString(relationship.Friend.Name);
                    /*
                    message.AppendInteger (relationship.Friend.IsOnline);
                    message.AppendBool (!relationship.Friend.AppearOffline && relationship.Friend.IsOnline);
                    message.AppendBool (!relationship.Friend.HideInRoom && relationship.Friend.InRoom);
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
                    message.AppendBool(false);
                    session.Send(message);
                }
            }
        }

        #endregion Methods
    }
}