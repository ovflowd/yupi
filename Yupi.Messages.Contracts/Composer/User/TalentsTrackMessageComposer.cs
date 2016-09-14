namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class TalentsTrackMessageComposer : AbstractComposer<TalentType, IList<Talent>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, TalentType trackType, IList<Talent> talents)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}