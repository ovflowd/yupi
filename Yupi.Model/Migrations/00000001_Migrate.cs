using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using FluentMigrator;

namespace
Yupi.Model.Db.Migrations
{
    [Migration (00000001)]
    internal class Migrate : Migration
    {

        public const string ConfigurationData = "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";

        public override void Up ()
        {

            Create.Table ("Achievement")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Category")
            .AsString (255).Nullable ()
            .WithColumn ("GroupName")
            .AsString (255).Nullable ()
            ;



            Create.PrimaryKey ("PK_Achievement")
                  .OnTable ("Achievement")


        .Columns ("Id");


            Create.Table ("AchievementLevel")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Level")
            .AsInt32 ().Nullable ()
            .WithColumn ("Requirement")
            .AsInt32 ().Nullable ()
            .WithColumn ("RewardActivityPoints")
            .AsInt32 ().Nullable ()
            .WithColumn ("RewardActivityPointsType")
            .AsString (255).Nullable ()
            .WithColumn ("RewardPoints")
            .AsInt32 ().Nullable ()
            .WithColumn ("AchievementRef")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_AchievementLevel")
                  .OnTable ("AchievementLevel")


        .Columns ("Id");


            Create.Table ("AvatarEffect")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Activated")
            .AsBoolean ().Nullable ()
            .WithColumn ("ActivatedAt")
            .AsDateTime ().Nullable ()
            .WithColumn ("EffectId")
            .AsInt32 ().Nullable ()
            .WithColumn ("TotalDuration")
            .AsInt32 ().Nullable ()
            .WithColumn ("Type")
            .AsInt16 ().Nullable ()
            .WithColumn ("EffectComponentUserEffectComponent_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_AvatarEffect")
                  .OnTable ("AvatarEffect")


        .Columns ("Id");


            Create.Table ("Badge")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Code")
            .AsString (255).Nullable ()
            .WithColumn ("Slot")
            .AsInt32 ().Nullable ()
            .WithColumn ("BadgesUserBadgeComponentRef")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_Badge")
                  .OnTable ("Badge")


        .Columns ("Id");


            Create.Table ("BaseInfo")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("discriminator")
            .AsString (255).NotNullable ()
            .WithColumn ("Motto")
            .AsString (255).Nullable ()
            .WithColumn ("Name")
            .AsString (255).NotNullable ()
            .WithColumn ("Gender")
            .AsFixedLengthString (255).Nullable ()
            .WithColumn ("Look")
            .AsString (255).Nullable ()
            .WithColumn ("Owner_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("AnyoneCanRide")
            .AsBoolean ().Nullable ()
            .WithColumn ("BreadingTile_X")
            .AsSingle ().Nullable ()
            .WithColumn ("BreadingTile_Y")
            .AsSingle ().Nullable ()
            .WithColumn ("BreadingTile_Z")
            .AsSingle ().Nullable ()
            .WithColumn ("Color")
            .AsString (255).Nullable ()
            .WithColumn ("CreatedAt")
            .AsDateTime ().Nullable ()
            .WithColumn ("Energy")
            .AsInt32 ().Nullable ()
            .WithColumn ("Experience")
            .AsInt32 ().Nullable ()
            .WithColumn ("Hair")
            .AsInt32 ().Nullable ()
            .WithColumn ("HairDye")
            .AsInt32 ().Nullable ()
            .WithColumn ("HaveSaddle")
            .AsBoolean ().Nullable ()
            .WithColumn ("LastHealth")
            .AsDateTime ().Nullable ()
            .WithColumn ("Nutrition")
            .AsInt32 ().Nullable ()
            .WithColumn ("PlacedInRoom")
            .AsBoolean ().Nullable ()
            .WithColumn ("Position_X")
            .AsSingle ().Nullable ()
            .WithColumn ("Position_Y")
            .AsSingle ().Nullable ()
            .WithColumn ("Position_Z")
            .AsSingle ().Nullable ()
            .WithColumn ("Race")
            .AsInt32 ().Nullable ()
            .WithColumn ("RaceId")
            .AsInt32 ().Nullable ()
            .WithColumn ("Rarity")
            .AsInt32 ().Nullable ()
            .WithColumn ("Respect")
            .AsInt32 ().Nullable ()
            .WithColumn ("Type")
            .AsString (255).Nullable ()
            .WithColumn ("WaitingForBreading")
            .AsInt32 ().Nullable ()
            .WithColumn ("Room_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("AppearOffline")
            .AsBoolean ().Nullable ()
            .WithColumn ("BobbaFiltered")
            .AsInt32 ().Nullable ()
            .WithColumn ("CreateDate")
            .AsDateTime ().Nullable ()
            .WithColumn ("Email")
            .AsString (255).Nullable ()
            .WithColumn ("HasFriendRequestsDisabled")
            .AsBoolean ().Nullable ()
            .WithColumn ("HideInRoom")
            .AsBoolean ().Nullable ()
            .WithColumn ("LastIp")
            .AsString (255).Nullable ()
            .WithColumn ("LastOnline")
            .AsDateTime ().Nullable ()
            .WithColumn ("Muted")
            .AsBoolean ().Nullable ()
            .WithColumn ("Rank")
            .AsInt32 ().Nullable ()
            .WithColumn ("SpamFloodTime")
            .AsDateTime ().Nullable ()
            .WithColumn ("SpectatorMode")
            .AsBoolean ().Nullable ()
            .WithColumn ("FavouriteGroup_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("HomeRoom_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Subscription_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("BuilderInfoBuildersExpire")
            .AsInt32 ().Nullable ()
            .WithColumn ("BuilderInfoBuildersItemsMax")
            .AsInt32 ().Nullable ()
            .WithColumn ("BuilderInfoBuildersItemsUsed")
            .AsInt32 ().Nullable ()
            .WithColumn ("EffectComponentActiveEffect_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("PreferencesChatBubbleStyle")
            .AsInt32 ().Nullable ()
            .WithColumn ("PreferencesDisableCameraFollow")
            .AsBoolean ().Nullable ()
            .WithColumn ("PreferencesIgnoreRoomInvite")
            .AsBoolean ().Nullable ()
            .WithColumn ("PreferencesNavigatorHeight")
            .AsInt32 ().Nullable ()
            .WithColumn ("PreferencesNavigatorWidth")
            .AsInt32 ().Nullable ()
            .WithColumn ("PreferencesNewnaviX")
            .AsInt32 ().Nullable ()
            .WithColumn ("PreferencesNewnaviY")
            .AsInt32 ().Nullable ()
            .WithColumn ("PreferencesPreferOldChat")
            .AsBoolean ().Nullable ()
            .WithColumn ("PreferencesVolume1")
            .AsInt32 ().Nullable ()
            .WithColumn ("PreferencesVolume2")
            .AsInt32 ().Nullable ()
            .WithColumn ("PreferencesVolume3")
            .AsInt32 ().Nullable ()
            .WithColumn ("RespectDailyCompetitionVotes")
            .AsInt32 ().Nullable ()
            .WithColumn ("RespectDailyPetRespectPoints")
            .AsInt32 ().Nullable ()
            .WithColumn ("RespectDailyRespectPoints")
            .AsInt32 ().Nullable ()
            .WithColumn ("RespectRespect")
            .AsInt32 ().Nullable ()
            .WithColumn ("WalletAchievementPoints")
            .AsInt32 ().Nullable ()
            .WithColumn ("WalletCredits")
            .AsInt32 ().Nullable ()
            .WithColumn ("UserInfo_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_BaseInfo")
                  .OnTable ("BaseInfo")


        .Columns ("Id");


            Create.Table ("BaseItem")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("discriminator")
            .AsString (255).NotNullable ()
            .WithColumn ("AllowInventoryStack")
            .AsBoolean ().Nullable ()
            .WithColumn ("AllowMarketplaceSell")
            .AsBoolean ().Nullable ()
            .WithColumn ("AllowRecycle")
            .AsBoolean ().Nullable ()
            .WithColumn ("AllowTrade")
            .AsBoolean ().Nullable ()
            .WithColumn ("DimensionX")
            .AsInt32 ().Nullable ()
            .WithColumn ("DimensionY")
            .AsInt32 ().Nullable ()
            .WithColumn ("AdUrl")
            .AsString (255).Nullable ()
            .WithColumn ("Classname")
            .AsString (255).Nullable ()
            .WithColumn ("Stackable")
            .AsBoolean ().Nullable ()
            .WithColumn ("Height")
            .AsDecimal ().Nullable ()
            .WithColumn ("Revision")
            .AsInt32 ().Nullable ()
            .WithColumn ("FurniLine")
            .AsString (255).Nullable ()
            .WithColumn ("Name_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Description_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("DefaultDir")
            .AsInt32 ().Nullable ()
            .WithColumn ("InternalPartColors")
            .AsBinary (255).Nullable ()
            .WithColumn ("Color")
            .AsInt32 ().Nullable ()
            .WithColumn ("Song_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("RoomCompetition_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_BaseItem")
                  .OnTable ("BaseItem")


        .Columns ("Id");


            Create.Table ("CatalogOffer")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("discriminator")
            .AsString (255).NotNullable ()
            .WithColumn ("ActivityPointsType")
            .AsString (255).Nullable ()
            .WithColumn ("AllowGift")
            .AsBoolean ().Nullable ()
            .WithColumn ("Badge")
            .AsString (255).Nullable ()
            .WithColumn ("ClubLevel")
            .AsString (255).Nullable ()
            .WithColumn ("CostActivityPoints")
            .AsInt32 ().Nullable ()
            .WithColumn ("CostCredits")
            .AsInt32 ().Nullable ()
            .WithColumn ("IsRentable")
            .AsBoolean ().Nullable ()
            .WithColumn ("IsVisible")
            .AsBoolean ().Nullable ()
            .WithColumn ("Name")
            .AsString (255).Nullable ()
            .WithColumn ("Description")
            .AsString (255).Nullable ()
            .WithColumn ("ExpiresAt")
            .AsDateTime ().Nullable ()
            .WithColumn ("Icon")
            .AsString (255).Nullable ()
            .WithColumn ("Image")
            .AsString (255).Nullable ()
            .WithColumn ("PurchaseLimit")
            .AsInt32 ().Nullable ()
            .WithColumn ("StateCode")
            .AsString (255).Nullable ()
            .WithColumn ("CatalogPage_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_CatalogOffer")
                  .OnTable ("CatalogOffer")


        .Columns ("Id");


            Create.Table ("CatalogPage")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Icon")
            .AsInt32 ().Nullable ()
            .WithColumn ("IsRoot")
            .AsBoolean ().Nullable ()
            .WithColumn ("MinRank")
            .AsInt32 ().Nullable ()
            .WithColumn ("Type")
            .AsInt32 ().Nullable ()
            .WithColumn ("Visible")
            .AsBoolean ().Nullable ()
            .WithColumn ("Caption_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Layout_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("SelectedOffer_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("CatalogPage_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_CatalogPage")
                  .OnTable ("CatalogPage")


        .Columns ("Id");


            Create.Table ("CatalogPageLayout")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("discriminator")
            .AsString (255).NotNullable ()
            .WithColumn ("HeaderImage")
            .AsString (255).Nullable ()
            .WithColumn ("TeaserImage")
            .AsString (255).Nullable ()
            .WithColumn ("Content_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("VoucherDescription_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("SpecialImage")
            .AsString (255).Nullable ()
            .WithColumn ("TeaserImage1")
            .AsString (255).Nullable ()
            .WithColumn ("TeaserImage2")
            .AsString (255).Nullable ()
            .WithColumn ("TeaserImage3")
            .AsString (255).Nullable ()
            .WithColumn ("HeaderDescription_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Text_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Text1_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Text2_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Text3_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Text4_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Text5_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Description_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Enscription_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("SpecialText_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_CatalogPageLayout")
                  .OnTable ("CatalogPageLayout")


        .Columns ("Id");


            Create.Table ("CatalogProduct")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("discriminator")
            .AsString (255).NotNullable ()
            .WithColumn ("Amount")
            .AsInt32 ().Nullable ()
            .WithColumn ("Item_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("LimitedItemsLeft")
            .AsInt32 ().Nullable ()
            .WithColumn ("LimitedSeriesSize")
            .AsInt32 ().Nullable ()
            .WithColumn ("CatalogOffer_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_CatalogProduct")
                  .OnTable ("CatalogProduct")


        .Columns ("Id");


            Create.Table ("ChatMessage")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Bubble")
            .AsInt32 ().Nullable ()
            .WithColumn ("Message")
            .AsString (255).Nullable ()
            .WithColumn ("Timestamp")
            .AsDateTime ().Nullable ()
            .WithColumn ("Whisper")
            .AsBoolean ().Nullable ()
            .WithColumn ("User_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("RoomData_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_ChatMessage")
                  .OnTable ("ChatMessage")


        .Columns ("Id");


            Create.Table ("EcotronLevel")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            ;



            Create.PrimaryKey ("PK_EcotronLevel")
                  .OnTable ("EcotronLevel")


        .Columns ("Id");


            Create.Table ("EcotronReward")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("BaseItem_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("EcotronLevel_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_EcotronReward")
                  .OnTable ("EcotronReward")


        .Columns ("Id");


            Create.Table ("FriendRequest")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("From_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("To_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("RelationshipsRelationshipComponent_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_FriendRequest")
                  .OnTable ("FriendRequest")


        .Columns ("Id");


            Create.Table ("Group")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("AdminOnlyDeco")
            .AsInt32 ().Nullable ()
            .WithColumn ("Badge")
            .AsString (255).Nullable ()
            .WithColumn ("CreateTime")
            .AsInt32 ().Nullable ()
            .WithColumn ("Description")
            .AsString (255).Nullable ()
            .WithColumn ("Name")
            .AsString (255).Nullable ()
            .WithColumn ("State")
            .AsInt32 ().Nullable ()
            .WithColumn ("Colour1_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Colour2_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Creator_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Forum_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Room_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_Group")
                  .OnTable ("Group")


        .Columns ("Id");


            Create.Table ("GroupBackGroundColours")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Colour")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_GroupBackGroundColours")
                  .OnTable ("GroupBackGroundColours")


        .Columns ("Id");


            Create.Table ("GroupBaseColours")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Colour")
            .AsString (255).Nullable ()
            ;



            Create.PrimaryKey ("PK_GroupBaseColours")
                  .OnTable ("GroupBaseColours")


        .Columns ("Id");


            Create.Table ("GroupBases")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Value1")
            .AsString (255).Nullable ()
            .WithColumn ("Value2")
            .AsString (255).Nullable ()
            ;



            Create.PrimaryKey ("PK_GroupBases")
                  .OnTable ("GroupBases")


        .Columns ("Id");


            Create.Table ("GroupForum")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("ForumDescription")
            .AsString (255).Nullable ()
            .WithColumn ("ForumName")
            .AsString (255).Nullable ()
            .WithColumn ("ForumScore")
            .AsDouble ().Nullable ()
            .WithColumn ("WhoCanMod")
            .AsInt32 ().Nullable ()
            .WithColumn ("WhoCanPost")
            .AsInt32 ().Nullable ()
            .WithColumn ("WhoCanRead")
            .AsInt32 ().Nullable ()
            .WithColumn ("WhoCanThread")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_GroupForum")
                  .OnTable ("GroupForum")


        .Columns ("Id");


            Create.Table ("GroupForumPost")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Content")
            .AsString (255).Nullable ()
            .WithColumn ("Hidden")
            .AsBoolean ().Nullable ()
            .WithColumn ("Subject")
            .AsString (255).Nullable ()
            .WithColumn ("Timestamp")
            .AsDateTime ().Nullable ()
            .WithColumn ("HiddenBy_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Poster_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("GroupForumThread_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_GroupForumPost")
                  .OnTable ("GroupForumPost")


        .Columns ("Id");


            Create.Table ("GroupForumThread")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("CreatedAt")
            .AsDateTime ().Nullable ()
            .WithColumn ("Hidden")
            .AsBoolean ().Nullable ()
            .WithColumn ("Locked")
            .AsBoolean ().Nullable ()
            .WithColumn ("Pinned")
            .AsBoolean ().Nullable ()
            .WithColumn ("Subject")
            .AsString (255).Nullable ()
            .WithColumn ("Creator_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("HiddenBy_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("GroupForum_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_GroupForumThread")
                  .OnTable ("GroupForumThread")


        .Columns ("Id");


            Create.Table ("GroupSymbolColours")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Colour")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_GroupSymbolColours")
                  .OnTable ("GroupSymbolColours")


        .Columns ("Id");


            Create.Table ("GroupSymbols")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Value1")
            .AsString (255).Nullable ()
            .WithColumn ("Value2")
            .AsString (255).Nullable ()
            ;



            Create.PrimaryKey ("PK_GroupSymbols")
                  .OnTable ("GroupSymbols")


        .Columns ("Id");


            Create.Table ("HallOfFameElement")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Competition")
            .AsString (255).Nullable ()
            .WithColumn ("Score")
            .AsInt32 ().Nullable ()
            .WithColumn ("User_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_HallOfFameElement")
                  .OnTable ("HallOfFameElement")


        .Columns ("Id");


            Create.Table ("HotelLandingManager")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("FurniReward_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_HotelLandingManager")
                  .OnTable ("HotelLandingManager")


        .Columns ("Id");


            Create.Table ("HotelLandingPromos")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Body")
            .AsString (255).Nullable ()
            .WithColumn ("Button")
            .AsString (255).Nullable ()
            .WithColumn ("CreatedAt")
            .AsDateTime ().Nullable ()
            .WithColumn ("Image")
            .AsString (255).Nullable ()
            .WithColumn ("Title")
            .AsString (255).Nullable ()
            .WithColumn ("LinkUrl")
            .AsString (255).Nullable ()
            .WithColumn ("HotelLandingManager_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_HotelLandingPromos")
                  .OnTable ("HotelLandingPromos")


        .Columns ("Id");


            Create.Table ("Link")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("IsInternal")
            .AsBoolean ().Nullable ()
            .WithColumn ("Text")
            .AsString (255).Nullable ()
            .WithColumn ("URL")
            .AsString (255).Nullable ()
            .WithColumn ("ChatMessage_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_Link")
                  .OnTable ("Link")


        .Columns ("Id");


            Create.Table ("MessengerMessage")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Read")
            .AsBoolean ().Nullable ()
            .WithColumn ("Text")
            .AsString (255).Nullable ()
            .WithColumn ("Timestamp")
            .AsDateTime ().Nullable ()
            .WithColumn ("UnfilteredText")
            .AsString (255).Nullable ()
            .WithColumn ("From_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("To_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_MessengerMessage")
                  .OnTable ("MessengerMessage")


        .Columns ("Id");


            Create.Table ("Minimail")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Seen")
            .AsBoolean ().Nullable ()
            .WithColumn ("UserInfo_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_Minimail")
                  .OnTable ("Minimail")


        .Columns ("Id");


            Create.Table ("ModerationTemplate")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("AvatarBan")
            .AsBoolean ().Nullable ()
            .WithColumn ("BanHours")
            .AsInt16 ().Nullable ()
            .WithColumn ("BanMessage")
            .AsString (255).Nullable ()
            .WithColumn ("Caption")
            .AsString (255).Nullable ()
            .WithColumn ("Category")
            .AsInt16 ().Nullable ()
            .WithColumn ("CName")
            .AsString (255).Nullable ()
            .WithColumn ("Mute")
            .AsBoolean ().Nullable ()
            .WithColumn ("TradeLock")
            .AsBoolean ().Nullable ()
            .WithColumn ("WarningMessage")
            .AsString (255).Nullable ()
            ;



            Create.PrimaryKey ("PK_ModerationTemplate")
                  .OnTable ("ModerationTemplate")


        .Columns ("Id");


            Create.Table ("MoodlightData")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Enabled")
            .AsBoolean ().Nullable ()
            .WithColumn ("CurrentPreset_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_MoodlightData")
                  .OnTable ("MoodlightData")


        .Columns ("Id");


            Create.Table ("MoodlightPreset")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("BackgroundOnly")
            .AsBoolean ().Nullable ()
            .WithColumn ("ColorCode")
            .AsString (255).Nullable ()
            .WithColumn ("ColorIntensity")
            .AsInt32 ().Nullable ()
            .WithColumn ("MoodlightData_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_MoodlightPreset")
                  .OnTable ("MoodlightPreset")


        .Columns ("Id");


            Create.Table ("NavigatorCategory")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("discriminator")
            .AsString (255).NotNullable ()
            .WithColumn ("Caption")
            .AsString (255).Nullable ()
            .WithColumn ("IsImage")
            .AsBoolean ().Nullable ()
            .WithColumn ("IsOpened")
            .AsBoolean ().Nullable ()
            .WithColumn ("MinRank")
            .AsInt32 ().Nullable ()
            .WithColumn ("Visible")
            .AsBoolean ().Nullable ()
            .WithColumn ("NavigatorCategoryRef")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_NavigatorCategory")
                  .OnTable ("NavigatorCategory")


        .Columns ("Id");


            Create.Table ("Poll")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Invitation")
            .AsString (255).Nullable ()
            .WithColumn ("PollName")
            .AsString (255).Nullable ()
            .WithColumn ("Prize")
            .AsString (255).Nullable ()
            .WithColumn ("Thanks")
            .AsString (255).Nullable ()
            .WithColumn ("Room_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_Poll")
                  .OnTable ("Poll")


        .Columns ("Id");


            Create.Table ("PollQuestion")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("AnswerType")
            .AsString (255).Nullable ()
            .WithColumn ("CorrectAnswer")
            .AsString (255).Nullable ()
            .WithColumn ("Question")
            .AsString (255).Nullable ()
            .WithColumn ("Poll_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_PollQuestion")
                  .OnTable ("PollQuestion")


        .Columns ("Id");


            Create.Table ("Relationship")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Deleted")
            .AsBoolean ().Nullable ()
            .WithColumn ("Type")
            .AsInt32 ().Nullable ()
            .WithColumn ("Friend_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("RelationshipsRelationshipComponentRef")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_Relationship")
                  .OnTable ("Relationship")


        .Columns ("Id");


            Create.Table ("RoomCompetition")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Name")
            .AsString (255).Nullable ()
            ;



            Create.PrimaryKey ("PK_RoomCompetition")
                  .OnTable ("RoomCompetition")


        .Columns ("Id");


            Create.Table ("RoomCompetitionEntry")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Votes")
            .AsInt32 ().Nullable ()
            .WithColumn ("Room_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("RoomCompetition_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_RoomCompetitionEntry")
                  .OnTable ("RoomCompetitionEntry")


        .Columns ("Id");


            Create.Table ("RoomData")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("AllowPets")
            .AsBoolean ().Nullable ()
            .WithColumn ("AllowPetsEating")
            .AsBoolean ().Nullable ()
            .WithColumn ("AllowRightsOverride")
            .AsBoolean ().Nullable ()
            .WithColumn ("AllowWalkThrough")
            .AsBoolean ().Nullable ()
            .WithColumn ("CCTs")
            .AsString (255).Nullable ()
            .WithColumn ("Description")
            .AsString (255).Nullable ()
            .WithColumn ("Floor")
            .AsDecimal ().Nullable ()
            .WithColumn ("FloorThickness")
            .AsInt32 ().Nullable ()
            .WithColumn ("HideWall")
            .AsBoolean ().Nullable ()
            .WithColumn ("IsMuted")
            .AsBoolean ().Nullable ()
            .WithColumn ("LandScape")
            .AsDecimal ().Nullable ()
            .WithColumn ("Model")
            .AsInt32 ().Nullable ()
            .WithColumn ("Name")
            .AsString (255).Nullable ()
            .WithColumn ("NavigatorImage")
            .AsString (255).Nullable ()
            .WithColumn ("Password")
            .AsString (255).Nullable ()
            .WithColumn ("Score")
            .AsInt32 ().Nullable ()
            .WithColumn ("State")
            .AsInt32 ().Nullable ()
            .WithColumn ("TradeState")
            .AsInt32 ().Nullable ()
            .WithColumn ("Type")
            .AsString (255).Nullable ()
            .WithColumn ("UsersMax")
            .AsInt32 ().Nullable ()
            .WithColumn ("WallHeight")
            .AsInt32 ().Nullable ()
            .WithColumn ("WallPaper")
            .AsDecimal ().Nullable ()
            .WithColumn ("WallThickness")
            .AsInt32 ().Nullable ()
            .WithColumn ("Category_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Event_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Group_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Owner_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("ChatBalloon")
            .AsInt32 ().Nullable ()
            .WithColumn ("ChatFloodProtection")
            .AsInt32 ().Nullable ()
            .WithColumn ("ChatMaxDistance")
            .AsInt32 ().Nullable ()
            .WithColumn ("ChatSpeed")
            .AsInt32 ().Nullable ()
            .WithColumn ("ChatType")
            .AsInt32 ().Nullable ()
            .WithColumn ("ModerationSettingsWhoCanBan")
            .AsInt32 ().Nullable ()
            .WithColumn ("ModerationSettingsWhoCanKick")
            .AsInt32 ().Nullable ()
            .WithColumn ("ModerationSettingsWhoCanMute")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_RoomData")
                  .OnTable ("RoomData")


        .Columns ("Id");


            Create.Table ("RoomEvent")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Category")
            .AsInt32 ().Nullable ()
            .WithColumn ("Description")
            .AsString (255).Nullable ()
            .WithColumn ("ExpiresAt")
            .AsDateTime ().Nullable ()
            .WithColumn ("Name")
            .AsString (255).Nullable ()
            ;



            Create.PrimaryKey ("PK_RoomEvent")
                  .OnTable ("RoomEvent")


        .Columns ("Id");


            Create.Table ("RoomMute")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("ExpiresAt")
            .AsDateTime ().Nullable ()
            .WithColumn ("Entity_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("RoomData_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_RoomMute")
                  .OnTable ("RoomMute")


        .Columns ("Id");


            Create.Table ("SongData")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Artist")
            .AsString (255).Nullable ()
            .WithColumn ("CodeName")
            .AsString (255).Nullable ()
            .WithColumn ("Data")
            .AsString (255).Nullable ()
            .WithColumn ("LengthMiliseconds")
            .AsInt32 ().Nullable ()
            .WithColumn ("Name")
            .AsString (255).Nullable ()
            ;



            Create.PrimaryKey ("PK_SongData")
                  .OnTable ("SongData")


        .Columns ("Id");


            Create.Table ("Subscription")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("ActivateTime")
            .AsDateTime ().Nullable ()
            .WithColumn ("ExpireTime")
            .AsDateTime ().Nullable ()
            ;



            Create.PrimaryKey ("PK_Subscription")
                  .OnTable ("Subscription")


        .Columns ("Id");


            Create.Table ("SupportTicket")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Category")
            .AsInt32 ().Nullable ()
            .WithColumn ("CloseReason")
            .AsInt32 ().Nullable ()
            .WithColumn ("CreatedAt")
            .AsDateTime ().Nullable ()
            .WithColumn ("Message")
            .AsString (255).Nullable ()
            .WithColumn ("Score")
            .AsInt32 ().Nullable ()
            .WithColumn ("Status")
            .AsString (255).Nullable ()
            .WithColumn ("Type")
            .AsInt32 ().Nullable ()
            .WithColumn ("ReportedUser_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Room_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Sender_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Staff_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_SupportTicket")
                  .OnTable ("SupportTicket")


        .Columns ("Id");


            Create.Table ("Talent")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Type")
            .AsInt32 ().Nullable ()
            .WithColumn ("PrizeItem_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_Talent")
                  .OnTable ("Talent")


        .Columns ("Id");


            Create.Table ("TalentLevel")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Level")
            .AsInt32 ().Nullable ()
            .WithColumn ("Achievement_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("TalentRef")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_TalentLevel")
                  .OnTable ("TalentLevel")


        .Columns ("Id");


            Create.Table ("TradeLock")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("ExpiresAt")
            .AsDateTime ().Nullable ()
            .WithColumn ("UserInfo_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_TradeLock")
                  .OnTable ("TradeLock")


        .Columns ("Id");


            Create.Table ("Translation")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("LanguageId")
            .AsInt32 ().NotNullable ()
            .WithColumn ("Value")
            .AsString (255).NotNullable ()
            .WithColumn ("TString_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_Translation")
                  .OnTable ("Translation")


        .Columns ("Id");


            Create.Table ("TString")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Value")
            .AsString (255).NotNullable ()
            ;



            Create.PrimaryKey ("PK_TString")
                  .OnTable ("TString")


        .Columns ("Id");


            Create.Table ("UserAchievement")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Progress")
            .AsInt32 ().Nullable ()
            .WithColumn ("Achievement_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Level_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("UserInfo_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_UserAchievement")
                  .OnTable ("UserAchievement")


        .Columns ("Id");


            Create.Table ("UserBan")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("ExpiresAt")
            .AsDateTime ().Nullable ()
            .WithColumn ("IP")
            .AsString (255).Nullable ()
            .WithColumn ("MachineId")
            .AsString (255).Nullable ()
            .WithColumn ("Reason")
            .AsString (255).Nullable ()
            .WithColumn ("User_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_UserBan")
                  .OnTable ("UserBan")


        .Columns ("Id");


            Create.Table ("UserCaution")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("UserInfo_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_UserCaution")
                  .OnTable ("UserCaution")


        .Columns ("Id");


            Create.Table ("UserItem")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            ;



            Create.PrimaryKey ("PK_UserItem")
                  .OnTable ("UserItem")


        .Columns ("Id");


            Create.Table ("UserSearchLog")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Value1")
            .AsString (255).Nullable ()
            .WithColumn ("Value2")
            .AsString (255).Nullable ()
            .WithColumn ("UserInfo_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_UserSearchLog")
                  .OnTable ("UserSearchLog")


        .Columns ("Id");


            Create.Table ("UserTalent")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("State")
            .AsInt32 ().Nullable ()
            .WithColumn ("Level_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Talent_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("UserInfoRef")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_UserTalent")
                  .OnTable ("UserTalent")


        .Columns ("Id");


            Create.Table ("WardrobeItem")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("Gender")
            .AsString (255).Nullable ()
            .WithColumn ("Look")
            .AsString (255).Nullable ()
            .WithColumn ("Slot")
            .AsInt32 ().Nullable ()
            .WithColumn ("InventoryInventoryRef")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_WardrobeItem")
                  .OnTable ("WardrobeItem")


        .Columns ("Id");


            Create.Table ("YoutubeVideo")

            .WithColumn ("Id")
            .AsString (255).NotNullable ()
            .WithColumn ("Description")
            .AsString (255).Nullable ()
            .WithColumn ("Name")
            .AsString (255).Nullable ()
            ;



            Create.PrimaryKey ("PK_YoutubeVideo")
                  .OnTable ("YoutubeVideo")


        .Columns ("Id");


            Create.Table ("FloorItem")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("discriminator")
            .AsString (255).NotNullable ()
            .WithColumn ("Owner_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Badge")
            .AsString (255).Nullable ()
            .WithColumn ("CreatedAt")
            .AsDateTime ().Nullable ()
            .WithColumn ("BaseItem_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("LookFemale")
            .AsString (255).Nullable ()
            .WithColumn ("LookMale")
            .AsString (255).Nullable ()
            .WithColumn ("Gender")
            .AsString (255).Nullable ()
            .WithColumn ("Look")
            .AsString (255).Nullable ()
            .WithColumn ("Race")
            .AsInt32 ().Nullable ()
            .WithColumn ("Message")
            .AsString (255).Nullable ()
            .WithColumn ("User_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("PlayingVideo_id")
            .AsString (255).Nullable ()
            .WithColumn ("InventoryInventory_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_FloorItem")
                  .OnTable ("FloorItem")


        .Columns ("Id");


            Create.Table ("PetItem")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("BaseItem_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Info_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Owner_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("InventoryInventory_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_PetItem")
                  .OnTable ("PetItem")


        .Columns ("Id");


            Create.Table ("WallItem")

            .WithColumn ("Id")
            .AsInt32 ().NotNullable ().Identity ()
            .WithColumn ("discriminator")
            .AsString (255).NotNullable ()
            .WithColumn ("Owner_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Data_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("BaseItem_id")
            .AsInt32 ().Nullable ()
            .WithColumn ("Color")
            .AsString (255).Nullable ()
            .WithColumn ("Text")
            .AsString (255).Nullable ()
            .WithColumn ("Number")
            .AsDouble ().Nullable ()
            .WithColumn ("InventoryInventory_id")
            .AsInt32 ().Nullable ()
            ;



            Create.PrimaryKey ("PK_WallItem")
                  .OnTable ("WallItem")


        .Columns ("Id");


            Create.ForeignKey ("FK8AC9B5B9B51F9CE4")
                  .FromTable ("AchievementLevel")
                  .ForeignColumns ("AchievementRef")
                  .ToTable ("Achievement")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK1E173FAE75ECFEED")
                  .FromTable ("AvatarEffect")
                  .ForeignColumns ("EffectComponentUserEffectComponent_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK50A53C35E35C297E")
                  .FromTable ("Badge")
                  .ForeignColumns ("BadgesUserBadgeComponentRef")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK450D975B8005458D")
                  .FromTable ("BaseInfo")
                  .ForeignColumns ("Owner_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK450D975BF8FE1E37")
                  .FromTable ("BaseInfo")
                  .ForeignColumns ("Room_id")
                  .ToTable ("RoomData")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK450D975B9293653E")
                  .FromTable ("BaseInfo")
                  .ForeignColumns ("FavouriteGroup_id")
                  .ToTable ("Group")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK450D975BC93EC504")
                  .FromTable ("BaseInfo")
                  .ForeignColumns ("HomeRoom_id")
                  .ToTable ("RoomData")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK450D975BC465ED37")
                  .FromTable ("BaseInfo")
                  .ForeignColumns ("Subscription_id")
                  .ToTable ("Subscription")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK450D975BE1512F38")
                  .FromTable ("BaseInfo")
                  .ForeignColumns ("EffectComponentActiveEffect_id")
                  .ToTable ("AvatarEffect")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK450D975B7ED30A0B")
                  .FromTable ("BaseInfo")
                  .ForeignColumns ("UserInfo_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK5B7D8210C92BB699")
                  .FromTable ("BaseItem")
                  .ForeignColumns ("Name_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK5B7D82102AB945AC")
                  .FromTable ("BaseItem")
                  .ForeignColumns ("Description_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK5B7D82104E121F3F")
                  .FromTable ("BaseItem")
                  .ForeignColumns ("Song_id")
                  .ToTable ("SongData")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK5B7D8210EA5B8F6B")
                  .FromTable ("BaseItem")
                  .ForeignColumns ("RoomCompetition_id")
                  .ToTable ("RoomCompetition")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK7C1765AF218EC187")
                  .FromTable ("CatalogOffer")
                  .ForeignColumns ("CatalogPage_id")
                  .ToTable ("CatalogPage")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKBE955E6ED94EBF50")
                  .FromTable ("CatalogPage")
                  .ForeignColumns ("Caption_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKBE955E6E91DBDC5F")
                  .FromTable ("CatalogPage")
                  .ForeignColumns ("Layout_id")
                  .ToTable ("CatalogPageLayout")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKBE955E6E8C4FC83D")
                  .FromTable ("CatalogPage")
                  .ForeignColumns ("SelectedOffer_id")
                  .ToTable ("CatalogOffer")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKBE955E6E218EC187")
                  .FromTable ("CatalogPage")
                  .ForeignColumns ("CatalogPage_id")
                  .ToTable ("CatalogPage")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCFA36A78DFF0BB9B")
                  .FromTable ("CatalogPageLayout")
                  .ForeignColumns ("Content_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCFA36A785F2BA7BA")
                  .FromTable ("CatalogPageLayout")
                  .ForeignColumns ("VoucherDescription_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCFA36A7824B24313")
                  .FromTable ("CatalogPageLayout")
                  .ForeignColumns ("HeaderDescription_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCFA36A784D9405FF")
                  .FromTable ("CatalogPageLayout")
                  .ForeignColumns ("Text_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCFA36A784C7DC080")
                  .FromTable ("CatalogPageLayout")
                  .ForeignColumns ("Text1_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCFA36A784C7DC0E1")
                  .FromTable ("CatalogPageLayout")
                  .ForeignColumns ("Text2_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCFA36A784C7DC0C2")
                  .FromTable ("CatalogPageLayout")
                  .ForeignColumns ("Text3_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCFA36A784C7DC123")
                  .FromTable ("CatalogPageLayout")
                  .ForeignColumns ("Text4_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCFA36A784C7DC1FC")
                  .FromTable ("CatalogPageLayout")
                  .ForeignColumns ("Text5_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCFA36A782AB945AC")
                  .FromTable ("CatalogPageLayout")
                  .ForeignColumns ("Description_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCFA36A78F5E88CB8")
                  .FromTable ("CatalogPageLayout")
                  .ForeignColumns ("Enscription_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCFA36A787FC1D4B2")
                  .FromTable ("CatalogPageLayout")
                  .ForeignColumns ("SpecialText_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK55125810F8107B18")
                  .FromTable ("CatalogProduct")
                  .ForeignColumns ("Item_id")
                  .ToTable ("BaseItem")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK551258103DF7F84B")
                  .FromTable ("CatalogProduct")
                  .ForeignColumns ("CatalogOffer_id")
                  .ToTable ("CatalogOffer")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKB9820A059DFFE519")
                  .FromTable ("ChatMessage")
                  .ForeignColumns ("User_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKB9820A0575DE907")
                  .FromTable ("ChatMessage")
                  .ForeignColumns ("RoomData_id")
                  .ToTable ("RoomData")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK89B740A5F3865CDB")
                  .FromTable ("EcotronReward")
                  .ForeignColumns ("BaseItem_id")
                  .ToTable ("BaseItem")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK89B740A5321F574B")
                  .FromTable ("EcotronReward")
                  .ForeignColumns ("EcotronLevel_id")
                  .ToTable ("EcotronLevel")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK2357C3FFAB03AA0")
                  .FromTable ("FriendRequest")
                  .ForeignColumns ("From_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK2357C3FB550214F")
                  .FromTable ("FriendRequest")
                  .ForeignColumns ("To_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK2357C3F42A90AD8")
                  .FromTable ("FriendRequest")
                  .ForeignColumns ("RelationshipsRelationshipComponent_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKFBBA8E2754C3043A")
                  .FromTable ("Group")
                  .ForeignColumns ("Colour1_id")
                  .ToTable ("GroupSymbolColours")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKFBBA8E273F6DBC9")
                  .FromTable ("Group")
                  .ForeignColumns ("Colour2_id")
                  .ToTable ("GroupBackGroundColours")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKFBBA8E275D5B1940")
                  .FromTable ("Group")
                  .ForeignColumns ("Creator_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKFBBA8E27F428CA4C")
                  .FromTable ("Group")
                  .ForeignColumns ("Forum_id")
                  .ToTable ("GroupForum")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKFBBA8E27F8FE1E37")
                  .FromTable ("Group")
                  .ForeignColumns ("Room_id")
                  .ToTable ("RoomData")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK7A678264B2E044D5")
                  .FromTable ("GroupForumPost")
                  .ForeignColumns ("HiddenBy_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK7A678264ECC84987")
                  .FromTable ("GroupForumPost")
                  .ForeignColumns ("Poster_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK7A67826426F99CBB")
                  .FromTable ("GroupForumPost")
                  .ForeignColumns ("GroupForumThread_id")
                  .ToTable ("GroupForumThread")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK3466FFA5D5B1940")
                  .FromTable ("GroupForumThread")
                  .ForeignColumns ("Creator_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK3466FFAB2E044D5")
                  .FromTable ("GroupForumThread")
                  .ForeignColumns ("HiddenBy_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK3466FFA86438333")
                  .FromTable ("GroupForumThread")
                  .ForeignColumns ("GroupForum_id")
                  .ToTable ("GroupForum")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKA7354D6D9DFFE519")
                  .FromTable ("HallOfFameElement")
                  .ForeignColumns ("User_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKDF85EFA62FE6A4E8")
                  .FromTable ("HotelLandingManager")
                  .ForeignColumns ("FurniReward_id")
                  .ToTable ("BaseItem")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKDE8AA2F94C9CB897")
                  .FromTable ("HotelLandingPromos")
                  .ForeignColumns ("HotelLandingManager_id")
                  .ToTable ("HotelLandingManager")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK13647ACAD00CE56F")
                  .FromTable ("Link")
                  .ForeignColumns ("ChatMessage_id")
                  .ToTable ("ChatMessage")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKDB8AFE64FAB03AA0")
                  .FromTable ("MessengerMessage")
                  .ForeignColumns ("From_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKDB8AFE64B550214F")
                  .FromTable ("MessengerMessage")
                  .ForeignColumns ("To_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK5DE3454A7ED30A0B")
                  .FromTable ("Minimail")
                  .ForeignColumns ("UserInfo_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK1C0D29333C014DEF")
                  .FromTable ("MoodlightData")
                  .ForeignColumns ("CurrentPreset_id")
                  .ToTable ("MoodlightPreset")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK4E12B0E811B558E3")
                  .FromTable ("MoodlightPreset")
                  .ForeignColumns ("MoodlightData_id")
                  .ToTable ("MoodlightData")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKA1A08B333E8646A")
                  .FromTable ("NavigatorCategory")
                  .ForeignColumns ("NavigatorCategoryRef")
                  .ToTable ("NavigatorCategory")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKC2ECA37F8FE1E37")
                  .FromTable ("Poll")
                  .ForeignColumns ("Room_id")
                  .ToTable ("RoomData")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK55F7879950AE97E7")
                  .FromTable ("PollQuestion")
                  .ForeignColumns ("Poll_id")
                  .ToTable ("Poll")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKE2E4B70238A212C")
                  .FromTable ("Relationship")
                  .ForeignColumns ("Friend_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKE2E4B70D94B326B")
                  .FromTable ("Relationship")
                  .ForeignColumns ("RelationshipsRelationshipComponentRef")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK870DFD1CF8FE1E37")
                  .FromTable ("RoomCompetitionEntry")
                  .ForeignColumns ("Room_id")
                  .ToTable ("RoomData")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK870DFD1CEA5B8F6B")
                  .FromTable ("RoomCompetitionEntry")
                  .ForeignColumns ("RoomCompetition_id")
                  .ToTable ("RoomCompetition")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK2F2D6D4BE9D7EC2C")
                  .FromTable ("RoomData")
                  .ForeignColumns ("Category_id")
                  .ToTable ("NavigatorCategory")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK2F2D6D4BA1F9B824")
                  .FromTable ("RoomData")
                  .ForeignColumns ("Event_id")
                  .ToTable ("RoomEvent")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK2F2D6D4BE2440413")
                  .FromTable ("RoomData")
                  .ForeignColumns ("Group_id")
                  .ToTable ("Group")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK2F2D6D4B8005458D")
                  .FromTable ("RoomData")
                  .ForeignColumns ("Owner_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK9873AF411868503")
                  .FromTable ("RoomMute")
                  .ForeignColumns ("Entity_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK9873AF475DE907")
                  .FromTable ("RoomMute")
                  .ForeignColumns ("RoomData_id")
                  .ToTable ("RoomData")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKEB3E86EB851EAF78")
                  .FromTable ("SupportTicket")
                  .ForeignColumns ("ReportedUser_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKEB3E86EBF8FE1E37")
                  .FromTable ("SupportTicket")
                  .ForeignColumns ("Room_id")
                  .ToTable ("RoomData")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKEB3E86EB772B2EB7")
                  .FromTable ("SupportTicket")
                  .ForeignColumns ("Sender_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKEB3E86EBCA13F3E2")
                  .FromTable ("SupportTicket")
                  .ForeignColumns ("Staff_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK83599A5C261A9BE8")
                  .FromTable ("Talent")
                  .ForeignColumns ("PrizeItem_id")
                  .ToTable ("BaseItem")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKBA01A84E810D3643")
                  .FromTable ("TalentLevel")
                  .ForeignColumns ("Achievement_id")
                  .ToTable ("Achievement")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKBA01A84E4DB85828")
                  .FromTable ("TalentLevel")
                  .ForeignColumns ("TalentRef")
                  .ToTable ("Talent")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK255931D7ED30A0B")
                  .FromTable ("TradeLock")
                  .ForeignColumns ("UserInfo_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK7140856B5DC04EAB")
                  .FromTable ("Translation")
                  .ForeignColumns ("TString_id")
                  .ToTable ("TString")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK803E7074810D3643")
                  .FromTable ("UserAchievement")
                  .ForeignColumns ("Achievement_id")
                  .ToTable ("Achievement")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK803E70741F15AF0")
                  .FromTable ("UserAchievement")
                  .ForeignColumns ("Level_id")
                  .ToTable ("AchievementLevel")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK803E70747ED30A0B")
                  .FromTable ("UserAchievement")
                  .ForeignColumns ("UserInfo_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKED490F329DFFE519")
                  .FromTable ("UserBan")
                  .ForeignColumns ("User_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK924B42BA7ED30A0B")
                  .FromTable ("UserCaution")
                  .ForeignColumns ("UserInfo_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK5A0232137ED30A0B")
                  .FromTable ("UserSearchLog")
                  .ForeignColumns ("UserInfo_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK43646CE7E48F7765")
                  .FromTable ("UserTalent")
                  .ForeignColumns ("Level_id")
                  .ToTable ("TalentLevel")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK43646CE7EDD1ABEB")
                  .FromTable ("UserTalent")
                  .ForeignColumns ("Talent_id")
                  .ToTable ("Talent")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK43646CE79EAA9678")
                  .FromTable ("UserTalent")
                  .ForeignColumns ("UserInfoRef")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK1A40EC5BA3955DBB")
                  .FromTable ("WardrobeItem")
                  .ForeignColumns ("InventoryInventoryRef")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCC47F5B98005458D")
                  .FromTable ("FloorItem")
                  .ForeignColumns ("Owner_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCC47F5B92D27880A")
                  .FromTable ("FloorItem")
                  .ForeignColumns ("BaseItem_id")
                  .ToTable ("BaseItem")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCC47F5B99DFFE519")
                  .FromTable ("FloorItem")
                  .ForeignColumns ("User_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCC47F5B9CE0EEC7E")
                  .FromTable ("FloorItem")
                  .ForeignColumns ("PlayingVideo_id")
                  .ToTable ("YoutubeVideo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKCC47F5B93E370B44")
                  .FromTable ("FloorItem")
                  .ForeignColumns ("InventoryInventory_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK18B22AF4D88BF8C")
                  .FromTable ("PetItem")
                  .ForeignColumns ("BaseItem_id")
                  .ToTable ("BaseItem")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK18B22AF4430B2F0C")
                  .FromTable ("PetItem")
                  .ForeignColumns ("Info_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK18B22AF48005458D")
                  .FromTable ("PetItem")
                  .ForeignColumns ("Owner_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FK18B22AF43E370B44")
                  .FromTable ("PetItem")
                  .ForeignColumns ("InventoryInventory_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKF2C9C2278005458D")
                  .FromTable ("WallItem")
                  .ForeignColumns ("Owner_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKF2C9C227D8B3AC")
                  .FromTable ("WallItem")
                  .ForeignColumns ("Data_id")
                  .ToTable ("MoodlightData")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKF2C9C227CF2C6E49")
                  .FromTable ("WallItem")
                  .ForeignColumns ("BaseItem_id")
                  .ToTable ("BaseItem")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);


            Create.ForeignKey ("FKF2C9C2273E370B44")
                  .FromTable ("WallItem")
                  .ForeignColumns ("InventoryInventory_id")
                  .ToTable ("BaseInfo")
                  .PrimaryColumns ("Id")
                  .OnDelete (Rule.None)
                  .OnUpdate (Rule.None);

        }
        public override void Down ()
        {

            Delete.ForeignKey ("FKF2C9C2273E370B44")
                  .OnTable ("WallItem");


            Delete.ForeignKey ("FKF2C9C227CF2C6E49")
                  .OnTable ("WallItem");


            Delete.ForeignKey ("FKF2C9C227D8B3AC")
                  .OnTable ("WallItem");


            Delete.ForeignKey ("FKF2C9C2278005458D")
                  .OnTable ("WallItem");


            Delete.ForeignKey ("FK18B22AF43E370B44")
                  .OnTable ("PetItem");


            Delete.ForeignKey ("FK18B22AF48005458D")
                  .OnTable ("PetItem");


            Delete.ForeignKey ("FK18B22AF4430B2F0C")
                  .OnTable ("PetItem");


            Delete.ForeignKey ("FK18B22AF4D88BF8C")
                  .OnTable ("PetItem");


            Delete.ForeignKey ("FKCC47F5B93E370B44")
                  .OnTable ("FloorItem");


            Delete.ForeignKey ("FKCC47F5B9CE0EEC7E")
                  .OnTable ("FloorItem");


            Delete.ForeignKey ("FKCC47F5B99DFFE519")
                  .OnTable ("FloorItem");


            Delete.ForeignKey ("FKCC47F5B92D27880A")
                  .OnTable ("FloorItem");


            Delete.ForeignKey ("FKCC47F5B98005458D")
                  .OnTable ("FloorItem");


            Delete.ForeignKey ("FK1A40EC5BA3955DBB")
                  .OnTable ("WardrobeItem");


            Delete.ForeignKey ("FK43646CE79EAA9678")
                  .OnTable ("UserTalent");


            Delete.ForeignKey ("FK43646CE7EDD1ABEB")
                  .OnTable ("UserTalent");


            Delete.ForeignKey ("FK43646CE7E48F7765")
                  .OnTable ("UserTalent");


            Delete.ForeignKey ("FK5A0232137ED30A0B")
                  .OnTable ("UserSearchLog");


            Delete.ForeignKey ("FK924B42BA7ED30A0B")
                  .OnTable ("UserCaution");


            Delete.ForeignKey ("FKED490F329DFFE519")
                  .OnTable ("UserBan");


            Delete.ForeignKey ("FK803E70747ED30A0B")
                  .OnTable ("UserAchievement");


            Delete.ForeignKey ("FK803E70741F15AF0")
                  .OnTable ("UserAchievement");


            Delete.ForeignKey ("FK803E7074810D3643")
                  .OnTable ("UserAchievement");


            Delete.ForeignKey ("FK7140856B5DC04EAB")
                  .OnTable ("Translation");


            Delete.ForeignKey ("FK255931D7ED30A0B")
                  .OnTable ("TradeLock");


            Delete.ForeignKey ("FKBA01A84E4DB85828")
                  .OnTable ("TalentLevel");


            Delete.ForeignKey ("FKBA01A84E810D3643")
                  .OnTable ("TalentLevel");


            Delete.ForeignKey ("FK83599A5C261A9BE8")
                  .OnTable ("Talent");


            Delete.ForeignKey ("FKEB3E86EBCA13F3E2")
                  .OnTable ("SupportTicket");


            Delete.ForeignKey ("FKEB3E86EB772B2EB7")
                  .OnTable ("SupportTicket");


            Delete.ForeignKey ("FKEB3E86EBF8FE1E37")
                  .OnTable ("SupportTicket");


            Delete.ForeignKey ("FKEB3E86EB851EAF78")
                  .OnTable ("SupportTicket");


            Delete.ForeignKey ("FK9873AF475DE907")
                  .OnTable ("RoomMute");


            Delete.ForeignKey ("FK9873AF411868503")
                  .OnTable ("RoomMute");


            Delete.ForeignKey ("FK2F2D6D4B8005458D")
                  .OnTable ("RoomData");


            Delete.ForeignKey ("FK2F2D6D4BE2440413")
                  .OnTable ("RoomData");


            Delete.ForeignKey ("FK2F2D6D4BA1F9B824")
                  .OnTable ("RoomData");


            Delete.ForeignKey ("FK2F2D6D4BE9D7EC2C")
                  .OnTable ("RoomData");


            Delete.ForeignKey ("FK870DFD1CEA5B8F6B")
                  .OnTable ("RoomCompetitionEntry");


            Delete.ForeignKey ("FK870DFD1CF8FE1E37")
                  .OnTable ("RoomCompetitionEntry");


            Delete.ForeignKey ("FKE2E4B70D94B326B")
                  .OnTable ("Relationship");


            Delete.ForeignKey ("FKE2E4B70238A212C")
                  .OnTable ("Relationship");


            Delete.ForeignKey ("FK55F7879950AE97E7")
                  .OnTable ("PollQuestion");


            Delete.ForeignKey ("FKC2ECA37F8FE1E37")
                  .OnTable ("Poll");


            Delete.ForeignKey ("FKA1A08B333E8646A")
                  .OnTable ("NavigatorCategory");


            Delete.ForeignKey ("FK4E12B0E811B558E3")
                  .OnTable ("MoodlightPreset");


            Delete.ForeignKey ("FK1C0D29333C014DEF")
                  .OnTable ("MoodlightData");


            Delete.ForeignKey ("FK5DE3454A7ED30A0B")
                  .OnTable ("Minimail");


            Delete.ForeignKey ("FKDB8AFE64B550214F")
                  .OnTable ("MessengerMessage");


            Delete.ForeignKey ("FKDB8AFE64FAB03AA0")
                  .OnTable ("MessengerMessage");


            Delete.ForeignKey ("FK13647ACAD00CE56F")
                  .OnTable ("Link");


            Delete.ForeignKey ("FKDE8AA2F94C9CB897")
                  .OnTable ("HotelLandingPromos");


            Delete.ForeignKey ("FKDF85EFA62FE6A4E8")
                  .OnTable ("HotelLandingManager");


            Delete.ForeignKey ("FKA7354D6D9DFFE519")
                  .OnTable ("HallOfFameElement");


            Delete.ForeignKey ("FK3466FFA86438333")
                  .OnTable ("GroupForumThread");


            Delete.ForeignKey ("FK3466FFAB2E044D5")
                  .OnTable ("GroupForumThread");


            Delete.ForeignKey ("FK3466FFA5D5B1940")
                  .OnTable ("GroupForumThread");


            Delete.ForeignKey ("FK7A67826426F99CBB")
                  .OnTable ("GroupForumPost");


            Delete.ForeignKey ("FK7A678264ECC84987")
                  .OnTable ("GroupForumPost");


            Delete.ForeignKey ("FK7A678264B2E044D5")
                  .OnTable ("GroupForumPost");


            Delete.ForeignKey ("FKFBBA8E27F8FE1E37")
                  .OnTable ("Group");


            Delete.ForeignKey ("FKFBBA8E27F428CA4C")
                  .OnTable ("Group");


            Delete.ForeignKey ("FKFBBA8E275D5B1940")
                  .OnTable ("Group");


            Delete.ForeignKey ("FKFBBA8E273F6DBC9")
                  .OnTable ("Group");


            Delete.ForeignKey ("FKFBBA8E2754C3043A")
                  .OnTable ("Group");


            Delete.ForeignKey ("FK2357C3F42A90AD8")
                  .OnTable ("FriendRequest");


            Delete.ForeignKey ("FK2357C3FB550214F")
                  .OnTable ("FriendRequest");


            Delete.ForeignKey ("FK2357C3FFAB03AA0")
                  .OnTable ("FriendRequest");


            Delete.ForeignKey ("FK89B740A5321F574B")
                  .OnTable ("EcotronReward");


            Delete.ForeignKey ("FK89B740A5F3865CDB")
                  .OnTable ("EcotronReward");


            Delete.ForeignKey ("FKB9820A0575DE907")
                  .OnTable ("ChatMessage");


            Delete.ForeignKey ("FKB9820A059DFFE519")
                  .OnTable ("ChatMessage");


            Delete.ForeignKey ("FK551258103DF7F84B")
                  .OnTable ("CatalogProduct");


            Delete.ForeignKey ("FK55125810F8107B18")
                  .OnTable ("CatalogProduct");


            Delete.ForeignKey ("FKCFA36A787FC1D4B2")
                  .OnTable ("CatalogPageLayout");


            Delete.ForeignKey ("FKCFA36A78F5E88CB8")
                  .OnTable ("CatalogPageLayout");


            Delete.ForeignKey ("FKCFA36A782AB945AC")
                  .OnTable ("CatalogPageLayout");


            Delete.ForeignKey ("FKCFA36A784C7DC1FC")
                  .OnTable ("CatalogPageLayout");


            Delete.ForeignKey ("FKCFA36A784C7DC123")
                  .OnTable ("CatalogPageLayout");


            Delete.ForeignKey ("FKCFA36A784C7DC0C2")
                  .OnTable ("CatalogPageLayout");


            Delete.ForeignKey ("FKCFA36A784C7DC0E1")
                  .OnTable ("CatalogPageLayout");


            Delete.ForeignKey ("FKCFA36A784C7DC080")
                  .OnTable ("CatalogPageLayout");


            Delete.ForeignKey ("FKCFA36A784D9405FF")
                  .OnTable ("CatalogPageLayout");


            Delete.ForeignKey ("FKCFA36A7824B24313")
                  .OnTable ("CatalogPageLayout");


            Delete.ForeignKey ("FKCFA36A785F2BA7BA")
                  .OnTable ("CatalogPageLayout");


            Delete.ForeignKey ("FKCFA36A78DFF0BB9B")
                  .OnTable ("CatalogPageLayout");


            Delete.ForeignKey ("FKBE955E6E218EC187")
                  .OnTable ("CatalogPage");


            Delete.ForeignKey ("FKBE955E6E8C4FC83D")
                  .OnTable ("CatalogPage");


            Delete.ForeignKey ("FKBE955E6E91DBDC5F")
                  .OnTable ("CatalogPage");


            Delete.ForeignKey ("FKBE955E6ED94EBF50")
                  .OnTable ("CatalogPage");


            Delete.ForeignKey ("FK7C1765AF218EC187")
                  .OnTable ("CatalogOffer");


            Delete.ForeignKey ("FK5B7D8210EA5B8F6B")
                  .OnTable ("BaseItem");


            Delete.ForeignKey ("FK5B7D82104E121F3F")
                  .OnTable ("BaseItem");


            Delete.ForeignKey ("FK5B7D82102AB945AC")
                  .OnTable ("BaseItem");


            Delete.ForeignKey ("FK5B7D8210C92BB699")
                  .OnTable ("BaseItem");


            Delete.ForeignKey ("FK450D975B7ED30A0B")
                  .OnTable ("BaseInfo");


            Delete.ForeignKey ("FK450D975BE1512F38")
                  .OnTable ("BaseInfo");


            Delete.ForeignKey ("FK450D975BC465ED37")
                  .OnTable ("BaseInfo");


            Delete.ForeignKey ("FK450D975BC93EC504")
                  .OnTable ("BaseInfo");


            Delete.ForeignKey ("FK450D975B9293653E")
                  .OnTable ("BaseInfo");


            Delete.ForeignKey ("FK450D975BF8FE1E37")
                  .OnTable ("BaseInfo");


            Delete.ForeignKey ("FK450D975B8005458D")
                  .OnTable ("BaseInfo");


            Delete.ForeignKey ("FK50A53C35E35C297E")
                  .OnTable ("Badge");


            Delete.ForeignKey ("FK1E173FAE75ECFEED")
                  .OnTable ("AvatarEffect");


            Delete.ForeignKey ("FK8AC9B5B9B51F9CE4")
                  .OnTable ("AchievementLevel");


            Delete.Table ("WallItem");


            Delete.Table ("PetItem");


            Delete.Table ("FloorItem");


            Delete.Table ("YoutubeVideo");


            Delete.Table ("WardrobeItem");


            Delete.Table ("UserTalent");


            Delete.Table ("UserSearchLog");


            Delete.Table ("UserItem");


            Delete.Table ("UserCaution");


            Delete.Table ("UserBan");


            Delete.Table ("UserAchievement");


            Delete.Table ("TString");


            Delete.Table ("Translation");


            Delete.Table ("TradeLock");


            Delete.Table ("TalentLevel");


            Delete.Table ("Talent");


            Delete.Table ("SupportTicket");


            Delete.Table ("Subscription");


            Delete.Table ("SongData");


            Delete.Table ("RoomMute");


            Delete.Table ("RoomEvent");


            Delete.Table ("RoomData");


            Delete.Table ("RoomCompetitionEntry");


            Delete.Table ("RoomCompetition");


            Delete.Table ("Relationship");


            Delete.Table ("PollQuestion");


            Delete.Table ("Poll");


            Delete.Table ("NavigatorCategory");


            Delete.Table ("MoodlightPreset");


            Delete.Table ("MoodlightData");


            Delete.Table ("ModerationTemplate");


            Delete.Table ("Minimail");


            Delete.Table ("MessengerMessage");


            Delete.Table ("Link");


            Delete.Table ("HotelLandingPromos");


            Delete.Table ("HotelLandingManager");


            Delete.Table ("HallOfFameElement");


            Delete.Table ("GroupSymbols");


            Delete.Table ("GroupSymbolColours");


            Delete.Table ("GroupForumThread");


            Delete.Table ("GroupForumPost");


            Delete.Table ("GroupForum");


            Delete.Table ("GroupBases");


            Delete.Table ("GroupBaseColours");


            Delete.Table ("GroupBackGroundColours");


            Delete.Table ("Group");


            Delete.Table ("FriendRequest");


            Delete.Table ("EcotronReward");


            Delete.Table ("EcotronLevel");


            Delete.Table ("ChatMessage");


            Delete.Table ("CatalogProduct");


            Delete.Table ("CatalogPageLayout");


            Delete.Table ("CatalogPage");


            Delete.Table ("CatalogOffer");


            Delete.Table ("BaseItem");


            Delete.Table ("BaseInfo");


            Delete.Table ("Badge");


            Delete.Table ("AvatarEffect");


            Delete.Table ("AchievementLevel");


            Delete.Table ("Achievement");

        }
    }
}

