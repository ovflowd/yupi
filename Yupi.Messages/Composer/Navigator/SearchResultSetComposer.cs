// ---------------------------------------------------------------------------------
// <copyright file="SearchResultSetComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Navigator
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Yupi.Controller;
    using Yupi.Messages.Encoders;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Protocol.Buffers;

    // TODO Refactor
    public class SearchResultSetComposer : Yupi.Messages.Contracts.SearchResultSetComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string staticId, string query,
            IDictionary<NavigatorCategory, IList<RoomData>> results)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(staticId);
                message.AppendString(query);

                message.AppendInteger(results.Count);

                foreach (var result in results)
                {
                    message.AppendString(staticId);
                    message.AppendString(result.Key.Caption);
                    message.AppendInteger(1); // TODO actionAllowed ( 1 = Show More, 2 = Back)
                    message.AppendBool(!result.Key.IsOpened);
                    message.AppendInteger(result.Key.IsImage); // TODO ViewMode (Possible Values?)

                    // Room Count
                    message.AppendInteger(result.Value.Count);

                    foreach (RoomData roomData in result.Value)
                    {
                        message.Append(roomData);
                    }
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}