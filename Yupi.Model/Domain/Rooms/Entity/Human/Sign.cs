// ---------------------------------------------------------------------------------
// <copyright file="Sign.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Model.Domain
{
    using System;

    using Headspring;

    public class Sign : Enumeration<Sign>, IStatusString
    {
        #region Fields

        public static readonly Sign Eight = new Sign(8, "Eight");
        public static readonly Sign Exclamation_Mark = new Sign(13, "Exclamation_Mark");
        public static readonly Sign Five = new Sign(5, "Five");
        public static readonly Sign Four = new Sign(4, "Four");
        public static readonly Sign Heart = new Sign(11, "Heart");
        public static readonly Sign Nine = new Sign(9, "Nine");
        public static readonly Sign One = new Sign(1, "One");
        public static readonly Sign Seven = new Sign(7, "Seven");
        public static readonly Sign Six = new Sign(6, "Six");
        public static readonly Sign Skull = new Sign(12, "Skull");
        public static readonly Sign Smiley = new Sign(15, "Smiley");
        public static readonly Sign Soccer_Ball = new Sign(14, "Soccer_Ball");
        public static readonly Sign Soccer_Red_Card = new Sign(16, "Soccer_Red_Card");
        public static readonly Sign Soccer_Yellow_Card = new Sign(17, "Soccer_Yellow_Card");
        public static readonly Sign Ten = new Sign(10, "Ten");
        public static readonly Sign Three = new Sign(3, "Three");
        public static readonly Sign Two = new Sign(2, "Two");
        public static readonly Sign Zero = new Sign(0, "Zero");

        #endregion Fields

        #region Constructors

        protected Sign(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors

        #region Methods

        public string ToStatusString()
        {
            return "sign " + this.Value;
        }

        #endregion Methods
    }
}