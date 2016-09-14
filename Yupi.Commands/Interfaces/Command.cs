// ---------------------------------------------------------------------------------
// <copyright file="Command.cs" company="https://github.com/sant0ro/Yupi">
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

using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Interfaces
{
    /// <summary>
    ///     Class Command.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        ///     Gets or sets the minimum rank.
        /// </summary>
        /// <value>The minimum rank.</value>
		virtual short MinRank { public get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
		virtual string Description { public get; set; }

        /// <summary>
        ///     Gets or sets the usage.
        /// </summary>
        /// <value>The usage.</value>
		virtual string Usage { public get; set; }

        /// <summary>
        ///     Gets or sets the alias.
        /// </summary>
        /// <value>The alias.</value>
		virtual string Alias { public get; set; }

        /// <summary>
        ///     Gets or sets the minimum parameters.
        /// </summary>
        /// <value>The minimum parameters.</value>
		virtual short MinParams { public get; set; }

        /// <summary>
        ///     Executes the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="pms">The PMS.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract bool Execute(GameClient client, string[] pms);
    }
}