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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Model.Groups;
using FluentNHibernate.Data;
using Yupi.Model.Users;
using Yupi.Model.Navigator;


namespace Yupi.Model.Rooms
{
	public enum RoomState
	{
		OPEN,
		BELL,
		LOCKED
	}

	public class RoomData : Entity
	{
		public virtual RoomModel Model { get; set; }

		/// <summary>
		///     Allow Pets in Room
		/// </summary>
		public virtual bool AllowPets { get; set; }

		/// <summary>
		///     Allow Other Users Pets Eat in Room
		/// </summary>
		public virtual bool AllowPetsEating { get; set; }

		/// <summary>
		///     Allow Users Walk Through Other Users
		/// </summary>
		public virtual bool AllowWalkThrough { get; set; }

		/// <summary>
		///     Hide Wall in Room
		/// </summary>
		public virtual bool HideWall { get; set; }

		/// <summary>
		///     Allow Override Room Rights
		/// </summary>
		public virtual bool AllowRightsOverride { get; set; }

		/// <summary>
		///     Room Category
		/// </summary>
		public virtual NavigatorCategory Category { get; set; }

		/// <summary>
		///     Chat Balloon Type
		/// </summary>
		public virtual uint ChatBalloon { get; set; }

		/// <summary>
		///     Chat Walk Speed
		/// </summary>
		public virtual uint ChatSpeed { get; set; }

		/// <summary>
		///     Chat Max Distance each Other Message
		/// </summary>
		public virtual uint ChatMaxDistance { get; set; }

		/// <summary>
		///     Chat Flood Protection
		/// </summary>
		public virtual uint ChatFloodProtection { get; set; }

		/// <summary>
		///     Room Chat Type
		/// </summary>
		public virtual int ChatType { get; set; }

		/// <summary>
		///     Is Room In Competition
		/// </summary>
		public virtual int CompetitionStatus { get; set; }

		/// <summary>
		///     Amount of Votes of the Room
		/// </summary>
		public virtual int CompetitionVotes { get; set; }

		/// <summary>
		///     Room Description
		/// </summary>
		public virtual string Description { get; set; }

		/// <summary>
		///     Room Event
		/// </summary>
		public virtual RoomEvent Event { get; set; }

		/// <summary>
		///     Floor String?
		/// </summary>
		public virtual string Floor { get; set; }

		/// <summary>
		///     Room Flor Thickness
		/// </summary>
		public virtual int FloorThickness { get; set; }

		public virtual Group Group { get; set; }

		/// <summary>
		///     Landscape String
		/// </summary>
		public virtual string LandScape { get; set; }

		/// <summary>
		///     Room Name
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		///     Room Owner
		/// </summary>
		public virtual Habbo Owner { get; set; }
		/// <summary>
		///     Room Password
		/// </summary>
		public virtual string Password { get; set; }

		/// <summary>
		///     Room Score
		/// </summary>
		public virtual int Score { get; set; }

		/// <summary>
		///     Room Locked State
		/// </summary>
		public virtual RoomState State { get; set; }

		/// <summary>
		///     Room Tags
		/// </summary>
		public virtual IList<string> Tags { get; set; }

		/// <summary>
		///     Room Trade State
		/// </summary>
		public virtual int TradeState { get; set; }

		/// <summary>
		///     Room Type
		/// </summary>
		public virtual string Type { get; set; }

		/// <summary>
		///     Amount of Users on Room
		/// </summary>
		public virtual uint UsersNow { get; set; }

		/// <summary>
		///     Max Amount of Users on Room
		/// </summary>
		public virtual uint UsersMax { get; set; }

		/// <summary>
		///     Room Wall Height
		/// </summary>
		public virtual int WallHeight { get; set; }

		/// <summary>
		///     The wall paper
		/// </summary>
		public virtual string WallPaper { get; set; }

		/// <summary>
		///     Room Wall Tchickness
		/// </summary>
		public virtual int WallThickness { get; set; }

