using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser.Composers
{
    class NavigatorPreferencesComposer
    {
        internal static SimpleServerMessageBuffer Compose(UserPreferences userPreferences)
        {
            SimpleServerMessageBuffer navigatorPreferences = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("NewNavigatorSizeMessageComposer"));

            navigatorPreferences.AppendInteger(userPreferences.NewnaviX);
            navigatorPreferences.AppendInteger(userPreferences.NewnaviY);
            navigatorPreferences.AppendInteger(userPreferences.NavigatorWidth);
            navigatorPreferences.AppendInteger(userPreferences.NavigatorHeight);
            navigatorPreferences.AppendBool(false);
            navigatorPreferences.AppendInteger(1);

            return navigatorPreferences;
        }
    }
}
