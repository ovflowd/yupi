using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class PollQuestionsMessageComposer : Contracts.PollQuestionsMessageComposer
    {
        public override void Compose(ISender session, Poll poll)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(poll.Id);
                message.AppendString(poll.PollName);
                message.AppendString(poll.Thanks);
                message.AppendInteger(poll.Questions.Count);

                foreach (var question in poll.Questions)
                {
                    var questionNumber = poll.Questions.IndexOf(question) + 1;

                    message.AppendInteger(question.Id);
                    message.AppendInteger(questionNumber);
                    message.AppendInteger((int) question.AnswerType);
                    message.AppendString(question.Question);

                    if ((question.AnswerType == PollAnswerType.Selection)
                        || (question.AnswerType == PollAnswerType.RadioSelection))
                    {
                        message.AppendInteger(1);
                        message.AppendInteger(question.Answers.Count);

                        foreach (var awnser in question.Answers)
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
    }
}