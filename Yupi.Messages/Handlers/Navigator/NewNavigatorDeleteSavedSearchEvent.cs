namespace Yupi.Messages.Navigator
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Util;

    public class NewNavigatorDeleteSavedSearchEvent : AbstractHandler
    {
        #region Fields

        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public NewNavigatorDeleteSavedSearchEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int searchId = request.GetInteger();

            session.Info.NavigatorLog.RemoveAll(x => x.Id == searchId);

            UserRepository.Save(session.Info);

            router.GetComposer<NavigatorSavedSearchesComposer>().Compose(session, session.Info.NavigatorLog);
        }

        #endregion Methods
    }
}