namespace Yupi.Messages.Navigator
{
    using System;

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Util;

    public class NewNavigatorAddSavedSearchEvent : AbstractHandler
    {
        #region Fields

        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public NewNavigatorAddSavedSearchEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            // TODO Refactor
            string value1 = request.GetString();

            string value2 = request.GetString();

            UserSearchLog naviLog = new UserSearchLog()
            {
                Value1 = value1,
                Value2 = value2
            };

            session.Info.NavigatorLog.Add(naviLog);

            UserRepository.Save(session.Info);

            router.GetComposer<NavigatorSavedSearchesComposer>().Compose(session, session.Info.NavigatorLog);
        }

        #endregion Methods
    }
}