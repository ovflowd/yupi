using Yupi.Emulator.Core.Security.BlackWords;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Enable. This class cannot be inherited.
    /// </summary>
    internal sealed class AddBlackWord : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AddBlackWord" /> class.
        /// </summary>
        public AddBlackWord()
        {
            MinRank = 8;
            Description = "Adds a word to filter list.";
            Usage = ":addblackword type(hotel|insult|all) word";
            MinParams = 2;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            string type = pms[0];
            string word = pms[1];

            if (string.IsNullOrEmpty(word))
            {
                session.SendWhisper("String Can't be Empty!");

                return true;
            }

            BlackWordsManager.AddBlackWord(type, word);

            return true;
        }
    }
}