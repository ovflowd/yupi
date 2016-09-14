namespace Yupi.Messages.Music
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class RetrieveSongIDMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<SongData> SongRepository;

        #endregion Fields

        #region Constructors

        public RetrieveSongIDMessageEvent()
        {
            SongRepository = DependencyFactory.Resolve<IRepository<SongData>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            string name = message.GetString();

            SongData song = SongRepository.FindBy(x => x.CodeName == name);

            if (song != null)
            {
                router.GetComposer<RetrieveSongIDMessageComposer>().Compose(session, name, song.Id);
            }
        }

        #endregion Methods
    }
}