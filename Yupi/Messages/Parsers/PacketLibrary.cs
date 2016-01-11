using Yupi.Messages.Handlers;

namespace Yupi.Messages.Parsers
{
    /// <summary>
    ///     Class PacketLibrary.
    /// </summary>
    internal class PacketLibrary
    {
        /// <summary>
        ///     Initializes the crypto.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void InitCrypto(GameClientMessageHandler handler)
        {
            handler.InitCrypto();
        }

        /// <summary>
        ///     Secrets the key.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SecretKey(GameClientMessageHandler handler)
        {
            handler.SecretKey();
        }

        /// <summary>
        ///     Machines the identifier.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MachineId(GameClientMessageHandler handler)
        {
            handler.MachineId();
        }

        /// <summary>
        ///     Guides the message.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GuideMessage(GameClientMessageHandler handler)
        {
            handler.CallGuide();
        }

        /// <summary>
        ///     Sets the chat preferrence.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SetChatPreferrence(GameClientMessageHandler handler)
        {
            handler.SetChatPreferrence();
        }

        /// <summary>
        ///     Gets the helper tool.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetHelperTool(GameClientMessageHandler handler)
        {
            handler.OpenGuideTool();
        }

        /// <summary>
        ///     Gets the guide detached.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetGuideDetached(GameClientMessageHandler handler)
        {
            handler.AnswerGuideRequest();
        }

        /// <summary>
        ///     Logins the with ticket.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void LoginWithTicket(GameClientMessageHandler handler)
        {
            handler.LoginWithTicket();
        }

        /// <summary>
        ///     Invites the guide.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void InviteGuide(GameClientMessageHandler handler)
        {
            handler.InviteToRoom();
        }

        /// <summary>
        ///     Visits the room guide.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void VisitRoomGuide(GameClientMessageHandler handler)
        {
            handler.VisitRoom();
        }

        /// <summary>
        ///     Guides the end session.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GuideEndSession(GameClientMessageHandler handler)
        {
            handler.CloseGuideRequest();
        }

        /// <summary>
        ///     Cancels the call guide.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CancelCallGuide(GameClientMessageHandler handler)
        {
            handler.CancelCallGuide();
        }

        /// <summary>
        ///     Informations the retrieve.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void InfoRetrieve(GameClientMessageHandler handler)
        {
            handler.InfoRetrieve();
        }

        /// <summary>
        ///     Chats the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Chat(GameClientMessageHandler handler)
        {
            handler.Chat();
        }

        /// <summary>
        ///     Shouts the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Shout(GameClientMessageHandler handler)
        {
            handler.Shout();
        }

        /// <summary>
        ///     Requests the floor plan used coords.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RequestFloorPlanUsedCoords(GameClientMessageHandler handler)
        {
            handler.GetFloorPlanUsedCoords();
        }

        /// <summary>
        ///     Requests the floor plan door.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RequestFloorPlanDoor(GameClientMessageHandler handler)
        {
            handler.GetFloorPlanDoor();
        }

        /// <summary>
        ///     Opens the bully reporting.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OpenBullyReporting(GameClientMessageHandler handler)
        {
            handler.OpenBullyReporting();
        }

        /// <summary>
        ///     Sends the bully report.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SendBullyReport(GameClientMessageHandler handler)
        {
            handler.SendBullyReport();
        }

        /// <summary>
        ///     Navigators the get popular groups.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void NavigatorGetPopularGroups(GameClientMessageHandler handler)
        {
            handler.GetPopularGroups();
        }

        /// <summary>
        ///     Loads the club gifts.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void LoadClubGifts(GameClientMessageHandler handler)
        {
            handler.LoadClubGifts();
        }

        /// <summary>
        ///     Saves the heightmap.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveHeightmap(GameClientMessageHandler handler)
        {
            handler.SaveHeightmap();
        }

        /// <summary>
        ///     Accepts the poll.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AcceptPoll(GameClientMessageHandler handler)
        {
            handler.AcceptPoll();
        }

        /// <summary>
        ///     Refuses the poll.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RefusePoll(GameClientMessageHandler handler)
        {
            handler.RefusePoll();
        }

        /// <summary>
        ///     Answers the poll question.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AnswerPollQuestion(GameClientMessageHandler handler)
        {
            handler.AnswerPoll();
        }

        /// <summary>
        ///     Retrieves the song identifier.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RetrieveSongId(GameClientMessageHandler handler)
        {
            handler.RetrieveSongId();
        }

        /// <summary>
        ///     Tiles the height of the stack magic set.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TileStackMagicSetHeight(GameClientMessageHandler handler)
        {
            handler.TileStackMagicSetHeight();
        }

        /// <summary>
        ///     Enables the inventory effect.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void EnableInventoryEffect(GameClientMessageHandler handler)
        {
            handler.EnableEffect();
        }

        /// <summary>
        ///     Promotes the room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PromoteRoom(GameClientMessageHandler handler)
        {
            handler.PromoteRoom();
        }

        /// <summary>
        ///     Gets the promotionable rooms.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPromotionableRooms(GameClientMessageHandler handler)
        {
            handler.GetPromotionableRooms();
        }

        /// <summary>
        ///     Gets the room filter.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomFilter(GameClientMessageHandler handler)
        {
            handler.GetRoomFilter();
        }

        /// <summary>
        ///     Alters the room filter.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AlterRoomFilter(GameClientMessageHandler handler)
        {
            handler.AlterRoomFilter();
        }

        /// <summary>
        ///     Gets the tv player.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetTvPlayer(GameClientMessageHandler handler)
        {
            handler.GetTvPlayer();
        }

        /// <summary>
        ///     Chooses the tv player video.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ChooseTvPlayerVideo(GameClientMessageHandler handler)
        {
            handler.ChooseTvPlayerVideo();
        }

        /// <summary>
        ///     Gets the tv playlist.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetTvPlaylist(GameClientMessageHandler handler)
        {
            handler.ChooseTvPlaylist();
        }

        /// <summary>
        ///     Places the bot.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PlaceBot(GameClientMessageHandler handler)
        {
            handler.PlaceBot();
        }

        /// <summary>
        ///     Picks up bot.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PickUpBot(GameClientMessageHandler handler)
        {
            handler.PickUpBot();
        }

        /// <summary>
        ///     Gets the talents track.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetTalentsTrack(GameClientMessageHandler handler)
        {
            handler.Talents();
        }

