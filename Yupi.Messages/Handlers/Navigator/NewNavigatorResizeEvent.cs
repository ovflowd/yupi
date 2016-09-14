using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Domain.Components;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
    public class NewNavigatorResizeEvent : AbstractHandler
    {
        private readonly IRepository<UserPreferences> PreferenceRepository;

        public NewNavigatorResizeEvent()
        {
            PreferenceRepository = DependencyFactory.Resolve<IRepository<UserPreferences>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var x = request.GetInteger();
            var y = request.GetInteger();
            var width = request.GetInteger();
            var height = request.GetInteger();

            var preferences = session.Info.Preferences;

            preferences.NewnaviX = x;
            preferences.NewnaviY = y;
            preferences.NavigatorWidth = width;
            preferences.NavigatorHeight = height;

            PreferenceRepository.Save(preferences);
        }
    }
}