		/// <summary>
		///     Who Can Ban Users in Room
		/// </summary>
		public virtual int WhoCanBan { get; set; }

		/// <summary>
		///     Who Can Kick Users in Room
		/// </summary>
		public virtual int WhoCanKick { get; set; }

		/// <summary>
		///     Who Can Mute Users in Room
		/// </summary>
		public virtual int WhoCanMute { get; set; }

		/// <summary>
		///     Room Private Black Words
		/// </summary>
		public virtual IList<string> WordFilter { get; set; }

		/// <summary>
		///     Fills the null.
		/// </summary>
		/// <param name="id">The identifier.</param>
		public RoomData ()
		{
			Name = "Unknown Room";
			Description = "-";
			Type = "private";
			UsersNow = 0;
			UsersMax = 0;
			Score = 0;
			Tags = new List<string> ();
			AllowPets = true;
			AllowPetsEating = false;
			AllowWalkThrough = true;
			HideWall = false;
			Password = string.Empty;
			WallPaper = "0.0";
			Floor = "0.0";
			LandScape = "0.0";
			WallThickness = 0;
			FloorThickness = 0;
			Group = null;
			AllowRightsOverride = false;
			Event = null;
			WhoCanBan = 0;
			WhoCanKick = 0;
			WhoCanMute = 0;
			TradeState = 2;
			State = 0;
			WordFilter = new List<string> ();
			WallHeight = 0;
			CompetitionStatus = 0;
		}
			
		// TODO Remove when not used anymore
		/*
        /// <summary>
        ///     Serializes the specified messageBuffer.
        /// </summary>
        /// <param name="messageBuffer">The messageBuffer.</param>
        /// <param name="showEvents">if set to <c>true</c> [show events].</param>
        /// <param name="enterRoom"></param>
     public void Serialize(SimpleServerMessageBuffer messageBuffer, bool showEvents = false, bool enterRoom = false) 
		{
			messageBuffer.AppendInteger(data.Id);
			messageBuffer.AppendString(data.Name);
			messageBuffer.AppendInteger(data.OwnerId);
			messageBuffer.AppendString(data.Owner);
			messageBuffer.AppendInteger(data.State);
			messageBuffer.AppendInteger(data.UsersNow);
			messageBuffer.AppendInteger(data.UsersMax);
			messageBuffer.AppendString(data.Description);
			messageBuffer.AppendInteger(data.TradeState);
			messageBuffer.AppendInteger(data.Score);
			messageBuffer.AppendInteger(0);
			messageBuffer.AppendInteger(data.Category > 0 ? data.Category : 0);
			messageBuffer.AppendInteger(data.TagCount);

			foreach (string current in data.Tags.Where(current => current != null))
				messageBuffer.AppendString(current);

			string imageData = null;

			int enumType = enterRoom ? 32 : 0;

			PublicItem publicItem = Yupi.GetGame()?.GetNavigator()?.GetPublicRoom(data.Id);

			if (!string.IsNullOrEmpty(publicItem?.Image))
			{
				imageData = publicItem.Image;

				enumType += 1;
			}

			if (data.Group != null)
				enumType += 2;

			if (showEvents && data.Event != null)
				enumType += 4;

			if (data.Type == "private")
				enumType += 8;

			if (data.AllowPets)
				enumType += 16;

			messageBuffer.AppendInteger(enumType);

			if (imageData != null)
				messageBuffer.AppendString(imageData);

			if (data.Group != null)
			{
				messageBuffer.AppendInteger(data.Group.Id);
				messageBuffer.AppendString(data.Group.Name);
				messageBuffer.AppendString(data.Group.Badge);
			}

			if (showEvents && data.Event != null)
			{
				messageBuffer.AppendString(data.Event.Name);
				messageBuffer.AppendString(data.Event.Description);
				messageBuffer.AppendInteger((int)Math.Floor((data.Event.Time - Yupi.GetUnixTimeStamp()) / 60.0));
			}
		}*/
	}
}