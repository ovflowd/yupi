using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class BotSpeechListMessageComposer : AbstractComposer<int, BotInfo>
    {
        public override void Compose(ISender session, int num, BotInfo bot)
        {
            // Do nothing by default.
        }
    }
}