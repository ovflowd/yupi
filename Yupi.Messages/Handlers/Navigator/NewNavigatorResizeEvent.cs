namespace Yupi.Messages.Navigator
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Model.Repository;

    public class NewNavigatorResizeEvent : AbstractHandler
    {
        #region Fields

        private IRepository<UserPreferences> PreferenceRepository;

        #endregion Fields

        #region Constructors

        public NewNavigatorResizeEvent()
        {
            PreferenceRepository = DependencyFactory.Resolve<IRepository<UserPreferences>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int x = request.GetInteger();
            int y = request.GetInteger();
            int width = request.GetInteger();
            int height = request.GetInteger();

            UserPreferences preferences = session.Info.Preferences;

            preferences.NewnaviX = x;
            preferences.NewnaviY = y;
            preferences.NavigatorWidth = width;
            preferences.NavigatorHeight = height;

            PreferenceRepository.Save(preferences);
        }

        #endregion Methods
    }
}