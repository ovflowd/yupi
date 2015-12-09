using System;
using System.Linq;
using System.Threading;
using Yupi.Core.Io;
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
        private int _speechInterval;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericBot" /> class.
        /// </summary>
        /// <param name="roomBot">The room bot.</param>
        /// <param name="virtualId">The virtual identifier.</param>
        /// <param name="speechInterval">The speech interval.</param>
        internal GenericBot(RoomBot roomBot, int virtualId, int speechInterval)
        {
            _virtualId = virtualId;
            _speechInterval = speechInterval < 2 ? 2000 : speechInterval*1000;

            // Get random speach
            // @issue #80
            //if (roomBot != null && roomBot.RandomSpeech != null && roomBot.RandomSpeech.Any()) _chatTimer = new Timer(ChatTimerTick, null, _speechInterval, _speechInterval);

            if (roomBot != null && roomBot.AutomaticChat && roomBot.RandomSpeech != null && roomBot.RandomSpeech.Any())
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

            // @issue #80
            //if (GetBotData().RandomSpeech == null || !GetBotData().RandomSpeech.Any())

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
                    var randomPoint = GetRoom().GetGameMap().GetRandomWalkableSquare();

                    if (randomPoint.X == 0 || randomPoint.Y == 0)
                            return;

                    GetRoomUser().MoveTo(randomPoint.X, randomPoint.Y);
                    break;
                }
                case "specified_range":
                {
                    var list = GetRoom().GetGameMap().WalkableList.ToList();

                    if (!list.Any())
                            return;

                    var randomNumber = new Random(DateTime.Now.Millisecond + _virtualId ^ 2).Next(0, list.Count - 1);

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
                GetRoomUser().Chat(null, "Precisa de Algo?", false, 0);
                return;
            }   

            if (GetBotData().BotType != command.BotType)
                return;

            if(command.SpeechOutput != string.Empty)
                GetRoomUser().Chat(null, command.SpeechOutput, false, 0);

            if (command.ActionCommand != string.Empty && command.ActionCommandParameters != string.Empty)
                CommandsManager.TryExecute(command.ActionCommand, command.ActionCommandParameters, user.GetClient());

            switch (command.ActionBot)
            {
                default:
                case "bot_move_to_user":
                    GetRoomUser().MoveTo(user.SquareInFront);
                    break;
            }

            #region old
            //switch (message.Substring(1).ToLower())
            /*{
                case "ven":
                case "comehere":
                case "come here":
                case "ven aquí":
                case "come":
                case "vem aqui":
                case "venha":
                case "venha aqui":
                case "vem aquí":
                    GetRoomUser().Chat(null, "Estou Indo!", false, 0);
                    GetRoomUser().MoveTo(user.SquareInFront);
                    return;

                case "sirve":
                case "serve":
                case "sirva":
                    if (GetRoom().CheckRights(user.GetClient()))
                    {
                        foreach (var current in GetRoom().GetRoomUserManager().GetRoomUsers())
                            current.CarryItem(Random.Next(1, 38));
                        GetRoomUser().Chat(null, "Worth. Agora você tem algo para devorar todos.", false, 0);
                        return;
                    }
                    return;

                case "agua":
                case "água":
                case "té":
                case "te":
                case "tea":
                case "juice":
                case "water":
                case "zumo":
                    GetRoomUser().Chat(null, "Aqui você vai.", false, 0);
                    user.CarryItem(Random.Next(1, 3));
                    return;

                case "helado":
                case "icecream":
                case "sorvete":
                case "ice cream":
                    GetRoomUser()
                        .Chat(null, "Aqui você vai. Isso não é o idioma que se encaixam perto, hehe!", false, 0);
                    user.CarryItem(4);
                    return;

                case "rose":
                case "rosa":
                    GetRoomUser().Chat(null, "Aqui você vai ... você faz bem em sua nomeação.", false, 0);
                    user.CarryItem(Random.Next(1000, 1002));
                    return;

                case "girasol":
                case "girassol":
                case "sunflower":
                    GetRoomUser().Chat(null, "Aqui estão algumas muito agradável natureza.", false, 0);
                    user.CarryItem(1002);
                    return;

                case "flor":
                case "flower":
                    GetRoomUser().Chat(null, "Aqui estão algumas muito agradável da natureza.", false, 0);
                    if (Random.Next(1, 3) == 2)
                    {
                        user.CarryItem(Random.Next(1019, 1024));
                        return;
                    }
                    user.CarryItem(Random.Next(1006, 1010));
                    return;

                case "zanahoria":
                case "zana":
                case "carrot":
                case "cenoura":
                    GetRoomUser().Chat(null, "Aqui está um bom vegetal. Divirta-se!", false, 0);
                    user.CarryItem(3);
                    return;

                case "café":
                case "cafe":
                case "capuccino":
                case "coffee":
                case "latte":
                case "mocha":
                case "espresso":
                case "expreso":
                    GetRoomUser().Chat(null, "Aqui está o seu café. É espumante!", false, 0);
                    user.CarryItem(Random.Next(11, 18));
                    return;

                case "fruta":
                case "fruit":
                    GetRoomUser().Chat(null, "Aqui está um pouco saudável, fresco e natural. Aproveite!", false, 0);
                    user.CarryItem(Random.Next(36, 40));
                    return;

                case "naranja":
                case "orange":
                case "laranja":
                    GetRoomUser().Chat(null, "Aqui está um pouco saudável, fresco e natural. Aproveite!", false, 0);
                    user.CarryItem(38);
                    return;

                case "manzana":
                case "apple":
                case "maça":
                case "maçã":
                case "maca":
                case "macã":
                    GetRoomUser().Chat(null, "Aqui está um pouco saudável, fresco e natural. Aproveite!", false, 0);
                    user.CarryItem(37);
                    return;

                case "cola":
                case "habbocola":
                case "habbo cola":
                case "coca cola":
                case "cocacola":
                    GetRoomUser().Chat(null, "Aqui é uma bebida muito famosa macio.", false, 0);
                    user.CarryItem(19);
                    return;

                case "pear":
                case "pera":
                case "pêra":
                    GetRoomUser().Chat(null, "Aqui está um pouco saudável, fresco e natural. Aproveite!", false, 0);
                    user.CarryItem(36);
                    return;

                case "ananá":
                case "pineapple":
                case "piña":
                case "rodaja de piña":
                    GetRoomUser().Chat(null, "Aqui está um pouco saudável, fresco e natural. Aproveite!", false, 0);
                    user.CarryItem(39);
                    return;

                case "puta":
                case "puto":
                case "gilipollas":
                case "metemela":
                case "polla":
                case "pene":
                case "penis":
                case "idiot":
                case "fuck":
                case "bastardo":
                case "idiota":
                case "chupamela":
                case "tonta":
                case "tonto":
                case "mierda":
                case "vadia":
                case "prostituta":
                case "vaca":
                case "feiosa":
                case "filha da puta":
                case "gostosa":
                    GetRoomUser().Chat(null, "Não me trate mal, eh!", true, 0);
                    return;

                case "case comigo":
                    GetRoomUser().Chat(null, "Irei agora!", true, 0);
                    return;

                case "protocolo destruir":
                    GetRoomUser().Chat(null, "Iniciando Auto Destruição do Mundo!", true, 0);
                    return;

                case "lindo":
                case "hermoso":
                case "linda":
                case "guapa":
                case "beautiful":
                case "handsome":
                case "love":
                case "guapo":
                case "i love you":
                case "hermosa":
                case "preciosa":
                case "te amo":
                case "amor":
                case "mi amor":
                    GetRoomUser()
                        .Chat(null, "Eu sou um bot, err ... isto está a ficar desconfortável, você sabe?", false, 0);
                    return;

                case "tyrex":
                    GetRoomUser().Chat(null, "Por favor, me chame de Deus Tyrex!", true, 0);
                    return;*/
            #endregion
        }

        /// <summary>
        ///     Called when [user shout].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="message">The message.</param>
        internal override void OnUserShout(RoomUser user, string message)
        {
            GetRoomUser().Chat(null, "Não precisa gritar, caramba! Se precisa de algo basta vir aqui.", false, 0);
        }

        /// <summary>
        ///     Stops the timer tick.
        /// </summary>
        private void StopTimerTick()
        {
            if (_chatTimer == null) return;
            _chatTimer.Change(Timeout.Infinite, Timeout.Infinite);
            _chatTimer.Dispose();
            _chatTimer = null;
        }

        internal override void OnChatTick()
        {
            if (GetBotData() == null || GetRoomUser() == null || GetBotData().WasPicked ||
                GetBotData().RandomSpeech == null ||
                !GetBotData().RandomSpeech.Any())
            {
                StopTimerTick();
                return;
            }

            if (GetRoom() != null && GetRoom().MutedBots)
                return;

            var randomSpeech = GetBotData().GetRandomSpeech(GetBotData().MixPhrases);

            try
            {
                switch (randomSpeech)
                {
                    case ":sit":
                    {
                        var user = GetRoomUser();
                        if (user.RotBody%2 != 0) user.RotBody--;

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
                        var user = GetRoomUser();
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

                if (GetBotData() != null) randomSpeech = randomSpeech.Replace("%name%", GetBotData().Name);

                GetRoomUser().Chat(null, randomSpeech, false, 0);
            }
            catch (Exception e)
            {
                Writer.LogException(e.ToString());
            }
        }

        /// <summary>
        ///     Chats the timer tick.
        /// </summary>
        /// <param name="o">The o.</param>
        private void ChatTimerTick(object o)
        {
            OnChatTick();
        }
    }
}