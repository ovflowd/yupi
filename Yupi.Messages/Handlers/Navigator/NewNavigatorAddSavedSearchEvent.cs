using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
    public class NewNavigatorAddSavedSearchEvent : AbstractHandler
    {
        private readonly IRepository<UserInfo> UserRepository;

        public NewNavigatorAddSavedSearchEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            // TODO Refactor
            var value1 = request.GetString();

            var value2 = request.GetString();

            var naviLog = new UserSearchLog
            {
                Value1 = value1,
                Value2 = value2
            };

            session.Info.NavigatorLog.Add(naviLog);

            UserRepository.Save(session.Info);

            router.GetComposer<NavigatorSavedSearchesComposer>().Compose(session, session.Info.NavigatorLog);
        }
    }
}