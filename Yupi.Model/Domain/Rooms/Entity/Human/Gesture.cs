// ---------------------------------------------------------------------------------
// <copyright file="Gesture.cs" company="https://github.com/sant0ro/Yupi">
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

    public class Gesture : Enumeration<Gesture>
    {
        #region Fields

        public static readonly Gesture Angry = new Gesture(2, "agr");
        public static readonly Gesture None = new Gesture(0, "");
        public static readonly Gesture Sad = new Gesture(4, "sad");

        // TODO Can these be used by pets too?
        public static readonly Gesture Smile = new Gesture(1, "sml");
        public static readonly Gesture Surprised = new Gesture(3, "srp");

        #endregion Fields

        #region Constructors

        protected Gesture(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}