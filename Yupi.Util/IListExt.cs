// ---------------------------------------------------------------------------------
// <copyright file="IListExt.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Util
{
    using System;
    using System.Collections.Generic;

    public static class IListExt
    {
        #region Fields

        private static Random rnd = new Random();

        #endregion Fields

        #region Methods

        public static T Random<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                return default(T);
            }

            lock (rnd)
            {
                return list[rnd.Next(list.Count)];
            }
        }

        public static int RemoveAll<T>(this IList<T> list, Predicate<T> match)
        {
            if (list == null || match == null)
            {
                return 0;
            }

            int count = 0;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (match(list[i]))
                {
                    ++count;
                    list.RemoveAt(i);
                }
            }

            return count;
        }

        #endregion Methods
    }
}