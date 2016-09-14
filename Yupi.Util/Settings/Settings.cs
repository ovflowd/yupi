// ---------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Util.Settings
{
    using System;
    using System.IO;
    using System.Reflection;

    using Config.Net;

    public static class Settings
    {
        #region Fields

        private static readonly string ConfigDir;

        #endregion Fields

        #region Constructors

        static Settings()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string assemblyDir = Directory.GetParent(assemblyLocation).FullName;

            // TODO Get the directory structure sorted!
            assemblyDir = Directory.GetParent(assemblyDir).Parent.Parent.FullName;

            ConfigDir = Path.Combine(assemblyDir, "Config", "Settings");

            if (!Directory.Exists(ConfigDir))
            {
                Directory.CreateDirectory(ConfigDir);
            }
        }

        #endregion Constructors

        #region Methods

        public static string GetPath(string fileName)
        {
            return Path.Combine(ConfigDir, fileName);
        }

        #endregion Methods
    }
}