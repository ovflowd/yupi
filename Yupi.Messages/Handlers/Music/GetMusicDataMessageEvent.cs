using System;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using System.Linq;



namespace Yupi.Messages.Music
{
	public class GetMusicDataMessageEvent : AbstractHandler
	{
		private IRepository<SongData> SongRepository;

		public GetMusicDataMessageEvent ()
		{
			SongRepository = DependencyFactory.Resolve<IRepository<SongData>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			int count = message.GetInteger();

			List<int> songsIds = new List<int>();

			for (int i = 0; i < count; i++)
			{
				int songId = message.GetInteger ();
				songsIds.Add (songId);
			}

			var songs = SongRepository.All().Where(x => songsIds.Contains(x.Id)).ToList();

			router.GetComposer<SongsMessageComposer>().Compose(session, songs);
		}
	}
}

