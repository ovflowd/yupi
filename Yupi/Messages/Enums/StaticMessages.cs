using System.Collections.Generic;
using Yupi.Messages.Parsers;

namespace Yupi.Messages.Enums
{
    /// <summary>
    ///     Enum StaticMessage
    /// </summary>
    internal enum StaticMessage
    {
        /// <summary>
        ///     The error cant set item
        /// </summary>
        ErrorCantSetItem,

        /// <summary>
        ///     The error cant set not owner
        /// </summary>
        ErrorCantSetNotOwner,

        /// <summary>
        ///     The kicked
        /// </summary>
        Kicked,

        /// <summary>
        ///     The new way to open commands list
        /// </summary>
        NewWayToOpenCommandsList,

        /// <summary>
        ///     The user not found
        /// </summary>
        UserNotFound,

        AdviceMaxItems,
        AdvicePurchaseMaxItems,
        CatalogOffersConfiguration,
        FiguresetRedeemed
    }

    /// <summary>
    ///     Class StaticMessagesManager.
    /// </summary>
    internal static class StaticMessagesManager
    {
        /// <summary>
        ///     The cache
        /// </summary>
        private static readonly Dictionary<StaticMessage, byte[]> Cache = new Dictionary<StaticMessage, byte[]>();

        /// <summary>
        ///     Loads this instance.
        /// </summary>
        public static void Load()
        {
            Cache.Clear();

            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
            message.AppendString("furni_placement_error");
            message.AppendInteger(1);
            message.AppendString("message");
            message.AppendString("${room.error.cant_set_item}");
            Cache.Add(StaticMessage.ErrorCantSetItem, message.GetReversedBytes());

            message = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
            message.AppendString("furni_placement_error");
            message.AppendInteger(1);
            message.AppendString("message");
            message.AppendString("${room.error.cant_set_not_owner}");
            Cache.Add(StaticMessage.ErrorCantSetNotOwner, message.GetReversedBytes());

            message = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
            message.AppendString("game_promo_small");
            message.AppendInteger(4);
            message.AppendString("title");
            message.AppendString("${generic.notice}");
            message.AppendString("message");
            message.AppendString("Now, the commands page opens in a different way");
            message.AppendString("linkUrl");
            message.AppendString("event:habbopages/chat/newway");
            message.AppendString("linkTitle");
            message.AppendString("${mod.alert.link}");
            Cache.Add(StaticMessage.NewWayToOpenCommandsList, message.GetReversedBytes());

            message = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
            message.AppendString(string.Empty);
            message.AppendInteger(4);
            message.AppendString("title");
            message.AppendString("${generic.notice}");
            message.AppendString("message");
            message.AppendString("${catalog.gift_wrapping.receiver_not_found.title}");
            message.AppendString("linkUrl");
            message.AppendString("event:");
            message.AppendString("linkTitle");
            message.AppendString("ok");
            Cache.Add(StaticMessage.UserNotFound, message.GetReversedBytes());

            message = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
            message.AppendString(string.Empty);
            message.AppendInteger(4);
            message.AppendString("title");
            message.AppendString("${generic.notice}");
            message.AppendString("message");
            message.AppendString(
                "Has superado el máximo de furnis en el inventario. Solo se te mostrarán 2800 furnis, si quieres ver los restantes, coloca algunos furnis en tus salas.");
            message.AppendString("linkUrl");
            message.AppendString("event:");
            message.AppendString("linkTitle");
            message.AppendString("ok");
            Cache.Add(StaticMessage.AdviceMaxItems, message.GetReversedBytes());

            message = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
            message.AppendString(string.Empty);
            message.AppendInteger(4);
            message.AppendString("title");
            message.AppendString("${generic.notice}");
            message.AppendString("message");
            message.AppendString(
                "Has superado el máximo de furnis en el inventario. No puedes comprar más hasta que te desagas de algunos furnis.");
            message.AppendString("linkUrl");
            message.AppendString("event:");
            message.AppendString("linkTitle");
            message.AppendString("ok");
            Cache.Add(StaticMessage.AdvicePurchaseMaxItems, message.GetReversedBytes());

            message = new ServerMessage(LibraryParser.OutgoingRequest("CatalogueOfferConfigMessageComposer"));
            message.AppendInteger(100); // purchase_limit
            message.AppendInteger(6); // offer_multiplier
            message.AppendInteger(2); // free_objets_per_multiplier
            message.AppendInteger(1); // inversed_credit_reduction
            message.AppendInteger(2); // array count
            message.AppendInteger(40);
            message.AppendInteger(99);
            Cache.Add(StaticMessage.CatalogOffersConfiguration, message.GetReversedBytes());

            message = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
            message.AppendString(string.Empty);
            message.AppendInteger(4);
            message.AppendString("title");
            message.AppendString("${notification.figureset.redeemed.success.title}");
            message.AppendString("message");
            message.AppendString("${notification.figureset.redeemed.success.message}");
            message.AppendString("linkUrl");
            message.AppendString("event:avatareditor/open");
            message.AppendString("linkTitle");
            message.AppendString("${notification.figureset.redeemed.success.linkTitle}");
            Cache.Add(StaticMessage.FiguresetRedeemed, message.GetReversedBytes());

            message.Dispose();
        }

        /// <summary>
        ///     Gets the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] Get(StaticMessage type)
        {
            return Cache[type];
        }
    }
}