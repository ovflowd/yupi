using System.Collections.Generic;
using System.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Items.Datas
{
    /// <summary>
    ///     Class HighscoreData.
    /// </summary>
    internal class HighscoreData
    {
        /// <summary>
        ///     The last identifier
        /// </summary>
        internal int LastId;

        /// <summary>
        ///     The lines
        /// </summary>
        internal Dictionary<int, HighScoreLine> Lines;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HighscoreData" /> class.
        /// </summary>
        /// <param name="roomItem">The room item.</param>
        internal HighscoreData(RoomItem roomItem)
        {
            Lines = new Dictionary<int, HighScoreLine>();
            uint itemId = roomItem.Id;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM items_highscores WHERE item_id=" + itemId +
                                      " ORDER BY score DESC");

                DataTable table = queryReactor.GetTable();

                if (table == null)
                    return;

                foreach (DataRow row in table.Rows)
                {
                    Lines.Add((int) row["id"], new HighScoreLine((string) row["username"], (int) row["score"]));
                    LastId = (int) row["id"];
                }
            }
        }

        /// <summary>
        ///     Generates the extra data.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="message">The messageBuffer.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal SimpleServerMessageBuffer GenerateExtraData(RoomItem item, SimpleServerMessageBuffer messageBuffer)
        {
            messageBuffer.AppendInteger(6);
            messageBuffer.AppendString(item.ExtraData); //Ouvert/fermé

            if (item.GetBaseItem().Name.StartsWith("highscore_classic"))
                messageBuffer.AppendInteger(2);
            else if (item.GetBaseItem().Name.StartsWith("highscore_mostwin"))
                messageBuffer.AppendInteger(1);
            else if (item.GetBaseItem().Name.StartsWith("highscore_perteam"))
                messageBuffer.AppendInteger(0);

            messageBuffer.AppendInteger(0); //Time : ["alltime", "daily", "weekly", "monthly"]
            messageBuffer.AppendInteger(Lines.Count); //Count

            foreach (KeyValuePair<int, HighScoreLine> line in Lines)
            {
                messageBuffer.AppendInteger(line.Value.Score);
                messageBuffer.AppendInteger(1);
                messageBuffer.AppendString(line.Value.Username);
            }

            return messageBuffer;
        }

        /// <summary>
        ///     Adds the user score.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="username">The username.</param>
        /// <param name="score">The score.</param>
        internal void AddUserScore(RoomItem item, string username, int score)
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                if (item.GetBaseItem().Name.StartsWith("highscore_classic"))
                {
                    queryReactor.SetQuery(
                        "INSERT INTO items_highscores (item_id,username,score) VALUES (@itemid,@username,@score)");
                    queryReactor.AddParameter("itemid", item.Id);
                    queryReactor.AddParameter("username", username);
                    queryReactor.AddParameter("score", score);
                    queryReactor.RunQuery();
                }
                else if (item.GetBaseItem().Name.StartsWith("highscore_mostwin"))
                {
                    score = 1;
                    queryReactor.SetQuery(
                        "SELECT id,score FROM items_highscores WHERE username = @username AND item_id = @itemid");
                    queryReactor.AddParameter("itemid", item.Id);
                    queryReactor.AddParameter("username", username);

                    DataRow row = queryReactor.GetRow();

                    if (row != null)
                    {
                        queryReactor.SetQuery(
                            "UPDATE items_highscores SET score = score + 1 WHERE username = @username AND item_id = @itemid");
                        queryReactor.AddParameter("itemid", item.Id);
                        queryReactor.AddParameter("username", username);
                        queryReactor.RunQuery();
                        Lines.Remove((int) row["id"]);
                        score = (int) row["score"] + 1;
                    }
                    else
                    {
                        queryReactor.SetQuery(
                            "INSERT INTO items_highscores (item_id,username,score) VALUES (@itemid,@username,@score)");
                        queryReactor.AddParameter("itemid", item.Id);
                        queryReactor.AddParameter("username", username);
                        queryReactor.AddParameter("score", score);
                        queryReactor.RunQuery();
                    }
                }

                LastId++;
                Lines.Add(LastId, new HighScoreLine(username, score));
            }
        }
    }
}