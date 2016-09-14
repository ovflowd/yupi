namespace Yupi.Messages.Messenger
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Util;

    public class DeleteFriendMessageEvent : AbstractHandler
    {
        #region Fields

        private RelationshipController RelationshipController;

        #endregion Fields

        #region Constructors

        public DeleteFriendMessageEvent()
        {
            RelationshipController = DependencyFactory.Resolve<RelationshipController>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int count = request.GetInteger();
            for (int i = 0; i < count; i++)
            {
                int friendId = request.GetInteger();

                RelationshipController.Remove(session, friendId);
            }
        }

        #endregion Methods
    }
}