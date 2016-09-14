// ---------------------------------------------------------------------------------
// <copyright file="TalentsTrackMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.User
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class TalentsTrackMessageComposer : Yupi.Messages.Contracts.TalentsTrackMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, TalentType trackType, IList<Talent> talents)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(trackType.DisplayName);
                message.AppendInteger(talents.Count);

                int failLevel = -1;

                foreach (Talent current in talents)
                {
                    message.AppendInteger(current.Id);
                    int nm = failLevel == -1 ? 1 : 0; // TODO What does this mean?
                    message.AppendInteger(nm);

                    message.AppendInteger(current.Levels.Count);

                    foreach (TalentLevel child in current.Levels)
                    {
                        /*  UserAchievement achievment = child.Achievement;

                            int num;
                            // TODO Refactor What does num mean?!
                            if (failLevel != -1 && failLevel < child.Level) {
                                num = 0;
                            } else if (achievment == null) {
                                num = 1;
                            } else if (achievment.Level >=
                                child.AchievementLevel) {
                                num = 2;
                            } else {
                                num = 1;
                            }

                            message.AppendInteger (child.Achievement.Id);
                            message.AppendInteger (0); // TODO Magic constant

                            message.AppendString(child.Achievement.GroupName+child.Achievement.DefaultLevel);
                            message.AppendInteger(num);

                            message.AppendInteger(achievment != null ? achievment.Progress : 0);
                            message.AppendInteger(child.GetAchievement() == null ? 0
                                : child.GetAchievement().Levels[child.AchievementLevel].Requirement);

                            if (num != 2 && failLevel == -1)
                                failLevel = child.Level;
                                */
                    }

                    throw new NotImplementedException();

                    message.AppendInteger(0); // TODO Magic constant

                    message.AppendInteger(1); // Count
                    message.AppendString(""); // Prize name ( HABBO_CLUB_VIP_7_DAYS )
                    message.AppendInteger(0); // Prize value (7)
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}