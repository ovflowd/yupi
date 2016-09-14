namespace Yupi.Messages.Navigator
{
    using System;

    using Yupi.Model.Domain.Components;
    using Yupi.Protocol.Buffers;

    public class NewNavigatorSizeMessageComposer : Yupi.Messages.Contracts.NewNavigatorSizeMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserPreferences preferences)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(preferences.NewnaviX);
                message.AppendInteger(preferences.NewnaviY);
                message.AppendInteger(preferences.NavigatorWidth);
                message.AppendInteger(preferences.NavigatorHeight);
                message.AppendBool(false);
                message.AppendInteger(1);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}