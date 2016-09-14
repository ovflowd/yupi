// ---------------------------------------------------------------------------------
// <copyright file="UpdateMoodlightMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Items
{
    using System;
    using System.Text.RegularExpressions;

    public class UpdateMoodlightMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session, true) || room.MoodlightData == null)
                return;

            RoomItem item = room.GetRoomItemHandler().GetItem(room.MoodlightData.ItemId);

            if (item == null || item.GetBaseItem().InteractionType != Interaction.Dimmer)
                return;

            int preset = request.GetInteger();

            // TODO Meaning?
            int num2 = request.GetInteger();

            string color = request.GetString();
            int intensity = request.GetInteger();
            bool bgOnly = num2 >= 2;

            if (!IsValidColor (color) || !IsValidIntensity (intensity)) {
                return;
            }

            room.MoodlightData.Enabled = true;

            room.MoodlightData.CurrentPreset = preset;
            room.MoodlightData.UpdatePreset(preset, color, intensity, bgOnly);

            item.ExtraData = room.MoodlightData.GenerateExtraData();
            item.UpdateState();
            */
        }

        private bool IsValidColor(string hexRGB)
        {
            return Regex.IsMatch(hexRGB, "^#[0-9A-F]{6}$", RegexOptions.IgnoreCase);
        }

        private bool IsValidIntensity(int intensity)
        {
            return 0 <= intensity && intensity <= 255;
        }

        #endregion Methods
    }
}