// ---------------------------------------------------------------------------------
// <copyright file="HotelViewHallOfFameMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Protocol.Buffers;

    public class HotelViewHallOfFameMessageComposer : Yupi.Messages.Contracts.HotelViewHallOfFameMessageComposer
    {
        #region Fields

        private IRepository<HallOfFameElement> FameRepository;

        #endregion Fields

        #region Constructors

        public HotelViewHallOfFameMessageComposer()
        {
            FameRepository = DependencyFactory.Resolve<IRepository<HallOfFameElement>>();
        }

        #endregion Constructors

        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string code)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(code);

                List<HallOfFameElement> rankings =
                    FameRepository.FilterBy(x => x.Competition == code).OrderByDescending(x => x.Score).ToList();

                message.AppendInteger(rankings.Count);

                for (int rank = 1; rank <= rankings.Count; ++rank)
                {
                    HallOfFameElement element = rankings[rank - 1];
                    message.AppendInteger(element.User.Id);
                    message.AppendString(element.User.Name);
                    message.AppendString(element.User.Look);
                    message.AppendInteger(rank);
                    message.AppendInteger(element.Score);
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}