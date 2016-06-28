using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Game.Polls.Enums;


namespace Yupi.Emulator.Game.Polls
{
    /// <summary>
    ///     Class PollQuestion.
    /// </summary>
     public class PollQuestion
    {
        /// <summary>
        ///     The answers
        /// </summary>
     public List<string> Answers;

        /// <summary>
        ///     a type
        /// </summary>
     public PollAnswerType AType;

        /// <summary>
        ///     The correct answer
        /// </summary>
     public string CorrectAnswer;

        /// <summary>
        ///     The index
        /// </summary>
     public uint Index;

        /// <summary>
        ///     The question
        /// </summary>
     public string Question;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PollQuestion" /> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="question">The question.</param>
        /// <param name="aType">a type.</param>
        /// <param name="answers">The answers.</param>
        /// <param name="correctAnswer">The correct answer.</param>
     public PollQuestion(uint index, string question, int aType, IEnumerable<string> answers, string correctAnswer)
        {
            Index = index;
            Question = question;
            AType = (PollAnswerType) aType;
            Answers = answers.ToList();
            CorrectAnswer = correctAnswer;
        }

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
    }
}