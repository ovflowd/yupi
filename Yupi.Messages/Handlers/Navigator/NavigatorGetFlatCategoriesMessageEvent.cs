namespace Yupi.Messages.Navigator
{
    using System;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class NavigatorGetFlatCategoriesMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<FlatNavigatorCategory> NavigatorRepository;

        #endregion Fields

        #region Constructors

        public NavigatorGetFlatCategoriesMessageEvent()
        {
            NavigatorRepository = DependencyFactory.Resolve<IRepository<FlatNavigatorCategory>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<FlatCategoriesMessageComposer>()
                .Compose(session, NavigatorRepository.All().ToList(), session.Info.Rank);
        }

        #endregion Methods
    }
}