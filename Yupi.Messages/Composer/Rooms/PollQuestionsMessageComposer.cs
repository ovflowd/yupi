namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

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

                        foreach (string awnser in question.Answers)
                        {
                            // TODO Why twice?
                            message.AppendString(awnser);
                            message.AppendString(awnser);
                        }
                    }
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}