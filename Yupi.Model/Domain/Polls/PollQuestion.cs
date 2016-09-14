// ---------------------------------------------------------------------------------
// <copyright file="PollQuestion.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Model.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public class PollQuestion
    {
        #region Properties

        public virtual IList<string> Answers
        {
            get; protected set;
        }

        // TODO Rename
        public virtual PollAnswerType AnswerType
        {
            get; set;
        }

        // TODO Use id?
        public virtual string CorrectAnswer
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual string Question
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public PollQuestion()
        {
            Answers = new List<string>();
        }

        #endregion Constructors

        #region Other

        /*
        /// <summary>
        ///     Serializes the specified messageBuffer.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
        /// <param name="questionNumber">The question number.</param>
        public void Serialize(SimpleServerMessageBuffer messageBuffer, int questionNumber)
        {
            messageBuffer.AppendInteger(Index);
            messageBuffer.AppendInteger(questionNumber);
            messageBuffer.AppendInteger((int) AType);
            messageBuffer.AppendString(Question);

            if (AType != PollAnswerType.Selection && AType != PollAnswerType.RadioSelection)
                return;

            messageBuffer.AppendInteger(1);
            messageBuffer.AppendInteger(Answers.Count);

            foreach (string current in Answers)
            {
                messageBuffer.AppendString(current);
                messageBuffer.AppendString(current);
            }
        }
           */

        #endregion Other
    }
}