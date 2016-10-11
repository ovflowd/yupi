// ---------------------------------------------------------------------------------
// <copyright file="CreateRoomMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class CreateRoomMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<NavigatorCategory> NavigatorRepository;
        private IRepository<RoomData> RoomRepository;

        #endregion Fields

        #region Constructors

        public CreateRoomMessageEvent()
        {
            NavigatorRepository = DependencyFactory.Resolve<IRepository<NavigatorCategory>>();
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string name = request.GetString();
            string description = request.GetString();
            string roomModel = request.GetString();
            int categoryId = request.GetInteger();
            int maxVisitors = request.GetInteger();
            int tradeStateId = request.GetInteger();

            RoomModel model;
            TradingState tradeState;

            if (!RoomModel.TryParse(roomModel, out model)
                || !TradingState.TryFromInt32(tradeStateId, out tradeState))
            {
                return;
            }

            NavigatorCategory category = NavigatorRepository.Find(categoryId);

            if (category.MinRank > session.Info.Rank)
            {
                return;
            }

            // TODO Filter Name, Description, max visitors
            RoomData data = new RoomData()
            {
                Name = name,
                Description = description,
                Model = model,
                Category = category,
                UsersMax = maxVisitors,
                TradeState = tradeState,
                Owner = session.Info
            };

            RoomRepository.Save(data);

            router.GetComposer<OnCreateRoomInfoMessageComposer>().Compose(session, data);
        }

        #endregion Methods
    }
}