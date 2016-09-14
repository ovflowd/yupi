#region Header

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

#endregion Header

namespace Yupi.Model.Domain
{
    using System;

    /// <summary>
    ///     Class UserAchievement.
    /// </summary>
    public class UserAchievement
    {
        #region Properties

        public virtual Achievement Achievement
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual AchievementLevel Level
        {
            get; set;
        }

        public virtual int Progress
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public virtual bool CanIncreaseLevel()
        {
            if (this.Level.Level + 1 > Achievement.GetMaxLevel())
            {
                return false;
            }

            return (Progress >= Achievement.NextLevel(Level).Requirement);
        }

        public virtual void IncreaseLevel()
        {
            if (!CanIncreaseLevel())
            {
                throw new InvalidOperationException();
            }

            Level = Achievement.NextLevel(Level);
            Progress = 0;
        }

        #endregion Methods
    }
}