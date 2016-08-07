using System;




namespace Yupi.Messages.Other
{
	public class SaveRoomThumbnailMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			try
			{
				// TODO Refactor (exception driven control flow)
				int count = request.GetInteger();

				byte[] bytes = request.GetBytes(count);

				string outData = Converter.Deflate(bytes);

				WebManager.HttpPostJson(ServerExtraSettings.StoriesApiThumbnailServerUrl, outData);

				router.GetComposer<ThumbnailSuccessMessageComposer>().Compose(session);
			}
			catch
			{
				session.SendNotif("Please Try Again. This Area has too many elements.");
			}
		}
	}
}

