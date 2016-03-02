using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser.Composers
{
    class NavigatorSearchListResultComposer
    {
        internal static SimpleServerMessageBuffer Compose(string value1, string value2, GameClient session)
        {
            SimpleServerMessageBuffer newNavigator = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("SearchResultSetComposer"));

            newNavigator.AppendString(value1);
            newNavigator.AppendString(value2);
            newNavigator.AppendInteger(value2.Length > 0 ? 1 : Yupi.GetGame().GetNavigator().GetNewNavigatorLength(value1));

            if (value2.Length > 0)
                SearchResultList.SerializeSearches(value2, newNavigator, session);
            else
                SearchResultList.SerializeSearchResultListStatics(value1, true, newNavigator, session);

            return newNavigator;
        }
    }
}
