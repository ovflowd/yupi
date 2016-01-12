/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Yupi.Data;
using Yupi.Game.Commands;
using Yupi.Game.RoomBots.Interfaces;
using Yupi.Game.Rooms.User;
using Yupi.Game.Rooms.User.Path;

namespace Yupi.Game.RoomBots.Models
{
    /// <summary>
    ///     Class GenericBot.
    /// </summary>
    internal class GenericBot : BaseBot
    {
        /// <summary>
        ///     The random
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        ///     The _virtual identifier
        /// </summary>
        private readonly int _virtualId;

        /// <summary>
        ///     The _action count
        /// </summary>
        private int _actionCount;

        /// <summary>
        ///     The _chat timer
        /// </summary>
        private Timer _chatTimer;

        /// <summary>
        ///     The _speech interval
        /// </summary>
        private uint _speechInterval;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericBot" /> class.
        /// </summary>
        /// <param name="roomBot">The room bot.</param>
        /// <param name="virtualId">The virtual identifier.</param>
        /// <param name="speechInterval">The speech interval.</param>
        internal GenericBot(RoomBot roomBot, int virtualId, uint speechInterval)
        {
            _virtualId = virtualId;
            _speechInterval = speechInterval < 2 ? 2000 : speechInterval*1000;

            if ((roomBot?.AutomaticChat ?? false) && roomBot.RandomSpeech != null && roomBot.RandomSpeech.Any())
                _chatTimer = new Timer(ChatTimerTick, null, _speechInterval, _speechInterval);

            _actionCount = Random.Next(10, 30 + virtualId);
        }

        /// <summary>
        ///     Modifieds this instance.
        /// </summary>
        internal override void Modified()
        {
            if (GetBotData() == null)
                return;

            if (!GetBotData().AutomaticChat || GetBotData().RandomSpeech == null || !GetBotData().RandomSpeech.Any())
            {
                StopTimerTick();

                return;
            }

            _speechInterval = GetBotData().SpeechInterval < 2 ? 2000 : GetBotData().SpeechInterval*1000;

            if (_chatTimer == null)
            {
                _chatTimer = new Timer(ChatTimerTick, null, _speechInterval, _speechInterval);

                return;
            }

            _chatTimer.Change(_speechInterval, _speechInterval);
        }

        /// <summary>
        ///     Called when [timer tick].
        /// </summary>
        internal override void OnTimerTick()
        {
            if (GetBotData() == null)
                return;

            if (_actionCount > 0)
            {
                _actionCount--;

                return;
            }

            _actionCount = Random.Next(5, 45);

            switch (GetBotData().WalkingMode.ToLower())
            {
                case "freeroam":
                {
                    Point randomPoint = GetRoom().GetGameMap().GetRandomWalkableSquare();

                    if (randomPoint.X == 0 || randomPoint.Y == 0)
                        return;

                    GetRoomUser().MoveTo(randomPoint.X, randomPoint.Y);

                    break;
                }
                case "specified_range":
                {
                    List<Point> list = GetRoom().GetGameMap().WalkableList.ToList();

                    if (!list.Any())
                        return;

                    int randomNumber = new Random(DateTime.Now.Millisecond + _virtualId ^ 2).Next(0, list.Count - 1);

                    GetRoomUser().MoveTo(list[randomNumber].X, list[randomNumber].Y);
                    break;
                }
            }
        }

        /// <summary>
        ///     Called when [user say].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="message">The message.</param>
        internal override void OnUserSay(RoomUser user, string message)
        {
            if (Gamemap.TileDistance(GetRoomUser().X, GetRoomUser().Y, user.X, user.Y) > 16)
                return;

            if (message.Length < 2)
                return;

            BotCommand command = BotManager.GetBotCommandByInput(message.Substring(1).ToLower());

            if (command == null)
            {
                GetRoomUser().Chat(null, "Need Something?", false, 0); // @todo put this var in lang system

                return;
            }

            if (GetBotData().BotType != command.BotType)
                return;

            if (command.SpeechOutput != string.Empty)
                GetRoomUser().Chat(null, command.SpeechOutput, false, 0);

            switch (command.ActionBot)
            {
                case "bot_move_to_user":
                    GetRoomUser().MoveTo(user.SquareInFront);
                    break;
                default:
                    break;
            }

            if (command.ActionCommand != string.Empty && command.ActionCommandParameters != string.Empty)
                CommandsManager.TryExecute(command.ActionCommand, command.ActionCommandParameters, user.GetClient());
        }

