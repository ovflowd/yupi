using Yupi.Controller;
using Yupi.Protocol;

namespace Yupi.Messages.Navigator
{
    public class NavigatorMetaDataComposer : Contracts.NavigatorMetaDataComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                var views = NavigatorView.GetAll();

                message.AppendInteger(views.Length);

                foreach (var view in views)
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
    }
}