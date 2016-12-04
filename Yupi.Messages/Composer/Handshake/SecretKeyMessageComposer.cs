// ---------------------------------------------------------------------------------
// <copyright file="SecretKeyMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.Handshake
{
    using System;

    using Yupi.Crypto;
    using Yupi.Protocol.Buffers;
    using Yupi.Util.Settings;

    public class SecretKeyMessageComposer : Yupi.Messages.Contracts.SecretKeyMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                if(CryptoSettings.Instance.Enabled)
                {
                    message.AppendString(Encryption.GetInstance().GetRSADiffieHellmanPublicKey());
                    message.AppendBool(CryptoSettings.Instance.ServerRC4);
                }
                else
                {
                    message.AppendString("Crypto disabled");
                    message.AppendBool(false);
                }

                session.Send(message);
            }
        }

        #endregion Methods
    }
}