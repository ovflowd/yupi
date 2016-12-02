// ---------------------------------------------------------------------------------
// <copyright file="Achievement.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using Headspring;
    using Yupi.Util;

    public abstract class Achievement : Enumeration<Achievement>
    {
        private static Lazy<Achievement []> _Achievements = new Lazy<Achievement []> (GetEnumeration);

        protected enum ValuePrefix
        {
            Explore,
            Profile,
            RoomBuilder,
            Social,
            Tools
        }

        protected Achievement (int value, string name, int [] requirements, ValuePrefix prefix, bool display = true) : base (((int)prefix)*100+value, name)
        {
            Display = display;
            Levels = new AchievementLevel [requirements.Length];

            for (int i = 0; i < requirements.Length; ++i) {
                Levels [i] = new AchievementLevel (i + 1, requirements [i]);
            }
        }

        public abstract string Category { get; }

        #region Properties

        public AchievementLevel [] Levels {
            get; protected set;
        }

        public readonly bool Display;

        #endregion Properties

        #region Methods

        private static Achievement [] GetEnumeration ()
        {
            var types = typeof (Achievement).GetAllDerivedTypes ();

            IEnumerable<Achievement> enumeration = new List<Achievement> ();

            foreach (var type in types) {
                var achievements = type
                    .GetFields (BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                    .Where (info => typeof (Achievement).IsAssignableFrom (info.FieldType))
                    .Select (info => info.GetValue (null))
                    .Cast<Achievement> ()
                    .Where (x => x.Display);
                enumeration.Concat (achievements);
            }

            return enumeration.ToArray ();
        }

        public static new IReadOnlyCollection<Achievement> GetAll ()
        {
            return Array.AsReadOnly (_Achievements.Value);
        }

        public AchievementLevel DefaultLevel ()
        {
            return Levels [0];
        }

        public int GetMaxLevel ()
        {
            return Levels.Length - 1;
        }

        public AchievementLevel NextLevel (AchievementLevel current)
        {
            if (current.Level == GetMaxLevel ()) {
                return current;
            }
            return Levels.Single (x => x.Level == current.Level + 1);
        }

        #endregion Methods
    }
}