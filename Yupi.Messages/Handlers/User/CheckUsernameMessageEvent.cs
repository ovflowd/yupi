using System.Collections.Generic;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class CheckUsernameMessageEvent : AbstractHandler
    {
        protected IRepository<UserInfo> UserRepository;

        public CheckUsernameMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var newName = message.GetString();

            List<string> alternatives;
            var status = Validate(newName, session.Info.Name, out alternatives);

            if (status == Contracts.NameChangedUpdatesMessageComposer.Status.OK)
            {
                session.Info.Name = newName;

                router.GetComposer<UpdateUsernameMessageComposer>().Compose(session, newName);
                // TODO Refactor

                UserRepository.Save(session.Info);

                if (session.Room != null)
                    session.Room.EachUser(
                        roomSession =>
                        {
                            router.GetComposer<UserUpdateNameInRoomMessageComposer>()
                                .Compose(roomSession, session);
                        }
                    );

                // TODO Update room owner 

                // TODO Notify messenger
            }
        }

        protected Contracts.NameChangedUpdatesMessageComposer.Status Validate(string newName, string oldName,
            out List<string> alternatives)
        {
            alternatives = new List<string>();

            if (newName.ToLower() == oldName.ToLower())
                return Contracts.NameChangedUpdatesMessageComposer.Status.OK;

            if (newName.Length > 15) return Contracts.NameChangedUpdatesMessageComposer.Status.TOO_LONG;
            if (newName.Length < 3) return Contracts.NameChangedUpdatesMessageComposer.Status.TOO_SHORT;
            if (ContainsInvalidChars(newName)) return Contracts.NameChangedUpdatesMessageComposer.Status.INVALID_CHARS;
            if (DoesExist(newName))
            {
                alternatives = GetAlternatives(newName);
                return Contracts.NameChangedUpdatesMessageComposer.Status.IS_TAKEN;
            }
            return Contracts.NameChangedUpdatesMessageComposer.Status.OK;
        }

        protected bool DoesExist(string name)
        {
            return UserRepository.Exists(x => x.Name == name);
        }

        protected bool ContainsInvalidChars(string name)
        {
            // TODO Use ASCII ???
            const string pattern = "[abcdefghijklmnopqrstuvwxyz1234567890.,_-;:?!@áéíóúÁÉÍÓÚñÑÜüÝý]+";

            var lowerName = name.ToLower();

            string[] forbiddenWords = {"mod", "admin", "m0d"};

            foreach (var forbidden in forbiddenWords) if (lowerName.Contains(forbidden)) return true;

            return !Regex.IsMatch(name, pattern);
        }

        private List<string> GetAlternatives(string name)
        {
            var alternatives = new List<string>();
            // TODO Implement
            return alternatives;
        }
    }
}