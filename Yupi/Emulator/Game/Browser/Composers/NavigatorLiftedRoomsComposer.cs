using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser.Composers
{
    class NavigatorLiftedRoomsComposer
    {
         static SimpleServerMessageBuffer Compose()
        {
            SimpleServerMessageBuffer navigatorLiftedRooms = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("NavigatorLiftedRoomsComposer"));

            navigatorLiftedRooms.AppendInteger(Yupi.GetGame().GetNavigator().NavigatorHeaders.Count);

            foreach (NavigatorHeader navHeader in Yupi.GetGame().GetNavigator().NavigatorHeaders)
            {
                navigatorLiftedRooms.AppendInteger(navHeader.RoomId);
                navigatorLiftedRooms.AppendInteger(0);
                navigatorLiftedRooms.AppendString(navHeader.Image);
                navigatorLiftedRooms.AppendString(navHeader.Caption);
            }

            return navigatorLiftedRooms;
        }
    }
}
