using System.Linq;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class HotelViewHallOfFameMessageComposer : Contracts.HotelViewHallOfFameMessageComposer
    {
        private readonly IRepository<HallOfFameElement> FameRepository;

        public HotelViewHallOfFameMessageComposer()
        {
            FameRepository = DependencyFactory.Resolve<IRepository<HallOfFameElement>>();
        }

        public override void Compose(ISender session, string code)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(code);

                var rankings =
                    FameRepository.FilterBy(x => x.Competition == code).OrderByDescending(x => x.Score).ToList();

                message.AppendInteger(rankings.Count);

                for (var rank = 1; rank <= rankings.Count; ++rank)
                {
                    var element = rankings[rank - 1];
                    message.AppendInteger(element.User.Id);
                    message.AppendString(element.User.Name);
                    message.AppendString(element.User.Look);
                    message.AppendInteger(rank);
                    message.AppendInteger(element.Score);
                }
                session.Send(message);
            }
        }
    }
}