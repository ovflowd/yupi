using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Rooms.Data
{
    /// <summary>
    ///     Class RoomCompetition.
    /// </summary>
    internal class RoomCompetition
    {
        internal Dictionary<uint, RoomData> Entries;
        internal int Id;
        internal string Name;
        internal string[] RequiredFurnis;

        public RoomCompetition(int id, string name, string requiredFurnis)
        {
            Id = id;
            Name = name;
            RequiredFurnis = requiredFurnis.Split(';');
            Entries = new Dictionary<uint, RoomData>();

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM rooms_competitions_entries WHERE competition_id = " + Id);
                DataTable table = queryReactor.GetTable();
                if (table == null) return;
                foreach (DataRow row in table.Rows)
                {
                    uint roomId = (uint) row["room_id"];
                    RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId);
                    if (roomData == null) return;
                    roomData.CompetitionStatus = (int) row["status"];
                    roomData.CompetitionVotes = (int) row["votes"];
                    if (Entries.ContainsKey(roomId)) return;
                    Entries.Add(roomId, roomData);
                }
            }
        }

        internal bool HasAllRequiredFurnis(Room room)
        {
            if (room == null)
                return false;

            return RequiredFurnis.All(furni => room.GetRoomItemHandler().HasFurniByItemName(furni));
        }

        internal SimpleServerMessageBuffer AppendEntrySubmitMessage(SimpleServerMessageBuffer messageBuffer, int status, Room room = null)
        {
            messageBuffer.Init(PacketLibraryManager.OutgoingRequest("CompetitionEntrySubmitResultMessageComposer"));

            messageBuffer.AppendInteger(Id);
            messageBuffer.AppendString(Name);
            messageBuffer.AppendInteger(status);
            // 0 : roomSent - 1 : send room - 2 : confirm register - 3 : neededFurnis - 4 : doorClosed - 6 : acceptRules

            if (status != 3)
            {
                messageBuffer.AppendInteger(0);
                messageBuffer.AppendInteger(0);
            }
            else
            {
                messageBuffer.StartArray();

                foreach (string furni in RequiredFurnis)
                {
                    messageBuffer.AppendString(furni);
                    messageBuffer.SaveArray();
                }

                messageBuffer.EndArray();

                if (room == null)
                    messageBuffer.AppendInteger(0);
                else
                {
                    messageBuffer.StartArray();

                    foreach (string furni in RequiredFurnis)
                    {
                        if (!room.GetRoomItemHandler().HasFurniByItemName(furni))
                        {
                            messageBuffer.AppendString(furni);
                            messageBuffer.SaveArray();
                        }
                    }

                    messageBuffer.EndArray();
                }
            }

            return messageBuffer;
        }

        internal SimpleServerMessageBuffer AppendVoteMessage(SimpleServerMessageBuffer messageBuffer, Habbo user, int status = 0)
        {
            messageBuffer.Init(PacketLibraryManager.OutgoingRequest("CompetitionVotingInfoMessageComposer"));

            messageBuffer.AppendInteger(Id);
            messageBuffer.AppendString(Name);
            messageBuffer.AppendInteger(status); // 0 : vote - 1 : can't vote - 2 : you need the vote badge
            messageBuffer.AppendInteger(user.DailyCompetitionVotes);

            return messageBuffer;
        }
    }

    /// <summary>
    ///     Class RoomCompetitionManager.
    /// </summary>
    internal class RoomCompetitionManager
    {
        internal RoomCompetition Competition;

        public RoomCompetitionManager()
        {
            RefreshCompetitions();
        }

        public void RefreshCompetitions()
        {
            Competition = null;
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT * FROM rooms_competitions WHERE enabled = '1' LIMIT 1");
                DataRow row = queryReactor.GetRow();

                if (row == null)
                    return;

                Competition = new RoomCompetition((int) row["id"], (string) row["name"], (string) row["required_furnis"]);
            }
        }
    }
}