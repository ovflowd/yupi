namespace Yupi.Messages.Support
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class ModerationRoomToolMessageComposer : Yupi.Messages.Contracts.ModerationRoomToolMessageComposer
    {
        #region Fields

        private RoomManager RoomManager;

        #endregion Fields

        #region Constructors

        public ModerationRoomToolMessageComposer()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        #endregion Constructors

        #region Methods

        // TODO Refactor
        public override void Compose(Yupi.Protocol.ISender session, RoomData data, bool isLoaded)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(data.Id);
                message.AppendInteger(RoomManager.UsersNow(data));

                message.AppendBool(false); // TODO Meaning? (isOwnerInRoom?)

                message.AppendInteger(data.Owner.Id);
                message.AppendString(data.Owner.Name);
                message.AppendBool(isLoaded);
                message.AppendString(data.Name);
                message.AppendString(data.Description);
                message.AppendInteger(data.Tags.Count);

                foreach (string current in data.Tags)
                    message.AppendString(current);

                message.AppendBool(false);

                session.Send(message);
            }
        }

        #endregion Methods
    }
}