// ---------------------------------------------------------------------------------
// <copyright file="UnlockAchievementMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Achievements
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public class UnlockAchievementMessageComposer : Yupi.Messages.Contracts.UnlockAchievementMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Achievement achievement, int level, int pointReward,
            int pixelReward)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(achievement.Id);
                message.AppendInteger(level);
                message.AppendInteger(144);
                message.AppendString($"{achievement.GroupName}{level}");
                message.AppendInteger(pointReward);
                message.AppendInteger(pixelReward);
                message.AppendInteger(0);
                message.AppendInteger(10);
                message.AppendInteger(21);
                message.AppendString(level > 1 ? $"{achievement.GroupName}{level - 1}" : string.Empty);
                message.AppendString(achievement.Category);
                message.AppendBool(true);

                session.Send(message);
            }
        }

        #endregion Methods
    }
}