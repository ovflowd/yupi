using System.Collections.Generic;
using System.Data;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Items.Interfaces;
using Yupi.Messages;

namespace Yupi.Game.Items.Datas
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

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery("SELECT * FROM items_highscores WHERE item_id=" + itemId +
                                                " ORDER BY score DESC");

                DataTable table = commitableQueryReactor.GetTable();

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
        /// <param name="message">The message.</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage GenerateExtraData(RoomItem item, ServerMessage message)
        {
            message.AppendInteger(6);
            message.AppendString(item.ExtraData); //Ouvert/fermé

            if (item.GetBaseItem().Name.StartsWith("highscore_classic"))
                message.AppendInteger(2);
            else if (item.GetBaseItem().Name.StartsWith("highscore_mostwin"))
                message.AppendInteger(1);
            else if (item.GetBaseItem().Name.StartsWith("highscore_perteam"))
                message.AppendInteger(0);

            message.AppendInteger(0); //Time : ["alltime", "daily", "weekly", "monthly"]
            message.AppendInteger(Lines.Count); //Count

            foreach (KeyValuePair<int, HighScoreLine> line in Lines)
            {
                message.AppendInteger(line.Value.Score);
                message.AppendInteger(1);
                message.AppendString(line.Value.Username);
            }

            return message;
        }

        /// <summary>
        ///     Adds the user score.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="username">The username.</param>
        /// <param name="score">The score.</param>
        internal void AddUserScore(RoomItem item, string username, int score)
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                if (item.GetBaseItem().Name.StartsWith("highscore_classic"))
                {
                    commitableQueryReactor.SetQuery(
                        "INSERT INTO items_highscores (item_id,username,score) VALUES (@itemid,@username,@score)");
                    commitableQueryReactor.AddParameter("itemid", item.Id);
                    commitableQueryReactor.AddParameter("username", username);
                    commitableQueryReactor.AddParameter("score", score);
                    commitableQueryReactor.RunQuery();
                }
                else if (item.GetBaseItem().Name.StartsWith("highscore_mostwin"))
                {
                    score = 1;
                    commitableQueryReactor.SetQuery(
                        "SELECT id,score FROM items_highscores WHERE username = @username AND item_id = @itemid");
                    commitableQueryReactor.AddParameter("itemid", item.Id);
                    commitableQueryReactor.AddParameter("username", username);

                    DataRow row = commitableQueryReactor.GetRow();

                    if (row != null)
                    {
                        commitableQueryReactor.SetQuery(
                            "UPDATE items_highscores SET score = score + 1 WHERE username = @username AND item_id = @itemid");
                        commitableQueryReactor.AddParameter("itemid", item.Id);
                        commitableQueryReactor.AddParameter("username", username);
                        commitableQueryReactor.RunQuery();
                        Lines.Remove((int) row["id"]);
                        score = (int) row["score"] + 1;
                    }
                    else
                    {
                        commitableQueryReactor.SetQuery(
                            "INSERT INTO items_highscores (item_id,username,score) VALUES (@itemid,@username,@score)");
                        commitableQueryReactor.AddParameter("itemid", item.Id);
                        commitableQueryReactor.AddParameter("username", username);
                        commitableQueryReactor.AddParameter("score", score);
                        commitableQueryReactor.RunQuery();
                    }
                }

                LastId++;
                Lines.Add(LastId, new HighScoreLine(username, score));
            }
        }
    }
}