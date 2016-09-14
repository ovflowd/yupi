namespace Yupi.Messages.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Protocol.Buffers;

    public class HotelViewHallOfFameMessageComposer : Yupi.Messages.Contracts.HotelViewHallOfFameMessageComposer
    {
        #region Fields

        private IRepository<HallOfFameElement> FameRepository;

        #endregion Fields

        #region Constructors

        public HotelViewHallOfFameMessageComposer()
        {
            FameRepository = DependencyFactory.Resolve<IRepository<HallOfFameElement>>();
        }

        #endregion Constructors

        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string code)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(code);

                List<HallOfFameElement> rankings =
                    FameRepository.FilterBy(x => x.Competition == code).OrderByDescending(x => x.Score).ToList();

                message.AppendInteger(rankings.Count);

                for (int rank = 1; rank <= rankings.Count; ++rank)
                {
                    HallOfFameElement element = rankings[rank - 1];
                    message.AppendInteger(element.User.Id);
                    message.AppendString(element.User.Name);
                    message.AppendString(element.User.Look);
                    message.AppendInteger(rank);
                    message.AppendInteger(element.Score);
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}