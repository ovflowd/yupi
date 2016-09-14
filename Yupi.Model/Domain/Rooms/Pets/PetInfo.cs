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
using Yupi.Model.Domain.Components;
using System.Numerics;

namespace Yupi.Model.Domain
{
    public class PetInfo : BaseInfo
    {
        private int[] ExperienceLevels =
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

        #region Horse

        public virtual bool AnyoneCanRide { get; set; }

        public virtual int HairDye { get; set; }

        public virtual bool HaveSaddle { get; set; }

        #endregion

        public virtual Vector3 BreadingTile { get; set; }

        public virtual string Color { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual int Energy { get; set; }

        public virtual int Experience { get; set; }

        public virtual DateTime LastHealth { get; set; }

        public virtual uint Nutrition { get; set; }

        public virtual UserInfo Owner { get; set; }

        public virtual int Hair { get; set; }

        public virtual bool PlacedInRoom { get; set; }

        // TODO seems to be the same...
        public virtual int Race { get; set; }
        public virtual int RaceId { get; set; }

        public virtual int Rarity { get; set; }

        public virtual uint Respect { get; set; }

        public virtual RoomData Room { get; set; }

        public virtual string Type { get; set; }

        public virtual uint WaitingForBreading { get; set; }

        public virtual Vector3 Position { get; set; }

        [Ignore]
        public virtual int Level
        {
            get
            {
                // TODO Refactor
                for (int i = 0; i < ExperienceLevels.Length; i++)
                    if (Experience < ExperienceLevels[i])
                        return i + 1;

                return ExperienceLevels.Length + 1;
            }
        }

        [Ignore]
        public virtual int ExperienceGoal
        {
            get { return ExperienceLevels[Level - 1]; }
        }

        [Ignore]
        public virtual int Age
        {
            get { return (int) (DateTime.Now - CreatedAt).TotalDays; }
        }

        // TODO Refactor looks!
        [Ignore]
        public virtual string Look
        {
            get { return string.Concat(RaceId, " ", Race, " ", Color); }
        }

        /*
        public void OnRespect ()
        {
            Respect++;

            GameClient ownerSession = Yupi.GetGame ().GetClientManager ().GetClientByUserId (OwnerId);

            if (ownerSession != null)
                Yupi.GetGame ()
                    .GetAchievementManager ()
                    .ProgressUserAchievement (ownerSession, "ACH_PetRespectReceiver", 1);

            router.GetComposer<RespectPetMessageComposer> ().Compose (this.Room, this.VirtualId);
            router.GetComposer<PetRespectNotificationMessageComposer> ().Compose (this.Room, this);

            if (Type != "pet_monster" && Experience <= 51900)
                AddExperience (10);

            if (Type == "pet_monster")
                Energy = 100;

            LastHealth = DateTime.Now.AddSeconds (129600.0);
        }

        public void AddExperience (uint amount)
        {
            int oldExperienceGoal = ExperienceGoal;

            Experience += amount;

            if (Experience >= 51900)
                return;

            if (DbState != DatabaseUpdateState.NeedsInsert)
                DbState = DatabaseUpdateState.NeedsUpdate;

            if (Room == null)
                return;

            this.Room.Router.GetComposer<AddPetExperienceMessageComposer> ().Compose (this.Room, this, amount);

            if (Experience < oldExperienceGoal)
                return;

            GameClient ownerSession = Yupi.GetGame ().GetClientManager ().GetClientByUserId (OwnerId);

            Dictionary<uint, PetCommand> totalPetCommands = PetCommandHandler.GetAllPetCommands ();

            Dictionary<uint, PetCommand> petCommands = PetCommandHandler.GetPetCommandByPetType (Type);

            if (ownerSession == null)
                return;

            router.GetComposer<NotifyNewPetLevelMessageComposer> ().Compose (ownerSession, this);
            router.GetComposer<PetTrainerPanelMessageComposer> ().Compose (ownerSession, this.PetId);
        }

        public void PetEnergy (bool add)
        {
            uint num;

            if (add) {
                if (Energy > 100) {
                    Energy = 100;

                    return;
                }

                if (Energy > 85)
                    return;

                if (Energy > 85)
                    num = 100 - Energy;
                else
                    num = 10;
            } else
                num = 15;

            if (num <= 4)
                num = 15;

            uint randomNumber = (uint)Yupi.GetRandomNumber (4, (int)num);

            if (!add) {
                Energy -= randomNumber;

                if (Energy == 0)
                    Energy = 1;
            } else
                Energy += randomNumber;

            if (DbState != DatabaseUpdateState.NeedsInsert)
                DbState = DatabaseUpdateState.NeedsUpdate;
        }*/
    }
}