        /// <summary>
        ///     Called when [user shout].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="message">The message.</param>
        internal override void OnUserShout(RoomUser user, string message)
            => GetRoomUser().Chat(null, "Não precisa gritar, caramba! Se precisa de algo basta vir aqui.", false, 0);

        /// <summary>
        ///     Stops the timer tick.
        /// </summary>
        private void StopTimerTick()
        {
            if (_chatTimer == null)
                return;

            _chatTimer.Change(Timeout.Infinite, Timeout.Infinite);

            _chatTimer.Dispose();
            _chatTimer = null;
        }

        internal override void OnChatTick()
        {
            if (GetBotData() == null || GetRoomUser() == null || GetBotData().WasPicked ||
                GetBotData().RandomSpeech == null || !GetBotData().RandomSpeech.Any())
            {
                StopTimerTick();
                return;
            }

            if (GetRoom() != null && GetRoom().MutedBots)
                return;

            string randomSpeech = GetBotData().GetRandomSpeech(GetBotData().MixPhrases);

            try
            {
                switch (randomSpeech)
                {
                    case ":sit":
                    {
                        RoomUser user = GetRoomUser();

                        if (user.RotBody%2 != 0)
                            user.RotBody--;

                        user.Z = GetRoom().GetGameMap().SqAbsoluteHeight(user.X, user.Y);

                        if (!user.Statusses.ContainsKey("sit"))
                        {
                            user.UpdateNeeded = true;
                            user.Statusses.Add("sit", "0.55");
                        }

                        user.IsSitting = true;

                        return;
                    }
                    case ":stand":
                    {
                        RoomUser user = GetRoomUser();

                        if (user.IsSitting)
                        {
                            user.Statusses.Remove("sit");
                            user.IsSitting = false;
                            user.UpdateNeeded = true;
                        }
                        else if (user.IsLyingDown)
                        {
                            user.Statusses.Remove("lay");
                            user.IsLyingDown = false;
                            user.UpdateNeeded = true;
                        }

                        return;
                    }
                }

                if (GetRoom() != null)
                {
                    randomSpeech = randomSpeech.Replace("%user_count%",
                        GetRoom().GetRoomUserManager().GetRoomUserCount().ToString());
                    randomSpeech = randomSpeech.Replace("%item_count%",
                        GetRoom().GetRoomItemHandler().TotalItems.ToString());
                    randomSpeech = randomSpeech.Replace("%floor_item_count%",
                        GetRoom().GetRoomItemHandler().FloorItems.Keys.Count.ToString());
                    randomSpeech = randomSpeech.Replace("%wall_item_count%",
                        GetRoom().GetRoomItemHandler().WallItems.Keys.Count.ToString());

                    if (GetRoom().RoomData != null)
                    {
                        randomSpeech = randomSpeech.Replace("%roomname%", GetRoom().RoomData.Name);
                        randomSpeech = randomSpeech.Replace("%owner%", GetRoom().RoomData.Owner);
                    }
                }

                if (GetBotData() != null)
                    randomSpeech = randomSpeech.Replace("%name%", GetBotData().Name);

                GetRoomUser().Chat(null, randomSpeech, false, 0);
            }
            catch (Exception e)
            {
                ServerLogManager.LogException(e.ToString());
            }
        }

        /// <summary>
        ///     Chats the timer tick.
        /// </summary>
        /// <param name="o">The o.</param>
        private void ChatTimerTick(object o) => OnChatTick();
    }
}