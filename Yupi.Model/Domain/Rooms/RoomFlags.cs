namespace Yupi.Model.Domain
{
    using System;

    #region Enumerations

    [Flags]
    public enum RoomFlags
    {
        Default = 0,
        Image = 1,
        Group = 2,
        Event = 4,
        Private = 8,
        AllowPets = 16,
        EnterRoom = 32
    }

    #endregion Enumerations
}