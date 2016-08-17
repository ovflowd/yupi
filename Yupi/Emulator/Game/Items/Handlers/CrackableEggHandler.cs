using System;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Items.Interfaces;


namespace Yupi.Emulator.Game.Items.Handlers
{
    /// <summary>
    ///     Class CrackableEggHandler.
    /// </summary>
     public class CrackableEggHandler
    {
     public int MaxCracks(string itemName)
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
    }
}