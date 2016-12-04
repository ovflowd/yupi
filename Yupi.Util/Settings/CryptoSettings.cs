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

    public class CryptoSettings : SettingsContainer
    {
        private static readonly CryptoSettings instance = new CryptoSettings();

        // TODO Replace singleton with Dependency Injection
        public static CryptoSettings Instance
        {
            get 
            {
                return instance; 
            }
        }

        #region Fields

        public readonly Option<int> DHKeysSize = new Option<int>("Crypto.DHKeysSize", 128);
        public readonly Option<bool> Enabled = new Option<bool>("Crypto.Enabled", true);
        public readonly Option<string> RsaD = new Option<string>("Crypto.RsaD", "");
        public readonly Option<string> RsaE = new Option<string>("Crypto.RsaE", "");
        public readonly Option<string> RsaN = new Option<string>("Crypto.RsaN", "");
        public readonly Option<bool> ServerRC4 = new Option<bool>("Crypto.ServerRC4", true);

        #endregion Fields

        protected override void OnConfigure(IConfigConfiguration configuration)
        {
            configuration.UseIniFile(Settings.GetPath("crypto.ini"));
        }
    }
}