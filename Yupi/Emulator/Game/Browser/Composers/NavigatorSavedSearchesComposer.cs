using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser.Composers
{
    class NavigatorSavedSearchesComposer
    {
     public static SimpleServerMessageBuffer Compose(GameClient session)
        {
            SimpleServerMessageBuffer userSearchLog = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("NavigatorSavedSearchesComposer"));

            userSearchLog.AppendInteger(session.GetHabbo().NavigatorLogs.Count);

            foreach (UserSearchLog navi in session.GetHabbo().NavigatorLogs.Values)
            {
                userSearchLog.AppendInteger(navi.Id);
                userSearchLog.AppendString(navi.Value1);
                userSearchLog.AppendString(navi.Value2);
                userSearchLog.AppendString(string.Empty);
            }

            return userSearchLog;
        }
    }
}
