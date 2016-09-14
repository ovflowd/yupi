// ---------------------------------------------------------------------------------
// <copyright file="RelationshipMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Protocol.Buffers;

    public class RelationshipMessageComposer : Yupi.Messages.Contracts.RelationshipMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo habbo)
        {
            // TODO Refactor

            int num = habbo.Relationships.Relationships.Count(x => x.Type == 1);
            int num2 = habbo.Relationships.Relationships.Count(x => x.Type == 2);
            int num3 = habbo.Relationships.Relationships.Count(x => x.Type == 3);

            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(habbo.Id);
                message.AppendInteger(habbo.Relationships.Relationships.Count);

                foreach (Relationship current in habbo.Relationships.Relationships)
                {
                    message.AppendInteger(current.Type);
                    message.AppendInteger(current.Type == 1 ? num : (current.Type == 2 ? num2 : num3));
                    message.AppendInteger(current.Friend.Id);
                    message.AppendString(current.Friend.Name);
                    message.AppendString(current.Friend.Look);
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}