using System.Collections;
using System.Collections.Concurrent;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Items.Wired.Interfaces
{
    public interface IWiredCycler
    {
        Queue ToWork { get; set; }

        ConcurrentQueue<RoomUser> ToWorkConcurrentQueue { get; set; }

        bool OnCycle();
    }
}