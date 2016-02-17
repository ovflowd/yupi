using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;

namespace Yupi.Game.Polls
{
    /// <summary>
    ///     Class PollManager.
    /// </summary>
    internal class PollManager
    {
        /// <summary>
        ///     The polls
        /// </summary>
        internal Dictionary<uint, Poll> Polls;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PollManager" /> class.
        /// </summary>
        internal PollManager()
        {
            Polls = new Dictionary<uint, Poll>();
        }

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        /// <param name="pollLoaded">The poll loaded.</param>
        internal void Init(IQueryAdapter dbClient, out uint pollLoaded)
        {
            Init(dbClient);
            pollLoaded = (uint) Polls.Count;
        }

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void Init(IQueryAdapter dbClient)
        {
            Polls.Clear();

            dbClient.SetQuery("SELECT * FROM polls_data WHERE enabled = '1'");

            DataTable table = dbClient.GetTable();

            if (table == null)
                return;

            foreach (DataRow dataRow in table.Rows)
            {
                uint num = uint.Parse(dataRow["id"].ToString());

                dbClient.SetQuery($"SELECT * FROM polls_questions WHERE poll_id = {num}");

                DataTable table2 = dbClient.GetTable();

                List<PollQuestion> list = (from DataRow dataRow2 in table2.Rows
                    select
                        new PollQuestion(uint.Parse(dataRow2["id"].ToString()), (string) dataRow2["question"],
                            int.Parse(dataRow2["answertype"].ToString()), dataRow2["answers"].ToString().Split('|'),
                            (string) dataRow2["correct_answer"])).ToList();

                Poll value = new Poll(num, uint.Parse(dataRow["room_id"].ToString()), (string) dataRow["caption"],
                    (string) dataRow["invitation"], (string) dataRow["greetings"], (string) dataRow["prize"],
                    int.Parse(dataRow["type"].ToString()), list);

                Polls.Add(num, value);
            }
        }

        /// <summary>
        ///     Tries the get poll.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="poll">The poll.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool TryGetPoll(uint roomId, out Poll poll)
        {
            foreach (Poll current in Polls.Values.Where(current => current.RoomId == roomId))
            {
                poll = current;
                return true;
            }

            poll = null;

            return false;
        }

        /// <summary>
        ///     Tries the get poll by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Poll.</returns>
        internal Poll TryGetPollById(uint id) => Polls.Values.FirstOrDefault(current => current.Id == id);
    }
}