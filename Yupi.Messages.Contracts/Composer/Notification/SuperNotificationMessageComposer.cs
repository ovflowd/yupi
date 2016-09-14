using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class SuperNotificationMessageComposer : AbstractComposer
    {
        public virtual void Compose(Yupi.Protocol.ISender session, string title, string content, string url = "",
            string urlName = "", string unknown = "", int unknown2 = 4)
        {
            // Do nothing by default.
        }
    }
}