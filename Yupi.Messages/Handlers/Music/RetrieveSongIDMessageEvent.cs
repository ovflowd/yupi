using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.Music
{
	public class RetrieveSongIDMessageEvent : AbstractHandler
	{
		private IRepository<SongData> SongRepository;

		public RetrieveSongIDMessageEvent ()
		{
			SongRepository = DependencyFactory.Resolve<IRepository<SongData>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string name = message.GetString();

			SongData song = SongRepository.FindBy (x => x.CodeName == name);

			if (song != null) {
				router.GetComposer<RetrieveSongIDMessageComposer> ().Compose (session, name, song.Id);
			}
		}
	}
}

