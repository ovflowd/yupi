namespace Yupi.Model.Domain.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SongMachineComponent
    {
        #region Properties

        [Ignore]
        public virtual int Capacity
        {
            get { return 20; // TODO What is the real capacity?
            }
        }

        [Ignore]
        public virtual IReadOnlyList<SongItem> SongItems
        {
            get { return _SongItems.AsReadOnly(); }
        }

        protected virtual List<SongItem> _SongItems
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public SongMachineComponent()
        {
            _SongItems = new List<SongItem>();
        }

        #endregion Constructors

        #region Methods

        public virtual SongItem Find(int id)
        {
            return _SongItems.SingleOrDefault(x => x.Id == id);
        }

        public virtual void Remove(SongItem item)
        {
            _SongItems.Remove(item);
        }

        public virtual bool TryAdd(SongItem item)
        {
            if (_SongItems.Count < Capacity)
            {
                _SongItems.Add(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Methods
    }
}