using System;
using Yupi.Data.Base.Adapters.Interfaces;

namespace Yupi.Game.Items.Handlers
{
    /// <summary>
    ///     Class HopperHandler.
    /// </summary>
    internal static class HopperHandler
    {
        /// <summary>
        ///     Gets a hopper.
        /// </summary>
        /// <param name="curRoom">The current room.</param>
        /// <returns>System.UInt32.</returns>
        internal static uint GetAHopper(uint curRoom)
        {
            uint result;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    $"SELECT room_id FROM items_hopper WHERE room_id <> '{curRoom}' ORDER BY RAND() LIMIT 1");

                uint num = Convert.ToUInt32(commitableQueryReactor.GetInteger());
                result = num;
            }

            return result;
        }

        /// <summary>
        ///     Gets the hopper identifier.
        /// </summary>
        /// <param name="nextRoom">The next room.</param>
        /// <returns>System.UInt32.</returns>
        internal static uint GetHopperId(uint nextRoom)
        {
            uint result;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery("SELECT hopper_id FROM items_hopper WHERE room_id = @room LIMIT 1");
                commitableQueryReactor.AddParameter("room", nextRoom);

                string theString = commitableQueryReactor.GetString();

                result = theString == null ? 0u : Convert.ToUInt32(theString);
            }

            return result;
        }
    }
}