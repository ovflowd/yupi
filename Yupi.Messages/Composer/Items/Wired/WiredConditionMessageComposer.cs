// ---------------------------------------------------------------------------------
// <copyright file="WiredConditionMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Wired
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class WiredConditionMessageComposer : Yupi.Messages.Contracts.WiredConditionMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, FloorItem item, List<FloorItem> list,
            string extraString)
        {
            // TODO Won't work properly. Must implement composer correctly...
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(false);
                message.AppendInteger(5);

                if (list == null)
                {
                    list = new List<FloorItem>();
                }
                message.AppendInteger(list.Count);
                foreach (FloorItem current20 in list)
                {
                    message.AppendInteger(current20.Id);
                }
                throw new NotImplementedException();
                //message.AppendInteger(item.BaseItem.SpriteId);
                message.AppendInteger(item.Id);
                message.AppendString(extraString);
                message.AppendInteger(3);

                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(0);

                message.AppendInteger(0);
                message.AppendInteger(0);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}