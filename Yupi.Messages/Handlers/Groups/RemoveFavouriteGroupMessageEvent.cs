namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class RemoveFavouriteGroupMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public RemoveFavouriteGroupMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            request.GetUInt32(); // TODO Unused!
            session.Info.FavouriteGroup = null;

            UserRepository.Save(session.Info);

            router.GetComposer<FavouriteGroupMessageComposer>().Compose(session, session.Info.Id);
            router.GetComposer<ChangeFavouriteGroupMessageComposer>().Compose(session, null, 0);
        }

        #endregion Methods
    }
}