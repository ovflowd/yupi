using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomEnterErrorMessageComposer : AbstractComposer<RoomEnterErrorMessageComposer.Error>
    {
        public enum Error
        {
            ROOM_FULL = 1,
            BANNED = 4 // TODO Verify
        }

        public override void Compose(ISender session, Error error)
        {
            // Do nothing by default.
        }
    }
}