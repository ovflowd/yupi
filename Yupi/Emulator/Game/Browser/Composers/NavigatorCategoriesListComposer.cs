using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser.Composers
{
    class NavigatorCategoriesListComposer
    {
        internal static SimpleServerMessageBuffer Compose()
        {
            SimpleServerMessageBuffer collapsedCategories = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("NavigatorCategorys"));

            collapsedCategories.AppendInteger(Yupi.GetGame().GetNavigator().FlatCatsCount + 4);

            foreach (PublicCategory flat in Yupi.GetGame().GetNavigator().PrivateCategories.Values)
                collapsedCategories.AppendString($"category__{flat.Caption}");

            collapsedCategories.AppendString("recommended");
            collapsedCategories.AppendString("new_ads");
            collapsedCategories.AppendString("staffpicks");
            collapsedCategories.AppendString("official");

            return collapsedCategories;
        }
    }
}
