using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser.Composers
{
    class NavigatorPreferencesComposer
    {
         static SimpleServerMessageBuffer Compose(GameClient session)
        {
            SimpleServerMessageBuffer navigatorPreferences = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("NewNavigatorSizeMessageComposer"));

            navigatorPreferences.AppendInteger(session.GetHabbo().Preferences.NewnaviX);
            navigatorPreferences.AppendInteger(session.GetHabbo().Preferences.NewnaviY);
            navigatorPreferences.AppendInteger(session.GetHabbo().Preferences.NavigatorWidth);
            navigatorPreferences.AppendInteger(session.GetHabbo().Preferences.NavigatorHeight);
            navigatorPreferences.AppendBool(false);
            navigatorPreferences.AppendInteger(1);

            return navigatorPreferences;
        }
    }
}
