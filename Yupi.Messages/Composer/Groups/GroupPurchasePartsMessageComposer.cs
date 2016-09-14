using System.Linq;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;

namespace Yupi.Messages.Groups
{
    // TODO Rename
    public class GroupPurchasePartsMessageComposer : Contracts.GroupPurchasePartsMessageComposer
    {
        private IRepository<GroupBackGroundColours> BackgroundColorRepository;
        private IRepository<GroupBaseColours> BaseColorRepository;
        private IRepository<GroupBases> GroupBaseRepository;
        private IRepository<GroupSymbolColours> SymbolColorRepository;
        private IRepository<GroupSymbols> SymbolRepository;

        // TODO Refactor
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                var bases = GroupBaseRepository.All().ToList();
                var symbols = SymbolRepository.All().ToList();
                var baseColors = BaseColorRepository.All().ToList();
                var symbolColors = SymbolColorRepository.All().ToList();
                var backgroundColors = BackgroundColorRepository.All().ToList();

                message.AppendInteger(bases.Count);

                foreach (var groupBase in bases)
                {
                    message.AppendInteger(groupBase.Id);
                    message.AppendString(groupBase.Value1);
                    message.AppendString(groupBase.Value2);
                }

                message.AppendInteger(symbols.Count);

                foreach (var symbol in symbols)
                {
                    message.AppendInteger(symbol.Id);
                    message.AppendString(symbol.Value1);
                    message.AppendString(symbol.Value2);
                }

                message.AppendInteger(baseColors.Count);

                foreach (var baseColor in baseColors)
                {
                    message.AppendInteger(baseColor.Id);
                    message.AppendString(baseColor.Colour);
                }

                message.AppendInteger(symbolColors.Count);

                foreach (var symbolColor in symbolColors)
                {
                    message.AppendInteger(symbolColor.Id);
                    message.AppendString(symbolColor.Colour.ToString());
                }

                message.AppendInteger(backgroundColors.Count);

                foreach (var current5 in backgroundColors)
                {
                    message.AppendInteger(current5.Id);
                    message.AppendString(current5.Colour.ToString());
                }

                session.Send(message);
            }
        }
    }
}