        /// <summary>
        ///     Prepares the campaing.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PrepareCampaing(GameClientMessageHandler handler)
        {
            handler.PrepareCampaing();
        }

        /// <summary>
        ///     Pongs the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Pong(GameClientMessageHandler handler)
        {
            handler.Pong();
        }

        /// <summary>
        ///     Disconnects the event.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DisconnectEvent(GameClientMessageHandler handler)
        {
            handler.DisconnectEvent();
        }

        /// <summary>
        ///     Latencies the test.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void LatencyTest(GameClientMessageHandler handler)
        {
            handler.LatencyTest();
        }

        /// <summary>
        ///     Receptions the view.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ReceptionView(GameClientMessageHandler handler)
        {
            handler.GoToHotelView();
        }

        /// <summary>
        ///     Called when [confirmation event].
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OnlineConfirmationEvent(GameClientMessageHandler handler)
        {
            handler.OnlineConfirmationEvent();
        }

        /// <summary>
        ///     Retrives the citizen ship status.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RetriveCitizenShipStatus(GameClientMessageHandler handler)
        {
            handler.RetrieveCitizenship();
        }

        /// <summary>
        ///     Refreshes the promo event.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RefreshPromoEvent(GameClientMessageHandler handler)
        {
            handler.RefreshPromoEvent();
        }

        /// <summary>
        ///     Widgets the container.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void WidgetContainer(GameClientMessageHandler handler)
        {
            handler.WidgetContainers();
        }

        /// <summary>
        ///     Landings the community goal.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void LandingCommunityGoal(GameClientMessageHandler handler)
        {
            handler.LandingCommunityGoal();
        }

        /// <summary>
        ///     Removes the handitem.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveHanditem(GameClientMessageHandler handler)
        {
            handler.RemoveHanditem();
        }

        /// <summary>
        ///     Redeems the voucher.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RedeemVoucher(GameClientMessageHandler handler)
        {
            handler.RedeemVoucher();
        }

        /// <summary>
        ///     Gives the handitem.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GiveHanditem(GameClientMessageHandler handler)
        {
            handler.GiveHanditem();
        }

        /// <summary>
        ///     Initializes the help tool.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void InitHelpTool(GameClientMessageHandler handler)
        {
            handler.InitHelpTool();
        }

        /// <summary>
        ///     Submits the help ticket.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SubmitHelpTicket(GameClientMessageHandler handler)
        {
            handler.SubmitHelpTicket();
        }

        /// <summary>
        ///     Deletes the pending CFH.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DeletePendingCfh(GameClientMessageHandler handler)
        {
            handler.DeletePendingCfh();
        }

        /// <summary>
        ///     Mods the get user information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModGetUserInfo(GameClientMessageHandler handler)
        {
            handler.ModGetUserInfo();
        }

        /// <summary>
        ///     Mods the get user chatlog.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModGetUserChatlog(GameClientMessageHandler handler)
        {
            handler.ModGetUserChatlog();
        }

        /// <summary>
        ///     Messages from a guy.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MessageFromAGuy(GameClientMessageHandler handler)
        {
            handler.GuideSpeak();
        }

        /// <summary>
        ///     Mods the get room chatlog.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModGetRoomChatlog(GameClientMessageHandler handler)
        {
            handler.ModGetRoomChatlog();
        }

        /// <summary>
        ///     Mods the get room tool.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModGetRoomTool(GameClientMessageHandler handler)
        {
            handler.ModGetRoomTool();
        }

        /// <summary>
        ///     Mods the pick ticket.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModPickTicket(GameClientMessageHandler handler)
        {
            handler.ModPickTicket();
        }

        /// <summary>
        ///     Mods the release ticket.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModReleaseTicket(GameClientMessageHandler handler)
        {
            handler.ModReleaseTicket();
        }

        /// <summary>
        ///     Mods the close ticket.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModCloseTicket(GameClientMessageHandler handler)
        {
            handler.ModCloseTicket();
        }

        /// <summary>
        ///     Mods the get ticket chatlog.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModGetTicketChatlog(GameClientMessageHandler handler)
        {
            handler.ModGetTicketChatlog();
        }

        /// <summary>
        ///     Mods the get room visits.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModGetRoomVisits(GameClientMessageHandler handler)
        {
            handler.ModGetRoomVisits();
        }

        /// <summary>
        ///     Mods the send room alert.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModSendRoomAlert(GameClientMessageHandler handler)
        {
            handler.ModSendRoomAlert();
        }

        /// <summary>
        ///     Mods the perform room action.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModPerformRoomAction(GameClientMessageHandler handler)
        {
            handler.ModPerformRoomAction();
        }

        /// <summary>
        ///     Mods the send user caution.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModSendUserCaution(GameClientMessageHandler handler)
        {
            handler.ModSendUserCaution();
        }

        /// <summary>
        ///     Mods the send user message.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModSendUserMessage(GameClientMessageHandler handler)
        {
            handler.ModSendUserMessage();
        }

        /// <summary>
        ///     Mods the kick user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModKickUser(GameClientMessageHandler handler)
        {
            handler.ModKickUser();
        }

        /// <summary>
        ///     Mods the mute user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModMuteUser(GameClientMessageHandler handler)
        {
            handler.ModMuteUser();
        }

        /// <summary>
        ///     Mods the lock trade.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModLockTrade(GameClientMessageHandler handler)
        {
            handler.ModLockTrade();
        }

        /// <summary>
        ///     Mods the ban user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModBanUser(GameClientMessageHandler handler)
        {
            handler.ModBanUser();
        }

        /// <summary>
        ///     Initializes the messenger.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void InitMessenger(GameClientMessageHandler handler)
        {
            //handler.InitMessenger();
        }

        /// <summary>
        ///     Friendses the list update.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void FriendsListUpdate(GameClientMessageHandler handler)
        {
            handler.FriendsListUpdate();
        }

        /// <summary>
        ///     Removes the buddy.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveBuddy(GameClientMessageHandler handler)
        {
            handler.RemoveBuddy();
        }

        /// <summary>
        ///     Searches the habbo.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SearchHabbo(GameClientMessageHandler handler)
        {
            handler.SearchHabbo();
        }

        /// <summary>
        ///     Accepts the request.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AcceptRequest(GameClientMessageHandler handler)
        {
            handler.AcceptRequest();
        }

