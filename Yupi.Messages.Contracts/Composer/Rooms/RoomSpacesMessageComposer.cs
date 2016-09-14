using Headspring;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomSpacesMessageComposer :
        AbstractComposer<RoomSpacesMessageComposer.RoomSpacesType, RoomData>
    {
        public override void Compose(ISender session, RoomSpacesType type, RoomData data)
        {
            // Do nothing by default.
        }

        public class RoomSpacesType : Enumeration<RoomSpacesType>
        {
            public static readonly RoomSpacesType Wallpaper = new RoomSpacesType(0, "wallpaper");
            public static readonly RoomSpacesType Floor = new RoomSpacesType(1, "floor");
            public static readonly RoomSpacesType Landscape = new RoomSpacesType(2, "landscape");

            // TODO landscapeanim

            private RoomSpacesType(int value, string displayName) : base(value, displayName)
            {
            }
        }
    }
}