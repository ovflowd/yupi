namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;
    using System.Globalization;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class SetRoomUserMessageComposer : AbstractComposer<IList<RoomEntity>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, IList<RoomEntity> users)
        {
            // Do nothing by default.
        }

        public virtual void Compose(Yupi.Protocol.ISender room, RoomEntity user)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}