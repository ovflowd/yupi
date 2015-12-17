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
using Yupi.Data;
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
        private int _actionTimer;

        /// <summary>
        ///     The _energy timer
        /// </summary>
        private int _energyTimer;

        /// <summary>
        ///     The _speech timer
        /// </summary>
        private int _speechTimer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PetBot" /> class.
        /// </summary>
        /// <param name="virtualId">The virtual identifier.</param>
        internal PetBot(int virtualId)
        {
            _speechTimer = new Random((virtualId ^ 2) + DateTime.Now.Millisecond).Next(10, 60);
            _actionTimer = new Random((virtualId ^ 2) + DateTime.Now.Millisecond).Next(10, 30 + virtualId);
            _energyTimer = new Random((virtualId ^ 2) + DateTime.Now.Millisecond).Next(10, 60);
        }

        /// <summary>
        ///     Called when [self enter room].
        /// </summary>
        internal override void OnSelfEnterRoom()
        {
            Point randomWalkableSquare = GetRoom().GetGameMap().GetRandomWalkableSquare();

            if (GetRoomUser() != null && GetRoomUser().PetData.Type != 16u)
                GetRoomUser().MoveTo(randomWalkableSquare.X, randomWalkableSquare.Y);
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

            if (!command.PetTypes.Contains(roomUser.PetData.Type.ToString()))
                return;

            if (roomUser.PetData.Level < command.MinLevel)
                return;

            if (roomUser.PetData.Energy < 20 || roomUser.PetData.Nutrition < 25)
                command.CommandAction = "lazy";

            RemovePetStatus();

            roomUser.PetData.AddExperience((int)command.GainedExperience);

            roomUser.Statusses.Add(command.PetStatus, string.Empty);
            roomUser.Statusses.Add("gst", command.PetGesture);

            roomUser.UpdateNeeded = true;

            _actionTimer = 25;
            _energyTimer = 10;

            roomUser.FollowingOwner = null;

            SubtractAttributes();

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
                        case 3:
                            coord = GetRoom().GetRoomItemHandler().GetRandomBreedingTerrier(roomUser.PetData);
                            break;

                        case 4:
                            coord = GetRoom().GetRoomItemHandler().GetRandomBreedingBear(roomUser.PetData);
                            break;
                    }

                    if (coord == new Point())
                    {
                        ServerMessage alert = new ServerMessage(LibraryParser.OutgoingRequest("PetBreedErrorMessageComposer"));

                        alert.AppendInteger(0);

                        user.GetClient().SendMessage(alert);
                    }

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

                    IEnumerable<RoomItem> petNest = GetRoom().GetRoomItemHandler().FloorItems.Values.Where(x => x.GetBaseItem().InteractionType == Interaction.PetNest);

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
                    string[] valueDefault = PetLocale.GetValue("pet.done");
                    string messageDefault = valueDefault[new Random().Next(0, valueDefault.Length - 1)];

                    roomUser.Chat(null, messageDefault, false, 0);
                    break;
            }
        }

        /// <summary>
        ///     Called when [timer tick].
        /// </summary>
        internal override void OnTimerTick()
        {
            if (_speechTimer <= 0)
            {
                RoomUser roomUser = GetRoomUser();

                if (roomUser != null)
                {
                    if (roomUser.PetData.DbState != DatabaseUpdateState.NeedsInsert)
                        roomUser.PetData.DbState = DatabaseUpdateState.NeedsUpdate;

                    Random random = new Random();

                    RemovePetStatus();

                    string[] value = PetLocale.GetValue($"speech.pet{roomUser.PetData.Type}");

                    string text = value[random.Next(0, value.Length - 1)];

                    if (GetRoom() != null && !GetRoom().MutedPets)
                        roomUser.Chat(null, text, false, 0);
                    else
                        roomUser.Statusses.Add(text, ServerUserChatTextHandler.GetString(roomUser.Z));
                }

                _speechTimer = Yupi.GetRandomNumber(20, 120);
            }
            else
                _speechTimer--;

            if (_actionTimer <= 0 && GetRoomUser() != null)
            {
                try
                {
                    _actionTimer = GetRoomUser().FollowingOwner != null ? 2 : Yupi.GetRandomNumber(15, 40 + GetRoomUser().PetData.VirtualId);

                    RemovePetStatus();

                    _actionTimer = Yupi.GetRandomNumber(15, 40 + GetRoomUser().PetData.VirtualId);

                    if (GetRoomUser().RidingHorse != true)
                    {
                        RemovePetStatus();

                        if (GetRoomUser().FollowingOwner != null)
                        {
                            GetRoomUser().MoveTo(GetRoomUser().FollowingOwner.SquareBehind);
                        }
                        else
                        {
                            if (GetRoomUser().PetData.Type == 16) return; //Monsterplants can't move
                            Point nextCoord = GetRoom().GetGameMap().GetRandomValidWalkableSquare();
                            GetRoomUser().MoveTo(nextCoord.X, nextCoord.Y);
                        }
                    }

                    if (new Random().Next(2, 15) % 2 == 0)
                    {
                        if (GetRoomUser().PetData.Type == 16)
                        {
                            MoplaBreed breed = GetRoomUser().PetData.MoplaBreed;
                            GetRoomUser().PetData.Energy--;
                            GetRoomUser().AddStatus("gst", breed.LiveState == MoplaState.Dead ? "sad" : "sml");
                            GetRoomUser()
                                .PetData.MoplaBreed.OnTimerTick(GetRoomUser().PetData.LastHealth,
                                    GetRoomUser().PetData.UntilGrown);
                        }
                        else
                        {
                            if (GetRoomUser().PetData.Energy < 30) GetRoomUser().AddStatus("lay", "");
                            else
                            {
                                GetRoomUser().AddStatus("gst", "joy");
                                if (new Random().Next(1, 7) == 3) GetRoomUser().AddStatus("snf", "");
                            }
                        }
                        GetRoomUser().UpdateNeeded = true;
                    }

                    goto IL_1B5;
                }
                catch (Exception pException)
                {
                    ServerLogManager.HandleException(pException, "PetBot.OnTimerTick");

                    goto IL_1B5;
                }
            }

            _actionTimer--;

            IL_1B5:

            if (_energyTimer <= 0)
            {
                RemovePetStatus();

                RoomUser roomUser2 = GetRoomUser();

                roomUser2?.PetData.PetEnergy(true);

                _energyTimer = Yupi.GetRandomNumber(30, 120);

                return;
            }

            _energyTimer--;
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
        private void SubtractAttributes()
        {
            RoomUser roomUser = GetRoomUser();

            if (roomUser == null)
                return;

            if (roomUser.PetData.Energy < 11)
                roomUser.PetData.Energy = 0;
            else
                roomUser.PetData.Energy -= 10;

            if (roomUser.PetData.Nutrition < 6)
                roomUser.PetData.Nutrition = 0;
            else
                roomUser.PetData.Nutrition -= 5;
        }
    }
}