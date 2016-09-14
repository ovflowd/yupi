using System.Linq;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
    public class NavigatorGetFlatCategoriesMessageEvent : AbstractHandler
    {
        private readonly IRepository<FlatNavigatorCategory> NavigatorRepository;

        public NavigatorGetFlatCategoriesMessageEvent()
        {
            NavigatorRepository = DependencyFactory.Resolve<IRepository<FlatNavigatorCategory>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            router.GetComposer<FlatCategoriesMessageComposer>()
                .Compose(session, NavigatorRepository.All().ToList(), session.Info.Rank);
        }
    }
}