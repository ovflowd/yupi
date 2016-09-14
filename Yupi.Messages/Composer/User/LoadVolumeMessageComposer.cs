using Yupi.Model.Domain.Components;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class LoadVolumeMessageComposer : Contracts.LoadVolumeMessageComposer
    {
        public override void Compose(ISender session, UserPreferences preferences)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(preferences.Volume1);
                message.AppendInteger(preferences.Volume2);
                message.AppendInteger(preferences.Volume3);
                message.AppendBool(preferences.PreferOldChat);
                message.AppendBool(preferences.IgnoreRoomInvite);
                message.AppendBool(preferences.DisableCameraFollow);
                // TODO Add to preferences
                message.AppendInteger(3); // collapse friends (3 = no) 
                message.AppendInteger(preferences.ChatBubbleStyle.Value); //bubble

                session.Send(message);
            }
        }
    }
}