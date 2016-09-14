using System.Collections.Generic;
using System.Linq;

namespace Yupi.Model.Domain
{
    public class PollQuestion
    {
        public virtual int Id { get; protected set; }

        public virtual IList<string> Answers { get; protected set; }

        // TODO Rename
        public virtual PollAnswerType AnswerType { get; set; }

        // TODO Use id?
        public virtual string CorrectAnswer { get; set; }
        public virtual string Question { get; set; }

        public PollQuestion()
        {
            Answers = new List<string>();
        }

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
    }
}