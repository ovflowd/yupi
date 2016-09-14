using System.Linq;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class GetTalentsTrackMessageEvent : AbstractHandler
    {
        private readonly IRepository<Talent> TalentRepository;

        public GetTalentsTrackMessageEvent()
        {
            TalentRepository = DependencyFactory.Resolve<IRepository<Talent>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var trackType = message.GetString();
            TalentType talentType;

            if (TalentType.TryParse(trackType, out talentType))
            {
                var talents = TalentRepository.FilterBy(x => x.Type == talentType).ToList();

                router.GetComposer<TalentsTrackMessageComposer>().Compose(session, talentType, talents);
            }
        }
    }
}