        /// <summary>
        ///     Declines the request.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DeclineRequest(GameClientMessageHandler handler)
        {
            handler.DeclineRequest();
        }

        /// <summary>
        ///     Requests the buddy.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RequestBuddy(GameClientMessageHandler handler)
        {
            handler.RequestBuddy();
        }

        /// <summary>
        ///     Sends the instant messenger.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SendInstantMessenger(GameClientMessageHandler handler)
        {
            handler.SendInstantMessenger();
        }

        /// <summary>
        ///     Follows the buddy.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void FollowBuddy(GameClientMessageHandler handler)
        {
            handler.FollowBuddy();
        }

        /// <summary>
        ///     Sends the instant invite.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SendInstantInvite(GameClientMessageHandler handler)
        {
            handler.SendInstantInvite();
        }

        /// <summary>
        ///     Homes the room stuff.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void HomeRoomStuff(GameClientMessageHandler handler)
        {
            handler.HomeRoom();
        }

        /// <summary>
        ///     Adds the favorite.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AddFavorite(GameClientMessageHandler handler)
        {
            handler.AddFavorite();
        }

        /// <summary>
        ///     Removes the favorite.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveFavorite(GameClientMessageHandler handler)
        {
            handler.RemoveFavorite();
        }

        /// <summary>
        ///     Gets the flat cats.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetFlatCats(GameClientMessageHandler handler)
        {
            handler.GetFlatCats();
        }

        /// <summary>
        ///     Enters the inquired room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void EnterInquiredRoom(GameClientMessageHandler handler)
        {
            handler.EnterInquiredRoom();
        }

        /// <summary>
        ///     Gets the pubs.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPubs(GameClientMessageHandler handler)
        {
            handler.GetPubs();
        }

        /// <summary>
        ///     Saves the branding.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveBranding(GameClientMessageHandler handler)
        {
            handler.SaveBranding();
        }

        /// <summary>
        ///     Gets the room information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomInfo(GameClientMessageHandler handler)
        {
            handler.GetRoomInfo();
        }

        /// <summary>
        ///     Gets the popular rooms.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPopularRooms(GameClientMessageHandler handler)
        {
            handler.GetPopularRooms();
        }

        /// <summary>
        ///     Gets the recommended rooms.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRecommendedRooms(GameClientMessageHandler handler)
        {
            handler.GetRecommendedRooms();
        }

        /// <summary>
        ///     Gets the high rated rooms.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetHighRatedRooms(GameClientMessageHandler handler)
        {
            handler.GetHighRatedRooms();
        }

        /// <summary>
        ///     Gets the friends rooms.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetFriendsRooms(GameClientMessageHandler handler)
        {
            handler.GetFriendsRooms();
        }

        /// <summary>
        ///     Gets the rooms with friends.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomsWithFriends(GameClientMessageHandler handler)
        {
            handler.GetRoomsWithFriends();
        }

        /// <summary>
        ///     Gets the own rooms.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetOwnRooms(GameClientMessageHandler handler)
        {
            handler.GetOwnRooms();
        }

        /// <summary>
        ///     News the navigator flat cats.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void NewNavigatorFlatCats(GameClientMessageHandler handler)
        {
            handler.NewNavigatorFlatCats();
        }

        /// <summary>
        ///     Gets the favorite rooms.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetFavoriteRooms(GameClientMessageHandler handler)
        {
            handler.GetFavoriteRooms();
        }

        /// <summary>
        ///     Gets the recent rooms.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRecentRooms(GameClientMessageHandler handler)
        {
            handler.GetRecentRooms();
        }

        /// <summary>
        ///     Gets the popular tags.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPopularTags(GameClientMessageHandler handler)
        {
            handler.GetPopularTags();
        }

        /// <summary>
        ///     Performs the search.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PerformSearch(GameClientMessageHandler handler)
        {
            handler.PerformSearch();
        }

        /// <summary>
        ///     Searches the by tag.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SearchByTag(GameClientMessageHandler handler)
        {
            handler.SearchByTag();
        }

        /// <summary>
        ///     Performs the search2.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PerformSearch2(GameClientMessageHandler handler)
        {
            handler.PerformSearch2();
        }

        /// <summary>
        ///     Opens the flat.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OpenFlat(GameClientMessageHandler handler)
        {
            handler.OpenFlat();
        }

        /// <summary>
        ///     Gets the voume.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetVoume(GameClientMessageHandler handler)
        {
            handler.LoadSettings();
        }

        /// <summary>
        ///     Saves the volume.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveVolume(GameClientMessageHandler handler)
        {
            handler.SaveSettings();
        }

        /// <summary>
        ///     Gets the pub.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPub(GameClientMessageHandler handler)
        {
            handler.GetPub();
        }

        /// <summary>
        ///     Opens the pub.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OpenPub(GameClientMessageHandler handler)
        {
            handler.OpenPub();
        }

        /// <summary>
        ///     Gets the inventory.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetInventory(GameClientMessageHandler handler)
        {
            handler.GetInventory();
        }

        /// <summary>
        ///     Gets the room data1.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomData1(GameClientMessageHandler handler)
        {
            handler.GetRoomData1();
        }

        /// <summary>
        ///     Gets the room data2.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomData2(GameClientMessageHandler handler)
        {
            handler.GetRoomData2();
        }

        /// <summary>
        ///     Gets the room data3.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomData3(GameClientMessageHandler handler)
        {
            handler.GetRoomData3();
        }

        /// <summary>
        ///     Requests the floor items.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RequestFloorItems(GameClientMessageHandler handler)
        {
            handler.RequestFloorItems();
        }

        /// <summary>
        ///     Requests the wall items.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RequestWallItems(GameClientMessageHandler handler)
        {
            handler.RequestWallItems();
        }

        /// <summary>
        ///     Called when [room user add].
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OnRoomUserAdd(GameClientMessageHandler handler)
        {
            handler.OnRoomUserAdd();
        }

        /// <summary>
        ///     Reqs the load room for user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ReqLoadRoomForUser(GameClientMessageHandler handler)
        {
            handler.ReqLoadRoomForUser();
        }

        /// <summary>
        ///     Enters the on room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void EnterOnRoom(GameClientMessageHandler handler)
        {
            handler.EnterOnRoom();
        }

        /// <summary>
        ///     Clears the room loading.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ClearRoomLoading(GameClientMessageHandler handler)
        {
            handler.ClearRoomLoading();
        }

