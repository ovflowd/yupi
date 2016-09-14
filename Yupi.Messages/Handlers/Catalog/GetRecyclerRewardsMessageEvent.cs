using System.Linq;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Catalog
{
    public class GetRecyclerRewardsMessageEvent : AbstractHandler
    {
        private readonly IRepository<EcotronLevel> EcotronRepository;

        public GetRecyclerRewardsMessageEvent()
        {
            EcotronRepository = DependencyFactory.Resolve<IRepository<EcotronLevel>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            router.GetComposer<RecyclerRewardsMessageComposer>().Compose(session, EcotronRepository.All().ToArray());
        }
    }
}