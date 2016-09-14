// ---------------------------------------------------------------------------------
// <copyright file="ServerFactory.cs" company="https://github.com/sant0ro/Yupi">
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

    public class ServerFactory<T>
    {
        #region Methods

        public static IServer<T> CreateServer(int port)
        {
            IServerSettings settings = new ServerSettings()
            {
                IP = "Any",
                Port = port,
                MaxWorkingThreads = 0, // Let the OS decide
                MinWorkingThreads = 0,
                MinIOThreads = 0,
                MaxIOThreads = 0,
                BufferSize = 4096,
                Backlog = 100,
                MaxConnections = 10000 // Maximum overall connections
            };

            return CreateServer(settings);
        }

        public static IServer<T> CreateServer(IServerSettings settings)
        {
            CrossDomainSettings flashPolicy = new CrossDomainSettings("*", settings.Port);

            // TODO Add selection for SuperSocket vs DotNetty
            return new SuperSocketImpl.SuperServer<T>(settings, flashPolicy);
        }

        #endregion Methods
    }
}