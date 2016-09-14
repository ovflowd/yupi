namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class SuperNotificationMessageComposer : AbstractComposer
    {
        #region Methods

        public virtual void Compose(Yupi.Protocol.ISender session, string title, string content, string url = "",
            string urlName = "", string unknown = "", int unknown2 = 4)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}