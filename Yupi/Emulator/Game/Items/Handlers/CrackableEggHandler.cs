using System;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Items.Handlers
{
    /// <summary>
    ///     Class CrackableEggHandler.
    /// </summary>
    internal class CrackableEggHandler
    {
        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void Initialize(IQueryAdapter dbClient)
        {
        }

        internal int MaxCracks(string itemName)
        {
            switch (itemName)
            {
                case "easter13_egg_0":
                    return 1000;

                case "easter13_egg_1":
                    return 5000;

                case "easter13_egg_2":
                    return 10000;

                case "easter13_egg_3":
                    return 20000;

                default:
                    return 1;
            }
        }

        internal SimpleServerMessageBuffer GetServerMessage(SimpleServerMessageBuffer messageBuffer, RoomItem item)
        {
            int cracks = 0;
            int cracksMax = MaxCracks(item.GetBaseItem().Name);

            if (Yupi.IsNum(item.ExtraData))
                cracks = Convert.ToInt16(item.ExtraData);

            string state = "0";

            if (cracks >= cracksMax)
                state = "14";
            else if (cracks >= cracksMax*6/7)
                state = "12";
            else if (cracks >= cracksMax*5/7)
                state = "10";
            else if (cracks >= cracksMax*4/7)
                state = "8";
            else if (cracks >= cracksMax*3/7)
                state = "6";
            else if (cracks >= cracksMax*2/7)
                state = "4";
            else if (cracks >= cracksMax*1/7)
                state = "2";

            messageBuffer.AppendInteger(7);
            messageBuffer.AppendString(state); //state (0-7)
            messageBuffer.AppendInteger(cracks); //actual
            messageBuffer.AppendInteger(cracksMax); //max

            return messageBuffer;
        }
    }
}