        /// <summary>
        ///     Moves the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Move(GameClientMessageHandler handler)
        {
            handler.Move();
        }

        /// <summary>
        ///     Determines whether this instance [can create room] the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CanCreateRoom(GameClientMessageHandler handler)
        {
            handler.CanCreateRoom();
        }

        /// <summary>
        ///     Creates the room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CreateRoom(GameClientMessageHandler handler)
        {
            handler.CreateRoom();
        }

        /// <summary>
        ///     Gets the room information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomInformation(GameClientMessageHandler handler)
        {
            handler.ParseRoomDataInformation();
        }

        /// <summary>
        ///     Gets the room edit data.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomEditData(GameClientMessageHandler handler)
        {
            handler.GetRoomEditData();
        }

        /// <summary>
        ///     Saves the room data.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveRoomData(GameClientMessageHandler handler)
        {
            handler.SaveRoomData();
        }

        /// <summary>
        ///     Gives the rights.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GiveRights(GameClientMessageHandler handler)
        {
            handler.GiveRights();
        }

        /// <summary>
        ///     Takes the rights.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TakeRights(GameClientMessageHandler handler)
        {
            handler.TakeRights();
        }

        /// <summary>
        ///     Takes all rights.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TakeAllRights(GameClientMessageHandler handler)
        {
            handler.TakeAllRights();
        }

        /// <summary>
        ///     Habboes the camera.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void HabboCamera(GameClientMessageHandler handler)
        {
            handler.HabboCamera();
        }

        /// <summary>
        ///     Called when [click].
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OnClick(GameClientMessageHandler handler)
        {
            handler.OnClick();
        }

        /// <summary>
        ///     Kicks the user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void KickUser(GameClientMessageHandler handler)
        {
            handler.KickUser();
        }

        /// <summary>
        ///     Bans the user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void BanUser(GameClientMessageHandler handler)
        {
            handler.BanUser();
        }

        /// <summary>
        ///     Sets the home room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SetHomeRoom(GameClientMessageHandler handler)
        {
            handler.SetHomeRoom();
        }

        /// <summary>
        ///     Deletes the room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DeleteRoom(GameClientMessageHandler handler)
        {
            handler.DeleteRoom();
        }

        /// <summary>
        ///     Looks at.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void LookAt(GameClientMessageHandler handler)
        {
            handler.LookAt();
        }

        /// <summary>
        ///     Starts the typing.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void StartTyping(GameClientMessageHandler handler)
        {
            handler.StartTyping();
        }

        /// <summary>
        ///     Stops the typing.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void StopTyping(GameClientMessageHandler handler)
        {
            handler.StopTyping();
        }

        /// <summary>
        ///     Ignores the user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void IgnoreUser(GameClientMessageHandler handler)
        {
            handler.IgnoreUser();
        }

        /// <summary>
        ///     Unignores the user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UnignoreUser(GameClientMessageHandler handler)
        {
            handler.UnignoreUser();
        }

        /// <summary>
        ///     Determines whether this instance [can create room event] the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CanCreateRoomEvent(GameClientMessageHandler handler)
        {
            handler.CanCreateRoomEvent();
        }

        /// <summary>
        ///     Signs the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Sign(GameClientMessageHandler handler)
        {
            handler.Sign();
        }

        /// <summary>
        ///     Gets the user tags.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetUserTags(GameClientMessageHandler handler)
        {
            handler.GetUserTags();
        }

        /// <summary>
        ///     Gets the user badges.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetUserBadges(GameClientMessageHandler handler)
        {
            handler.GetUserBadges();
        }

        /// <summary>
        ///     Rates the room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RateRoom(GameClientMessageHandler handler)
        {
            handler.RateRoom();
        }

        /// <summary>
        ///     Dances the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Dance(GameClientMessageHandler handler)
        {
            handler.Dance();
        }

        /// <summary>
        ///     Answers the doorbell.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AnswerDoorbell(GameClientMessageHandler handler)
        {
            handler.AnswerDoorbell();
        }

        /// <summary>
        ///     Applies the room effect.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ApplyRoomEffect(GameClientMessageHandler handler)
        {
            handler.ApplyRoomEffect();
        }

        /// <summary>
        ///     Places the post it.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PlacePostIt(GameClientMessageHandler handler)
        {
            handler.PlacePostIt();
        }

        /// <summary>
        ///     Places the item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PlaceItem(GameClientMessageHandler handler)
        {
            handler.PlaceItem();
        }

        /// <summary>
        ///     Takes the item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TakeItem(GameClientMessageHandler handler)
        {
            handler.TakeItem();
        }

        /// <summary>
        ///     Moves the item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MoveItem(GameClientMessageHandler handler)
        {
            handler.MoveItem();
        }

        /// <summary>
        ///     Moves the wall item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MoveWallItem(GameClientMessageHandler handler)
        {
            handler.MoveWallItem();
        }

        /// <summary>
        ///     Triggers the item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TriggerItem(GameClientMessageHandler handler)
        {
            handler.TriggerItem();
        }

        /// <summary>
        ///     Triggers the item dice special.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TriggerItemDiceSpecial(GameClientMessageHandler handler)
        {
            handler.TriggerItemDiceSpecial();
        }

        /// <summary>
        ///     Opens the postit.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OpenPostit(GameClientMessageHandler handler)
        {
            handler.OpenPostit();
        }

        /// <summary>
        ///     Saves the postit.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SavePostit(GameClientMessageHandler handler)
        {
            handler.SavePostit();
        }

        /// <summary>
        ///     Deletes the postit.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DeletePostit(GameClientMessageHandler handler)
        {
            handler.DeletePostit();
        }

        /// <summary>
        ///     Opens the present.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OpenPresent(GameClientMessageHandler handler)
        {
            handler.OpenGift();
        }

        /// <summary>
        ///     Gets the moodlight.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetMoodlight(GameClientMessageHandler handler)
        {
            handler.GetMoodlight();
        }

        /// <summary>
        ///     Updates the moodlight.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateMoodlight(GameClientMessageHandler handler)
        {
            handler.UpdateMoodlight();
        }

        /// <summary>
        ///     Switches the moodlight status.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SwitchMoodlightStatus(GameClientMessageHandler handler)
        {
            handler.SwitchMoodlightStatus();
        }

