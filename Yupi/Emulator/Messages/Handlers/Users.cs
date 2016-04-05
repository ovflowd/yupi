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
	d8'                 88                 
	d8'                  88     

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

	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using Yupi.Emulator.Data.Base.Adapters.Interfaces;
	using Yupi.Emulator.Game.Achievements.Structs;
	using Yupi.Emulator.Game.GameClients.Interfaces;
	using Yupi.Emulator.Game.Groups.Structs;
	using Yupi.Emulator.Game.Rooms;
	using Yupi.Emulator.Game.Rooms.Data;
	using Yupi.Emulator.Game.Rooms.Data.Models;
	using Yupi.Emulator.Game.Rooms.User;
	using Yupi.Emulator.Game.Users;
	using Yupi.Emulator.Game.Users.Badges.Models;
	using Yupi.Emulator.Game.Users.Messenger.Structs;
	using Yupi.Emulator.Game.Users.Relationships;




	namespace Yupi.Emulator.Messages.Handlers
	{
	/// <summary>
	///     Class MessageHandler.
	/// </summary>
	 partial class MessageHandler
	{
		/*
	    /// <summary>
	    ///     Applies the effect.
	    /// </summary>
	     void ApplyEffect()
	    {
	        int effectId = Request.GetInteger();
	        RoomUser roomUserByHabbo =
	            Yupi.GetGame()
	                .GetRoomManager()
	                .GetRoom(Session.GetHabbo().CurrentRoomId)
	                .GetRoomUserManager()
	                .GetRoomUserByHabbo(Session.GetHabbo().UserName);
	        if (!roomUserByHabbo.RidingHorse)
	            Session.GetHabbo().GetAvatarEffectsInventoryComponent().ActivateCustomEffect(effectId);
	    }
		*/
	/*
	    /// <summary>
	    ///     Gets the user information.
	    /// </summary>
	     void GetUserInfo()
	    {
	        GetResponse().Init(PacketLibraryManager.OutgoingHandler("UpdateUserDataMessageComposer"));
	        GetResponse().AppendInteger(-1);
	        GetResponse().AppendString(Session.GetHabbo().Look);
	        GetResponse().AppendString(Session.GetHabbo().Gender.ToLower());
	        GetResponse().AppendString(Session.GetHabbo().Motto);
	        GetResponse().AppendInteger(Session.GetHabbo().AchievementPoints);
	        SendResponse();
	        GetResponse().Init(PacketLibraryManager.OutgoingHandler("AchievementPointsMessageComposer"));
	        GetResponse().AppendInteger(Session.GetHabbo().AchievementPoints);
	        SendResponse();
	    }
		*/
		/*
	     void SetRoomCameraPreferences()
	    {
	        bool enable = Request.GetBool();
	        Session.GetHabbo().Preferences.DisableCameraFollow = enable;
	        Session.GetHabbo().Preferences.Save();
	    }
		*/

	     /// <summary>
	    ///     Gets the friends count.
	    /// </summary>
	    /// <param name="userId">The user identifier.</param>
	    /// <returns>System.Int32.</returns>
	     static int GetFriendsCount(uint userId)
	    {
	        int result;

	        using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
	        {
	            queryReactor.SetQuery(
	                "SELECT COUNT(*) FROM messenger_friendships WHERE user_one_id = @id OR user_two_id = @id;");
	            queryReactor.AddParameter("id", userId);

	            result = queryReactor.GetInteger();
	        }

	        return result;
	    }

	    /// <summary>
	    ///     Gets the relationships.
	    /// </summary>
	     void GetRelationships()
	    {
	        uint userId = Request.GetUInt32();
	        Habbo habboForId = Yupi.GetHabboById(userId);
	        if (habboForId == null)
	            return;
	        Random rand = new Random();
	        habboForId.Relationships = (
	            from x in habboForId.Relationships
	            orderby rand.Next()
	            select x).ToDictionary(item => item.Key,
	                item => item.Value);
	        int num = habboForId.Relationships.Count(x => x.Value.Type == 1);
	        int num2 = habboForId.Relationships.Count(x => x.Value.Type == 2);
	        int num3 = habboForId.Relationships.Count(x => x.Value.Type == 3);
	        Response.Init(PacketLibraryManager.OutgoingHandler("RelationshipMessageComposer"));
	        Response.AppendInteger(habboForId.Id);
	        Response.AppendInteger(habboForId.Relationships.Count);
	        foreach (Relationship current in habboForId.Relationships.Values)
	        {
	            Habbo habboForId2 = Yupi.GetHabboById(Convert.ToUInt32(current.UserId));
	            if (habboForId2 == null)
	            {
	                Response.AppendInteger(0);
	                Response.AppendInteger(0);
	                Response.AppendInteger(0);
	                Response.AppendString("Placeholder");
	                Response.AppendString("hr-115-42.hd-190-1.ch-215-62.lg-285-91.sh-290-62");
	            }
	            else
	            {
	                Response.AppendInteger(current.Type);
	                Response.AppendInteger(current.Type == 1 ? num : (current.Type == 2 ? num2 : num3));
	                Response.AppendInteger(current.UserId);
	                Response.AppendString(habboForId2.UserName);
	                Response.AppendString(habboForId2.Look);
	            }
	        }
	        SendResponse();
	    }

	    /// <summary>
	    ///     Sets the relationship.
	    /// </summary>
	     void SetRelationship()
	    {
	        uint userId = Request.GetUInt32();
	        uint targetId = Request.GetUInt32();

	        using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
	        {
	            if (targetId == 0)
	            {
	                queryReactor.SetQuery("SELECT id FROM users_relationships WHERE user_id=@id AND target=@target LIMIT 1");
	                queryReactor.AddParameter("id", Session.GetHabbo().Id);
	                queryReactor.AddParameter("target", userId);

	                object integerResult = queryReactor.GetInteger();

	                int integer;

	                int.TryParse(integerResult.ToString(), out integer);

	                if (integer > 0 && Session.GetHabbo().Relationships.ContainsKey(integer))
	                {
	                    queryReactor.SetQuery("DELETE FROM users_relationships WHERE user_id=@id AND target=@target LIMIT 1");
	                    queryReactor.AddParameter("id", Session.GetHabbo().Id);
	                    queryReactor.AddParameter("target", userId);
	                    queryReactor.RunQuery();

	                    Session.GetHabbo().Relationships.Remove(integer);
	                }                 
	            }
	            else
	            {
	                queryReactor.SetQuery("SELECT id FROM users_relationships WHERE user_id=@id AND target=@target LIMIT 1");
	                queryReactor.AddParameter("id", Session.GetHabbo().Id);
	                queryReactor.AddParameter("target", userId);

	                object integerResult = queryReactor.GetInteger();

	                int integer;

	                int.TryParse(integerResult.ToString(), out integer);

	                if (integer > 0 && Session.GetHabbo().Relationships.ContainsKey(integer))
	                {
	                    queryReactor.SetQuery("DELETE FROM users_relationships WHERE user_id=@id AND target=@target LIMIT 1");
	                    queryReactor.AddParameter("id", Session.GetHabbo().Id);
	                    queryReactor.AddParameter("target", userId);
	                    queryReactor.RunQuery();

	                    Session.GetHabbo().Relationships.Remove(integer);
	                }
	            }

	            if (userId > 0 && targetId > 0)
	            {
	                queryReactor.SetQuery("INSERT INTO users_relationships (user_id, target, type) VALUES (@id, @target, @type)");
	                queryReactor.AddParameter("id", Session.GetHabbo().Id);
	                queryReactor.AddParameter("target", userId);
	                queryReactor.AddParameter("type", targetId);

	                object integerResult = queryReactor.InsertQuery();

	                int relationShipId;

	                int.TryParse(integerResult.ToString(), out relationShipId);

	                if(relationShipId > 0)
	                    Session.GetHabbo().Relationships.Add(relationShipId, new Relationship(relationShipId, (int)userId, (int)targetId));
	            }
	        }

	        if (userId > 0)
	        {
	            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

	            if (clientByUserId != null)
	                Session.GetHabbo().GetMessenger().UpdateFriend(userId, clientByUserId, true);
	        }
	    }

	    /// <summary>
	    ///     Talentses this instance.
	    /// </summary>
	    /// <exception cref="System.NullReferenceException"></exception>
	     void Talents()
	    {
	        string trackType = Request.GetString();

	        List<Talent> talents = Yupi.GetGame().GetTalentManager().GetTalents(trackType, -1);

	        int failLevel = -1;

	        if (talents == null)
	            return;

	        Response.Init(PacketLibraryManager.OutgoingHandler("TalentsTrackMessageComposer"));

	        Response.AppendString(trackType);
	        Response.AppendInteger(talents.Count);

	        foreach (Talent current in talents)
	        {
	            Response.AppendInteger(current.Level);

	            int nm = failLevel == -1 ? 1 : 0;
	            Response.AppendInteger(nm);

	            List<Talent> talents2 = Yupi.GetGame().GetTalentManager().GetTalents(trackType, current.Id);

	            Response.AppendInteger(talents2.Count);

	            foreach (Talent current2 in talents2)
	            {
	                if (current2.GetAchievement() == null)
	                    throw new NullReferenceException(
	                        $"The following talent achievement can't be found: {current2.AchievementGroup}");

	                int num = failLevel != -1 && failLevel < current2.Level
	                    ? 0
	                    : Session.GetHabbo().GetAchievementData(current2.AchievementGroup) == null
	                        ? 1
	                        : Session.GetHabbo().GetAchievementData(current2.AchievementGroup).Level >=
	                          current2.AchievementLevel
	                            ? 2
	                            : 1;

	                Response.AppendInteger(current2.GetAchievement().Id);
	                Response.AppendInteger(0);
	                Response.AppendString($"{current2.AchievementGroup}{current2.AchievementLevel}");
	                Response.AppendInteger(num);
	                Response.AppendInteger(Session.GetHabbo().GetAchievementData(current2.AchievementGroup) != null
	                    ? Session.GetHabbo().GetAchievementData(current2.AchievementGroup).Progress
	                    : 0);
	                Response.AppendInteger(current2.GetAchievement() == null
	                    ? 0
	                    : current2.GetAchievement().Levels[current2.AchievementLevel].Requirement);

	                if (num != 2 && failLevel == -1)
	                    failLevel = current2.Level;
	            }

	            Response.AppendInteger(0);

	            if (current.Type == "citizenship" && current.Level == 4)
	            {
	                Response.AppendInteger(2);
	                Response.AppendString("HABBO_CLUB_VIP_7_DAYS");
	                Response.AppendInteger(7);
	                Response.AppendString(current.Prize);
	                Response.AppendInteger(0);
	            }
	            else
	            {
	                Response.AppendInteger(1);
	                Response.AppendString(current.Prize);
	                Response.AppendInteger(0);
	            }
	        }

	        SendResponse();
	    }

	    /// <summary>
	    ///     Completes the safety quiz.
	    /// </summary>
	     void CompleteSafteyQuiz()
	    {
	        Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_SafetyQuizGraduate", 1);
	    }

	    /// <summary>
	    ///     Hotels the view countdown.
	    /// </summary>
	     void HotelViewCountdown()
	    {
	        string time = Request.GetString();
	        DateTime date;
	        DateTime.TryParse(time, out date);
	        TimeSpan diff = date - DateTime.Now;
	        Response.Init(PacketLibraryManager.OutgoingHandler("HotelViewCountdownMessageComposer"));
	        Response.AppendString(time);
	        Response.AppendInteger(Convert.ToInt32(diff.TotalSeconds));
	        SendResponse();
	        Console.WriteLine(diff.TotalSeconds);
	    }

	     void FindMoreFriends()
	    {
	        KeyValuePair<RoomData, uint>[] allRooms = Yupi.GetGame().GetRoomManager().GetActiveRooms();
	        if (allRooms != null)
	        {
	            Random rnd = new Random();
	            RoomData randomRoom = allRooms[rnd.Next(allRooms.Length)].Key;
	            SimpleServerMessageBuffer success = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("FindMoreFriendsSuccessMessageComposer"));
	            if (randomRoom == null)
	            {
	                success.AppendBool(false);
	                Session.SendMessage(success);
	                return;
	            }
	            success.AppendBool(true);
	            Session.SendMessage(success);
	            SimpleServerMessageBuffer roomFwd = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("RoomForwardMessageComposer"));
	            roomFwd.AppendInteger(randomRoom.Id);
	            Session.SendMessage(roomFwd);
	        }
	    }

	     void HotelViewRequestBadge()
	    {
	        string name = Request.GetString();
	        Dictionary<string, string> hotelViewBadges = Yupi.GetGame().GetHotelView().HotelViewBadges;
	        if (!hotelViewBadges.ContainsKey(name))
	            return;
	        string badge = hotelViewBadges[name];
	        Session.GetHabbo().GetBadgeComponent().GiveBadge(badge, true, Session, true);
	    }

	     void GetCameraPrice()
	    {
	        GetResponse().Init(PacketLibraryManager.OutgoingHandler("SetCameraPriceMessageComposer"));
	        GetResponse().AppendInteger(0); //credits
	        GetResponse().AppendInteger(10); //duckets
	        SendResponse();
	    }

	     void GetHotelViewHallOfFame()
	    {
	        string code = Request.GetString();
	        GetResponse().Init(PacketLibraryManager.OutgoingHandler("HotelViewHallOfFameMessageComposer"));
	        GetResponse().AppendString(code);
	        IEnumerable<HallOfFameElement> rankings = Yupi.GetGame().GetHallOfFame().Rankings.Where(e => e.Competition == code);
	        GetResponse().StartArray();
	        int rank = 1;
	        foreach (HallOfFameElement element in rankings)
	        {
	            Habbo user = Yupi.GetHabboById(element.UserId);
	            if (user == null) continue;

	            GetResponse().AppendInteger(user.Id);
	            GetResponse().AppendString(user.UserName);
	            GetResponse().AppendString(user.Look);
	            GetResponse().AppendInteger(rank);
	            GetResponse().AppendInteger(element.Score);
	            rank++;
	            GetResponse().SaveArray();
	        }
	        GetResponse().EndArray();
	        SendResponse();
	    }
	}
	}