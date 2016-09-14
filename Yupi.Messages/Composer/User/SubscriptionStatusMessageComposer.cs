using System;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class SubscriptionStatusMessageComposer : Contracts.SubscriptionStatusMessageComposer
    {
        public override void Compose(ISender session, Subscription subscription)
        {
            // TODO refactor
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString("club_habbo"); // product name

                if ((subscription != null) && subscription.IsValid())
                {
                    var days = (subscription.ExpireTime - DateTime.Now).Days;
                    var activeFor = (DateTime.Now - subscription.ActivateTime).Days;
                    var months = days/31;

                    message.AppendInteger(days - months*31);
                    message.AppendInteger(1);
                    message.AppendInteger(months);
                    message.AppendInteger(1);
                    message.AppendBool(true);
                    message.AppendBool(true);
                    message.AppendInteger(activeFor);
                    message.AppendInteger(activeFor);
                    message.AppendInteger(10);
                }
                else
                {
                    // TODO Create Subscription.None
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendBool(false);
                    message.AppendBool(false);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                }
                session.Send(message);
            }
        }
    }
}