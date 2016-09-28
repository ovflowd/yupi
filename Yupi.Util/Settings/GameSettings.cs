// ---------------------------------------------------------------------------------
// <copyright file="GameSettings.cs" company="https://github.com/sant0ro/Yupi">
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

    using Config.Net;

    public static class GameSettings
    {
        #region Fields

        public static readonly Setting<bool> DebugConnections = new Setting<bool>("Debug.Connections", false);
        public static readonly Setting<bool> DebugDatabase = new Setting<bool>("Debug.Database", false);
        public static readonly Setting<bool> DebugPackets = new Setting<bool>("Debug.Packets", false);
        public static readonly Setting<bool> DebugRooms = new Setting<bool>("Debug.Rooms", false);
        public static readonly Setting<bool> EnableCamera = new Setting<bool>("game.feature.camera", false);
        public static readonly Setting<string> GameBind = new Setting<string>("Game.Bind", "127.0.0.1");
        public static readonly Setting<int> GameConLimit = new Setting<int>("Game.ConLimit", 5000);
        public static readonly Setting<int> GameConLimitPerIp = new Setting<int>("Game.ConLimitPerIp", 10);
        public static readonly Setting<int> GamePort = new Setting<int>("Game.Port", 3000);
        public static readonly Setting<string> Release = new Setting<string>("RELEASE.Release", "PRODUCTION-201510201205-42435347");
        public static readonly Setting<string> RestAuthKey = new Setting<string>("Rest.AuthKey", "");
        public static readonly Setting<string> RestBind = new Setting<string>("REST.Bind", "127.0.0.1");
        public static readonly Setting<int> RestPort = new Setting<int>("Rest.Port", 8080);
        public static readonly Setting<string> RestWhiteList = new Setting<string>("Rest.WhiteList", "127.0.0.1");

        #endregion Fields

        #region Constructors

        static GameSettings()
        {
            Cfg.Configuration.UseIniFile(Settings.GetPath("game.ini"));
        }

        #endregion Constructors
    }
}