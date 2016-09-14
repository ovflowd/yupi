namespace Yupi.Model.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public class PollQuestion
    {
        #region Constructors

        public PollQuestion()
        {
            Answers = new List<string>();
        }

        #endregion Constructors

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