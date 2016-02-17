using System.Collections;
using System.Collections.Concurrent;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.Items.Wired.Interfaces
{
    public interface IWiredCycler
    {
        Queue ToWork { get; set; }

        ConcurrentQueue<RoomUser> ToWorkConcurrentQueue { get; set; }

        bool OnCycle();
    }
}