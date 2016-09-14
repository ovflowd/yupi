namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Protocol.Buffers;

    public abstract class PetTrainerPanelMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int petId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}