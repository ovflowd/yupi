using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser.Composers
{
    class NavigatorFlatCategoriesListComposer
    {
        internal static SimpleServerMessageBuffer Compose()
        {
            List<PublicCategory> flatcat = Yupi.GetGame().GetNavigator().PrivateCategories.OfType<PublicCategory>().ToList();

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("NavigatorNewFlatCategoriesMessageComposer"));

            messageBuffer.AppendInteger(flatcat.Count);

            foreach (PublicCategory cat in flatcat)
            {
                messageBuffer.AppendInteger(cat.Id);
                messageBuffer.AppendInteger(cat.UsersNow);
                messageBuffer.AppendInteger(500);
            }

            return messageBuffer;
        }
    }
}
