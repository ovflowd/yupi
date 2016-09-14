// ---------------------------------------------------------------------------------
// <copyright file="UserUpdateLookMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class UserUpdateLookMessageEvent : AbstractHandler
    {
        #region Fields

        private AchievementManager AchievementManager;
        private MessengerController MessengerController;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public UserUpdateLookMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
            MessengerController = DependencyFactory.Resolve<MessengerController>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            string gender = message.GetString();
            string look = message.GetString();

            // TODO Validate gender & look
            session.Info.Look = look;
            session.Info.Gender = gender;
            UserRepository.Save(session.Info);

            AchievementManager.ProgressUserAchievement(session, "ACH_AvatarLooks", 1);

            if (session.Room == null)
                return;

            router.GetComposer<UpdateAvatarAspectMessageComposer>().Compose(session, session.Info);

            session.Room.EachUser(
                (roomSession) =>
                {
                    roomSession.Router.GetComposer<UpdateUserDataMessageComposer>()
                        .Compose(roomSession, session.Info, session.RoomEntity.Id);
                }
            );

            MessengerController.UpdateUser(session.Info);
        }

        #endregion Methods
    }
}