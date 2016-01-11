using System.Collections.Generic;
using System.Linq;
using Yupi.Game.Catalogs;
using Yupi.Game.Catalogs.Composers;
using Yupi.Game.Catalogs.Interfaces;
using Yupi.Game.Catalogs.Wrappers;
using Yupi.Game.Groups.Structs;
using Yupi.Messages.Enums;
using Yupi.Messages.Parsers;

namespace Yupi.Messages.Handlers
{
    /// <summary>
    ///     Class GameClientMessageHandler.
    /// </summary>
    internal partial class GameClientMessageHandler
    {
        /// <summary>
        ///     Catalogues the index.
        /// </summary>
        public void CatalogueIndex()
        {
            uint rank = Session.GetHabbo().Rank;

            if (rank < 1)
                rank = 1;

            Session.SendMessage(StaticMessage.CatalogOffersConfiguration);
            Session.SendMessage(CatalogPageComposer.ComposeIndex(rank, Request.GetString().ToUpper()));
        }

        /// <summary>
        ///     Catalogues the page.
        /// </summary>
        public void CataloguePage()
        {
            uint pageId = Request.GetUInteger();

            Request.GetInteger();

            CatalogPage cPage = Yupi.GetGame().GetCatalog().GetPage(pageId);

            if (cPage == null || !cPage.Enabled || !cPage.Visible || cPage.MinRank > Session.GetHabbo().Rank)
                return;

            Session.SendMessage(cPage.CachedContentsMessage);
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
            Response.Init(LibraryParser.OutgoingRequest("ReloadEcotronMessageComposer"));
            Response.AppendInteger(1);
            Response.AppendInteger(0);
            SendResponse();
        }

        /// <summary>
        ///     Gifts the wrapping configuration.
        /// </summary>
        public void GiftWrappingConfig()
        {
            Response.Init(LibraryParser.OutgoingRequest("GiftWrappingConfigurationMessageComposer"));
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
        public void GetRecyclerRewards()
        {
            Response.Init(LibraryParser.OutgoingRequest("RecyclerRewardsMessageComposer"));

            List<int> ecotronRewardsLevels = Yupi.GetGame().GetCatalog().GetEcotronRewardsLevels();

            Response.AppendInteger(ecotronRewardsLevels.Count);

            foreach (int current in ecotronRewardsLevels)
            {
                Response.AppendInteger(current);
                Response.AppendInteger(current);

                List<EcotronReward> ecotronRewardsForLevel =
                    Yupi.GetGame().GetCatalog().GetEcotronRewardsForLevel(uint.Parse(current.ToString()));

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

            uint pageId = Request.GetUInteger();
            uint itemId = Request.GetUInteger();
            string extraData = Request.GetString();
            uint priceAmount = Request.GetUInteger();

            Yupi.GetGame()
                .GetCatalog()
                .HandlePurchase(Session, pageId, itemId, extraData, priceAmount, false, string.Empty, string.Empty, 0, 0,
                    0, false, 0u);
        }

        /// <summary>
        ///     Purchases the gift.
        /// </summary>
        public void PurchaseGift()
        {
            uint pageId = Request.GetUInteger();
            uint itemId = Request.GetUInteger();
            string extraData = Request.GetString();
            string giftUser = Request.GetString();
            string giftMessage = Request.GetString();
            int giftSpriteId = Request.GetInteger();
            int giftLazo = Request.GetInteger();
            int giftColor = Request.GetInteger();
            bool undef = Request.GetBool();

            Yupi.GetGame()
                .GetCatalog()
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

            Response.Init(LibraryParser.OutgoingRequest("CheckPetNameMessageComposer"));
            Response.AppendInteger(i);
            Response.AppendString(petName);
            SendResponse();
        }

        /// <summary>
        ///     Catalogues the offer.
        /// </summary>
        public void CatalogueOffer()
        {
            uint num = Request.GetUInteger();

            CatalogItem catalogItem = Yupi.GetGame().GetCatalog().GetItemFromOffer(num);

            if (catalogItem == null || CatalogManager.LastSentOffer == num)
                return;

            CatalogManager.LastSentOffer = num;

            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("CatalogOfferMessageComposer"));

            CatalogPageComposer.ComposeItem(catalogItem, message);
            Session.SendMessage(message);
        }

        /// <summary>
        ///     Catalogues the offer configuration.
        /// </summary>
        public void CatalogueOfferConfig()
        {
            Response.Init(LibraryParser.OutgoingRequest("CatalogueOfferConfigMessageComposer"));
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
        internal void SerializeGroupFurniPage()
        {
            HashSet<GroupMember> userGroups = Yupi.GetGame().GetGroupManager().GetUserGroups(Session.GetHabbo().Id);

            Response.Init(LibraryParser.OutgoingRequest("GroupFurniturePageMessageComposer"));

            List<ServerMessage> responseList = new List<ServerMessage>();

            foreach (
                Group habboGroup in
                    userGroups.Where(current => current != null)
                        .Select(current => Yupi.GetGame().GetGroupManager().GetGroup(current.GroupId)))
            {
                if (habboGroup == null)
                    continue;

                ServerMessage subResponse = new ServerMessage();
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