        /// <summary>
        ///     Initializes the trade.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void InitTrade(GameClientMessageHandler handler)
        {
            handler.InitTrade();
        }

        /// <summary>
        ///     Offers the trade item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OfferTradeItem(GameClientMessageHandler handler)
        {
            handler.OfferTradeItem();
        }

        /// <summary>
        ///     Takes the back trade item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TakeBackTradeItem(GameClientMessageHandler handler)
        {
            handler.TakeBackTradeItem();
        }

        /// <summary>
        ///     Stops the trade.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void StopTrade(GameClientMessageHandler handler)
        {
            handler.StopTrade();
        }

        /// <summary>
        ///     Accepts the trade.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AcceptTrade(GameClientMessageHandler handler)
        {
            handler.AcceptTrade();
        }

        /// <summary>
        ///     Unaccepts the trade.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UnacceptTrade(GameClientMessageHandler handler)
        {
            handler.UnacceptTrade();
        }

        /// <summary>
        ///     Completes the trade.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CompleteTrade(GameClientMessageHandler handler)
        {
            handler.CompleteTrade();
        }

        /// <summary>
        ///     Gives the respect.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GiveRespect(GameClientMessageHandler handler)
        {
            handler.GiveRespect();
        }

        /// <summary>
        ///     Applies the effect.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ApplyEffect(GameClientMessageHandler handler)
        {
            handler.ApplyEffect();
        }

        /// <summary>
        ///     Enables the effect.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void EnableEffect(GameClientMessageHandler handler)
        {
            handler.EnableEffect();
        }

        /// <summary>
        ///     Recycles the items.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RecycleItems(GameClientMessageHandler handler)
        {
            handler.RecycleItems();
        }

        /// <summary>
        ///     Redeems the exchange furni.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RedeemExchangeFurni(GameClientMessageHandler handler)
        {
            handler.RedeemExchangeFurni();
        }

        /// <summary>
        ///     Kicks the bot.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void KickBot(GameClientMessageHandler handler)
        {
            handler.KickBot();
        }

        /// <summary>
        ///     Places the pet.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PlacePet(GameClientMessageHandler handler)
        {
            handler.PlacePet();
        }

        /// <summary>
        ///     Gets the pet information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPetInfo(GameClientMessageHandler handler)
        {
            handler.GetPetInfo();
        }

        /// <summary>
        ///     Picks up pet.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PickUpPet(GameClientMessageHandler handler)
        {
            handler.PickUpPet();
        }

        /// <summary>
        ///     Composts the monsterplant.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CompostMonsterplant(GameClientMessageHandler handler)
        {
            handler.CompostMonsterplant();
        }

        /// <summary>
        ///     Moves the pet.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MovePet(GameClientMessageHandler handler)
        {
            handler.MovePet();
        }

        /// <summary>
        ///     Respects the pet.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RespectPet(GameClientMessageHandler handler)
        {
            handler.RespectPet();
        }

        /// <summary>
        ///     Adds the saddle.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AddSaddle(GameClientMessageHandler handler)
        {
            handler.AddSaddle();
        }

        /// <summary>
        ///     Removes the saddle.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveSaddle(GameClientMessageHandler handler)
        {
            handler.RemoveSaddle();
        }

        /// <summary>
        ///     Rides the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Ride(GameClientMessageHandler handler)
        {
            handler.MountOnPet();
        }

        /// <summary>
        ///     Unrides the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Unride(GameClientMessageHandler handler)
        {
            handler.CancelMountOnPet();
        }

        /// <summary>
        ///     Saves the wired.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveWired(GameClientMessageHandler handler)
        {
            handler.SaveWired();
        }

        /// <summary>
        ///     Saves the wired condition.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveWiredCondition(GameClientMessageHandler handler)
        {
            handler.SaveWiredConditions();
        }

        /// <summary>
        ///     Gets the music data.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetMusicData(GameClientMessageHandler handler)
        {
            handler.GetMusicData();
        }

        /// <summary>
        ///     Adds the playlist item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AddPlaylistItem(GameClientMessageHandler handler)
        {
            handler.AddPlaylistItem();
        }

        /// <summary>
        ///     Removes the playlist item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemovePlaylistItem(GameClientMessageHandler handler)
        {
            handler.RemovePlaylistItem();
        }

        /// <summary>
        ///     Gets the disks.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetDisks(GameClientMessageHandler handler)
        {
            handler.GetDisks();
        }

        /// <summary>
        ///     Gets the playlists.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPlaylists(GameClientMessageHandler handler)
        {
            handler.GetPlaylists();
        }

        /// <summary>
        ///     Gets the user information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetUserInfo(GameClientMessageHandler handler)
        {
            handler.GetUserInfo();
        }

        /// <summary>
        ///     Loads the profile.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void LoadProfile(GameClientMessageHandler handler)
        {
            handler.LoadProfile();
        }

        /// <summary>
        ///     Gets the balance.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetBalance(GameClientMessageHandler handler)
        {
            handler.GetBalance();
        }

        /// <summary>
        ///     Gets the subscription data.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetSubscriptionData(GameClientMessageHandler handler)
        {
            handler.GetSubscriptionData();
        }

        /// <summary>
        ///     Gets the badges.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetBadges(GameClientMessageHandler handler)
        {
            handler.GetBadges();
        }

        /// <summary>
        ///     Updates the badges.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateBadges(GameClientMessageHandler handler)
        {
            handler.UpdateBadges();
        }

        /// <summary>
        ///     Gets the achievements.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetAchievements(GameClientMessageHandler handler)
        {
            handler.GetAchievements();
        }

        /// <summary>
        ///     Changes the look.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ChangeLook(GameClientMessageHandler handler)
        {
            handler.ChangeLook();
        }

        /// <summary>
        ///     Changes the motto.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ChangeMotto(GameClientMessageHandler handler)
        {
            handler.ChangeMotto();
        }

        /// <summary>
        ///     Gets the wardrobe.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetWardrobe(GameClientMessageHandler handler)
        {
            handler.GetWardrobe();
        }

        /// <summary>
        ///     Allows all ride.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AllowAllRide(GameClientMessageHandler handler)
        {
            handler.AllowAllRide();
        }

        /// <summary>
        ///     Saves the wardrobe.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveWardrobe(GameClientMessageHandler handler)
        {
            handler.SaveWardrobe();
        }

        /// <summary>
        ///     Gets the pets inventory.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPetsInventory(GameClientMessageHandler handler)
        {
            handler.GetPetsInventory();
        }

