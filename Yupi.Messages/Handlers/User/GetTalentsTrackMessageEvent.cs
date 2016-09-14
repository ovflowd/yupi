namespace Yupi.Messages.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GetTalentsTrackMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Talent> TalentRepository;

        #endregion Fields

        #region Constructors

        public GetTalentsTrackMessageEvent()
        {
            TalentRepository = DependencyFactory.Resolve<IRepository<Talent>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            string trackType = message.GetString();
            TalentType talentType;

            if (TalentType.TryParse(trackType, out talentType))
            {
                List<Talent> talents = TalentRepository.FilterBy(x => x.Type == talentType).ToList();

                router.GetComposer<TalentsTrackMessageComposer>().Compose(session, talentType, talents);
            }
        }

        #endregion Methods
    }
}