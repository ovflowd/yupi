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

using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Game.RoomBots.Enumerators;
using Yupi.Emulator.Game.RoomBots.Interfaces;
using Yupi.Emulator.Game.RoomBots.Models;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Emulator.Game.RoomBots
{
    /// <summary>
    ///     Class RoomBot.
    /// </summary>
     public class RoomBot
    {
        /// <summary>
        ///     The ai type
        /// </summary>
     public AiType AiType;

        /// <summary>
        ///     The automatic chat
        /// </summary>
     public bool AutomaticChat, MixPhrases;

        /// <summary>
        ///     The bot identifier
        /// </summary>
     public uint BotId;

        /// <summary>
        ///     The bot type
        /// </summary>
     public string BotType;

        /// <summary>
        ///     The dance identifier
        /// </summary>
     public uint DanceId;

        /// <summary>
        ///     The gender
        /// </summary>
     public string Gender;

        /// <summary>
        ///     The last spoken phrase
        /// </summary>
     public int LastSpokenPhrase;

        /// <summary>
        ///     The look
        /// </summary>
     public string Look;

        /// <summary>
        ///     The maximum x
        /// </summary>
     public int MaxX;

        /// <summary>
        ///     The maximum y
        /// </summary>
     public int MaxY;

        /// <summary>
        ///     The minimum x
        /// </summary>
     public int MinX;

        /// <summary>
        ///     The minimum y
        /// </summary>
     public int MinY;

        /// <summary>
        ///     The motto
        /// </summary>
     public string Motto;

        /// <summary>
        ///     The name
        /// </summary>
     public string Name;

        /// <summary>
        ///     The owner identifier
        /// </summary>
     public uint OwnerId;

        /// <summary>
        ///     The random speech
        /// </summary>
     public List<string> RandomSpeech;

        /// <summary>
        ///     The responses
        /// </summary>
     public List<string> Responses;

        /// <summary>
        ///     The room identifier
        /// </summary>
     public uint RoomId;

        /// <summary>
        ///     The room user
        /// </summary>
     public RoomUser RoomUser;

        /// <summary>
        ///     The rot
        /// </summary>
     public int Rot;

        /// <summary>
        ///     The speech interval
        /// </summary>
     public uint SpeechInterval;

        /// <summary>
        ///     The virtual identifier
        /// </summary>
     public int VirtualId;

        /// <summary>
        ///     The walking mode
        /// </summary>
     public string WalkingMode;

        /// <summary>
        ///     The was picked
        /// </summary>
     public bool WasPicked;

        /// <summary>
        ///     The x
        /// </summary>
     public int X;

        /// <summary>
        ///     The y
        /// </summary>
     public int Y;

        /// <summary>
        ///     The z
        /// </summary>
     public double Z;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomBot" /> class.
        /// </summary>
        /// <param name="botId">The bot identifier.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="aiType">Type of the ai.</param>
        /// <param name="botType"></param>
     public RoomBot(uint botId, uint ownerId, AiType aiType, string botType)
        {
            OwnerId = ownerId;
            BotId = botId;
            AiType = aiType;
            VirtualId = -1;
            BotType = botType;
            RoomUser = null;
            LastSpokenPhrase = 1;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomBot" /> class.
        /// </summary>
        /// <param name="botId">The bot identifier.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="aiType">Type of the ai.</param>
        /// <param name="walkingMode">The walking mode.</param>
        /// <param name="name">The name.</param>
        /// <param name="motto">The motto.</param>
        /// <param name="look">The look.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="rot">The rot.</param>
        /// <param name="speeches">The speeches.</param>
        /// <param name="responses">The responses.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="dance">The dance.</param>
        /// <param name="botType"></param>
     public RoomBot(uint botId, uint ownerId, uint roomId, AiType aiType, string walkingMode, string name,
            string motto, string look, int x, int y, double z, int rot, List<string> speeches, List<string> responses,
            string gender, uint dance, string botType)
        {
            OwnerId = ownerId;
            BotId = botId;
            RoomId = roomId;
            AiType = aiType;
            WalkingMode = walkingMode;
            Name = name;
            Motto = motto;
            Look = look;
            X = x;
            Y = y;
            Z = z;
            Rot = rot;
            Gender = gender.ToUpper();
            VirtualId = -1;
            RoomUser = null;
            BotType = botType;
            DanceId = dance;
            RandomSpeech = speeches;
            Responses = responses;
            LastSpokenPhrase = 1;
            WasPicked = roomId == 0;
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is pet.
        /// </summary>
        /// <value><c>true</c> if this instance is pet; otherwise, <c>false</c>.</value>
     public bool IsPet => AiType == AiType.Pet;

        /// <summary>
        ///     Updates the specified room identifier.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="walkingMode">The walking mode.</param>
        /// <param name="name">The name.</param>
        /// <param name="motto">The motto.</param>
        /// <param name="look">The look.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="rot">The rot.</param>
        /// <param name="minX">The minimum x.</param>
        /// <param name="minY">The minimum y.</param>
        /// <param name="maxX">The maximum x.</param>
        /// <param name="maxY">The maximum y.</param>
        /// <param name="speeches">The speeches.</param>
        /// <param name="responses">The responses.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="dance">The dance.</param>
        /// <param name="speechInterval">The speech interval.</param>
        /// <param name="automaticChat">if set to <c>true</c> [automatic chat].</param>
        /// <param name="mixPhrases">if set to <c>true</c> [mix phrases].</param>
     public void Update(uint roomId, string walkingMode, string name, string motto, string look, int x, int y,
            double z, int rot, int minX, int minY, int maxX, int maxY, List<string> speeches, List<string> responses,
            string gender, uint dance, uint speechInterval, bool automaticChat, bool mixPhrases)
        {
            RoomId = roomId;
            WalkingMode = walkingMode;
            Name = name;
            Motto = motto;
            Look = look;
            X = x;
            Y = y;
            Z = z;
            Rot = rot;
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
            Gender = gender.ToUpper();
            VirtualId = -1;
            RoomUser = null;
            DanceId = dance;
            RandomSpeech = speeches;
            Responses = responses;
            WasPicked = roomId == 0;
            MixPhrases = mixPhrases;
            AutomaticChat = automaticChat;
            SpeechInterval = speechInterval;
        }

        /// <summary>
        ///     Gets the random speech.
        /// </summary>
        /// <param name="mixPhrases">if set to <c>true</c> [mix phrases].</param>
        /// <returns>System.String.</returns>
     public string GetRandomSpeech(bool mixPhrases)
        {
            if (!RandomSpeech.Any())
                return string.Empty;

            if (mixPhrases)
                return RandomSpeech[Yupi.GetRandomNumber(0, RandomSpeech.Count - 1)];

            if (LastSpokenPhrase >= RandomSpeech.Count)
                LastSpokenPhrase = 1;

            string result = RandomSpeech[LastSpokenPhrase - 1];

            LastSpokenPhrase++;

            return result;
        }

        /// <summary>
        ///     Generates the bot ai.
        /// </summary>
        /// <param name="virtualId">The virtual identifier.</param>
        /// <param name="botId">The bot identifier.</param>
        /// <returns>BotAI.</returns>
     public BotAi GenerateBotAi(int virtualId, int botId)
        {
            AiType aiType = AiType;

            if (aiType == AiType.Pet)
                return new PetBot(virtualId);

            return new GenericBot(this, virtualId, SpeechInterval);
        }
    }
}