        /// <summary>
        ///     Opens the quests.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OpenQuests(GameClientMessageHandler handler)
        {
            //handler.OpenQuests();
        }

        /// <summary>
        ///     Starts the quest.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void StartQuest(GameClientMessageHandler handler)
        {
            //handler.StartQuest();
        }

        /// <summary>
        ///     Stops the quest.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void StopQuest(GameClientMessageHandler handler)
        {
            //handler.StopQuest();
        }

        /// <summary>
        ///     Gets the current quest.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetCurrentQuest(GameClientMessageHandler handler)
        {
            //handler.GetCurrentQuest();
        }

        /// <summary>
        ///     Gets the group badges.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetGroupBadges(GameClientMessageHandler handler)
        {
            handler.InitRoomGroupBadges();
        }

        /// <summary>
        ///     Gets the bot inv.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetBotInv(GameClientMessageHandler handler)
        {
            handler.GetBotsInventory();
        }

        /// <summary>
        ///     Saves the room bg.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveRoomBg(GameClientMessageHandler handler)
        {
            handler.SaveRoomBg();
        }

        /// <summary>
        ///     Goes the room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GoRoom(GameClientMessageHandler handler)
        {
            handler.GoRoom();
        }

        /// <summary>
        ///     Sits the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Sit(GameClientMessageHandler handler)
        {
            handler.Sit();
        }

        /// <summary>
        ///     Gets the event rooms.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetEventRooms(GameClientMessageHandler handler)
        {
            handler.GetEventRooms();
        }

        /// <summary>
        ///     Starts the seasonal quest.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void StartSeasonalQuest(GameClientMessageHandler handler)
        {
            //handler.StartSeasonalQuest();
        }

        /// <summary>
        ///     Saves the mannequin.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveMannequin(GameClientMessageHandler handler)
        {
            handler.SaveMannequin();
        }

        /// <summary>
        ///     Saves the mannequin2.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveMannequin2(GameClientMessageHandler handler)
        {
            handler.SaveMannequin2();
        }

        /// <summary>
        ///     Serializes the group purchase page.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SerializeGroupPurchasePage(GameClientMessageHandler handler)
        {
            handler.SerializeGroupPurchasePage();
        }

        /// <summary>
        ///     Serializes the group purchase parts.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SerializeGroupPurchaseParts(GameClientMessageHandler handler)
        {
            handler.SerializeGroupPurchaseParts();
        }

        /// <summary>
        ///     Purchases the group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PurchaseGroup(GameClientMessageHandler handler)
        {
            handler.PurchaseGroup();
        }

        /// <summary>
        ///     Serializes the group information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SerializeGroupInfo(GameClientMessageHandler handler)
        {
            handler.SerializeGroupInfo();
        }

        /// <summary>
        ///     Serializes the group members.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SerializeGroupMembers(GameClientMessageHandler handler)
        {
            handler.SerializeGroupMembers();
        }

        /// <summary>
        ///     Makes the group admin.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MakeGroupAdmin(GameClientMessageHandler handler)
        {
            handler.MakeGroupAdmin();
        }

        /// <summary>
        ///     Removes the group admin.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveGroupAdmin(GameClientMessageHandler handler)
        {
            handler.RemoveGroupAdmin();
        }

        /// <summary>
        ///     Accepts the membership.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AcceptMembership(GameClientMessageHandler handler)
        {
            handler.AcceptMembership();
        }

        /// <summary>
        ///     Declines the membership.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DeclineMembership(GameClientMessageHandler handler)
        {
            handler.DeclineMembership();
        }

        /// <summary>
        ///     Removes the member.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveMember(GameClientMessageHandler handler)
        {
            handler.RemoveMember();
        }

        /// <summary>
        ///     Joins the group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void JoinGroup(GameClientMessageHandler handler)
        {
            handler.JoinGroup();
        }

        /// <summary>
        ///     Makes the fav.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MakeFav(GameClientMessageHandler handler)
        {
            handler.MakeFav();
        }

        /// <summary>
        ///     Removes the fav.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveFav(GameClientMessageHandler handler)
        {
            handler.RemoveFav();
        }

        /// <summary>
        ///     Receives the nux gifts.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ReceiveNuxGifts(GameClientMessageHandler handler)
        {
            handler.ReceiveNuxGifts();
        }

        /// <summary>
        ///     Accepts the nux gifts.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AcceptNuxGifts(GameClientMessageHandler handler)
        {
            handler.AcceptNuxGifts();
        }

        /// <summary>
        ///     Reads the forum thread.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ReadForumThread(GameClientMessageHandler handler)
        {
            handler.ReadForumThread();
        }

        /// <summary>
        ///     Publishes the forum thread.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PublishForumThread(GameClientMessageHandler handler)
        {
            handler.PublishForumThread();
        }

        /// <summary>
        ///     Updates the forum thread.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateForumThread(GameClientMessageHandler handler)
        {
            handler.UpdateThreadState();
        }

        /// <summary>
        ///     Alters the state of the forum thread.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AlterForumThreadState(GameClientMessageHandler handler)
        {
            handler.AlterForumThreadState();
        }

        /// <summary>
        ///     Gets the forum thread root.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetForumThreadRoot(GameClientMessageHandler handler)
        {
            handler.GetGroupForumThreadRoot();
        }

        /// <summary>
        ///     Gets the group forum data.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetGroupForumData(GameClientMessageHandler handler)
        {
            handler.GetGroupForumData();
        }

        /// <summary>
        ///     Gets the group forums.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetGroupForums(GameClientMessageHandler handler)
        {
            handler.GetGroupForums();
        }

        /// <summary>
        ///     Manages the group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ManageGroup(GameClientMessageHandler handler)
        {
            handler.ManageGroup();
        }

        /// <summary>
        ///     Updates the name of the group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateGroupName(GameClientMessageHandler handler)
        {
            handler.UpdateGroupName();
        }

        /// <summary>
        ///     Updates the group badge.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateGroupBadge(GameClientMessageHandler handler)
        {
            handler.UpdateGroupBadge();
        }

        /// <summary>
        ///     Updates the group colours.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateGroupColours(GameClientMessageHandler handler)
        {
            handler.UpdateGroupColours();
        }

        /// <summary>
        ///     Updates the group settings.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateGroupSettings(GameClientMessageHandler handler)
        {
            handler.UpdateGroupSettings();
        }

