// ---------------------------------------------------------------------------------
// <copyright file="ServerSettings.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Net
{
    using System;

    public class ServerSettings : IServerSettings
    {
        #region Properties

        public int Backlog
        {
            get; set;
        }

        public int BufferSize
        {
            get; set;
        }

        public string IP
        {
            get; set;
        }

        public int MaxConnections
        {
            get; set;
        }

        public int MaxIOThreads
        {
            get; set;
        }

        public int MaxWorkingThreads
        {
            get; set;
        }

        public int MinIOThreads
        {
            get; set;
        }

        public int MinWorkingThreads
        {
            get; set;
        }

        public int Port
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public ServerSettings()
        {
            MinIOThreads = 0;
            MaxIOThreads = 0;
            BufferSize = 4096;
            Backlog = 100;
            MaxConnections = 1000;
        }

        #endregion Constructors
    }
}