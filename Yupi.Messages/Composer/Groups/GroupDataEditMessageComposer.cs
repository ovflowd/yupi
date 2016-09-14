// ---------------------------------------------------------------------------------
// <copyright file="GroupDataEditMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class GroupDataEditMessageComposer : Yupi.Messages.Contracts.GroupDataEditMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Group group)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0);
                message.AppendBool(true);
                message.AppendInteger(group.Id);
                message.AppendString(group.Name);
                message.AppendString(group.Description);
                message.AppendInteger(group.Room.Id);
                message.AppendInteger(group.Colour1.Colour);
                message.AppendInteger(group.Colour2.Colour);
                message.AppendInteger(group.State);
                message.AppendInteger(group.AdminOnlyDeco);
                message.AppendBool(false);
                message.AppendString(string.Empty);

                // TODO Hardcoded stuff..

                string[] array = group.Badge.Replace("b", string.Empty).Split('s');

                message.AppendInteger(5);

                int num = 5 - array.Length;

                int num2 = 0;
                string[] array2 = array;

                foreach (string text in array2)
                {
                    message.AppendInteger(text.Length >= 6
                        ? uint.Parse(text.Substring(0, 3))
                        : uint.Parse(text.Substring(0, 2)));
                    message.AppendInteger(text.Length >= 6
                        ? uint.Parse(text.Substring(3, 2))
                        : uint.Parse(text.Substring(2, 2)));

                    if (text.Length < 5)
                        message.AppendInteger(0);
                    else if (text.Length >= 6)
                        message.AppendInteger(uint.Parse(text.Substring(5, 1)));
                    else
                        message.AppendInteger(uint.Parse(text.Substring(4, 1)));
                }

                while (num2 != num)
                {
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                    num2++;
                }

                message.AppendString(group.Badge);
                message.AppendInteger(group.Members.Count);

                session.Send(message);
            }
        }

        #endregion Methods
    }
}