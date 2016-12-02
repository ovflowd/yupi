// ---------------------------------------------------------------------------------
// <copyright file="PetInfo.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    using Yupi.Model.Domain.Components;

    public class PetInfo : BaseInfo
    {
        #region Fields

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

        #endregion Fields

        #region Properties

        [Ignore]
        public virtual int Age
        {
            get { return (int) (DateTime.Now - CreatedAt).TotalDays; }
        }

        public virtual Vector3 BreadingTile
        {
            get; set;
        }

        [Required]
        public virtual string Color
        {
            get; set;
        }

        [Required]
        public virtual DateTime CreatedAt
        {
            get; set;
        }

        [Required]
        public virtual int Energy
        {
            get; set;
        }

        [Required]
        public virtual int Experience
        {
            get; set;
        }

        [Ignore]
        public virtual int ExperienceGoal
        {
            get { return ExperienceLevels[Level - 1]; }
        }

        [Required]
        public virtual int Hair
        {
            get; set;
        }

        [Required]
        public virtual int HairDye
        {
            get; set;
        }

        [Required]
        public virtual bool HaveSaddle
        {
            get; set;
        }

        [Required]
        public virtual DateTime LastHealth
        {
            get; set;
        }

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

        // TODO Refactor looks!
        [Ignore]
        public virtual string Look
        {
            get {
                throw new NotImplementedException ();
                //return string.Concat(RaceId, " ", Race, " ", Color); 
            }
        }

        [Required]
        public virtual int Nutrition
        {
            get; set;
        }

        [Required]
        public virtual UserInfo Owner
        {
            get; set;
        }

        public virtual Vector3 Position
        {
            get; set;
        }

        // TODO seems to be the same...
        [Required]
        public virtual int Race
        {
            get; set;
        }

        [Required]
        public virtual int Rarity
        {
            get; set;
        }

        [Required]
        public virtual int Respect
        {
            get; set;
        }

        public virtual RoomData Room
        {
            get; set;
        }

        [Required]
        public virtual string Type
        {
            get; set;
        }

        #endregion Properties

        #region Other

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

        #endregion Other
    }
}