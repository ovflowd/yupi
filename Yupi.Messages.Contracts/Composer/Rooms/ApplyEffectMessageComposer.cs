namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class ApplyEffectMessageComposer : AbstractComposer<RoomEntity, AvatarEffect>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomEntity entity, AvatarEffect effect)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}