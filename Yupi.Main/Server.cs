// ---------------------------------------------------------------------------------
// <copyright file="Server.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Main
{
    using System;

    using log4net;
    using log4net.Appender;
    using log4net.Repository.Hierarchy;

    using Yupi.Controller;
    using Yupi.Messages;
    using Yupi.Messages.Achievements;
    using Yupi.Messages.User;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Net;
    using Yupi.Protocol.Buffers;
    using Yupi.Rest;
    using Yupi.Util;
    using Util.Settings;
    using Crypto;
    using System.Numerics;
    using Crypto.Utils;

    public class Server
    {
        #region Fields

        private ClientManager ClientManager;
        private RestServer RestServer;
        private IServer<Habbo> TCPServer;

        #endregion Fields

        #region Constructors

        public Server()
        {
            SetupLogger();

            if (CryptoSettings.Enabled)
                Encryption.GetInstance(new Crypto.Cryptography.RSACParameters(RSACUtils.Base64ToBigInteger(CryptoSettings.RsaD), RSACUtils.Base64ToBigInteger(CryptoSettings.RsaN), RSACUtils.Base64ToBigInteger(CryptoSettings.RsaE)), CryptoSettings.DHKeysSize);

            // TODO Close Session & Multiple sessions!
            var factory = ModelHelper.CreateFactory();
            var dbSession = factory.OpenSession();

            DependencyFactory.RegisterInstance(dbSession);

            // TODO Don't run this if DB is not new!
            ModelHelper.Populate();
            Router.Default = new Router(GameSettings.Release, "../../../Config/",
                typeof(AchievementProgressMessageComposer).Assembly);

            ClientManager = DependencyFactory.Resolve<ClientManager>();

            RestServer = new RestServer();

            SetupTCP();
        }

        #endregion Constructors

        #region Methods

        public void Run()
        {
            TCPServer.Start();
            RestServer.Start();
        }

        private void SetupLogger()
        {
            Hierarchy hierarchy = (Hierarchy) LogManager.GetRepository();
            hierarchy.Root.Level = log4net.Core.Level.Debug;
            hierarchy.RaiseConfigurationChanged(EventArgs.Empty);

            AppenderSkeleton appender;
            if (MonoUtil.IsRunningOnMono())
            {
                appender = new log4net.Appender.ConsoleAppender();
            }
            else
            {
                appender = new log4net.Appender.ColoredConsoleAppender();
            }
            // TODO Add File Appender
            appender.Layout = new log4net.Layout.PatternLayout(@"%date [%c{2}] %-5level %message%newline");
            appender.Threshold = log4net.Core.Level.Debug;
            appender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(appender);
            /*
            var fileAppender = new log4net.Appender.FileAppender ();
            fileAppender.AppendToFile = true;
            fileAppender.Layout = new log4net.Layout.PatternLayout(@"%date %-5level %message%newline");
            fileAppender.Threshold = log4net.Core.Level.Debug;
            fileAppender.File = "log.txt";
            fileAppender.ActivateOptions ();
            log4net.Config.BasicConfigurator.Configure(fileAppender);*/
        }

        private void SetupTCP()
        {
            TCPServer = ServerFactory<Habbo>.CreateServer(GameSettings.GamePort);

            TCPServer.OnConnectionOpened += ClientManager.AddClient; // TODO Connection security!
            TCPServer.OnConnectionClosed += ClientManager.RemoveClient;
            TCPServer.OnMessageReceived += (ISession<Habbo> session, byte[] body) =>
            {
                //using(global::Yupi.Emulator.Messages.Buffers.SimpleClientMessageBuffer message = ClientMessageFactory.GetClientMessage()) {
                // TODO When using message pool the SimpleClientMessageBuffer becomes invalid (after several messages) -> DEBUG
                ClientMessage message = new ClientMessage();
                message.Setup(body);
                Router.Default.Handle(session.UserData, message);
            };
        }

        #endregion Methods
    }
}