using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Support
{
    public class ModerationRoomToolMessageComposer : Contracts.ModerationRoomToolMessageComposer
    {
        private readonly RoomManager RoomManager;

        public ModerationRoomToolMessageComposer()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        // TODO Refactor
        public override void Compose(ISender session, RoomData data, bool isLoaded)
        {
            using (var message = Pool.GetMessageBuffer(Id))
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

                foreach (var current in data.Tags)
                    message.AppendString(current);

                message.AppendBool(false);

                session.Send(message);
            }
        }
    }
}