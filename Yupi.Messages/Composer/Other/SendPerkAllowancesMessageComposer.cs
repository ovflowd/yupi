using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Other
{
    public class SendPerkAllowancesMessageComposer : Contracts.SendPerkAllowancesMessageComposer
    {
        // TODO Refactor (hardcoded)
        public override void Compose(ISender session, UserInfo info, bool enableBetaCamera)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(11); // Count

                message.AppendString("BUILDER_AT_WORK"); // Name
                message.AppendString(string.Empty); // Error Message
                message.AppendBool(true); // Allowed

                message.AppendString("VOTE_IN_COMPETITIONS");
                message.AppendString("requirement.unfulfilled.helper_level_2");
                message.AppendBool(false);

                // FIXME
                message.AppendString("USE_GUIDE_TOOL");
                message.AppendString("requirement.unfulfilled.helper_level_4");
                message.AppendBool(false);
                /*
                message.AppendString((info.TalentStatus == "helper" && info.CurrentTalentLevel >= 4) || (info.Rank >= 4) ? string.Empty : "requirement.unfulfilled.helper_level_4");
                message.AppendBool((info.TalentStatus == "helper" && info.CurrentTalentLevel >= 4) || (info.Rank >= 4));
*/
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
                // FIXME
                message.AppendBool(false); // info.TalentStatus == "helper" || info.CurrentTalentLevel >= 4

                message.AppendString("MOUSE_ZOOM");
                message.AppendString(string.Empty);
                message.AppendBool(false);

                var tradeLocked = info.CanTrade();

                message.AppendString("TRADE");
                message.AppendString(tradeLocked ? string.Empty : "requirement.unfulfilled.no_trade_lock");
                message.AppendBool(tradeLocked);

                message.AppendString("CAMERA");
                message.AppendString(string.Empty);
                message.AppendBool(enableBetaCamera);

                message.AppendString("NAVIGATOR_PHASE_TWO_2014");
                message.AppendString(string.Empty);
                message.AppendBool(true);

                session.Send(message);
            }
        }
    }
}