using System.Linq;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
    public class NewNavigatorMessageEvent : AbstractHandler
    {
        private readonly IRepository<NavigatorCategory> NavigatorRepository;

        public NewNavigatorMessageEvent()
        {
            NavigatorRepository = DependencyFactory.Resolve<IRepository<NavigatorCategory>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            router.GetComposer<NavigatorMetaDataComposer>().Compose(session);
            router.GetComposer<NavigatorLiftedRoomsComposer>().Compose(session);
            router.GetComposer<NavigatorCategoriesComposer>().Compose(session, NavigatorRepository.All().ToList());
            router.GetComposer<NavigatorSavedSearchesComposer>().Compose(session, session.Info.NavigatorLog);
            router.GetComposer<NewNavigatorSizeMessageComposer>().Compose(session, session.Info.Preferences);
        }
    }
}