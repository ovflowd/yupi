// ---------------------------------------------------------------------------------
// <copyright file="PacketLibrary.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using MadMilkman.Ini;

    public class PacketLibrary
    {
        #region Fields

        private static readonly log4net.ILog Logger = log4net.LogManager
            .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string ConfigDir;
        private Dictionary<string, short> Incoming;
        private Dictionary<string, short> Outgoing;
        private string Release;

        #endregion Fields

        #region Constructors

        public PacketLibrary(string release, string configDir)
        {
            this.Release = release;
            this.ConfigDir = configDir;
            Reload();
        }

        #endregion Constructors

        #region Methods

        public short GetIncomingId(string name)
        {
            short id;

            Incoming.TryGetValue(name, out id);

            if (id == default(short))
            {
                Logger.WarnFormat("Unknown incoming message: {0}", name);
            }

            return id;
        }

        public short GetOutgoingId(string name)
        {
            short id;

            Outgoing.TryGetValue(name, out id);

            if (id == default(short))
            {
                Logger.WarnFormat("Unknown outgoing message: {0}", name);
            }

            return id;
        }

        public void Reload()
        {
            LoadIncoming();
            LoadOutgoing();
        }

        private void LoadIncoming()
        {
            ReadFiles("*.incoming", ref Incoming);
        }

        private void LoadOutgoing()
        {
            ReadFiles("*.outgoing", ref Outgoing);
        }

        private void ReadFiles(string pattern, ref Dictionary<string, short> packets)
        {
            packets = new Dictionary<string, short>();

            string path = Path.Combine(ConfigDir, "Packets", Release);

            string[] files = Directory.GetFiles(path, pattern);

            foreach (string file in files)
            {
                ReadIni(file, packets);
            }
        }

        private void ReadIni(string file, Dictionary<string, short> packets)
        {
            IniFile ini = new IniFile();
            ini.Load(file);

            foreach (IniSection section in ini.Sections)
            {
                foreach (IniKey key in section.Keys)
                {
                    short id;
                    key.TryParseValue<short>(out id);

                    if (id == default(short))
                    {
                        // TODO Log warning
                        continue;
                    }

                    packets.Add(key.Name, id);
                }
            }
        }

        #endregion Methods
    }
}