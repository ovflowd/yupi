// ---------------------------------------------------------------------------------
// <copyright file="LTD.cs" company="https://github.com/sant0ro/Yupi">
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
using Yupi.Emulator.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;



namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class LTD. This class cannot be inherited.
    /// </summary>
     public sealed class Ltd : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Ltd" /> class.
        /// </summary>
        public Ltd()
        {
            MinRank = 7;
            Description = "Atualiza os LTDS";
            Usage = ":ltd";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                FurnitureDataManager.SetCache();
                Yupi.GetGame().GetItemManager().LoadItems(adapter);
                Yupi.GetGame().GetCatalogManager().Init(adapter);
                Yupi.GetGame().ReloadItems();
                FurnitureDataManager.Clear();
            }
            Yupi.GetGame()
                .GetClientManager()
                .QueueBroadcaseMessage(
                    new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("PublishShopMessageComposer")));
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SuperNotificationMessageComposer"));
            messageBuffer.AppendString("ninja_promo_LTD");
            messageBuffer.AppendInteger(4);
            messageBuffer.AppendString("title");
            messageBuffer.AppendString("Novo Raro Limitado!");
            messageBuffer.AppendString("message");
            messageBuffer.AppendString(
                "<i><h1>Como Assim?</h1>, Um Novo Raro Limitado foi Adicionado na Loja!<br> Descubra como ele é Abrindo a Loja!</br>");
            messageBuffer.AppendString("linkUrl");
            messageBuffer.AppendString("event:catalog/open/ultd_furni");
            messageBuffer.AppendString("linkTitle");
            messageBuffer.AppendString("Ver o Raro");

            Yupi.GetGame().GetClientManager().QueueBroadcaseMessage(messageBuffer);
            return true;
        }
    }
}