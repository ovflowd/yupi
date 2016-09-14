// ---------------------------------------------------------------------------------
// <copyright file="AddBlackWord.cs" company="https://github.com/sant0ro/Yupi">
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
/* copyright */
using Yupi.Emulator.Core.Security.BlackWords;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Enable. This class cannot be inherited.
    /// </summary>
     public sealed class AddBlackWord : Command
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