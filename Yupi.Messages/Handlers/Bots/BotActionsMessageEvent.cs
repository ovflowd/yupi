// ---------------------------------------------------------------------------------
// <copyright file="BotActionsMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Messages.Bots
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Messages.Notification;
    using Yupi.Messages.Rooms;

    public class BotActionsMessageEvent : AbstractHandler
    {
        #region Methods

        // TODO Refactor
        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            uint botId = request.GetUInt32();
            int action = request.GetInteger();

            string data = Yupi.FilterInjectionChars(request.GetString());

            RoomUser bot = room.GetRoomUserManager().GetBot(botId);

            bool flag = false;

            switch (action)
            {
            case 1:
                bot.BotData.Look = session.GetHabbo ().Look;
                router.GetComposer<SetRoomUserMessageComposer> ().Compose (room, bot, room.GetGameMap ().GotPublicPool);
                break;
            case 2:
                try {
                    string[] array = data.Split (new[] { ";#;" }, StringSplitOptions.None);

                    string[] speechsJunk =
                        array [0].Substring (0, array [0].Length > 1024 ? 1024 : array [0].Length)
                            .Split (Convert.ToChar (13));

                    bool speak = array [1] == "true";

                    uint speechDelay = uint.Parse (array [2]);

                    bool mix = array [3] == "true";
                    if (speechDelay < 7)
                        speechDelay = 7;

                    string speechs =
                        speechsJunk.Where (
                            speech =>
                            !string.IsNullOrEmpty (speech) &&
                            (!speech.ToLower ().Contains ("update") || !speech.ToLower ().Contains ("set")))
                            .Aggregate (string.Empty,
                            (current, speech) =>
                                current +
                            ServerUserChatTextHandler.FilterHtml (speech, session.GetHabbo ().GotCommand ("ha")) +
                            ";");

                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
                        queryReactor.SetQuery ("UPDATE bots_data SET automatic_chat = @autochat, speaking_interval = @interval, mix_phrases = @mix_phrases, speech = @speech WHERE id = @botid");

                        queryReactor.AddParameter ("autochat", speak ? "1" : "0");
                        queryReactor.AddParameter ("interval", speechDelay);
                        queryReactor.AddParameter ("mix_phrases", mix ? "1" : "0");
                        queryReactor.AddParameter ("speech", speechs);
                        queryReactor.AddParameter ("botid", botId);
                        queryReactor.RunQuery ();
                    }
                    List<string> randomSpeech = speechs.Split (';').ToList ();

                    room.GetRoomUserManager ()
                        .UpdateBot (bot.VirtualId, bot, bot.BotData.Name, bot.BotData.Motto, bot.BotData.Look,
                        bot.BotData.Gender, randomSpeech, null, speak, speechDelay, mix);
                } catch (Exception e) {
                    YupiLogManager.LogException (e, "Failed Manage Bot Actions. BAD.", "Yupi.Room");
                }
                break;
            case 3:
                bot.BotData.WalkingMode = bot.BotData.WalkingMode == "freeroam" ? "stand" : "freeroam";
                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery("UPDATE bots_data SET walk_mode = @walkmode WHERE id = @botid");
                    queryReactor.AddParameter("walkmode", bot.BotData.WalkingMode);
                    queryReactor.AddParameter("botid", botId);
                    queryReactor.RunQuery();
                }
                router.GetComposer<SetRoomUserMessageComposer> ().Compose (room, bot, room.GetGameMap ().GotPublicPool);
                break;
            case 4:
                if (bot.BotData.DanceId > 0)
                    bot.BotData.DanceId = 0;
                else
                {
                    Random random = new Random();
                    bot.DanceId = (uint) random.Next(1, 4);
                    bot.BotData.DanceId = bot.DanceId;
                }

                router.GetComposer<DanceStatusMessageComposer> ().Compose (session.GetHabbo ().CurrentRoom, bot.VirtualId, bot.BotData.DanceId);
                break;

            case 5:
                string name = ServerUserChatTextHandler.FilterHtml(data, session.GetHabbo().GotCommand("ha"));
                if (name.Length < 15)
                    bot.BotData.Name = name;
                else
                {
                    router.GetComposer<GeneralErrorHabboMessageComposer> ().Compose (session, 4);
                    break;
                }

                router.GetComposer<SetRoomUserMessageComposer> ().Compose (room, bot, room.GetGameMap ().GotPublicPool);
            default:
                router.GetComposer<SetRoomUserMessageComposer> ().Compose (room, bot, room.GetGameMap ().GotPublicPool);
            }
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}