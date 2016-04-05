using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser.Composers
{
    class NavigatorFlatCategoriesListComposer
    {
         static SimpleServerMessageBuffer Compose(GameClient session)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("FlatCategoriesMessageComposer"));

            simpleServerMessageBuffer.StartArray();

            foreach (PublicCategory flatCat in Yupi.GetGame().GetNavigator().PrivateCategories.Values)
            {
                simpleServerMessageBuffer.Clear();

                if (flatCat == null)
                    continue;

                simpleServerMessageBuffer.AppendInteger(flatCat.Id);
                simpleServerMessageBuffer.AppendString(flatCat.Caption);
                simpleServerMessageBuffer.AppendBool(flatCat.MinRank <= session.GetHabbo().Rank);
                simpleServerMessageBuffer.AppendBool(false);
                simpleServerMessageBuffer.AppendString("NONE");
                simpleServerMessageBuffer.AppendString(string.Empty);
                simpleServerMessageBuffer.AppendBool(false);

                simpleServerMessageBuffer.SaveArray();
            }

            simpleServerMessageBuffer.EndArray();

            return simpleServerMessageBuffer;
        }
    }
}
