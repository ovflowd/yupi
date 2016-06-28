using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interactions.Models;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Messages;


namespace Yupi.Emulator.Game.Items.Interactions.Controllers
{
     public class InteractorMannequin : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (!item.ExtraData.Contains('\u0005'.ToString()))
                return;

            string[] array = item.ExtraData.Split('\u0005');

            session.GetHabbo().Gender = array[0].ToUpper() == "F" ? "F" : "M";

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Clear();

            string[] array2 = array[1].Split('.');

            foreach (string text in array2)
            {
                string[] array3 = session.GetHabbo().Look.Split('.');

                foreach (string text2 in array3)
                {
                    if (text2.Split('-')[0] == text.Split('-')[0])
                    {
                        if (dictionary.ContainsKey(text2.Split('-')[0]) && !dictionary.ContainsValue(text))
                        {
                            dictionary.Remove(text2.Split('-')[0]);
                            dictionary.Add(text2.Split('-')[0], text);
                        }
                        else
                        {
                            if (!dictionary.ContainsKey(text2.Split('-')[0]) && !dictionary.ContainsValue(text))
                                dictionary.Add(text2.Split('-')[0], text);
                        }
                    }
                    else
                    {
                        if (!dictionary.ContainsKey(text2.Split('-')[0]))
                            dictionary.Add(text2.Split('-')[0], text2);
                    }
                }
            }

            string text3 = dictionary.Values.Aggregate("", (current1, current) => $"{current1}{current}.");

            session.GetHabbo().Look = text3.TrimEnd('.');

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    $"UPDATE users SET look = @look, gender = @gender WHERE id = {session.GetHabbo().Id}");
                queryReactor.AddParameter("look", session.GetHabbo().Look);
                queryReactor.AddParameter("gender", session.GetHabbo().Gender);
                queryReactor.RunQuery();
            }

            session.GetMessageHandler()
                .GetResponse()
                .Init(PacketLibraryManager.OutgoingHandler("UpdateUserDataMessageComposer"));
            session.GetMessageHandler().GetResponse().AppendInteger(-1);
            session.GetMessageHandler().GetResponse().AppendString(session.GetHabbo().Look);
            session.GetMessageHandler().GetResponse().AppendString(session.GetHabbo().Gender.ToLower());
            session.GetMessageHandler().GetResponse().AppendString(session.GetHabbo().Motto);
            session.GetMessageHandler().GetResponse().AppendInteger(session.GetHabbo().AchievementPoints);
            session.GetMessageHandler().SendResponse();

            RoomUser roomUserByHabbo = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("UpdateUserDataMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(roomUserByHabbo.VirtualId);
            simpleServerMessageBuffer.AppendString(session.GetHabbo().Look);
            simpleServerMessageBuffer.AppendString(session.GetHabbo().Gender.ToLower());
            simpleServerMessageBuffer.AppendString(session.GetHabbo().Motto);
            simpleServerMessageBuffer.AppendInteger(session.GetHabbo().AchievementPoints);

            session.GetHabbo().CurrentRoom.SendMessage(simpleServerMessageBuffer);
        }
    }
}