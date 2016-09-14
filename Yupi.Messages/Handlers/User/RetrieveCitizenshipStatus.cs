using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class RetrieveCitizenshipStatus : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var whatever = message.GetString(); // TODO What does the string contain?
            router.GetComposer<CitizenshipStatusMessageComposer>().Compose(session, whatever);
        }
    }
}