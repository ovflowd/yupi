using Yupi.Emulator.Game.Rooms.Items;

namespace Yupi.Emulator.Game.Items
{
    /// <summary>
    ///     Delegate OnItemTrigger
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    public delegate void OnItemTrigger(object sender, ItemTriggeredArgs e);
}