namespace Yupi.Messages.Contracts
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;

    public class UpdateUserStatusMessageComposer : AbstractComposer<IList<RoomEntity>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<RoomEntity> entities)
        {
        }

        #endregion Methods
    }
}