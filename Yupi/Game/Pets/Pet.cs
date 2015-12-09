using System;
using System.Collections.Generic;
using System.Drawing;
using Yupi.Game.Pets.Enums;
using Yupi.Game.Rooms;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Pets
{
    /// <summary>
    ///     Class Pet.
    /// </summary>
    internal class Pet
    {
        /// <summary>
        ///     The anyone can ride
        /// </summary>
        internal int AnyoneCanRide;

        /// <summary>
        ///     The breading tile
        /// </summary>
        internal Point BreadingTile;

        /// <summary>
        ///     The color
        /// </summary>
        internal string Color;

        /// <summary>
        ///     The creation stamp
        /// </summary>
        internal double CreationStamp;

        /// <summary>
        ///     The database state
        /// </summary>
        internal DatabaseUpdateState DbState;

        /// <summary>
        ///     The energy
        /// </summary>
        internal int Energy;

        /// <summary>
        ///     The experience
        /// </summary>
        internal int Experience;

        /// <summary>
        ///     The experience levels
        /// </summary>
        internal int[] ExperienceLevels =
        {
            100,
            200,
            400,
            600,
            1000,
            1300,
            1800,
            2400,
            3200,
            4300,
            7200,
            8500,
            10100,
            13300,
            17500,
            23000,
            51900
        };

        /// <summary>
        ///     The hair dye
        /// </summary>
        internal int HairDye;

        /// <summary>
        ///     The have saddle
        /// </summary>
        internal bool HaveSaddle;

        /// <summary>
        ///     The last health
        /// </summary>
        internal DateTime LastHealth;

        /// <summary>
        ///     The mopla breed
        /// </summary>
        internal MoplaBreed MoplaBreed;

        /// <summary>
        ///     The name
        /// </summary>
        internal string Name;

        /// <summary>
        ///     The nutrition
        /// </summary>
        internal int Nutrition;

        /// <summary>
        ///     The owner identifier
        /// </summary>
        internal uint OwnerId;

        /// <summary>
        ///     The pet commands
        /// </summary>
        internal Dictionary<short, bool> PetCommands;

        /// <summary>
        ///     The pet hair
        /// </summary>
        internal int PetHair;

        /// <summary>
        ///     The pet identifier
        /// </summary>
        internal uint PetId;

        /// <summary>
        ///     The placed in room
        /// </summary>
        internal bool PlacedInRoom;

        /// <summary>
        ///     The race
        /// </summary>
        internal string Race;

        /// <summary>
        ///     The rarity
        /// </summary>
        internal int Rarity;

        /// <summary>
        ///     The respect
        /// </summary>
        internal int Respect;

        /// <summary>
        ///     The room identifier
        /// </summary>
        internal uint RoomId;

        /// <summary>
        ///     The type
        /// </summary>
        internal uint Type;

        /// <summary>
        ///     The until grown
        /// </summary>
        internal DateTime UntilGrown;

        /// <summary>
        ///     The virtual identifier
        /// </summary>
        internal int VirtualId;

        /// <summary>
        ///     The waiting for breading
        /// </summary>
        internal uint WaitingForBreading;

        /// <summary>
        ///     The x
        /// </summary>
        internal int X;

        /// <summary>
        ///     The y
        /// </summary>
        internal int Y;

        /// <summary>
        ///     The z
        /// </summary>
        internal double Z;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Pet" /> class.
        /// </summary>
        /// <param name="petId">The pet identifier.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="race">The race.</param>
        /// <param name="color">The color.</param>
        /// <param name="experience">The experience.</param>
        /// <param name="energy">The energy.</param>
        /// <param name="nutrition">The nutrition.</param>
        /// <param name="respect">The respect.</param>
        /// <param name="creationStamp">The creation stamp.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="havesaddle">if set to <c>true</c> [havesaddle].</param>
        /// <param name="anyoneCanRide">The anyone can ride.</param>
        /// <param name="dye">The dye.</param>
        /// <param name="petHer">The pet her.</param>
        /// <param name="rarity">The rarity.</param>
        /// <param name="lastHealth">The last health.</param>
        /// <param name="untilGrown">The until grown.</param>
        /// <param name="moplaBreed">The mopla breed.</param>
        internal Pet(uint petId, uint ownerId, uint roomId, string name, uint type, string race, string color,
            int experience, int energy, int nutrition, int respect, double creationStamp, int x, int y, double z,
            bool havesaddle, int anyoneCanRide, int dye, int petHer, int rarity, DateTime lastHealth,
            DateTime untilGrown, MoplaBreed moplaBreed)
        {
            PetId = petId;
            OwnerId = ownerId;
            RoomId = roomId;
            Name = name;
            Type = type;
            Race = race;
            Color = color;
            Experience = experience;
            Energy = energy;
            Nutrition = nutrition;
            Respect = respect;
            CreationStamp = creationStamp;
            X = x;
            Y = y;
            Z = z;
            PlacedInRoom = false;
            DbState = DatabaseUpdateState.Updated;
            HaveSaddle = havesaddle;
            AnyoneCanRide = anyoneCanRide;
            PetHair = petHer;
            HairDye = dye;
            Rarity = rarity;
            LastHealth = lastHealth;
            UntilGrown = untilGrown;
            MoplaBreed = moplaBreed;
            PetCommands = PetCommandHandler.GetPetCommands(this);
            WaitingForBreading = 0;
        }

        /// <summary>
        ///     Gets the maximum level.
        /// </summary>
        /// <value>The maximum level.</value>
        internal static int MaxLevel => 20;

        /// <summary>
        ///     Gets the maximum energy.
        /// </summary>
        /// <value>The maximum energy.</value>
        internal static int MaxEnergy => 100;

        /// <summary>
        ///     Gets the maximum nutrition.
        /// </summary>
        /// <value>The maximum nutrition.</value>
        internal static int MaxNutrition => 150;

        /// <summary>
        ///     Gets the room.
        /// </summary>
        /// <value>The room.</value>
        internal Room Room => !IsInRoom ? null : Yupi.GetGame().GetRoomManager().GetRoom(RoomId);

        /// <summary>
        ///     Gets a value indicating whether this instance is in room.
        /// </summary>
        /// <value><c>true</c> if this instance is in room; otherwise, <c>false</c>.</value>
        internal bool IsInRoom => RoomId > 0u;

        /// <summary>
        ///     Gets the level.
        /// </summary>
        /// <value>The level.</value>
        internal int Level
        {
            get
            {
                {
                    for (var i = 0; i < ExperienceLevels.Length; i++)
                        if (Experience < ExperienceLevels[i])
                            return i + 1;
                    return ExperienceLevels.Length + 1;
                }
            }
        }

        /// <summary>
        ///     Gets the experience goal.
        /// </summary>
        /// <value>The experience goal.</value>
        internal int ExperienceGoal => ExperienceLevels[(Level - 1)];

        /// <summary>
        ///     Gets the age.
        /// </summary>
        /// <value>The age.</value>
        internal int Age
        {
            get
            {
                var creation = Yupi.UnixToDateTime(CreationStamp);
                return (int) (DateTime.Now - creation).TotalDays;
            }
        }

        /// <summary>
        ///     Gets the look.
        /// </summary>
        /// <value>The look.</value>
        internal string Look => string.Concat(Type, " ", Race, " ", Color);

        /// <summary>
        ///     Gets the name of the owner.
        /// </summary>
        /// <value>The name of the owner.</value>
        internal string OwnerName => Yupi.GetGame().GetClientManager().GetNameById(OwnerId);

        /// <summary>
        ///     Determines whether the specified command has command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns><c>true</c> if the specified command has command; otherwise, <c>false</c>.</returns>
        internal bool HasCommand(short command)
        {
            return PetCommands.ContainsKey(command) && PetCommands[command];
        }

        /// <summary>
        ///     Called when [respect].
        /// </summary>
        internal void OnRespect()
        {
            {
                Respect++;
                var ownerSession = Yupi.GetGame().GetClientManager().GetClientByUserId(OwnerId);
                if (ownerSession != null)
                    Yupi.GetGame()
                        .GetAchievementManager()
                        .ProgressUserAchievement(ownerSession, "ACH_PetRespectReceiver", 1);
                var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("RespectPetMessageComposer"));
                serverMessage.AppendInteger(VirtualId);
                serverMessage.AppendBool(true);
                Room.SendMessage(serverMessage);

                serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("PetRespectNotificationMessageComposer"));
                serverMessage.AppendInteger(1);
                serverMessage.AppendInteger(VirtualId);
                SerializeInventory(serverMessage);
                Room.SendMessage(serverMessage);

                if (DbState != DatabaseUpdateState.NeedsInsert)
                    DbState = DatabaseUpdateState.NeedsUpdate;
                if (Type != 16 && Experience <= 51900)
                    AddExperience(10);
                if (Type == 16)
                    Energy = 100;
                LastHealth = DateTime.Now.AddSeconds(129600.0);
            }
        }

        /// <summary>
        ///     Adds the experience.
        /// </summary>
        /// <param name="amount">The amount.</param>
        internal void AddExperience(int amount)
        {
            {
                var oldExperienceGoal = ExperienceGoal;
                Experience += amount;
                if (Experience >= 51900)
                    return;
                if (DbState != DatabaseUpdateState.NeedsInsert)
                    DbState = DatabaseUpdateState.NeedsUpdate;
                if (Room == null)
                    return;
                var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("AddPetExperienceMessageComposer"));
                serverMessage.AppendInteger(PetId);
                serverMessage.AppendInteger(VirtualId);
                serverMessage.AppendInteger(amount);
                Room.SendMessage(serverMessage);
                if (Experience < oldExperienceGoal)
                    return;
                var ownerSession = Yupi.GetGame().GetClientManager().GetClientByUserId(OwnerId);

                // Reset pet commands
                PetCommands.Clear();
                PetCommands = PetCommandHandler.GetPetCommands(this);

                if (ownerSession == null)
                    return;
                var levelNotify = new ServerMessage(LibraryParser.OutgoingRequest("NotifyNewPetLevelMessageComposer"));
                SerializeInventory(levelNotify, true);
                ownerSession.SendMessage(levelNotify);

                var tp = new ServerMessage();
                tp.Init(LibraryParser.OutgoingRequest("PetTrainerPanelMessageComposer"));
                tp.AppendInteger(PetId);

                var availableCommands = new List<short>();

                tp.AppendInteger(PetCommands.Count);
                foreach (var sh in PetCommands.Keys)
                {
                    tp.AppendInteger(sh);
                    if (PetCommands[sh])
                        availableCommands.Add(sh);
                }

                tp.AppendInteger(availableCommands.Count);
                foreach (var sh in availableCommands)
                    tp.AppendInteger(sh);
                ownerSession.SendMessage(tp);
            }
        }

        /// <summary>
        ///     Pets the energy.
        /// </summary>
        /// <param name="add">if set to <c>true</c> [add].</param>
        internal void PetEnergy(bool add)
        {
            {
                int num;
                if (add)
                {
                    if (Energy > 100)
                    {
                        Energy = 100;
                        return;
                    }
                    if (Energy > 85)
                        return;
                    if (Energy > 85)
                        num = MaxEnergy - Energy;
                    else
                        num = 10;
                }
                else
                    num = 15;
                if (num <= 4)
                    num = 15;
                var randomNumber = Yupi.GetRandomNumber(4, num);
                if (!add)
                {
                    Energy -= randomNumber;
                    if (Energy < 0)
                        Energy = 1;
                }
                else
                    Energy += randomNumber;
                if (DbState != DatabaseUpdateState.NeedsInsert)
                    DbState = DatabaseUpdateState.NeedsUpdate;
            }
        }

        /// <summary>
        ///     Serializes the inventory.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="levelAfterName">if set to <c>true</c> [level after name].</param>
        internal void SerializeInventory(ServerMessage message, bool levelAfterName = false)
        {
            message.AppendInteger(PetId);
            message.AppendString(Name);
            if (levelAfterName)
                message.AppendInteger(Level);
            message.AppendInteger(Type);
            message.AppendInteger(int.Parse(Race));
            message.AppendString((Type == 16u) ? "ffffff" : Color);
            message.AppendInteger((Type == 16u) ? 0u : Type);
            if (Type == 16u && MoplaBreed != null)
            {
                var array = MoplaBreed.PlantData.Substring(12).Split(' ');
                var array2 = array;
                foreach (var s in array2)
                    message.AppendInteger(int.Parse(s));
                message.AppendInteger(MoplaBreed.GrowingStatus);
                return;
            }
            message.AppendInteger(0);
            message.AppendInteger(0);
        }

        /// <summary>
        ///     Manages the gestures.
        /// </summary>
        internal void ManageGestures()
        {
        }

        /// <summary>
        ///     Serializes the information.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeInfo()
        {
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("PetInfoMessageComposer"));
            serverMessage.AppendInteger(PetId);
            serverMessage.AppendString(Name);
            if (Type == 16)
            {
                serverMessage.AppendInteger(MoplaBreed.GrowingStatus);
                serverMessage.AppendInteger(7);
            }
            else
            {
                serverMessage.AppendInteger(Level);
                serverMessage.AppendInteger(MaxLevel);
            }
            serverMessage.AppendInteger(Experience);
            serverMessage.AppendInteger(ExperienceGoal);
            serverMessage.AppendInteger(Energy);
            serverMessage.AppendInteger(MaxEnergy);
            serverMessage.AppendInteger(Nutrition);
            serverMessage.AppendInteger(MaxNutrition);
            serverMessage.AppendInteger(Respect);
            serverMessage.AppendInteger(OwnerId);
            serverMessage.AppendInteger(Age);
            serverMessage.AppendString(OwnerName);
            serverMessage.AppendInteger(Type == 16 ? 0 : 1);
            serverMessage.AppendBool(HaveSaddle);
            serverMessage.AppendBool(false); //mountingId
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(AnyoneCanRide);
            if (Type == 16) serverMessage.AppendBool(MoplaBreed.LiveState == MoplaState.Grown);
            else serverMessage.AppendBool(false);
            serverMessage.AppendBool(false);
            if (Type == 16) serverMessage.AppendBool(MoplaBreed.LiveState == MoplaState.Dead);
            else serverMessage.AppendBool(false);
            serverMessage.AppendInteger(Rarity);
            if (Type == 16u)
            {
                serverMessage.AppendInteger(129600);
                var lastHealthSeconds = (int) (LastHealth - DateTime.Now).TotalSeconds;
                var untilGrownSeconds = (int) (UntilGrown - DateTime.Now).TotalSeconds;

                if (lastHealthSeconds < 0) lastHealthSeconds = 0;
                if (untilGrownSeconds < 0) untilGrownSeconds = 0;

                switch (MoplaBreed.LiveState)
                {
                    case MoplaState.Dead:
                        serverMessage.AppendInteger(0);
                        serverMessage.AppendInteger(0);
                        break;

                    case MoplaState.Grown:
                        serverMessage.AppendInteger(lastHealthSeconds);
                        serverMessage.AppendInteger(0);
                        break;

                    default:
                        serverMessage.AppendInteger(lastHealthSeconds);
                        serverMessage.AppendInteger(untilGrownSeconds);
                        break;
                }
            }
            else
            {
                serverMessage.AppendInteger(-1);
                serverMessage.AppendInteger(-1);
                serverMessage.AppendInteger(-1);
            }

            serverMessage.AppendBool(true); // Allow breed?
            return serverMessage;
        }
    }
}