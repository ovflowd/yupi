// ---------------------------------------------------------------------------------
// <copyright file="PollQuestionsMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;
    // TODO USE IMPLEMENTATION :D
    public class PollQuestionsMessageComposer : Yupi.Messages.Contracts.PollQuestionsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Poll poll)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(poll.Id);
                message.AppendString(poll.PollName);
                message.AppendString(poll.Thanks);
                message.AppendInteger(poll.Questions.Count);

                foreach (PollQuestion question in poll.Questions)
                {
                    int questionNumber = poll.Questions.IndexOf(question) + 1;

                    message.AppendInteger(question.Id);
                    message.AppendInteger(questionNumber);
                    message.AppendInteger((int) question.AnswerType);
                    message.AppendString(question.Question);

                    if (question.AnswerType == PollAnswerType.Selection
                        || question.AnswerType == PollAnswerType.RadioSelection)
                    {
                        message.AppendInteger(1);
                        message.AppendInteger(question.Answers.Count);

                        foreach (PollAwnser awnser in question.Answers)
                        {
                            // TODO Why twice?
                            message.AppendString(awnser.Text);
                            message.AppendString(awnser.Text);
                        }
                    }
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}