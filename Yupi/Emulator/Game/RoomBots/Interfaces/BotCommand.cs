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
using System.Data;

namespace Yupi.Emulator.Game.RoomBots.Interfaces
{
    /// <summary>
    ///     Class CatalogBot.
    /// </summary>
     class BotCommand
    {
        /// <summary>
        ///     The action bot
        /// </summary>
         string ActionBot;

        /// <summary>
        ///     The action command
        /// </summary>
         string ActionCommand;

        /// <summary>
        ///     The action command parameters
        /// </summary>
         string ActionCommandParameters;

        /// <summary>
        ///     The bot type
        /// </summary>
         string BotType;

         bool ForceBotCommand;

        /// <summary>
        ///     The speeches speech identifier
        /// </summary>
         uint SpeechesSpeechId;

        /// <summary>
        ///     The bot SpeechInput
        /// </summary>
         string SpeechInput;

        /// <summary>
        ///     The bot SpeechInputAlias
        /// </summary>
         string[] SpeechInputAlias;

        /// <summary>
        ///     The bot gender
        /// </summary>
         bool SpeechIsFromSpeeches;

        /// <summary>
        ///     The bot SpeechOutput
        /// </summary>
         string SpeechOutput;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CatalogBot" /> class.
        /// </summary>
        /// <param name="row">The row.</param>
         BotCommand(DataRow row)
        {
            BotType = row["bot_type"].ToString();
            SpeechInput = row["speech_input"].ToString();
            SpeechInputAlias = row["speech_input_alias"].ToString().Split(';');
            SpeechOutput = row["speech_output"].ToString();
            SpeechIsFromSpeeches = Convert.ToBoolean(uint.Parse(row["speech_is_from_speeches"].ToString()));
            SpeechesSpeechId = uint.Parse(row["speech_speeches_id"].ToString());
            ActionCommand = row["action_command"].ToString();
            ActionCommandParameters = row["action_command_parameters"].ToString();
            ActionBot = row["action_bot"].ToString();
            ForceBotCommand = row["force_command_execution"].ToString() == "1";
        }
    }
}