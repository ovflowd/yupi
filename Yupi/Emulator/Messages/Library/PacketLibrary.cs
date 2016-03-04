/**
     Because i love chocolat...      
    88 88  
    "" 88  
       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8' 88 
   d8'  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using Yupi.Emulator.Messages.Handlers;

namespace Yupi.Emulator.Messages.Library
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
        internal static void InitCrypto(MessageHandler handler) => handler.InitCrypto();

        /// <summary>
        ///     Secrets the key.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SecretKey(MessageHandler handler) => handler.SecretKey();

        /// <summary>
        ///     Machines the identifier.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MachineId(MessageHandler handler) => handler.MachineId();

        /// <summary>
        ///     Guides the message.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GuideMessage(MessageHandler handler) => handler.GuideMessage();

        /// <summary>
        ///     Sets the chat preferrence.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SetChatPreferrence(MessageHandler handler) => handler.SetChatPreferrence();

        /// <summary>
        ///     Gets the helper tool.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetHelperTool(MessageHandler handler) => handler.GetHelperTool();

        /// <summary>
        ///     Gets the guide detached.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetGuideDetached(MessageHandler handler) => handler.GetGuideDetached();

        /// <summary>
        ///     Logins the with ticket.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void LoginWithTicket(MessageHandler handler) => handler.LoginWithTicket();

        /// <summary>
        ///     Invites the guide.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void InviteGuide(MessageHandler handler) => handler.InviteGuide();

        /// <summary>
        ///     Visits the room guide.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void VisitRoomGuide(MessageHandler handler) => handler.VisitRoomGuide();

        /// <summary>
        ///     Guides the end session.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GuideEndSession(MessageHandler handler) => handler.GuideEndSession();

        /// <summary>
        ///     Cancels the call guide.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CancelCallGuide(MessageHandler handler) => handler.CancelCallGuide();

        /// <summary>
        ///     Informations the retrieve.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void InfoRetrieve(MessageHandler handler) => handler.InfoRetrieve();

        /// <summary>
        ///     Chats the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Chat(MessageHandler handler) => handler.Chat();

        /// <summary>
        ///     Shouts the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Shout(MessageHandler handler) => handler.Shout();

        /// <summary>
        ///     Requests the floor plan used coords.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RequestFloorPlanUsedCoords(MessageHandler handler) => handler.RequestFloorPlanUsedCoords();

        /// <summary>
        ///     Requests the floor plan door.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RequestFloorPlanDoor(MessageHandler handler) => handler.RequestFloorPlanDoor();

        /// <summary>
        ///     Opens the bully reporting.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OpenBullyReporting(MessageHandler handler) => handler.OpenBullyReporting();

        /// <summary>
        ///     Sends the bully report.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SendBullyReport(MessageHandler handler) => handler.SendBullyReport();

        /// <summary>
        ///     Loads the club gifts.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void LoadClubGifts(MessageHandler handler) => handler.LoadClubGifts();

        /// <summary>
        ///     Saves the heightmap.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveHeightmap(MessageHandler handler) => handler.SaveHeightmap();

        /// <summary>
        ///     Accepts the poll.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AcceptPoll(MessageHandler handler) => handler.AcceptPoll();

        /// <summary>
        ///     Refuses the poll.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RefusePoll(MessageHandler handler) => handler.RefusePoll();

        /// <summary>
        ///     Answers the poll question.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AnswerPollQuestion(MessageHandler handler) => handler.AnswerPollQuestion();

        /// <summary>
        ///     Retrieves the song identifier.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RetrieveSongId(MessageHandler handler) => handler.RetrieveSongId();

        /// <summary>
        ///     Tiles the height of the stack magic set.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TileStackMagicSetHeight(MessageHandler handler) => handler.TileStackMagicSetHeight();

        /// <summary>
        ///     Enables the inventory effect.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void EnableInventoryEffect(MessageHandler handler) => handler.EnableEffect();

        /// <summary>
        ///     Promotes the room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PromoteRoom(MessageHandler handler) => handler.PromoteRoom();

        /// <summary>
        ///     Gets the promotionable rooms.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPromotionableRooms(MessageHandler handler) => handler.GetPromotionableRooms();


        /// <summary>
        ///     Gets the room filter.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomFilter(MessageHandler handler) => handler.GetRoomFilter();


        /// <summary>
        ///     Alters the room filter.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AlterRoomFilter(MessageHandler handler) => handler.AlterRoomFilter();

        /// <summary>
        ///     Gets the tv player.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetTvPlayer(MessageHandler handler) => handler.GetTvPlayer();

        /// <summary>
        ///     Chooses the tv player video.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ChooseTvPlayerVideo(MessageHandler handler) => handler.ChooseTvPlayerVideo();

        /// <summary>
        ///     Gets the tv playlist.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetTvPlaylist(MessageHandler handler) => handler.GetTvPlaylist();

        /// <summary>
        ///     Places the bot.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PlaceBot(MessageHandler handler) => handler.PlaceBot();

        /// <summary>
        ///     Picks up bot.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PickUpBot(MessageHandler handler) => handler.PickUpBot();

        /// <summary>
        ///     Gets the talents track.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetTalentsTrack(MessageHandler handler) => handler.Talents();

        /// <summary>
        ///     Prepares the campaing.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PrepareCampaing(MessageHandler handler) => handler.PrepareCampaing();

        /// <summary>
        ///     Pongs the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Pong(MessageHandler handler) => handler.Pong();

        /// <summary>
        ///     Disconnects the event.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DisconnectEvent(MessageHandler handler) => handler.DisconnectEvent();

        /// <summary>
        ///     Latencies the test.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void LatencyTest(MessageHandler handler) => handler.LatencyTest();

        /// <summary>
        ///     Receptions the view.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ReceptionView(MessageHandler handler) => handler.ReceptionView();

        /// <summary>
        ///     Called when [confirmation event].
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OnlineConfirmationEvent(MessageHandler handler) => handler.OnlineConfirmationEvent();

        /// <summary>
        ///     Retrives the citizen ship status.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RetriveCitizenShipStatus(MessageHandler handler) => handler.RetriveCitizenShipStatus();

        /// <summary>
        ///     Refreshes the promo event.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RefreshPromoEvent(MessageHandler handler) => handler.RefreshPromoEvent();

        /// <summary>
        ///     Widgets the container.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void WidgetContainer(MessageHandler handler) => handler.WidgetContainer();

        /// <summary>
        ///     Landings the community goal.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void LandingCommunityGoal(MessageHandler handler) => handler.LandingCommunityGoal();

        /// <summary>
        ///     Removes the handitem.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveHanditem(MessageHandler handler) => handler.RemoveHanditem();

        /// <summary>
        ///     Redeems the voucher.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RedeemVoucher(MessageHandler handler) => handler.RedeemVoucher();

        /// <summary>
        ///     Gives the handitem.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GiveHanditem(MessageHandler handler) => handler.GiveHanditem();

        /// <summary>
        ///     Initializes the help tool.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void InitHelpTool(MessageHandler handler) => handler.InitHelpTool();

        /// <summary>
        ///     Submits the help ticket.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SubmitHelpTicket(MessageHandler handler) => handler.SubmitHelpTicket();

        /// <summary>
        ///     Deletes the pending CFH.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DeletePendingCfh(MessageHandler handler) => handler.DeletePendingCfh();

        /// <summary>
        ///     Mods the get user information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModGetUserInfo(MessageHandler handler) => handler.ModGetUserInfo();

        /// <summary>
        ///     Mods the get user chatlog.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModGetUserChatlog(MessageHandler handler) => handler.ModGetUserChatlog();

        /// <summary>
        ///     Messages from a guy.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MessageFromAGuy(MessageHandler handler) => handler.MessageFromAGuy();

        /// <summary>
        ///     Mods the get room chatlog.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModGetRoomChatlog(MessageHandler handler) => handler.ModGetRoomChatlog();

        /// <summary>
        ///     Mods the get room tool.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModGetRoomTool(MessageHandler handler) => handler.ModGetRoomTool();

        /// <summary>
        ///     Mods the pick ticket.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModPickTicket(MessageHandler handler) => handler.ModPickTicket();

        /// <summary>
        ///     Mods the release ticket.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModReleaseTicket(MessageHandler handler) => handler.ModReleaseTicket();

        /// <summary>
        ///     Mods the close ticket.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModCloseTicket(MessageHandler handler) => handler.ModCloseTicket();

        /// <summary>
        ///     Mods the get ticket chatlog.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModGetTicketChatlog(MessageHandler handler) => handler.ModGetTicketChatlog();

        /// <summary>
        ///     Mods the get room visits.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModGetRoomVisits(MessageHandler handler) => handler.ModGetRoomVisits();

        /// <summary>
        ///     Mods the send room alert.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModSendRoomAlert(MessageHandler handler) => handler.ModSendRoomAlert();

        /// <summary>
        ///     Mods the perform room action.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModPerformRoomAction(MessageHandler handler) => handler.ModPerformRoomAction();

        /// <summary>
        ///     Mods the send user caution.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModSendUserCaution(MessageHandler handler) => handler.ModSendUserCaution();

        /// <summary>
        ///     Mods the send user message.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModSendUserMessage(MessageHandler handler) => handler.ModSendUserMessage();

        /// <summary>
        ///     Mods the kick user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModKickUser(MessageHandler handler) => handler.ModKickUser();

        /// <summary>
        ///     Mods the mute user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModMuteUser(MessageHandler handler) => handler.ModMuteUser();

        /// <summary>
        ///     Mods the lock trade.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModLockTrade(MessageHandler handler) => handler.ModLockTrade();

        /// <summary>
        ///     Mods the ban user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ModBanUser(MessageHandler handler) => handler.ModBanUser();

        /// <summary>
        ///     Friendses the list update.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void FriendsListUpdate(MessageHandler handler) => handler.FriendsListUpdate();

        /// <summary>
        ///     Removes the buddy.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveBuddy(MessageHandler handler) => handler.RemoveBuddy();

        /// <summary>
        ///     Searches the habbo.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SearchHabbo(MessageHandler handler) => handler.SearchHabbo();

        /// <summary>
        ///     Accepts the request.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AcceptRequest(MessageHandler handler) => handler.AcceptRequest();

        /// <summary>
        ///     Declines the request.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DeclineRequest(MessageHandler handler) => handler.DeclineRequest();

        /// <summary>
        ///     Requests the buddy.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RequestBuddy(MessageHandler handler) => handler.RequestBuddy();

        /// <summary>
        ///     Sends the instant messenger.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SendInstantMessenger(MessageHandler handler) => handler.SendInstantMessenger();

        /// <summary>
        ///     Follows the buddy.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void FollowBuddy(MessageHandler handler) => handler.FollowBuddy();

        /// <summary>
        ///     Sends the instant invite.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SendInstantInvite(MessageHandler handler) => handler.SendInstantInvite();

        /// <summary>
        ///     Adds the favorite.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AddFavorite(MessageHandler handler) => handler.AddFavorite();

        /// <summary>
        ///     Removes the favorite.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveFavorite(MessageHandler handler) => handler.RemoveFavorite();

        /// <summary>
        ///     Saves the branding.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveBranding(MessageHandler handler) => handler.SaveBranding();

        /// <summary>
        ///     Gets the room information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomInfo(MessageHandler handler) => handler.GetRoomInfo();

        /// <summary>
        ///     News the navigator flat cats.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void NewNavigatorFlatCats(MessageHandler handler) => handler.NewNavigatorFlatCats();

        /// <summary>
        ///     Opens the flat.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OpenFlat(MessageHandler handler) => handler.OpenFlat();

        /// <summary>
        ///     Gets the voume.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetVoume(MessageHandler handler) => handler.GetVoume();

        /// <summary>
        ///     Saves the volume.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveVolume(MessageHandler handler) => handler.SaveVolume();

        /// <summary>
        ///     Gets the pub.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPub(MessageHandler handler) => handler.GetPub();

        /// <summary>
        ///     Opens the pub.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OpenPub(MessageHandler handler) => handler.OpenPub();

        /// <summary>
        ///     Gets the inventory.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetInventory(MessageHandler handler) => handler.GetInventory();

        /// <summary>
        ///     Gets the room data2.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomData2(MessageHandler handler) => handler.GetRoomData2();

        /// <summary>
        ///     Gets the room data3.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomData3(MessageHandler handler) => handler.GetRoomData3();

        /// <summary>
        ///     Called when [room user add].
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OnRoomUserAdd(MessageHandler handler) => handler.OnRoomUserAdd();

        /// <summary>
        ///     Reqs the load room for user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ReqLoadRoomForUser(MessageHandler handler) => handler.ReqLoadRoomForUser();

        /// <summary>
        ///     Enters the on room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void EnterOnRoom(MessageHandler handler) => handler.EnterOnRoom();

        /// <summary>
        ///     Clears the room loading.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ClearRoomLoading(MessageHandler handler) => handler.ClearRoomLoading();

        /// <summary>
        ///     Moves the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Move(MessageHandler handler) => handler.Move();

        /// <summary>
        ///     Determines whether this instance [can create room] the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CanCreateRoom(MessageHandler handler) => handler.CanCreateRoom();

        /// <summary>
        ///     Creates the room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CreateRoom(MessageHandler handler) => handler.CreateRoom();

        /// <summary>
        ///     Gets the room information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomInformation(MessageHandler handler) => handler.GetRoomInformation();

        /// <summary>
        ///     Gets the room edit data.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomEditData(MessageHandler handler) => handler.GetRoomEditData();

        /// <summary>
        ///     Saves the room data.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveRoomData(MessageHandler handler) => handler.SaveRoomData();

        /// <summary>
        ///     Gives the rights.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GiveRights(MessageHandler handler) => handler.GiveRights();

        /// <summary>
        ///     Takes the rights.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TakeRights(MessageHandler handler) => handler.TakeRights();

        /// <summary>
        ///     Takes all rights.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TakeAllRights(MessageHandler handler) => handler.TakeAllRights();

        /// <summary>
        ///     Habboes the camera.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void HabboCamera(MessageHandler handler) => handler.HabboCamera();

        /// <summary>
        ///     Kicks the user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void KickUser(MessageHandler handler) => handler.KickUser();

        /// <summary>
        ///     Bans the user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void BanUser(MessageHandler handler) => handler.BanUser();

        /// <summary>
        ///     Sets the home room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SetHomeRoom(MessageHandler handler) => handler.SetHomeRoom();

        /// <summary>
        ///     Deletes the room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DeleteRoom(MessageHandler handler) => handler.DeleteRoom();

        /// <summary>
        ///     Looks at.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void LookAt(MessageHandler handler) => handler.LookAt();

        /// <summary>
        ///     Starts the typing.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void StartTyping(MessageHandler handler) => handler.StartTyping();

        /// <summary>
        ///     Stops the typing.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void StopTyping(MessageHandler handler) => handler.StopTyping();

        /// <summary>
        ///     Ignores the user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void IgnoreUser(MessageHandler handler) => handler.IgnoreUser();

        /// <summary>
        ///     Unignores the user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UnignoreUser(MessageHandler handler) => handler.UnignoreUser();

        /// <summary>
        ///     Determines whether this instance [can create room event] the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CanCreateRoomEvent(MessageHandler handler) => handler.CanCreateRoomEvent();

        /// <summary>
        ///     Signs the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Sign(MessageHandler handler) => handler.Sign();

        /// <summary>
        ///     Gets the user tags.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetUserTags(MessageHandler handler) => handler.GetUserTags();

        /// <summary>
        ///     Gets the user badges.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetUserBadges(MessageHandler handler) => handler.GetUserBadges();

        /// <summary>
        ///     Rates the room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RateRoom(MessageHandler handler) => handler.RateRoom();

        /// <summary>
        ///     Dances the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Dance(MessageHandler handler) => handler.Dance();

        /// <summary>
        ///     Answers the doorbell.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AnswerDoorbell(MessageHandler handler) => handler.AnswerDoorbell();

        /// <summary>
        ///     Applies the room effect.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ApplyRoomEffect(MessageHandler handler) => handler.ApplyRoomEffect();

        /// <summary>
        ///     Places the post it.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PlacePostIt(MessageHandler handler) => handler.PlacePostIt();

        /// <summary>
        ///     Places the item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PlaceItem(MessageHandler handler) => 

handler.PlaceItem();


        /// <summary>
        ///     Takes the item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TakeItem(MessageHandler handler) => 

handler.TakeItem();


        /// <summary>
        ///     Moves the item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MoveItem(MessageHandler handler) => 

handler.MoveItem();


        /// <summary>
        ///     Moves the wall item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MoveWallItem(MessageHandler handler) => 

handler.MoveWallItem();


        /// <summary>
        ///     Triggers the item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TriggerItem(MessageHandler handler) => 

handler.TriggerItem();


        /// <summary>
        ///     Triggers the item dice special.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TriggerItemDiceSpecial(MessageHandler handler) => 

handler.TriggerItemDiceSpecial();


        /// <summary>
        ///     Opens the postit.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OpenPostit(MessageHandler handler) => 

handler.OpenPostit();


        /// <summary>
        ///     Saves the postit.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SavePostit(MessageHandler handler) => 

handler.SavePostit();


        /// <summary>
        ///     Deletes the postit.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DeletePostit(MessageHandler handler) => 

handler.DeletePostit();


        /// <summary>
        ///     Opens the present.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OpenPresent(MessageHandler handler) => 

handler.OpenGift();


        /// <summary>
        ///     Gets the moodlight.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetMoodlight(MessageHandler handler) => 

handler.GetMoodlight();


        /// <summary>
        ///     Updates the moodlight.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateMoodlight(MessageHandler handler) => 

handler.UpdateMoodlight();


        /// <summary>
        ///     Switches the moodlight status.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SwitchMoodlightStatus(MessageHandler handler) => 

handler.SwitchMoodlightStatus();


        /// <summary>
        ///     Initializes the trade.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void InitTrade(MessageHandler handler) => 

handler.InitTrade();


        /// <summary>
        ///     Offers the trade item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void OfferTradeItem(MessageHandler handler) => 

handler.OfferTradeItem();


        /// <summary>
        ///     Takes the back trade item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void TakeBackTradeItem(MessageHandler handler) => 

handler.TakeBackTradeItem();


        /// <summary>
        ///     Stops the trade.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void StopTrade(MessageHandler handler) => 

handler.StopTrade();


        /// <summary>
        ///     Accepts the trade.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AcceptTrade(MessageHandler handler) => 

handler.AcceptTrade();


        /// <summary>
        ///     Unaccepts the trade.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UnacceptTrade(MessageHandler handler) => 

handler.UnacceptTrade();


        /// <summary>
        ///     Completes the trade.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CompleteTrade(MessageHandler handler) => 

handler.CompleteTrade();


        /// <summary>
        ///     Gives the respect.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GiveRespect(MessageHandler handler) => 

handler.GiveRespect();


        /// <summary>
        ///     Applies the effect.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ApplyEffect(MessageHandler handler) => 

handler.ApplyEffect();


        /// <summary>
        ///     Enables the effect.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void EnableEffect(MessageHandler handler) => 

handler.EnableEffect();


        /// <summary>
        ///     Recycles the items.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RecycleItems(MessageHandler handler) => 

handler.RecycleItems();


        /// <summary>
        ///     Redeems the exchange furni.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RedeemExchangeFurni(MessageHandler handler) => 

handler.RedeemExchangeFurni();


        /// <summary>
        ///     Kicks the bot.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void KickBot(MessageHandler handler) => 

handler.KickBot();


        /// <summary>
        ///     Places the pet.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PlacePet(MessageHandler handler) => 

handler.PlacePet();


        /// <summary>
        ///     Gets the pet information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPetInfo(MessageHandler handler) => 

handler.GetPetInfo();


        /// <summary>
        ///     Picks up pet.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PickUpPet(MessageHandler handler) => 

handler.PickUpPet();


        /// <summary>
        ///     Composts the monsterplant.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CompostMonsterplant(MessageHandler handler) => 

handler.CompostMonsterplant();


        /// <summary>
        ///     Moves the pet.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MovePet(MessageHandler handler) => 

handler.MovePet();


        /// <summary>
        ///     Respects the pet.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RespectPet(MessageHandler handler) => 

handler.RespectPet();


        /// <summary>
        ///     Adds the saddle.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AddSaddle(MessageHandler handler) => 

handler.AddSaddle();


        /// <summary>
        ///     Removes the saddle.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveSaddle(MessageHandler handler) => 

handler.RemoveSaddle();


        /// <summary>
        ///     Rides the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Ride(MessageHandler handler) => 

handler.Ride();


        /// <summary>
        ///     Unrides the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Unride(MessageHandler handler) => 

handler.Unride();


        /// <summary>
        ///     Saves the wired.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveWired(MessageHandler handler) => 

handler.SaveWired();


        /// <summary>
        ///     Saves the wired condition.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveWiredCondition(MessageHandler handler) => 

handler.SaveWiredCondition();


        /// <summary>
        ///     Gets the music data.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetMusicData(MessageHandler handler) => 

handler.GetMusicData();


        /// <summary>
        ///     Adds the playlist item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AddPlaylistItem(MessageHandler handler) => 

handler.AddPlaylistItem();


        /// <summary>
        ///     Removes the playlist item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemovePlaylistItem(MessageHandler handler) => 

handler.RemovePlaylistItem();


        /// <summary>
        ///     Gets the disks.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetDisks(MessageHandler handler) => 

handler.GetDisks();


        /// <summary>
        ///     Gets the playlists.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPlaylists(MessageHandler handler) => 

handler.GetPlaylists();


        /// <summary>
        ///     Gets the user information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetUserInfo(MessageHandler handler) => 

handler.GetUserInfo();


        /// <summary>
        ///     Loads the profile.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void LoadProfile(MessageHandler handler) => 

handler.LoadProfile();


        /// <summary>
        ///     Gets the balance.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetBalance(MessageHandler handler) => 

handler.GetBalance();


        /// <summary>
        ///     Gets the subscription data.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetSubscriptionData(MessageHandler handler) => 

handler.GetSubscriptionData();


        /// <summary>
        ///     Gets the badges.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetBadges(MessageHandler handler) => 

handler.GetBadges();


        /// <summary>
        ///     Updates the badges.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateBadges(MessageHandler handler) => 

handler.UpdateBadges();


        /// <summary>
        ///     Gets the achievements.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetAchievements(MessageHandler handler) => 

handler.GetAchievements();


        /// <summary>
        ///     Changes the look.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ChangeLook(MessageHandler handler) => 

handler.ChangeLook();


        /// <summary>
        ///     Changes the motto.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ChangeMotto(MessageHandler handler) => 

handler.ChangeMotto();


        /// <summary>
        ///     Gets the wardrobe.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetWardrobe(MessageHandler handler) => 

handler.GetWardrobe();


        /// <summary>
        ///     Allows all ride.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AllowAllRide(MessageHandler handler) => 

handler.AllowAllRide();


        /// <summary>
        ///     Saves the wardrobe.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveWardrobe(MessageHandler handler) => 

handler.SaveWardrobe();


        /// <summary>
        ///     Gets the pets inventory.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPetsInventory(MessageHandler handler) => 

handler.GetPetsInventory();

        /// <summary>
        ///     Gets the group badges.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetGroupBadges(MessageHandler handler) => 

handler.GetGroupBadges();


        /// <summary>
        ///     Gets the bot inv.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetBotInv(MessageHandler handler) => 

handler.GetBotInv();


        /// <summary>
        ///     Saves the room bg.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveRoomBg(MessageHandler handler) => 

handler.SaveRoomBg();


        /// <summary>
        ///     Goes the room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GoRoom(MessageHandler handler) => 

handler.GoRoom();


        /// <summary>
        ///     Sits the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Sit(MessageHandler handler) => 

handler.Sit();


        /// <summary>
        ///     Saves the mannequin.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveMannequin(MessageHandler handler) => 

handler.SaveMannequin();


        /// <summary>
        ///     Saves the mannequin2.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveMannequin2(MessageHandler handler) => 

handler.SaveMannequin2();


        /// <summary>
        ///     Serializes the group purchase page.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SerializeGroupPurchasePage(MessageHandler handler) => 

handler.SerializeGroupPurchasePage();


        /// <summary>
        ///     Serializes the group purchase parts.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SerializeGroupPurchaseParts(MessageHandler handler) => 

handler.SerializeGroupPurchaseParts();


        /// <summary>
        ///     Purchases the group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PurchaseGroup(MessageHandler handler) => 

handler.PurchaseGroup();


        /// <summary>
        ///     Serializes the group information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SerializeGroupInfo(MessageHandler handler) => 

handler.SerializeGroupInfo();


        /// <summary>
        ///     Serializes the group members.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SerializeGroupMembers(MessageHandler handler) => 

handler.SerializeGroupMembers();


        /// <summary>
        ///     Makes the group admin.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MakeGroupAdmin(MessageHandler handler) => 

handler.MakeGroupAdmin();


        /// <summary>
        ///     Removes the group admin.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveGroupAdmin(MessageHandler handler) => 

handler.RemoveGroupAdmin();


        /// <summary>
        ///     Accepts the membership.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AcceptMembership(MessageHandler handler) => 

handler.AcceptMembership();


        /// <summary>
        ///     Declines the membership.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DeclineMembership(MessageHandler handler) => 

handler.DeclineMembership();


        /// <summary>
        ///     Joins the group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void JoinGroup(MessageHandler handler) => 

handler.JoinGroup();


        /// <summary>
        ///     Makes the fav.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MakeFav(MessageHandler handler) => 

handler.MakeFav();


        /// <summary>
        ///     Removes the fav.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveFav(MessageHandler handler) => 

handler.RemoveFav();


        /// <summary>
        ///     Reads the forum thread.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ReadForumThread(MessageHandler handler) => 

handler.ReadForumThread();


        /// <summary>
        ///     Publishes the forum thread.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PublishForumThread(MessageHandler handler) => 

handler.PublishForumThread();


        /// <summary>
        ///     Updates the forum thread.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateForumThread(MessageHandler handler) => 

handler.UpdateForumThread();


        /// <summary>
        ///     Alters the state of the forum thread.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AlterForumThreadState(MessageHandler handler) => 

handler.AlterForumThreadState();


        /// <summary>
        ///     Gets the forum thread root.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetForumThreadRoot(MessageHandler handler) => 

handler.GetGroupForumThreadRoot();


        /// <summary>
        ///     Gets the group forum data.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetGroupForumData(MessageHandler handler) => 

handler.GetGroupForumData();


        /// <summary>
        ///     Gets the group forums.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetGroupForums(MessageHandler handler) => 

handler.GetGroupForums();


        /// <summary>
        ///     Manages the group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ManageGroup(MessageHandler handler) => 

handler.ManageGroup();


        /// <summary>
        ///     Updates the name of the group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateGroupName(MessageHandler handler) => 

handler.UpdateGroupName();


        /// <summary>
        ///     Updates the group badge.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateGroupBadge(MessageHandler handler) => 

handler.UpdateGroupBadge();


        /// <summary>
        ///     Updates the group colours.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateGroupColours(MessageHandler handler) => 

handler.UpdateGroupColours();


        /// <summary>
        ///     Updates the group settings.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateGroupSettings(MessageHandler handler) => 

handler.UpdateGroupSettings();


        /// <summary>
        ///     Serializes the group furni page.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SerializeGroupFurniPage(MessageHandler handler) => 

handler.SerializeGroupFurniPage();


        /// <summary>
        ///     Ejects the furni.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void EjectFurni(MessageHandler handler) => 

handler.EjectFurni();


        /// <summary>
        ///     Mutes the user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MuteUser(MessageHandler handler) => 

handler.MuteUser();


        /// <summary>
        ///     Checks the name.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CheckName(MessageHandler handler) => 

handler.CheckName();


        /// <summary>
        ///     Changes the name.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ChangeName(MessageHandler handler) => 

handler.ChangeName();


        /// <summary>
        ///     Gets the trainer panel.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetTrainerPanel(MessageHandler handler) => 

handler.GetTrainerPanel();


        /// <summary>
        ///     Updates the event information.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateEventInfo(MessageHandler handler) => 

handler.UpdateEventInfo();


        /// <summary>
        ///     Gets the room banned users.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRoomBannedUsers(MessageHandler handler) => 

handler.GetRoomBannedUsers();


        /// <summary>
        ///     Userses the with rights.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UsersWithRights(MessageHandler handler) => 

handler.UsersWithRights();


        /// <summary>
        ///     Unbans the user.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UnbanUser(MessageHandler handler) => 

handler.UnbanUser();


        /// <summary>
        ///     Manages the bot actions.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ManageBotActions(MessageHandler handler) => 

handler.ManageBotActions();


        /// <summary>
        ///     Handles the bot speech list.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void HandleBotSpeechList(MessageHandler handler) => 

handler.HandleBotSpeechList();


        /// <summary>
        ///     Gets the relationships.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetRelationships(MessageHandler handler) => 

handler.GetRelationships();


        /// <summary>
        ///     Sets the relationship.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SetRelationship(MessageHandler handler) => 

handler.SetRelationship();


        /// <summary>
        ///     Automatics the room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AutoRoom(MessageHandler handler) => 

handler.AutoRoom();


        /// <summary>
        ///     Mutes all.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void MuteAll(MessageHandler handler) => 

handler.MuteAll();


        /// <summary>
        ///     Completes the saftey quiz.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CompleteSafteyQuiz(MessageHandler handler) => 

handler.CompleteSafteyQuiz();


        /// <summary>
        ///     Removes the favourite room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RemoveFavouriteRoom(MessageHandler handler) => 

handler.RemoveFavouriteRoom();


        /// <summary>
        ///     Rooms the user action.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RoomUserAction(MessageHandler handler) => 

handler.RoomUserAction();


        /// <summary>
        ///     Saves the football outfit.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveFootballOutfit(MessageHandler handler) => 

handler.SaveFootballOutfit();


        /// <summary>
        ///     Confirms the love lock.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ConfirmLoveLock(MessageHandler handler) => 

handler.ConfirmLoveLock();


        /// <summary>
        ///     Builderses the club update furni count.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void BuildersClubUpdateFurniCount(MessageHandler handler) => 

handler.BuildersClubUpdateFurniCount();


        /// <summary>
        ///     Places the builders furniture.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PlaceBuildersFurniture(MessageHandler handler) => 

handler.PlaceBuildersFurniture();


        /// <summary>
        ///     Whispers the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void Whisper(MessageHandler handler) => 

handler.Whisper();


        /// <summary>
        ///     Catalogues the index.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CatalogueIndex(MessageHandler handler) => 

handler.CatalogueIndex();


        /// <summary>
        ///     Catalogues the page.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CataloguePage(MessageHandler handler) => 

handler.CataloguePage();


        /// <summary>
        ///     Catalogues the club page.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CatalogueClubPage(MessageHandler handler) => 

handler.CatalogueClubPage();


        /// <summary>
        ///     Catalogues the offers configuration.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CatalogueOffersConfig(MessageHandler handler) => 

handler.CatalogueOffersConfig();


        /// <summary>
        ///     Catalogues the single offer.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CatalogueSingleOffer(MessageHandler handler) => 

handler.CatalogueSingleOffer();


        /// <summary>
        ///     Checks the name of the pet.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void CheckPetName(MessageHandler handler) => 

handler.CheckPetName();


        /// <summary>
        ///     Purchases the item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PurchaseItem(MessageHandler handler) => 

handler.PurchaseItem();


        /// <summary>
        ///     Purchases the gift.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PurchaseGift(MessageHandler handler) => 

handler.PurchaseGift();


        /// <summary>
        ///     Gets the pet breeds.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetPetBreeds(MessageHandler handler) => 

handler.GetPetBreeds();


        /// <summary>
        ///     Reloads the ecotron.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ReloadEcotron(MessageHandler handler) => 

handler.ReloadEcotron();


        /// <summary>
        ///     Gifts the wrapping configuration.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GiftWrappingConfig(MessageHandler handler) => 

handler.GiftWrappingConfig();


        /// <summary>
        ///     Recyclers the rewards.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RecyclerRewards(MessageHandler handler) => 

handler.RecyclerRewards();


        /// <summary>
        ///     Requests the leave group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void RequestLeaveGroup(MessageHandler handler) => 

handler.RequestLeaveGroup();


        /// <summary>
        ///     Confirms the leave group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ConfirmLeaveGroup(MessageHandler handler) => 

handler.ConfirmLeaveGroup();


        /// <summary>
        ///     News the navigator.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void NewNavigator(MessageHandler handler) => 

handler.NewNavigator();


        /// <summary>
        ///     Searches the new navigator.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SearchNewNavigator(MessageHandler handler) => 

handler.SearchNewNavigator();


        /// <summary>
        ///     News the navigator delete saved search.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void NewNavigatorDeleteSavedSearch(MessageHandler handler) => 

handler.NewNavigatorDeleteSavedSearch();


        /// <summary>
        ///     News the navigator resize.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void NewNavigatorResize(MessageHandler handler) => 

handler.NewNavigatorResize();


        /// <summary>
        ///     News the navigator add saved search.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void NewNavigatorAddSavedSearch(MessageHandler handler) => 

handler.NewNavigatorAddSavedSearch();


        /// <summary>
        ///     Pets the breed result.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PetBreedResult(MessageHandler handler) => 

handler.PetBreedResult();


        /// <summary>
        ///     Pets the breed cancel.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PetBreedCancel(MessageHandler handler) => 

handler.PetBreedCancel();


        /// <summary>
        ///     Games the center load game.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GameCenterLoadGame(MessageHandler handler) => 

handler.GameCenterLoadGame();


        /// <summary>
        ///     Games the center join queue.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GameCenterJoinQueue(MessageHandler handler) => 

handler.GameCenterJoinQueue();


        /// <summary>
        ///     Hotels the view countdown.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void HotelViewCountdown(MessageHandler handler) => 

handler.HotelViewCountdown();


        /// <summary>
        ///     Places the builders wall item.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PlaceBuildersWallItem(MessageHandler handler) => 

handler.PlaceBuildersWallItem();


        /// <summary>
        ///     Targeteds the offer buy.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void PurchaseTargetedOffer(MessageHandler handler) => 

handler.PurchaseTargetedOffer();


        /// <summary>
        ///     Ambassadors the alert.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void AmbassadorAlert(MessageHandler handler) => 

handler.AmbassadorAlert();


        /// <summary>
        ///     Goes the name of to room by.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GoToRoomByName(MessageHandler handler) => handler.GoToRoomByName();


        /// <summary>
        ///     Saves the room thumbnail.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SaveRoomThumbnail(MessageHandler handler) => handler.SaveRoomThumbnail();

        /// <summary>
        ///     Uses the purchasable clothing.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UsePurchasableClothing(MessageHandler handler) => handler.UsePurchasableClothing();

        /// <summary>
        ///     Gets the user look.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetUserLook(MessageHandler handler) => handler.GetUserLook();

        /// <summary>
        ///     Sets the invitations preference.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SetInvitationsPreference(MessageHandler handler) => handler.SetInvitationsPreference();

        /// <summary>
        ///     Finds the more friends.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void FindMoreFriends(MessageHandler handler) => handler.FindMoreFriends();

        /// <summary>
        ///     Hotels the view request badge.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void HotelViewRequestBadge(MessageHandler handler) => handler.HotelViewRequestBadge();

        /// <summary>
        ///     Gets the camera price.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetCameraPrice(MessageHandler handler) => handler.GetCameraPrice();

        /// <summary>
        ///     Toggles the staff pick.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void ToggleStaffPick(MessageHandler handler) => handler.ToggleStaffPick();

        /// <summary>
        ///     Gets the hotel view hall of fame.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetHotelViewHallOfFame(MessageHandler handler) => handler.GetHotelViewHallOfFame();

        /// <summary>
        ///     Submits the room to competition.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SubmitRoomToCompetition(MessageHandler handler) => handler.SubmitRoomToCompetition();

        /// <summary>
        ///     Enters the room queue.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void EnterRoomQueue(MessageHandler handler) => handler.EnterRoomQueue();

        /// <summary>
        ///     Gets the camera request.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void GetCameraRequest(MessageHandler handler) => handler.GetCameraRequest();

        /// <summary>
        ///     Votes for room.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void VoteForRoom(MessageHandler handler) => handler.VoteForRoom();

        /// <summary>
        ///     Updates the forum settings.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void UpdateForumSettings(MessageHandler handler) => handler.UpdateForumSettings();

        /// <summary>
        ///     Sets the room camera preferences.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void SetRoomCameraPreferences(MessageHandler handler) => handler.SetRoomCameraPreferences();

        /// <summary>
        ///     Deletes the group.
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal static void DeleteGroup(MessageHandler handler) => handler.DeleteGroup();

        /// <summary>
        ///     Delegate GetProperty
        /// </summary>
        /// <param name="handler">The handler.</param>
        internal delegate void GetProperty(MessageHandler handler);
    }
}