using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SuperNotificationMessageComposer : AbstractComposer
    {
        public virtual void Compose(ISender session, string title, string content, string url = "", string urlName = "",
            string unknown = "", int unknown2 = 4)
        {
            // Do nothing by default.
        }
    }
}