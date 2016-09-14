// ---------------------------------------------------------------------------------
// <copyright file="T.cs" company="https://github.com/sant0ro/Yupi">
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

    using NGettext;

    public class T
    {
        #region Fields

        private static readonly ICatalog _Catalog = new Catalog("Example", "./locale");

        #endregion Fields

        #region Methods

        public static string _(string text)
        {
            return _Catalog.GetString(text);
        }

        public static string _(string text, params object[] args)
        {
            return _Catalog.GetString(text, args);
        }

        public static string _n(string text, string pluralText, long n)
        {
            return _Catalog.GetPluralString(text, pluralText, n);
        }

        public static string _n(string text, string pluralText, long n, params object[] args)
        {
            return _Catalog.GetPluralString(text, pluralText, n, args);
        }

        public static string _p(string context, string text)
        {
            return _Catalog.GetParticularString(context, text);
        }

        public static string _p(string context, string text, params object[] args)
        {
            return _Catalog.GetParticularString(context, text, args);
        }

        public static string _pn(string context, string text, string pluralText, long n)
        {
            return _Catalog.GetParticularPluralString(context, text, pluralText, n);
        }

        public static string _pn(string context, string text, string pluralText, long n, params object[] args)
        {
            return _Catalog.GetParticularPluralString(context, text, pluralText, n, args);
        }

        #endregion Methods
    }
}