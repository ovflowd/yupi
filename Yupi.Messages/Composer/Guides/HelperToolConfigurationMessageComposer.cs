namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Protocol.Buffers;

    public class HelperToolConfigurationMessageComposer : Yupi.Messages.Contracts.HelperToolConfigurationMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, bool onDuty, int guideCount, int helperCount,
            int guardianCount)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(onDuty);
                message.AppendInteger(guideCount);
                message.AppendInteger(helperCount);
                message.AppendInteger(guardianCount);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}