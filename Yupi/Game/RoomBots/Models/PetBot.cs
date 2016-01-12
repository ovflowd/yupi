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
using System.Drawing;
using System.Linq;
using Yupi.Core.Io;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Pets;
using Yupi.Game.Pets.Enums;
using Yupi.Game.Pets.Structs;
using Yupi.Game.Rooms.User;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.RoomBots.Models
{
    /// <summary>
    ///     Class PetBot.
    /// </summary>
    internal class PetBot : BaseBot
    {
        /// <summary>
        ///     The _action timer
        /// </summary>
        private uint _actionTimer;

        /// <summary>
        ///     The _energy timer
        /// </summary>
        private uint _energyTimer;

        /// <summary>
        ///     The _speech timer
        /// </summary>
        private uint _speechTimer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PetBot" /> class.
        /// </summary>
        /// <param name="virtualId">The virtual identifier.</param>
        internal PetBot(int virtualId)
        {
            _speechTimer = (uint) new Random((virtualId ^ 2) + DateTime.Now.Millisecond).Next(20, 80);
            _actionTimer = (uint) new Random((virtualId ^ 2) + DateTime.Now.Millisecond).Next(20, 50);
            _energyTimer = (uint) new Random((virtualId ^ 2) + DateTime.Now.Millisecond).Next(20, 80);
        }

        /// <summary>
        ///     Called when [self enter room].
        /// </summary>
        internal override void OnSelfEnterRoom()
        {
            RoomUser roomUser = GetRoomUser();

            if (roomUser == null)
                return;

            Point randomWalkableSquare = GetRoom().GetGameMap().GetRandomWalkableSquare();

            if (roomUser.PetData.Type != "pet_monster" && randomWalkableSquare.X != 0 && randomWalkableSquare.Y != 0)
                roomUser.MoveTo(randomWalkableSquare.X, randomWalkableSquare.Y);

            roomUser.UpdateNeeded = true;
        }

        /// <summary>
        ///     Called when [user enter room].
        /// </summary>
        /// <param name="user">The user.</param>
        internal override void OnUserEnterRoom(RoomUser user)
        {
            if (user.GetClient() == null || user.GetClient().GetHabbo() == null)
                return;

            RoomUser roomUser = GetRoomUser();

            if (roomUser == null || user.GetClient().GetHabbo().UserName != roomUser.PetData.OwnerName)
                return;

            Random random = new Random();

            string[] value = PetLocale.GetValue("welcome.speech.pet");
            string message = value[random.Next(0, value.Length - 1)];

            message += user.GetUserName();

            roomUser.Statusses.Clear();
            roomUser.UpdateNeeded = true;
            roomUser.Chat(null, message, false, 0);
        }

        /// <summary>
        ///     Called when [user say].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="msg">The MSG.</param>
        internal override void OnUserSay(RoomUser user, string msg)
        {
            RoomUser roomUser = GetRoomUser();

            if (roomUser.PetData.OwnerId != user.GetClient().GetHabbo().Id)
                return;

            if (string.IsNullOrEmpty(msg))
                msg = " ";

            PetCommand command = PetCommandHandler.GetPetCommandByInput(msg.Substring(1).ToLower());

            if (!command.PetTypes.Contains(roomUser.PetData.Type))
                return;

            if (roomUser.PetData.Level < command.MinLevel)
                return;

            RemovePetStatus();

            _actionTimer = 25;
            _energyTimer = 10;

            if (roomUser.PetData.Energy < command.LostEnergy && roomUser.PetData.Nutrition < 25 ||
                roomUser.PetData.Energy < command.LostEnergy)
            {
                roomUser.UpdateNeeded = true;

                string[] valueLazy = PetLocale.GetValue("pet.lazy");
                string messageLazy = valueLazy[new Random().Next(0, valueLazy.Length - 1)];

                roomUser.Chat(null, messageLazy, false, 0);

                return;
            }

            roomUser.UpdateNeeded = true;

            roomUser.PetData.AddExperience(command.GainedExperience);

            roomUser.Statusses.Add(command.PetStatus, string.Empty);
            roomUser.Statusses.Add("gst", command.PetGesture);

            roomUser.FollowingOwner = null;

            SubtractAttributes(command.LostEnergy);

            Random random = new Random();

            string[] value = PetLocale.GetValue(command.PetSpeech);
            string message = value[random.Next(0, value.Length - 1)];

            roomUser.Statusses.Clear();
            roomUser.Chat(null, message, false, 0);

            /* other gestures that isnt listed */

            // roomUser.Statusses.Add("jmp", "");
            //roomUser.Statusses.Add("gst", "joy");

            //roomUser.AddStatus("lay", "");
            //roomUser.AddStatus("gst", "eyb");

            //roomUser.Statusses.Add("beg", "");
            //roomUser.Statusses.Add("gst", "sml");

            switch (command.CommandAction)
            {
                case "follow":
                    roomUser.FollowingOwner = roomUser;

                    RemovePetStatus();

                    switch (roomUser.RotBody)
                    {
                        case 0:
                            roomUser.MoveTo(roomUser.X + 2, roomUser.Y);
                            break;

                        case 1:
                            roomUser.MoveTo(roomUser.X - 2, roomUser.Y - 2);
                            break;

                        case 2:
                            roomUser.MoveTo(roomUser.X, roomUser.Y + 2);
                            break;

                        case 3:
                            roomUser.MoveTo(roomUser.X + 2, roomUser.Y - 2);
                            break;

                        case 4:
                            roomUser.MoveTo(roomUser.X - 2, roomUser.Y);
                            break;

                        case 5:
                            roomUser.MoveTo(roomUser.X + 2, roomUser.Y + 2);
                            break;

                        case 6:
                            roomUser.MoveTo(roomUser.X, roomUser.Y - 2);
                            break;

                        case 7:
                            roomUser.MoveTo(roomUser.X - 2, roomUser.Y + 2);
                            break;
                    }

                    break;
                case "breed":
                    Point coord = new Point();

                    switch (roomUser.PetData.Type)
                    {
                        case "pet_terrier":
                            coord = GetRoom().GetRoomItemHandler().GetRandomBreedingTerrier(roomUser.PetData);
                            break;

                        case "pet_bear":
                            coord = GetRoom().GetRoomItemHandler().GetRandomBreedingBear(roomUser.PetData);
                            break;
                    }

                    if (coord == new Point())
                    {
                        ServerMessage alert = new ServerMessage(LibraryParser.OutgoingRequest("PetBreedErrorMessageComposer"));

                        alert.AppendInteger(0);

                        user.GetClient().SendMessage(alert);
                    }

                    roomUser.MoveTo(coord, true);

                    break;
                case "sleep":
                    string[] valueSleep = PetLocale.GetValue("tired");
                    string messageSleep = valueSleep[new Random().Next(0, valueSleep.Length - 1)];

                    roomUser.Chat(null, messageSleep, false, 0);
                    break;
                case "unknown":
                    string[] valueUnknown = PetLocale.GetValue("pet.unknowncommand");
                    string messageUnknown = valueUnknown[new Random().Next(0, valueUnknown.Length - 1)];

                    roomUser.Chat(null, messageUnknown, false, 0);
                    break;
                case "lazy":
                    string[] valueLazy = PetLocale.GetValue("pet.lazy");
                    string messageLazy = valueLazy[new Random().Next(0, valueLazy.Length - 1)];

                    roomUser.Chat(null, messageLazy, false, 0);
                    break;
                case "nest":
                    RemovePetStatus();

                    IEnumerable<RoomItem> petNest =
                        GetRoom()
                            .GetRoomItemHandler()
                            .FloorItems.Values.Where(x => x.GetBaseItem().InteractionType == Interaction.PetNest);

                    IEnumerable<RoomItem> enumerable = petNest as RoomItem[] ?? petNest.ToArray();

                    // @todo rewrite this to redo actionsss
                    if (!enumerable.Any())
                        command.CommandAction = "lazy";

                    RoomItem roomItems = enumerable.FirstOrDefault();

                    if (roomItems != null)
                        roomUser.MoveTo(roomItems.X, roomItems.Y);

                    roomUser.PetData.AddExperience(40);

                    break;
                case "default":
                    break;
            }
        }

        /// <summary>
        ///     Called when [timer tick].
        /// </summary>
        internal override void OnTimerTick()
        {
            RoomUser roomUser = GetRoomUser();

            if (roomUser == null)
                return;

            Random random = new Random();

            if (_actionTimer > 0)
                _actionTimer--;

            if (_speechTimer > 0)
                _speechTimer--;

            if (_energyTimer > 0)
                _energyTimer--;

            if (roomUser.X == roomUser.GoalX && roomUser.Y == roomUser.GoalY && roomUser.Statusses.ContainsKey("mv") && !roomUser.IsWalking)
                roomUser.ClearMovement();

            if (_speechTimer == 0)
            {
                _speechTimer = (uint) new Random().Next(20, 100);

                if (roomUser.PetData.DbState != DatabaseUpdateState.NeedsInsert)
                    roomUser.PetData.DbState = DatabaseUpdateState.NeedsUpdate;

                string[] value = PetLocale.GetValue($"speech.pet{roomUser.PetData.Type.Replace("pet", string.Empty)}");

                string text = value[random.Next(0, value.Length - 1)];

                if (GetRoom() != null && !GetRoom().MutedPets)
                    roomUser.Chat(null, text, false, 0);
                else
                    roomUser.Statusses.Add(text, ServerUserChatTextHandler.GetString(roomUser.Z));
            }

            if (_actionTimer == 0)
            {
                _actionTimer = (uint) random.Next(10, 40);

                if (roomUser.FollowingOwner != null)
                    _actionTimer = 2;

                if (!roomUser.RidingHorse)
                {
                    if (roomUser.PetData.Type == "pet_monster")
                        return;

                    if (roomUser.FollowingOwner != null)
                    {
                        roomUser.MoveTo(roomUser.FollowingOwner.SquareInFront);
                        roomUser.FollowingOwner = null;
                    }
                        
                    if (roomUser.FollowingOwner == null)
                    {
                        Point randomPoint = GetRoom().GetGameMap().GetRandomWalkableSquare();

                        if (randomPoint.X == 0 || randomPoint.Y == 0)
                            return;

                        roomUser.MoveTo(randomPoint.X, randomPoint.Y);
                    }
                }

                if (random.Next(2, 5)%2 == 0)
                {
                    RemovePetStatus();

                    switch (roomUser.PetData.Type)
                    {
                        case "pet_monster":
                        {
                            MoplaBreed breed = GetRoomUser().PetData.MoplaBreed;

                            roomUser.PetData.Energy--;

                            roomUser.AddStatus("gst", breed.LiveState == MoplaState.Dead ? "sad" : "sml");

                            roomUser.PetData.MoplaBreed.OnTimerTick(roomUser.PetData.LastHealth,
                                roomUser.PetData.UntilGrown);
                        }
                            break;
                        default:
                        {
                            if (roomUser.PetData.Energy < 30 || random.Next(2, 5)%2 == 0)
                                roomUser.AddStatus("lay", string.Empty);
                            else if ((roomUser.PetData.Energy < 30 && roomUser.PetData.Nutrition < 30) ||
                                     roomUser.PetData.Nutrition < 30 || random.Next(2, 5)%2 == 0)
                                roomUser.AddStatus("snf", string.Empty);
                            else if (GetRoomUser().PetData.Energy >= 30)
                                roomUser.AddStatus("gst", "joy");
                            else
                                roomUser.AddStatus("gst", "sml");
                            }
                            break;
                    }             
                }
            }

            if (_energyTimer == 0)
            {
                _energyTimer = (uint) random.Next(30, 120);

                roomUser.PetData.PetEnergy(true);
            }
        }

        /// <summary>
        ///     Removes the pet status.
        /// </summary>
        private void RemovePetStatus()
        {
            RoomUser roomUser = GetRoomUser();

            if (roomUser == null)
                return;

            roomUser.Statusses.Clear();
            roomUser.UpdateNeeded = true;
        }

        /// <summary>
        ///     Subtracts the attributes.
        /// </summary>
        private void SubtractAttributes(uint energy = 10, uint nutrition = 5)
        {
            RoomUser roomUser = GetRoomUser();

            if (roomUser == null)
                return;

            if (roomUser.PetData.Energy < 11)
                roomUser.PetData.Energy = 0;
            else
                roomUser.PetData.Energy -= energy;

            if (roomUser.PetData.Nutrition < 6)
                roomUser.PetData.Nutrition = 0;
            else
                roomUser.PetData.Nutrition -= nutrition;
        }
    }
}