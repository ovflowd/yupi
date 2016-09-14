using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
    public class CheckPetnameMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var petName = message.GetString();
            var statusCode = 0;

            // TODO Use enum

            if (petName.Length > 15)
                statusCode = 1;
            else if (petName.Length < 3)
                statusCode = 2;

            /* TODO Reimplement
            else if (!Yupi.Emulator.Yupi.IsValidAlphaNumeric(petName))
                statusCode = 3;
                */

            router.GetComposer<CheckPetNameMessageComposer>().Compose(session, statusCode, petName);
        }
    }
}