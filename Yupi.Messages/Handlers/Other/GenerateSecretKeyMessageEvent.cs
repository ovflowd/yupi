using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
    public class GenerateSecretKeyMessageEvent : AbstractHandler
    {
        public override bool RequireUser
        {
            get { return false; }
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            request.GetString(); // TODO unused

            router.GetComposer<SecretKeyMessageComposer>().Compose(session);
        }
    }
}