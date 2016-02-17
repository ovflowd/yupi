/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System.Collections.Generic;
using System.Linq;
using Yupi.Core.Algorithms.Astar.Interfaces;

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
                int midpoint = (low + high)/2;

                if (MComparer.Compare(value, InnerList[midpoint]) == 0)
                    return midpoint;
                if (MComparer.Compare(value, InnerList[midpoint]) == -1)
                    high = midpoint - 1;
                else
                    low = midpoint + 1;
            }

            return low;
        }

        /// <summary>
        ///     Push an object onto the PQ
        /// </summary>
        /// <param name="item">The new object</param>
        /// <returns>
        ///     The index in the list where the object is _now_. This will change when objects are taken from or put onto the
        ///     PQ.
        /// </returns>
        public void Push(T item)
        {
            int location = BinarySearch(item);
            InnerList.Insert(location, item);
        }

        /// <summary>
        ///     Get the smallest object and remove it.
        /// </summary>
        /// <returns>The smallest object</returns>
        public T Pop()
        {
            if (!InnerList.Any())
                return default(T);

            T item = InnerList[0];
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