namespace Yupi.Messages.Pets
{
    using System;

    public class PetGetInformationMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            if (session.GetHabbo() == null || session.GetHabbo().CurrentRoom == null)
                return;

            uint petId = request.GetUInt32();

            // TODO Refactor! Pet != User
            RoomUser pet = session.GetHabbo().CurrentRoom.GetRoomUserManager().GetPet(petId);

            if (pet?.PetData == null)
                return;

            router.GetComposer<PetInfoMessageComposer> ().Compose (session, pet);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}