        /// <summary>
        ///     Serializes the group furni page.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SerializeGroupFurniPage(GameClientMessageHandler handler)
        {
            handler.SerializeGroupFurniPage();
        }

        /// <summary>
        ///     Ejects the furni.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void EjectFurni(GameClientMessageHandler handler)
        {
            handler.EjectFurni();
        }

        /// <summary>
        ///     Mutes the user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MuteUser(GameClientMessageHandler handler)
        {
            handler.MuteUser();
        }

        /// <summary>
        ///     Checks the name.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CheckName(GameClientMessageHandler handler)
        {
            handler.CheckName();
        }

        /// <summary>
        ///     Changes the name.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ChangeName(GameClientMessageHandler handler)
        {
            handler.ChangeName();
        }

        /// <summary>
        ///     Gets the trainer panel.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetTrainerPanel(GameClientMessageHandler handler)
        {
            handler.GetTrainerPanel();
        }

        /// <summary>
        ///     Updates the event information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateEventInfo(GameClientMessageHandler handler)
        {
            handler.UpdateEventInfo();
        }

        /// <summary>
        ///     Gets the room banned users.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomBannedUsers(GameClientMessageHandler handler)
        {
            handler.GetBannedUsers();
        }

        /// <summary>
        ///     Userses the with rights.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UsersWithRights(GameClientMessageHandler handler)
        {
            handler.UsersWithRights();
        }

        /// <summary>
        ///     Unbans the user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UnbanUser(GameClientMessageHandler handler)
        {
            handler.UnbanUser();
        }

        /// <summary>
        ///     Manages the bot actions.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ManageBotActions(GameClientMessageHandler handler)
        {
            handler.ManageBotActions();
        }

        /// <summary>
        ///     Handles the bot speech list.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void HandleBotSpeechList(GameClientMessageHandler handler)
        {
            handler.HandleBotSpeechList();
        }

        /// <summary>
        ///     Gets the relationships.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRelationships(GameClientMessageHandler handler)
        {
            handler.GetRelationships();
        }

        /// <summary>
        ///     Sets the relationship.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SetRelationship(GameClientMessageHandler handler)
        {
            handler.SetRelationship();
        }

        /// <summary>
        ///     Automatics the room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AutoRoom(GameClientMessageHandler handler)
        {
            handler.RoomOnLoad();
        }

        /// <summary>
        ///     Mutes all.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MuteAll(GameClientMessageHandler handler)
        {
            handler.MuteAll();
        }

        /// <summary>
        ///     Completes the saftey quiz.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CompleteSafteyQuiz(GameClientMessageHandler handler)
        {
            handler.CompleteSafetyQuiz();
        }

        /// <summary>
        ///     Removes the favourite room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveFavouriteRoom(GameClientMessageHandler handler)
        {
            handler.RemoveFavouriteRoom();
        }

        /// <summary>
        ///     Rooms the user action.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RoomUserAction(GameClientMessageHandler handler)
        {
            handler.RoomUserAction();
        }

        /// <summary>
        ///     Saves the football outfit.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveFootballOutfit(GameClientMessageHandler handler)
        {
            handler.SaveFootballOutfit();
        }

        /// <summary>
        ///     Confirms the love lock.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ConfirmLoveLock(GameClientMessageHandler handler)
        {
            handler.ConfirmLoveLock();
        }

        /// <summary>
        ///     Builderses the club update furni count.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void BuildersClubUpdateFurniCount(GameClientMessageHandler handler)
        {
            handler.BuildersClubUpdateFurniCount();
        }

        /// <summary>
        ///     Gets the client version message event.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetClientVersionMessageEvent(GameClientMessageHandler handler)
        {
            handler.GetClientVersionMessageEvent();
        }

        /// <summary>
        ///     Places the builders furniture.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PlaceBuildersFurniture(GameClientMessageHandler handler)
        {
            handler.PlaceBuildersFurniture();
        }

        /// <summary>
        ///     Whispers the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Whisper(GameClientMessageHandler handler)
        {
            handler.Whisper();
        }

        /// <summary>
        ///     Catalogues the index.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CatalogueIndex(GameClientMessageHandler handler)
        {
            handler.CatalogueIndex();
        }

        /// <summary>
        ///     Catalogues the page.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CataloguePage(GameClientMessageHandler handler)
        {
            handler.CataloguePage();
        }

        /// <summary>
        ///     Catalogues the club page.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CatalogueClubPage(GameClientMessageHandler handler)
        {
            handler.CatalogueClubPage();
        }

        /// <summary>
        ///     Catalogues the offers configuration.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CatalogueOffersConfig(GameClientMessageHandler handler)
        {
            handler.CatalogueOfferConfig();
        }

        /// <summary>
        ///     Catalogues the single offer.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CatalogueSingleOffer(GameClientMessageHandler handler)
        {
            handler.CatalogueOffer();
        }

        /// <summary>
        ///     Checks the name of the pet.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CheckPetName(GameClientMessageHandler handler)
        {
            handler.CheckPetName();
        }

        /// <summary>
        ///     Purchases the item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PurchaseItem(GameClientMessageHandler handler)
        {
            handler.PurchaseItem();
        }

        /// <summary>
        ///     Purchases the gift.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PurchaseGift(GameClientMessageHandler handler)
        {
            handler.PurchaseGift();
        }

        /// <summary>
        ///     Gets the pet breeds.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPetBreeds(GameClientMessageHandler handler)
        {
            handler.GetPetBreeds();
        }

        /// <summary>
        ///     Reloads the ecotron.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ReloadEcotron(GameClientMessageHandler handler)
        {
            handler.ReloadEcotron();
        }

        /// <summary>
        ///     Gifts the wrapping configuration.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GiftWrappingConfig(GameClientMessageHandler handler)
        {
            handler.GiftWrappingConfig();
        }

        /// <summary>
        ///     Recyclers the rewards.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RecyclerRewards(GameClientMessageHandler handler)
        {
            handler.GetRecyclerRewards();
        }

        /// <summary>
        ///     Requests the leave group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RequestLeaveGroup(GameClientMessageHandler handler)
        {
            handler.RequestLeaveGroup();
        }

        /// <summary>
        ///     Confirms the leave group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ConfirmLeaveGroup(GameClientMessageHandler handler)
        {
            handler.ConfirmLeaveGroup();
        }

