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

    public class GameSettings : SettingsContainer
    {
        private static readonly GameSettings instance = new GameSettings();

        public static GameSettings Instance
        {
            get 
            {
                return instance; 
            }
        }

        #region Fields

        public readonly Option<bool> DebugConnections = new Option<bool>("Debug.Connections", false);
        public readonly Option<bool> DebugDatabase = new Option<bool>("Debug.Database", false);
        public readonly Option<bool> DebugPackets = new Option<bool>("Debug.Packets", false);
        public readonly Option<bool> DebugRooms = new Option<bool>("Debug.Rooms", false);
        public readonly Option<bool> EnableCamera = new Option<bool>("game.feature.camera", false);
        public readonly Option<string> GameBind = new Option<string>("Game.Bind", "127.0.0.1");
        public readonly Option<int> GameConLimit = new Option<int>("Game.ConLimit", 5000);
        public readonly Option<int> GameConLimitPerIp = new Option<int>("Game.ConLimitPerIp", 10);
        public readonly Option<int> GamePort = new Option<int>("Game.Port", 3000);
        public readonly Option<string> Release = new Option<string>("RELEASE.Release", "PRODUCTION-201510201205-42435347");
        public readonly Option<string> RestAuthKey = new Option<string>("Rest.AuthKey", "");
        public readonly Option<string> RestBind = new Option<string>("REST.Bind", "127.0.0.1");
        public readonly Option<int> RestPort = new Option<int>("Rest.Port", 8080);
        public readonly Option<string> RestWhiteList = new Option<string>("Rest.WhiteList", "127.0.0.1");

        #endregion Fields

        protected override void OnConfigure(IConfigConfiguration configuration)
        {
            configuration.UseIniFile(Settings.GetPath("game.ini"));
        }
    }
}