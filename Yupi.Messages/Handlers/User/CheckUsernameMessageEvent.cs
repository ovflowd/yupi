// ---------------------------------------------------------------------------------
// <copyright file="CheckUsernameMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Messages.User
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text.RegularExpressions;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class CheckUsernameMessageEvent : AbstractHandler
    {
        #region Fields

        protected IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public CheckUsernameMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            string newName = message.GetString();

            List<string> alternatives;
            NameChangedUpdatesMessageComposer.Status status = Validate(newName, session.Info.Name, out alternatives);

            if (status == NameChangedUpdatesMessageComposer.Status.OK)
            {
                session.Info.Name = newName;

                router.GetComposer<UpdateUsernameMessageComposer>().Compose(session, newName);
                // TODO Refactor

                UserRepository.Save(session.Info);

                if (session.Room != null)
                {
                    session.Room.EachUser(
                        (roomSession) =>
                        {
                            router.GetComposer<UserUpdateNameInRoomMessageComposer>()
                                .Compose(roomSession, session);
                        }
                    );
                }

                // TODO Update room owner

                // TODO Notify messenger
            }
        }

        protected bool ContainsInvalidChars(string name)
        {
            // TODO Use ASCII ???
            const string pattern = "[abcdefghijklmnopqrstuvwxyz1234567890.,_-;:?!@áéíóúÁÉÍÓÚñÑÜüÝý]+";

            string lowerName = name.ToLower();

            string[] forbiddenWords = {"mod", "admin", "m0d"};

            foreach (string forbidden in forbiddenWords)
            {
                if (lowerName.Contains(forbidden))
                {
                    return true;
                }
            }

            return !Regex.IsMatch(name, pattern);
        }

        protected bool DoesExist(string name)
        {
            return UserRepository.Exists((x) => x.Name == name);
        }

        protected NameChangedUpdatesMessageComposer.Status Validate(string newName, string oldName,
            out List<string> alternatives)
        {
            alternatives = new List<string>();

            if (newName.ToLower() == oldName.ToLower())
            {
                return NameChangedUpdatesMessageComposer.Status.OK;
            }

            if (newName.Length > 15)
            {
                // TODO Magic constant !
                return NameChangedUpdatesMessageComposer.Status.TOO_LONG;
            }
            else if (newName.Length < 3)
            {
                return NameChangedUpdatesMessageComposer.Status.TOO_SHORT;
            }
            else if (ContainsInvalidChars(newName))
            {
                return NameChangedUpdatesMessageComposer.Status.INVALID_CHARS;
            }
            else if (DoesExist(newName))
            {
                alternatives = GetAlternatives(newName);
                return NameChangedUpdatesMessageComposer.Status.IS_TAKEN;
            }
            else
            {
                return NameChangedUpdatesMessageComposer.Status.OK;
            }
        }

        private List<string> GetAlternatives(string name)
        {
            List<string> alternatives = new List<string>();
            // TODO Implement
            return alternatives;
        }

        #endregion Methods
    }
}