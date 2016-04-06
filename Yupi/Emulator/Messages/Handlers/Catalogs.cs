using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Game.Catalogs;
using Yupi.Emulator.Game.Catalogs.Composers;
using Yupi.Emulator.Game.Catalogs.Interfaces;
using Yupi.Emulator.Game.Catalogs.Wrappers;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Enums;

namespace Yupi.Emulator.Messages.Handlers
{
    /// <summary>
    ///     Class MessageHandler.
    /// </summary>
     public partial class MessageHandler
    {
        /// <summary>
        ///     Catalogues the page.
        /// </summary>
     public void CataloguePage()
        {
            uint pageId = Request.GetUInt32();

            Request.GetInteger();

            CatalogPage cPage = Yupi.GetGame().GetCatalogManager().GetPage(pageId);

            if (cPage == null || !cPage.Enabled || !cPage.Visible || cPage.MinRank > Session.GetHabbo().Rank)
                return;

            Session.SendMessage(cPage.CachedContentsMessageBuffer);
        }

        /// <summary>
        ///     Catalogues the club page.
        /// </summary>
     public void CatalogueClubPage()
        {
            int requestType = Request.GetInteger();

            Session.SendMessage(CatalogPageComposer.ComposeClubPurchasePage(Session, requestType));
        }

        /// <summary>
        ///     Reloads the ecotron.
        /// </summary>
     public void ReloadEcotron()
        {
            Response.Init(PacketLibraryManager.OutgoingHandler("ReloadEcotronMessageComposer"));
            Response.AppendInteger(1);
            Response.AppendInteger(0);
            SendResponse();
        }

        /// <summary>
        ///     Gifts the wrapping configuration.
        /// </summary>
     public void GiftWrappingConfig()
        {
            Response.Init(PacketLibraryManager.OutgoingHandler("GiftWrappingConfigurationMessageComposer"));
            Response.AppendBool(true);
            Response.AppendInteger(1);
            Response.AppendInteger(GiftWrapper.GiftWrappersList.Count);

            foreach (int i in GiftWrapper.GiftWrappersList)
                Response.AppendInteger(i);

            Response.AppendInteger(8);

            for (uint i = 0u; i != 8; i++)
                Response.AppendInteger(i);

            Response.AppendInteger(11);

            for (uint i = 0u; i != 11; i++)
                Response.AppendInteger(i);

            Response.AppendInteger(GiftWrapper.OldGiftWrappers.Count);

            foreach (int i in GiftWrapper.OldGiftWrappers)
                Response.AppendInteger(i);

            SendResponse();
        }

        /// <summary>
        ///     Gets the recycler rewards.
        /// </summary>
     public void RecyclerRewards()
        {
            Response.Init(PacketLibraryManager.OutgoingHandler("RecyclerRewardsMessageComposer"));

            List<int> ecotronRewardsLevels = Yupi.GetGame().GetCatalogManager().GetEcotronRewardsLevels();

            Response.AppendInteger(ecotronRewardsLevels.Count);

            foreach (int current in ecotronRewardsLevels)
            {
                Response.AppendInteger(current);
                Response.AppendInteger(current);

                List<EcotronReward> ecotronRewardsForLevel =
                    Yupi.GetGame().GetCatalogManager().GetEcotronRewardsForLevel(uint.Parse(current.ToString()));

                Response.AppendInteger(ecotronRewardsForLevel.Count);

                foreach (EcotronReward current2 in ecotronRewardsForLevel)
                {
                    Response.AppendString(current2.GetBaseItem().PublicName);
                    Response.AppendInteger(1);
                    Response.AppendString(current2.GetBaseItem().Type.ToString());
                    Response.AppendInteger(current2.GetBaseItem().SpriteId);
                }
            }

            SendResponse();
        }

        /// <summary>
        ///     Purchases the item.
        /// </summary>
     public void PurchaseItem()
        {
            if (Session?.GetHabbo() == null)
                return;

            if (Session.GetHabbo().GetInventoryComponent().TotalItems >= 2799)
            {
                Session.SendMessage(CatalogPageComposer.PurchaseOk(0, string.Empty, 0));
                Session.SendMessage(StaticMessage.AdvicePurchaseMaxItems);
                return;
            }

            uint pageId = Request.GetUInt32();
            uint itemId = Request.GetUInt32();
            string extraData = Request.GetString();
            uint priceAmount = Request.GetUInt32();

            Yupi.GetGame()
                .GetCatalogManager()
                .HandlePurchase(Session, pageId, itemId, extraData, priceAmount, false, string.Empty, string.Empty, 0, 0,
                    0, false, 0u);
        }

