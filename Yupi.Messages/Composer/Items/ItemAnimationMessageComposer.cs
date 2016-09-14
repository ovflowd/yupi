// ---------------------------------------------------------------------------------
// <copyright file="ItemAnimationMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Items
{
    using System;
    using System.Drawing;
    using System.Globalization;

    using Yupi.Protocol.Buffers;

    public class ItemAnimationMessageComposer : Yupi.Messages.Contracts.ItemAnimationMessageComposer
    {
        #region Methods

        // TODO Refactor
        public override void Compose(Yupi.Protocol.ISender session, Tuple<Point, double> pos,
            Tuple<Point, double> nextPos, uint rollerId, uint affectedId, ItemAnimationMessageComposer.Type type)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(pos.Item1.X);
                message.AppendInteger(pos.Item1.Y);
                message.AppendInteger(nextPos.Item1.X);
                message.AppendInteger(nextPos.Item1.Y);

                message.AppendInteger((int) type);

                switch (type)
                {
                    case Type.ITEM:
                        message.AppendInteger(affectedId);
                        break;
                    case Type.USER:
                        message.AppendInteger(rollerId);
                        message.AppendInteger(2);
                        message.AppendInteger(affectedId);
                        break;
                }

                message.AppendString(pos.Item2.ToString(CultureInfo.InvariantCulture));
                message.AppendString(nextPos.Item2.ToString(CultureInfo.InvariantCulture));
                message.AppendInteger(rollerId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}