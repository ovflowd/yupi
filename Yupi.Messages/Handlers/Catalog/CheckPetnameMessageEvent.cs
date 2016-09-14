namespace Yupi.Messages.Catalog
{
    using System;

    public class CheckPetnameMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            string petName = message.GetString();
            int statusCode = 0;

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

        #endregion Methods
    }
}