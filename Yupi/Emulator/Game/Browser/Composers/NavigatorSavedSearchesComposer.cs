using System.Collections.Generic;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser.Composers
{
    class NavigatorSavedSearchesComposer
    {
        internal static SimpleServerMessageBuffer Compose(Dictionary<int, UserSearchLog> userSearchLogEntries)
        {
            SimpleServerMessageBuffer userSearchLog = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("NavigatorSavedSearchesComposer"));

            userSearchLog.AppendInteger(userSearchLogEntries.Count);

            foreach (UserSearchLog navi in userSearchLogEntries.Values)
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
