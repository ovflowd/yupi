using System.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;


namespace Yupi.Emulator.Game.Items.Datas
{
    /// <summary>
    ///     Class TonerData.
    /// </summary>
     public class TonerData
    {
        /// <summary>
        ///     The data1
        /// </summary>
     public int Data1, Data2, Data3;

        /// <summary>
        ///     The enabled
        /// </summary>
     public int Enabled;

        /// <summary>
        ///     The item identifier
        /// </summary>
     public uint ItemId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TonerData" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
     public TonerData(uint item)
        {
            ItemId = item;
            DataRow row;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    $"SELECT enabled,data1,data2,data3 FROM items_toners WHERE id={ItemId} LIMIT 1");
                row = queryReactor.GetRow();
            }

            if (row == null)
            {
                Data1 = Data2 = Data3 = 1;
                return;
            }

            Enabled = int.Parse(row[0].ToString());
            Data1 = (int) row[1];
            Data2 = (int) row[2];
            Data3 = (int) row[3];
        }
    }
}