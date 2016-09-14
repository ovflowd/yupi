using Yupi.Model.Domain.Components;
using Yupi.Protocol;

namespace Yupi.Messages.Navigator
{
    public class NewNavigatorSizeMessageComposer : Contracts.NewNavigatorSizeMessageComposer
    {
        public override void Compose(ISender session, UserPreferences preferences)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(preferences.NewnaviX);
                message.AppendInteger(preferences.NewnaviY);
                message.AppendInteger(preferences.NavigatorWidth);
                message.AppendInteger(preferences.NavigatorHeight);
                message.AppendBool(false);
                message.AppendInteger(1);
                session.Send(message);
            }
        }
    }
}