        /// <summary>
        ///     Purchases the gift.
        /// </summary>
     public void PurchaseGift()
        {
            uint pageId = Request.GetUInt32();
            uint itemId = Request.GetUInt32();
            string extraData = Request.GetString();
            string giftUser = Request.GetString();
            string giftMessage = Request.GetString();
            int giftSpriteId = Request.GetInteger();
            int giftLazo = Request.GetInteger();
            int giftColor = Request.GetInteger();
            bool undef = Request.GetBool();

            Yupi.GetGame()
                .GetCatalogManager()
                .HandlePurchase(Session, pageId, itemId, extraData, 1, true, giftUser, giftMessage, giftSpriteId,
                    giftLazo, giftColor, undef, 0u);
        }

        /// <summary>
        ///     Checks the name of the pet.
        /// </summary>
     public void CheckPetName()
        {
            string petName = Request.GetString();
            int i = 0;

            if (petName.Length > 15)
                i = 1;
            else if (petName.Length < 3)
                i = 2;
            else if (!Yupi.IsValidAlphaNumeric(petName))
                i = 3;

            Response.Init(PacketLibraryManager.OutgoingHandler("CheckPetNameMessageComposer"));
            Response.AppendInteger(i);
            Response.AppendString(petName);
            SendResponse();
        }

        /// <summary>
        ///     Catalogues the offer.
        /// </summary>
     public void CatalogueSingleOffer()
        {
            uint num = Request.GetUInt32();

            CatalogItem catalogItem = Yupi.GetGame().GetCatalogManager().GetItemFromOffer(num);

            if (catalogItem == null || CatalogManager.LastSentOffer == num)
                return;

            CatalogManager.LastSentOffer = num;

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("CatalogOfferMessageComposer"));

            CatalogPageComposer.ComposeItem(catalogItem, messageBuffer);

            Session.SendMessage(messageBuffer);
        }

        /// <summary>
        ///     Catalogues the offer configuration.
        /// </summary>
     public void CatalogueOffersConfig()
        {
            Response.Init(PacketLibraryManager.OutgoingHandler("CatalogueOfferConfigMessageComposer"));
            Response.AppendInteger(100);
            Response.AppendInteger(6);
            Response.AppendInteger(1);
            Response.AppendInteger(1);
            Response.AppendInteger(2);
            Response.AppendInteger(40);
            Response.AppendInteger(99);
            SendResponse();
        }

        /// <summary>
        ///     Serializes the group furni page.
        /// </summary>
     public void SerializeGroupFurniPage()
        {
            HashSet<GroupMember> userGroups = Yupi.GetGame().GetGroupManager().GetUserGroups(Session.GetHabbo().Id);

            Response.Init(PacketLibraryManager.OutgoingHandler("GroupFurniturePageMessageComposer"));

            List<SimpleServerMessageBuffer> responseList = new List<SimpleServerMessageBuffer>();

            foreach (
                Group habboGroup in
                    userGroups.Where(current => current != null)
                        .Select(current => Yupi.GetGame().GetGroupManager().GetGroup(current.GroupId)))
            {
                if (habboGroup == null)
                    continue;

                SimpleServerMessageBuffer subResponse = new SimpleServerMessageBuffer();
                subResponse.AppendInteger(habboGroup.Id);
                subResponse.AppendString(habboGroup.Name);
                subResponse.AppendString(habboGroup.Badge);
                subResponse.AppendString(Yupi.GetGame().GetGroupManager().SymbolColours.Contains(habboGroup.Colour1)
                    ? ((GroupSymbolColours)
                        Yupi.GetGame().GetGroupManager().SymbolColours[habboGroup.Colour1]).Colour
                    : "4f8a00");
                subResponse.AppendString(
                    Yupi.GetGame().GetGroupManager().BackGroundColours.Contains(habboGroup.Colour2)
                        ? ((GroupBackGroundColours)
                            Yupi.GetGame().GetGroupManager().BackGroundColours[habboGroup.Colour2]).Colour
                        : "4f8a00");
                subResponse.AppendBool(habboGroup.CreatorId == Session.GetHabbo().Id);
                subResponse.AppendInteger(habboGroup.CreatorId);
                subResponse.AppendBool(habboGroup.Forum.Id != 0);

                responseList.Add(subResponse);
            }

            Response.AppendInteger(responseList.Count());
            Response.AppendServerMessages(responseList);

            responseList.Clear();

            SendResponse();
        }
    }
}