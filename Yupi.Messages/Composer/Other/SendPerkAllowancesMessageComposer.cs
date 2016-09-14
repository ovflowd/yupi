// ---------------------------------------------------------------------------------
// <copyright file="SendPerkAllowancesMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Other
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class SendPerkAllowancesMessageComposer : Yupi.Messages.Contracts.SendPerkAllowancesMessageComposer
    {
        #region Methods

        // TODO Refactor (hardcoded)
        public override void Compose(Yupi.Protocol.ISender session, UserInfo info, bool enableBetaCamera)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(11); // Count

                message.AppendString("BUILDER_AT_WORK"); // Name
                message.AppendString(string.Empty); // Error Message
                message.AppendBool(true); // Allowed

                message.AppendString("VOTE_IN_COMPETITIONS");
                message.AppendString("requirement.unfulfilled.helper_level_2");
                message.AppendBool(false);

                // FIXME
                message.AppendString("USE_GUIDE_TOOL");
                message.AppendString("requirement.unfulfilled.helper_level_4");
                message.AppendBool(false);
                /*
                message.AppendString((info.TalentStatus == "helper" && info.CurrentTalentLevel >= 4) || (info.Rank >= 4) ? string.Empty : "requirement.unfulfilled.helper_level_4");
                message.AppendBool((info.TalentStatus == "helper" && info.CurrentTalentLevel >= 4) || (info.Rank >= 4));
            */
                message.AppendString("JUDGE_CHAT_REVIEWS");
                message.AppendString("requirement.unfulfilled.helper_level_6");
                message.AppendBool(false);

                message.AppendString("NAVIGATOR_ROOM_THUMBNAIL_CAMERA");
                message.AppendString(string.Empty);
                message.AppendBool(true);

                message.AppendString("CALL_ON_HELPERS");
                message.AppendString(string.Empty);
                message.AppendBool(true);

                message.AppendString("CITIZEN");
                message.AppendString(string.Empty);
                // FIXME
                message.AppendBool(false); // info.TalentStatus == "helper" || info.CurrentTalentLevel >= 4

                message.AppendString("MOUSE_ZOOM");
                message.AppendString(string.Empty);
                message.AppendBool(false);

                bool tradeLocked = info.CanTrade();

                message.AppendString("TRADE");
                message.AppendString(tradeLocked ? string.Empty : "requirement.unfulfilled.no_trade_lock");
                message.AppendBool(tradeLocked);

                message.AppendString("CAMERA");
                message.AppendString(string.Empty);
                message.AppendBool(enableBetaCamera);

                message.AppendString("NAVIGATOR_PHASE_TWO_2014");
                message.AppendString(string.Empty);
                message.AppendBool(true);

                session.Send(message);
            }
        }

        #endregion Methods
    }
}