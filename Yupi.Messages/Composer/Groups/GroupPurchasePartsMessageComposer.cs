using System;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Groups
{
	// TODO Rename
	public class GroupPurchasePartsMessageComposer : AbstractComposerVoid
	{
		// TODO Refactor
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (Yupi.GetGame ().GetGroupManager ().Bases.Count);

				foreach (GroupBases current in Yupi.GetGame().GetGroupManager().Bases) {
					message.AppendInteger (current.Id);
					message.AppendString (current.Value1);
					message.AppendString (current.Value2);
				}

				message.AppendInteger (Yupi.GetGame ().GetGroupManager ().Symbols.Count);

				foreach (GroupSymbols current2 in Yupi.GetGame().GetGroupManager().Symbols) {
					message.AppendInteger (current2.Id);
					message.AppendString (current2.Value1);
					message.AppendString (current2.Value2);
				}

				message.AppendInteger (Yupi.GetGame ().GetGroupManager ().BaseColours.Count);

				foreach (GroupBaseColours current3 in Yupi.GetGame().GetGroupManager().BaseColours) {
					message.AppendInteger (current3.Id);
					message.AppendString (current3.Colour);
				}

				message.AppendInteger (Yupi.GetGame ().GetGroupManager ().SymbolColours.Count);

				foreach (GroupSymbolColours current4 in Yupi.GetGame().GetGroupManager().SymbolColours.Values) {
					message.AppendInteger (current4.Id);
					message.AppendString (current4.Colour);
				}

				message.AppendInteger (Yupi.GetGame ().GetGroupManager ().BackGroundColours.Count);

				foreach (GroupBackGroundColours current5 in Yupi.GetGame().GetGroupManager().BackGroundColours.Values) {
					message.AppendInteger (current5.Id);
					message.AppendString (current5.Colour);
				}

				session.Send (message);
			}
		}
	}
}

