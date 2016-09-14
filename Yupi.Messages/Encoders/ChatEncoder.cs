// ---------------------------------------------------------------------------------
// <copyright file="ChatEncoder.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Encoders
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public static class ChatEncoder
    {
        #region Methods

        public static void Append(this ServerMessage message, ChatMessage entry, int count)
        {
            message.AppendInteger(entry.Entity.Id);
            message.AppendString(entry.FilteredMessage());
            message.AppendInteger(entry.GetEmotion().Value);
            message.AppendInteger(entry.Bubble.Value);

            // Replaces placeholders the way String.Format does: {0}
            message.AppendInteger(entry.Links.Count);

            foreach (Link link in entry.Links)
            {
                message.AppendString(link.URL);
                message.AppendString(link.Text);
                message.AppendBool(link.IsInternal);
            }

            // Count is used to detect lag (client side)
            message.AppendInteger(count);
        }

        #endregion Methods
    }
}