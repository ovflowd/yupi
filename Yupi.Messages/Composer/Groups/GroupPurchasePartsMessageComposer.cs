// ---------------------------------------------------------------------------------
// <copyright file="GroupPurchasePartsMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Groups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Protocol.Buffers;

    // TODO Rename
    public class GroupPurchasePartsMessageComposer : Yupi.Messages.Contracts.GroupPurchasePartsMessageComposer
    {
        #region Fields

        private IRepository<GroupBackGroundColours> BackgroundColorRepository;
        private IRepository<GroupBaseColours> BaseColorRepository;
        private IRepository<GroupBases> GroupBaseRepository;
        private IRepository<GroupSymbolColours> SymbolColorRepository;
        private IRepository<GroupSymbols> SymbolRepository;

        #endregion Fields

        #region Methods

        // TODO Refactor
        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                List<GroupBases> bases = GroupBaseRepository.All().ToList();
                List<GroupSymbols> symbols = SymbolRepository.All().ToList();
                List<GroupBaseColours> baseColors = BaseColorRepository.All().ToList();
                List<GroupSymbolColours> symbolColors = SymbolColorRepository.All().ToList();
                List<GroupBackGroundColours> backgroundColors = BackgroundColorRepository.All().ToList();

                message.AppendInteger(bases.Count);

                foreach (GroupBases groupBase in bases)
                {
                    message.AppendInteger(groupBase.Id);
                    message.AppendString(groupBase.Value1);
                    message.AppendString(groupBase.Value2);
                }

                message.AppendInteger(symbols.Count);

                foreach (GroupSymbols symbol in symbols)
                {
                    message.AppendInteger(symbol.Id);
                    message.AppendString(symbol.Value1);
                    message.AppendString(symbol.Value2);
                }

                message.AppendInteger(baseColors.Count);

                foreach (GroupBaseColours baseColor in baseColors)
                {
                    message.AppendInteger(baseColor.Id);
                    message.AppendString(baseColor.Colour);
                }

                message.AppendInteger(symbolColors.Count);

                foreach (GroupSymbolColours symbolColor in symbolColors)
                {
                    message.AppendInteger(symbolColor.Id);
                    message.AppendString(symbolColor.Colour.ToString());
                }

                message.AppendInteger(backgroundColors.Count);

                foreach (GroupBackGroundColours current5 in backgroundColors)
                {
                    message.AppendInteger(current5.Id);
                    message.AppendString(current5.Colour.ToString());
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}