#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Yupi.Core.Algorithms.Astar
{
    internal class PriorityQueue<T, TX> where T : IWeightAddable<TX>
    {
        public List<T> InnerList;
        protected IComparer<T> MComparer;

        public PriorityQueue(IComparer<T> comparer, int size)
        {
            MComparer = comparer;
            InnerList = new List<T>(size);
        }

        protected virtual int OnCompare(int i, int j)
        {
            return MComparer.Compare(InnerList[i], InnerList[j]);
        }

        private int BinarySearch(T value)
        {
            int low = 0, high = InnerList.Count - 1;

            while (low <= high)
            {
                var midpoint = (low + high) / 2;

                // check to see if value is equal to item in array
                if (MComparer.Compare(value, InnerList[midpoint]) == 0)
                    return midpoint;
                if (MComparer.Compare(value, InnerList[midpoint]) == -1)
                    high = midpoint - 1;
                else
                    low = midpoint + 1;
            }

            // item was not found
            return low;
        }

        /// <summary>
        /// Push an object onto the PQ
        /// </summary>
        /// <param name="item">The new object</param>
        /// <returns>The index in the list where the object is _now_. This will change when objects are taken from or put onto the PQ.</returns>
        public void Push(T item)
        {
            var location = BinarySearch(item);
            InnerList.Insert(location, item);
        }

        /// <summary>
        /// Get the smallest object and remove it.
        /// </summary>
        /// <returns>The smallest object</returns>
        public T Pop()
        {
            if (!InnerList.Any())
                return default(T);
            var item = InnerList[0];
            InnerList.RemoveAt(0);
            return item;
        }

        public void Update(T element, TX newValue)
        {
            InnerList.RemoveAt(BinarySearch(element));
            element.WeightChange = newValue;
            Push(element);
        }
    }
}