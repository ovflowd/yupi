namespace Yupi.Messages.Catalog
{
    using System;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GetRecyclerRewardsMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<EcotronLevel> EcotronRepository;

        #endregion Fields

        #region Constructors

        public GetRecyclerRewardsMessageEvent()
        {
            EcotronRepository = DependencyFactory.Resolve<IRepository<EcotronLevel>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<RecyclerRewardsMessageComposer>().Compose(session, EcotronRepository.All().ToArray());
        }

        #endregion Methods
    }
}