// ---------------------------------------------------------------------------------
// <copyright file="Vector2Ext.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Model
{
    using System;
    using System.Numerics;

    public static class Vector2Ext
    {
        #region Methods

        public static int CalculateRotation(this Vector3 source, Vector3 target)
        {
            return CalculateRotation(source.ToVector2(), target.ToVector2());
        }

        public static int CalculateRotation(this Vector2 source, Vector2 target)
        {
            double theta = Math.Atan2(target.Y - source.Y, target.X - source.X);

            double degree = theta*180/Math.PI;

            if (degree < 0)
            {
                degree += 360;
            }

            return (((int) degree + 90)/45)%8;
        }

        public static bool Equals(this Vector3 a, Vector2 b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static Vector2 ToVector2(this Vector3 source)
        {
            return new Vector2(source.X, source.Y);
        }

        #endregion Methods
    }
}