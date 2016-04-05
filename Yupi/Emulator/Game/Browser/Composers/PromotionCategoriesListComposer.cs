using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser.Composers
{
    class PromotionCategoriesListComposer
    {
         static SimpleServerMessageBuffer Compose()
        {
            SimpleServerMessageBuffer categories = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("CatalogPromotionGetCategoriesMessageComposer"));

            categories.AppendInteger(Yupi.GetGame().GetNavigator().PromoCategories.Count);

            foreach (PromoCategory cat in Yupi.GetGame().GetNavigator().PromoCategories.Values)
            {
                categories.AppendInteger(cat.Id);
                categories.AppendString(cat.Caption);
                categories.AppendBool(cat.Visible);
            }

            return categories;
        }
    }
}