        /// <summary>
        ///     News the navigator.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void NewNavigator(GameClientMessageHandler handler)
        {
            handler.NewNavigator();
        }

        /// <summary>
        ///     Searches the new navigator.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SearchNewNavigator(GameClientMessageHandler handler)
        {
            handler.SearchNewNavigator();
        }

        /// <summary>
        ///     News the navigator delete saved search.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void NewNavigatorDeleteSavedSearch(GameClientMessageHandler handler)
        {
            handler.NewNavigatorDeleteSavedSearch();
        }

        /// <summary>
        ///     News the navigator resize.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void NewNavigatorResize(GameClientMessageHandler handler)
        {
            handler.NewNavigatorResize();
        }

        /// <summary>
        ///     News the navigator add saved search.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void NewNavigatorAddSavedSearch(GameClientMessageHandler handler)
        {
            handler.NewNavigatorAddSavedSearch();
        }

        /// <summary>
        ///     News the navigator collapse category.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void NewNavigatorCollapseCategory(GameClientMessageHandler handler)
        {
            handler.NewNavigatorCollapseCategory();
        }

        /// <summary>
        ///     News the navigator uncollapse category.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void NewNavigatorUncollapseCategory(GameClientMessageHandler handler)
        {
            handler.NewNavigatorUncollapseCategory();
        }

        /// <summary>
        ///     Pets the breed result.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PetBreedResult(GameClientMessageHandler handler)
        {
            handler.PetBreedResult();
        }

        /// <summary>
        ///     Pets the breed cancel.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PetBreedCancel(GameClientMessageHandler handler)
        {
            handler.PetBreedCancel();
        }

        /// <summary>
        ///     Games the center load game.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GameCenterLoadGame(GameClientMessageHandler handler)
        {
            handler.GameCenterLoadGame();
        }

        /// <summary>
        ///     Games the center join queue.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GameCenterJoinQueue(GameClientMessageHandler handler)
        {
            handler.GameCenterJoinQueue();
        }

        /// <summary>
        ///     Hotels the view countdown.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void HotelViewCountdown(GameClientMessageHandler handler)
        {
            handler.HotelViewCountdown();
        }

        /// <summary>
        ///     Hotels the view dailyquest.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void HotelViewDailyquest(GameClientMessageHandler handler)
        {
            handler.HotelViewDailyquest();
        }

        /// <summary>
        ///     Places the builders wall item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PlaceBuildersWallItem(GameClientMessageHandler handler)
        {
            handler.PlaceBuildersWallItem();
        }

        /// <summary>
        ///     Targeteds the offer buy.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PurchaseTargetedOffer(GameClientMessageHandler handler)
        {
            handler.PurchaseTargetedOffer();
        }

        /// <summary>
        ///     Ambassadors the alert.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AmbassadorAlert(GameClientMessageHandler handler)
        {
            handler.AmbassadorAlert();
        }

        /// <summary>
        ///     Goes the name of to room by.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GoToRoomByName(GameClientMessageHandler handler)
        {
            handler.GoToRoomByName();
        }

        /// <summary>
        ///     Gets the uc panel.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetUcPanel(GameClientMessageHandler handler)
        {
            handler.GetUcPanel();
        }

        /// <summary>
        ///     Gets the uc panel hotel.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetUcPanelHotel(GameClientMessageHandler handler)
        {
            handler.GetUcPanelHotel();
        }

        /// <summary>
        ///     Saves the room thumbnail.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveRoomThumbnail(GameClientMessageHandler handler)
        {
            handler.SaveRoomThumbnail();
        }

        /// <summary>
        ///     Uses the purchasable clothing.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UsePurchasableClothing(GameClientMessageHandler handler)
        {
            handler.UsePurchasableClothing();
        }

        /// <summary>
        ///     Gets the user look.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetUserLook(GameClientMessageHandler handler)
        {
            handler.GetUserLook();
        }

        /// <summary>
        ///     Sets the invitations preference.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SetInvitationsPreference(GameClientMessageHandler handler)
        {
            handler.SetInvitationsPreference();
        }

        /// <summary>
        ///     Finds the more friends.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void FindMoreFriends(GameClientMessageHandler handler)
        {
            handler.FindMoreFriends();
        }

        /// <summary>
        ///     Hotels the view request badge.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void HotelViewRequestBadge(GameClientMessageHandler handler)
        {
            handler.HotelViewRequestBadge();
        }

        /// <summary>
        ///     Gets the camera price.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetCameraPrice(GameClientMessageHandler handler)
        {
            handler.GetCameraPrice();
        }

        /// <summary>
        ///     Toggles the staff pick.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ToggleStaffPick(GameClientMessageHandler handler)
        {
            handler.ToggleStaffPick();
        }

        /// <summary>
        ///     Gets the hotel view hall of fame.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetHotelViewHallOfFame(GameClientMessageHandler handler)
        {
            handler.GetHotelViewHallOfFame();
        }

        /// <summary>
        ///     Submits the room to competition.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SubmitRoomToCompetition(GameClientMessageHandler handler)
        {
            handler.SubmitRoomToCompetition();
        }

        /// <summary>
        ///     Enters the room queue.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void EnterRoomQueue(GameClientMessageHandler handler)
        {
            handler.EnterRoomQueue();
        }

        /// <summary>
        ///     Gets the camera request.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetCameraRequest(GameClientMessageHandler handler)
        {
            handler.GetCameraRequest();
        }

        /// <summary>
        ///     Votes for room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void VoteForRoom(GameClientMessageHandler handler)
        {
            handler.VoteForRoom();
        }

        /// <summary>
        ///     Updates the forum settings.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateForumSettings(GameClientMessageHandler handler)
        {
            handler.UpdateForumSettings();
        }

        /// <summary>
        ///     Friends the request list load.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void FriendRequestListLoad(GameClientMessageHandler handler)
        {
            handler.FriendRequestListLoad();
        }

        /// <summary>
        ///     Sets the room camera preferences.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SetRoomCameraPreferences(GameClientMessageHandler handler)
        {
            handler.SetRoomCameraPreferences();
        }

        /// <summary>
        ///     Deletes the group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DeleteGroup(GameClientMessageHandler handler)
        {
            handler.DeleteGroup();
        }

        /// <summary>
        ///     Delegate GetProperty
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal delegate void GetProperty(GameClientMessageHandler handler);
    }
}