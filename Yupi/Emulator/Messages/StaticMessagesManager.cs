using System.Collections.Generic;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Enums;

namespace Yupi.Emulator.Messages
{
    /// <summary>
    ///     Class StaticMessagesManager.
    ///     @TODO: Remove This Xit because is BAD. (The entire class)
    /// </summary>
    internal static class StaticMessagesManager
    {
        /// <summary>
        ///     Static Message Cache
        /// </summary>
        private static readonly Dictionary<StaticMessage, byte[]> Cache = new Dictionary<StaticMessage, byte[]>();

        /// <summary>
        ///     Loads this instance.
        /// </summary>
        public static void Load()
        {
            Cache.Clear();

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("SuperNotificationMessageComposer"));
            messageBuffer.AppendString("furni_placement_error");
            messageBuffer.AppendInteger(1);
            messageBuffer.AppendString("message");
            messageBuffer.AppendString("${room.error.cant_set_item}");
            Cache.Add(StaticMessage.ErrorCantSetItem, messageBuffer.GetReversedBytes());

            messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("SuperNotificationMessageComposer"));
            messageBuffer.AppendString("furni_placement_error");
            messageBuffer.AppendInteger(1);
            messageBuffer.AppendString("message");
            messageBuffer.AppendString("${room.error.cant_set_not_owner}");
            Cache.Add(StaticMessage.ErrorCantSetNotOwner, messageBuffer.GetReversedBytes());

            messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("SuperNotificationMessageComposer"));
            messageBuffer.AppendString("game_promo_small");
            messageBuffer.AppendInteger(4);
            messageBuffer.AppendString("title");
            messageBuffer.AppendString("${generic.notice}");
            messageBuffer.AppendString("message");
            messageBuffer.AppendString("Now, the commands page opens in a different way");
            messageBuffer.AppendString("linkUrl");
            messageBuffer.AppendString("event:habbopages/chat/newway");
            messageBuffer.AppendString("linkTitle");
            messageBuffer.AppendString("${mod.alert.link}");
            Cache.Add(StaticMessage.NewWayToOpenCommandsList, messageBuffer.GetReversedBytes());

            messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("SuperNotificationMessageComposer"));
            messageBuffer.AppendString(string.Empty);
            messageBuffer.AppendInteger(4);
            messageBuffer.AppendString("title");
            messageBuffer.AppendString("${generic.notice}");
            messageBuffer.AppendString("message");
            messageBuffer.AppendString("${catalog.gift_wrapping.receiver_not_found.title}");
            messageBuffer.AppendString("linkUrl");
            messageBuffer.AppendString("event:");
            messageBuffer.AppendString("linkTitle");
            messageBuffer.AppendString("ok");
            Cache.Add(StaticMessage.UserNotFound, messageBuffer.GetReversedBytes());

            messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("SuperNotificationMessageComposer"));
            messageBuffer.AppendString(string.Empty);
            messageBuffer.AppendInteger(4);
            messageBuffer.AppendString("title");
            messageBuffer.AppendString("${generic.notice}");
            messageBuffer.AppendString("message");
            messageBuffer.AppendString("You've exceeded the maximum furnis inventory. Only 2800 will show furnis if you want to see the others, places some Furni in your rooms.");
            messageBuffer.AppendString("linkUrl");
            messageBuffer.AppendString("event:");
            messageBuffer.AppendString("linkTitle");
            messageBuffer.AppendString("ok");
            Cache.Add(StaticMessage.AdviceMaxItems, messageBuffer.GetReversedBytes());

            messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("SuperNotificationMessageComposer"));
            messageBuffer.AppendString(string.Empty);
            messageBuffer.AppendInteger(4);
            messageBuffer.AppendString("title");
            messageBuffer.AppendString("${generic.notice}");
            messageBuffer.AppendString("message");
            messageBuffer.AppendString("You've exceeded the maximum furnis inventory. You can not buy more until you get rid of some furnis.");
            messageBuffer.AppendString("linkUrl");
            messageBuffer.AppendString("event:");
            messageBuffer.AppendString("linkTitle");
            messageBuffer.AppendString("ok");
            Cache.Add(StaticMessage.AdvicePurchaseMaxItems, messageBuffer.GetReversedBytes());

            messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("CatalogueOfferConfigMessageComposer"));
            messageBuffer.AppendInteger(100);
            messageBuffer.AppendInteger(6);
            messageBuffer.AppendInteger(2);
            messageBuffer.AppendInteger(1);
            messageBuffer.AppendInteger(2);
            messageBuffer.AppendInteger(40);
            messageBuffer.AppendInteger(99);
            Cache.Add(StaticMessage.CatalogOffersConfiguration, messageBuffer.GetReversedBytes());

            messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("SuperNotificationMessageComposer"));
            messageBuffer.AppendString(string.Empty);
            messageBuffer.AppendInteger(4);
            messageBuffer.AppendString("title");
            messageBuffer.AppendString("${notification.figureset.redeemed.success.title}");
            messageBuffer.AppendString("message");
            messageBuffer.AppendString("${notification.figureset.redeemed.success.messageBuffer}");
            messageBuffer.AppendString("linkUrl");
            messageBuffer.AppendString("event:avatareditor/open");
            messageBuffer.AppendString("linkTitle");
            messageBuffer.AppendString("${notification.figureset.redeemed.success.linkTitle}");
            Cache.Add(StaticMessage.FiguresetRedeemed, messageBuffer.GetReversedBytes());

            messageBuffer.Dispose();
        }

        /// <summary>
        ///     Get Static Message Entry
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] Get(StaticMessage type) => Cache[type];
    }
}
