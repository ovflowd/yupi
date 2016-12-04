// ---------------------------------------------------------------------------------
// <copyright file="SocialAchievement.cs" company="https://github.com/sant0ro/Yupi">
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
using System;
namespace Yupi.Model.Domain
{
    public class SocialAchievement : Achievement
    {
        public static readonly Achievement GiftGiver = new SocialAchievement (0, "GiftGiver", new int [] {
            1,6, 14, 26,46,86,146,236,366,566,816,1141,1541,2041,2591
        });

        public static readonly Achievement GiftReceiver = new SocialAchievement (1, "GiftReceiver", new int [] {
            1,6, 14, 26,46,86,146,236,366,566
        });

        public static readonly Achievement RespectGiven = new SocialAchievement (2, "RespectGiven", new int [] {
           2,5,10,20,40,70,110,170,250,350,470,610,770,950,1150,1370,1610,1870,2150,2450
        });

        public static readonly Achievement RespectEarned = new SocialAchievement (3, "RespectEarned", new int [] {
           1, 6, 16, 66, 166, 366, 566, 766, 966, 1166
        });

        public static readonly Achievement FriendListSize = new SocialAchievement (4, "FriendListSize", new int [] {
           2, 10, 30, 85, 150,250,350,500,700,800,900,1000,1100
        });


        public override string Category {
            get {
                return "social";
            }
        }

        private SocialAchievement (int value, string displayName, int [] requirements, bool display = true) : base (value, displayName, requirements, ValuePrefix.Social, display)
        {
        }
    }
}
