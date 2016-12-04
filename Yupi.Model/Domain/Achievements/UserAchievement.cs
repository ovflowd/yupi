// ---------------------------------------------------------------------------------
// <copyright file="UserAchievement.cs" company="https://github.com/sant0ro/Yupi">
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

    /// <summary>
    ///     Class UserAchievement.
    /// </summary>
    public class UserAchievement
    {
        #region Properties

        [Required]
        public virtual Achievement Achievement
        {
            get; set;
        }

        [Required]
        public virtual int Id
        {
            get; protected set;
        }

        [Ignore]
        public virtual AchievementLevel Level {
            get; protected set;
        }

        [Required]
        public virtual int InternalLevel {
            get {
                return Level.Level;
            }
            protected set {
                Level = Achievement.Levels [value];
            }
        }

        [Required]
        public virtual int Progress
        {
            get; set;
        }

        [Ignore]
        public virtual Badge Badge {
            get {
                return new Badge {
                    Code = "ACH_" + Achievement.DisplayName + Level.Level
                };
            }
        }

        #endregion Properties

        public UserAchievement (Achievement achievement)
        {
            #pragma warning disable RECS0021 // Warns about calls to virtual member functions occuring in the constructor
            this.Achievement = achievement;
            Level = achievement.DefaultLevel();
            #pragma warning restore RECS0021 // Warns about calls to virtual member functions occuring in the constructor
        }

        protected UserAchievement ()
        {

        }

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