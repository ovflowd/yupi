namespace Yupi.Messages.Contracts
{
    using Headspring;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class RoomSpacesMessageComposer : AbstractComposer<RoomSpacesMessageComposer.RoomSpacesType, RoomData>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomSpacesMessageComposer.RoomSpacesType type,
            RoomData data)
        {
            // Do nothing by default.
        }

        #endregion Methods

        #region Nested Types

        public class RoomSpacesType : Enumeration<RoomSpacesType>
        {
            #region Fields

            public static readonly RoomSpacesType Floor = new RoomSpacesType(1, "floor");
            public static readonly RoomSpacesType Landscape = new RoomSpacesType(2, "landscape");
            public static readonly RoomSpacesType Wallpaper = new RoomSpacesType(0, "wallpaper");

            #endregion Fields

            #region Constructors

            // TODO landscapeanim
            private RoomSpacesType(int value, string displayName)
                : base(value, displayName)
            {
            }

            #endregion Constructors
        }

        #endregion Nested Types
    }
}