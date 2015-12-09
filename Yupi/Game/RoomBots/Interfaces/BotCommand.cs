using System;
using System.Data;

namespace Yupi.Game.RoomBots.Interfaces
{
    /// <summary>
    /// Class CatalogBot.
    /// </summary>
    internal class BotCommand
    {
        /// <summary>
        /// The bot type
        /// </summary>
        internal string BotType;

        /// <summary>
        /// The bot SpeechInput
        /// </summary>
        internal string SpeechInput;

        /// <summary>
        /// The bot SpeechInputAlias
        /// </summary>
        internal string[] SpeechInputAlias;

        /// <summary>
        /// The bot SpeechOutput
        /// </summary>
        internal string SpeechOutput;

        /// <summary>
        /// The bot gender
        /// </summary>
        internal bool SpeechIsFromSpeeches;

        /// <summary>
        /// The speeches speech identifier
        /// </summary>
        internal uint SpeechesSpeechId;

        /// <summary>
        /// The action command
        /// </summary>
        internal string ActionCommand;

        /// <summary>
        /// The action command parameters
        /// </summary>
        internal string ActionCommandParameters;

        /// <summary>
        /// The action bot
        /// </summary>
        internal string ActionBot;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogBot" /> class.
        /// </summary>
        /// <param name="row">The row.</param>
        internal BotCommand(DataRow row)
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
        }
    }
}
