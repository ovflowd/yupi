using System.Collections.Generic;
using System.Linq;

namespace Yupi.Model.Domain.Components
{
    public class Inventory
    {
        public Inventory()
        {
            Wardrobe = new List<WardrobeItem>();
            Pets = new List<PetItem>();
            FloorItems = new List<FloorItem>();
            WallItems = new List<WallItem>();
        }

        [OneToMany]
        public virtual IList<WardrobeItem> Wardrobe { get; protected set; }

        public virtual IList<PetItem> Pets { get; protected set; }

        public virtual IList<FloorItem> FloorItems { get; protected set; }

        public virtual IList<WallItem> WallItems { get; protected set; }

        public virtual FloorItem GetFloorItem(int id)
        {
            return FloorItems.SingleOrDefault(x => x.Id == id);
        }

        // TODO Use visitor pattern to achieve this?
        public virtual void Add(Item item)
        {
            if (item is FloorItem) FloorItems.Add((FloorItem) item);
            else if (item is WallItem) WallItems.Add((WallItem) item);
            else if (item is PetItem) Pets.Add((PetItem) item);
        }
    }
}