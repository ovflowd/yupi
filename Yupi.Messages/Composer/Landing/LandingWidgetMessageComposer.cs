using Yupi.Protocol;

namespace Yupi.Messages.Landing
{
    public class LandingWidgetMessageComposer : Contracts.LandingWidgetMessageComposer
    {
        public override void Compose(ISender session, string text)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                if (!string.IsNullOrEmpty(text))
                {
                    // TODO Refactor
                    var array = text.Split(',');

                    message.AppendString(text);
                    message.AppendString(array[1]);
                }
                else
                {
                    message.AppendString(string.Empty);
                    message.AppendString(string.Empty);
                }
                session.Send(message);
            }
        }
    }
}