namespace Yupi.Messages.Navigator
{
    using System;

    using Yupi.Controller;
    using Yupi.Protocol.Buffers;

    public class NavigatorMetaDataComposer : Yupi.Messages.Contracts.NavigatorMetaDataComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                NavigatorView[] views = NavigatorView.GetAll();

                message.AppendInteger(views.Length);

                foreach (NavigatorView view in views)
                {
                    message.AppendString(view.DisplayName);
                    // TODO Could not find out where this is being used in client?!
                    message.AppendInteger(1); // Count Saved Searches
                    message.AppendInteger(1); // Saved Search Id
                    message.AppendString("query");
                    message.AppendString("filter");
                    message.AppendString("localization");
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}