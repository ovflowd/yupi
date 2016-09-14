using System.Collections.Generic;
using System.Linq;

namespace Yupi.Model.Domain.Components
{
    public class SongMachineComponent
    {
        public SongMachineComponent()
        {
            _SongItems = new List<SongItem>();
        }

        protected virtual List<SongItem> _SongItems { get; set; }

        [Ignore]
        public virtual IReadOnlyList<SongItem> SongItems
        {
            get { return _SongItems.AsReadOnly(); }
        }

        [Ignore]
        public virtual int Capacity
        {
            get { return 20; // TODO What is the real capacity?
            }
        }

        public virtual bool TryAdd(SongItem item)
        {
            if (_SongItems.Count < Capacity)
            {
                _SongItems.Add(item);
                return true;
            }
            return false;
        }

        public virtual void Remove(SongItem item)
        {
            _SongItems.Remove(item);
        }

        public virtual SongItem Find(int id)
        {
            return _SongItems.SingleOrDefault(x => x.Id == id);
        }
    }
}