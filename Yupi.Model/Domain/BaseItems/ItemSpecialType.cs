#region Header

// ---------------------------------------------------------------------------------
// <copyright file="InteractionType.cs" company="https://github.com/sant0ro/Yupi">
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

#endregion Header

namespace Yupi.Model
{
    using System;

    #region Enumerations

    /// <summary>
    /// These are the interaction types as listed in furnidata (specialtype)
    /// </summary>
    public enum ItemSpecialType
    {
        Default = 1,
        RoomWall = 2,
        RoomFloor = 3,
        RoomLandscape = 4,
        PostIt = 5,

        // (external_texts: poster_" + _loc5_.§_-6AY§.getLegacyString() + "_name)
        Poster = 6,

        SoundSet = 7,
        SongDisk = 8,
        Present = 9,
        EcotronBox = 10,
        Trophy = 11,

        // unused / CF_1200_ny16 ()
        Unknown = 12,

        // (<customparams>15 Blue</customparams>)
        LegacyHorseDye = 13,

        HorseHairstyle = 14,

        //  (<customparams>15 2,3 53,53</customparams>)
        HorseDye = 15,
        HorseSaddle = 16,
        GroupItem = 17,
        SnowWar = 18,
        MonsterplantSeed = 19,
        MonsterplantRevive = 20,
        MonsterplantReseed = 21,
        MonsterplantFertilizer = 22,
        Clothing = 23
    }

    #endregion Enumerations
}