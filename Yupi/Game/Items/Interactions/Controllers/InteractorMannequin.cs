using System;
using System.Collections.Generic;
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interactions.Models;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.User;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Items.Interactions.Controllers
{
    internal class InteractorMannequin : FurniInteractorModel
    {
        public override void OnTrigger(GameClient session, RoomItem item, int request, bool hasRights)
        {
            if (!item.ExtraData.Contains(Convert.ToChar(5).ToString()))
                return;

            string[] array = item.ExtraData.Split(Convert.ToChar(5));

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

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    $"UPDATE users SET look = @look, gender = @gender WHERE id = {session.GetHabbo().Id}");
                commitableQueryReactor.AddParameter("look", session.GetHabbo().Look);
                commitableQueryReactor.AddParameter("gender", session.GetHabbo().Gender);
                commitableQueryReactor.RunQuery();
            }

            session.GetMessageHandler()
                .GetResponse()
                .Init(LibraryParser.OutgoingRequest("UpdateUserDataMessageComposer"));
            session.GetMessageHandler().GetResponse().AppendInteger(-1);
            session.GetMessageHandler().GetResponse().AppendString(session.GetHabbo().Look);
            session.GetMessageHandler().GetResponse().AppendString(session.GetHabbo().Gender.ToLower());
            session.GetMessageHandler().GetResponse().AppendString(session.GetHabbo().Motto);
            session.GetMessageHandler().GetResponse().AppendInteger(session.GetHabbo().AchievementPoints);
            session.GetMessageHandler().SendResponse();

            RoomUser roomUserByHabbo = item.GetRoom().GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("UpdateUserDataMessageComposer"));

            serverMessage.AppendInteger(roomUserByHabbo.VirtualId);
            serverMessage.AppendString(session.GetHabbo().Look);
            serverMessage.AppendString(session.GetHabbo().Gender.ToLower());
            serverMessage.AppendString(session.GetHabbo().Motto);
            serverMessage.AppendInteger(session.GetHabbo().AchievementPoints);

            session.GetHabbo().CurrentRoom.SendMessage(serverMessage);
        }
    }
}