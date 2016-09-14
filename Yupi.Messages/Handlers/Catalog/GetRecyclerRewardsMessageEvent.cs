using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using System.Linq;

namespace Yupi.Messages.Catalog
{
    public class GetRecyclerRewardsMessageEvent : AbstractHandler
    {
        private IRepository<EcotronLevel> EcotronRepository;

        public GetRecyclerRewardsMessageEvent()
        {
            EcotronRepository = DependencyFactory.Resolve<IRepository<EcotronLevel>>();
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<RecyclerRewardsMessageComposer>().Compose(session, EcotronRepository.All().ToArray());
        }
    }
}