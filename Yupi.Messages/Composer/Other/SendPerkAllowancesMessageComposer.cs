using System;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Other
{
	public class SendPerkAllowancesMessageComposer : AbstractComposerVoid
	{
		// TODO Refactor (hardcoded)
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) { 
				message.AppendInteger(11);

				message.AppendString("BUILDER_AT_WORK");
				message.AppendString(string.Empty);
				message.AppendBool(true);

				message.AppendString("VOTE_IN_COMPETITIONS");
				message.AppendString("requirement.unfulfilled.helper_level_2");
				message.AppendBool(false);

				message.AppendString("USE_GUIDE_TOOL");
				message.AppendString((session.GetHabbo().TalentStatus == "helper" && session.GetHabbo().CurrentTalentLevel >= 4) || (session.GetHabbo().Rank >= 4) ? string.Empty : "requirement.unfulfilled.helper_level_4");
				message.AppendBool((session.GetHabbo().TalentStatus == "helper" && session.GetHabbo().CurrentTalentLevel >= 4) || (session.GetHabbo().Rank >= 4));

				message.AppendString("JUDGE_CHAT_REVIEWS");
				message.AppendString("requirement.unfulfilled.helper_level_6");
				message.AppendBool(false);

				message.AppendString("NAVIGATOR_ROOM_THUMBNAIL_CAMERA");
				message.AppendString(string.Empty);
				message.AppendBool(true);

				message.AppendString("CALL_ON_HELPERS");
				message.AppendString(string.Empty);
				message.AppendBool(true);

				message.AppendString("CITIZEN");
				message.AppendString(string.Empty);
				message.AppendBool(session.GetHabbo().TalentStatus == "helper" || session.GetHabbo().CurrentTalentLevel >= 4);

				message.AppendString("MOUSE_ZOOM");
				message.AppendString(string.Empty);
				message.AppendBool(false);

				bool tradeLocked = session.GetHabbo ().CheckTrading ();

				message.AppendString("TRADE");
				message.AppendString(tradeLocked ? string.Empty : "requirement.unfulfilled.no_trade_lock");
				message.AppendBool(tradeLocked);

				message.AppendString("CAMERA");
				message.AppendString(string.Empty);
				message.AppendBool(ServerExtraSettings.EnableBetaCamera);

				message.AppendString("NAVIGATOR_PHASE_TWO_2014");
				message.AppendString(string.Empty);
				message.AppendBool(true);
			}
		}
	}
}

