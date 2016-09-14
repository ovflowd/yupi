using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using System.Linq;
using System.Collections.Generic;


namespace Yupi.Messages.Groups
{
    // TODO Rename
    public class GroupPurchasePartsMessageComposer : Yupi.Messages.Contracts.GroupPurchasePartsMessageComposer
    {
        private IRepository<GroupBases> GroupBaseRepository;
        private IRepository<GroupSymbols> SymbolRepository;
        private IRepository<GroupBaseColours> BaseColorRepository;
        private IRepository<GroupSymbolColours> SymbolColorRepository;
        private IRepository<GroupBackGroundColours> BackgroundColorRepository;

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
    }
}