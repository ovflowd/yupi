using System;
using Yupi.Model.Domain.Components;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;

namespace Yupi.Messages.Navigator
{
	public class NewNavigatorResizeEvent : AbstractHandler
	{
		private IRepository<UserPreferences> PreferenceRepository;

		public NewNavigatorResizeEvent ()
		{
			PreferenceRepository = DependencyFactory.Resolve<IRepository<UserPreferences>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
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
	}
}

