// ---------------------------------------------------------------------------------
// <copyright file="GiveRespectMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class GiveRespectMessageEvent : AbstractHandler
    {
        #region Fields

        private AchievementManager AchievementManager;

        #endregion Fields

        #region Constructors

        public GiveRespectMessageEvent()
        {
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            Room room = session.Room;

            // TODO Should lock respect points
            if (room == null || session.Info.Respect.DailyRespectPoints <= 0)
                return;

            int userId = message.GetInteger();

            if (userId == session.Info.Id)
            {
                return;
            }

            UserEntity roomUserByHabbo = room.GetEntity(userId) as UserEntity;

            if (roomUserByHabbo == null)
                return;

            AchievementManager.ProgressUserAchievement(session, SocialAchievement.RespectGiven);
            AchievementManager.ProgressUserAchievement(roomUserByHabbo.User, SocialAchievement.RespectEarned);

            session.Info.Respect.DailyRespectPoints--;
            roomUserByHabbo.User.Info.Respect.Respect++;

            room.EachUser(
                (roomSession) =>
                {
                    roomSession.Router.GetComposer<GiveRespectsMessageComposer>()
                        .Compose(roomSession, roomUserByHabbo.Id, roomUserByHabbo.UserInfo.Respect.Respect);
                }
            );
        }

        #endregion Methods
    }
}