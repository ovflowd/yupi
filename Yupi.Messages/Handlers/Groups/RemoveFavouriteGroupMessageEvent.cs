using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class RemoveFavouriteGroupMessageEvent : AbstractHandler
    {
        private readonly IRepository<UserInfo> UserRepository;

        public RemoveFavouriteGroupMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            request.GetUInt32(); // TODO Unused!
            session.Info.FavouriteGroup = null;

            UserRepository.Save(session.Info);

            router.GetComposer<FavouriteGroupMessageComposer>().Compose(session, session.Info.Id);
            router.GetComposer<ChangeFavouriteGroupMessageComposer>().Compose(session, null, 0);
        }
    }
}