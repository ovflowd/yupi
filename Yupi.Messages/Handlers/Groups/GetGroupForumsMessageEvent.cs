namespace Yupi.Messages.Groups
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class GetGroupForumsMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int selectType = request.GetInteger();
            int startIndex = request.GetInteger();

            router.GetComposer<GroupForumListingsMessageComposer>().Compose(session, selectType, startIndex);
        }

        #endregion Methods
    }
}