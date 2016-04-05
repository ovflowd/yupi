using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Messages.Handlers
{
    /// <summary>
    ///     Class MessageHandler.
    /// </summary>
     partial class MessageHandler
    {
        /// <summary>
        ///     Gets the inventory.
        /// </summary>
         void GetInventory()
        {
            QueuedServerMessageBuffer queuedServerMessageBuffer = new QueuedServerMessageBuffer(Session.GetConnection());

            queuedServerMessageBuffer.AppendResponse(Session.GetHabbo().GetInventoryComponent().SerializeFloorItemInventory());
            queuedServerMessageBuffer.SendResponse();
        }
    }
}