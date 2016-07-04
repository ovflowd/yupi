using System;
using Yupi.Emulator.Game.Polls;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class PollQuestionsMessageComposer : AbstractComposer<Poll>
	{
		public override void Compose (Yupi.Protocol.ISender session, Poll poll)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(poll.Id);
				message.AppendString(poll.PollName);
				message.AppendString(poll.Thanks);
				message.AppendInteger(poll.Questions.Count);

				foreach (PollQuestion question in poll.Questions)
				{
					int questionNumber = poll.Questions.IndexOf(question) + 1;

					question.Serialize(message, questionNumber);
				}
				session.Send (message);
			}
		}
	}
}

