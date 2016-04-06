using System.Collections.Generic;
using System.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Emulator.Game.Users
{
    /// <summary>
    ///     Class HallOfFame.
    /// </summary>
     public class HallOfFame
    {
     public List<HallOfFameElement> Rankings;

     public HallOfFame()
        {
            Rankings = new List<HallOfFameElement>();
            RefreshHallOfFame();
        }

        public void RefreshHallOfFame()
        {
            Rankings.Clear();
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM users_rankings ORDER BY score DESC");
                DataTable table = queryReactor.GetTable();

                if (table == null)
                    return;

                foreach (DataRow row in table.Rows)
                    Rankings.Add(new HallOfFameElement((uint) row["user_id"], (int) row["score"],
                        (string) row["competition"]));
            }
        }
    }

    /// <summary>
    ///     Class HallOfFameElement.
    /// </summary>
     public class HallOfFameElement
    {
     public string Competition;
     public int Score;
     public uint UserId;

     public HallOfFameElement(uint userId, int score, string competition)
        {
            UserId = userId;
            Score = score;
            Competition = competition;
        }
    }
}