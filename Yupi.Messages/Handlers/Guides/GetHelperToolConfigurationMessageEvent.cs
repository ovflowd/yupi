namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;

    public class GetHelperToolConfigurationMessageEvent : AbstractHandler
    {
        #region Fields

        private GuideManager GuideManager;

        #endregion Fields

        #region Constructors

        public GetHelperToolConfigurationMessageEvent()
        {
            GuideManager = DependencyFactory.Resolve<GuideManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            bool onDuty = message.GetBool();

            // TODO Use these values
            message.GetBool();
            message.GetBool();
            message.GetBool();

            if (onDuty)
                GuideManager.Add(session);
            else
                GuideManager.Remove(session);

            router.GetComposer<HelperToolConfigurationMessageComposer>().Compose(session, onDuty,
                GuideManager.Guides.Count, GuideManager.Helpers.Count, GuideManager.Guardians.Count);
        }

        #endregion Methods
    }
}