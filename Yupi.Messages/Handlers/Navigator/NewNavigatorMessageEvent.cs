namespace Yupi.Messages.Navigator
{
    using System;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class NewNavigatorMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<NavigatorCategory> NavigatorRepository;

        #endregion Fields

        #region Constructors

        public NewNavigatorMessageEvent()
        {
            NavigatorRepository = DependencyFactory.Resolve<IRepository<NavigatorCategory>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<NavigatorMetaDataComposer>().Compose(session);
            router.GetComposer<NavigatorLiftedRoomsComposer>().Compose(session);
            router.GetComposer<NavigatorCategoriesComposer>().Compose(session, NavigatorRepository.All().ToList());
            router.GetComposer<NavigatorSavedSearchesComposer>().Compose(session, session.Info.NavigatorLog);
            router.GetComposer<NewNavigatorSizeMessageComposer>().Compose(session, session.Info.Preferences);
        }

        #endregion Methods
    }
}