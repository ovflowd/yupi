using System;




namespace Yupi.Messages.Other
{
	public class SaveRoomThumbnailMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			// TODO Refactor (exception driven control flow)
			int count = request.GetInteger ();

			// TODO Magic constant (50kB)
			if (count > 51200) {
				return;
			}

			byte[] bytes = request.GetBytes (count);

			throw new NotImplementedException ();

			/*
			string outData = Converter.Deflate (bytes);

			WebManager.HttpPostJson (ServerExtraSettings.StoriesApiThumbnailServerUrl, outData);
*/
			router.GetComposer<ThumbnailSuccessMessageComposer> ().Compose (session);
		}
	}
}

