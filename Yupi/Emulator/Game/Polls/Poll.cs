using System.Collections.Generic;
using Yupi.Emulator.Game.Polls.Enums;


namespace Yupi.Emulator.Game.Polls
{
    /// <summary>
    ///     Class Poll.
    /// </summary>
     public class Poll
    {
        /// <summary>
        ///     The answers negative
        /// </summary>
     public int AnswersNegative;

        /// <summary>
        ///     The answers positive
        /// </summary>
     public int AnswersPositive;

        /// <summary>
        ///     The identifier
        /// </summary>
     public uint Id;

        /// <summary>
        ///     The poll invitation
        /// </summary>
     public string PollInvitation;

        /// <summary>
        ///     The poll name
        /// </summary>
     public string PollName;

        /// <summary>
        ///     The prize
        /// </summary>
     public string Prize;

        /// <summary>
        ///     The questions
        /// </summary>
     public List<PollQuestion> Questions;

        /// <summary>
        ///     The room identifier
        /// </summary>
     public uint RoomId;

        /// <summary>
        ///     The thanks
        /// </summary>
     public string Thanks;

        /// <summary>
        ///     The type
        /// </summary>
     public PollType Type;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Poll" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="pollName">Name of the poll.</param>
        /// <param name="pollInvitation">The poll invitation.</param>
        /// <param name="thanks">The thanks.</param>
        /// <param name="prize">The prize.</param>
        /// <param name="type">The type.</param>
        /// <param name="questions">The questions.</param>
     public Poll(uint id, uint roomId, string pollName, string pollInvitation, string thanks, string prize,
            int type, List<PollQuestion> questions)
        {
            Id = id;
            RoomId = roomId;
            PollName = pollName;
            PollInvitation = pollInvitation;
            Thanks = thanks;
            Type = (PollType) type;
            Prize = prize;
            Questions = questions;
            AnswersPositive = 0;
            AnswersNegative = 0;
        }

        /// <summary>
        ///     Serializes the specified messageBuffer.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
     public void Serialize(SimpleServerMessageBuffer messageBuffer)
        {
            messageBuffer.AppendInteger(Id);
            messageBuffer.AppendString(string.Empty); //?
            messageBuffer.AppendString(PollInvitation);
            messageBuffer.AppendString("Test"); // whats this??
        }
    }
}