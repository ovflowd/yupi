using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
    public class SaveRoomThumbnailMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            // TODO Refactor (exception driven control flow)
            var count = request.GetInteger();

            // TODO Magic constant (50kB)
            if (count > 51200) return;

            var bytes = request.GetBytes(count);

            throw new NotImplementedException();

            /*
            string outData = Converter.Deflate (bytes);

            WebManager.HttpPostJson (ServerExtraSettings.StoriesApiThumbnailServerUrl, outData);
*/
            router.GetComposer<ThumbnailSuccessMessageComposer>().Compose(session);
        }
    }
}