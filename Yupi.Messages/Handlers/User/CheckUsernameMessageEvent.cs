using System;

using System.Data;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using System.Text.RegularExpressions;

namespace Yupi.Messages.User
{
	public class CheckUsernameMessageEvent : AbstractHandler
	{
		protected IRepository<UserInfo> UserRepository;

		public CheckUsernameMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string newName = message.GetString();

			List<string> alternatives;
			NameChangedUpdatesMessageComposer.Status status = Validate (newName, session.Info.UserName, out alternatives);

			if (status == NameChangedUpdatesMessageComposer.Status.OK) {
				session.Info.UserName = newName;

				router.GetComposer<UpdateUsernameMessageComposer> ().Compose (session, newName);
				// TODO Refactor

				UserRepository.Save (session.Info);

				if (session.Room != null)
				{
					router.GetComposer<UserUpdateNameInRoomMessageComposer> ()
						.Compose (session.Room, session);
				}

				// TODO Update room owner 

				// TODO Notify messenger
			}
		}

		protected NameChangedUpdatesMessageComposer.Status Validate(string newName, string oldName, out List<string> alternatives) {
			alternatives = new List<string> ();

			if (newName.ToLower() == oldName.ToLower())
			{
				return NameChangedUpdatesMessageComposer.Status.OK;
			}

			if (newName.Length > 15) { // TODO Magic constant !
				return NameChangedUpdatesMessageComposer.Status.TOO_LONG;
			} else if (newName.Length < 3) {
				return NameChangedUpdatesMessageComposer.Status.TOO_SHORT;
			} else if (ContainsInvalidChars(newName)) {
				return NameChangedUpdatesMessageComposer.Status.INVALID_CHARS;
			} else if (DoesExist (newName)) {
				alternatives = GetAlternatives (newName);
				return NameChangedUpdatesMessageComposer.Status.IS_TAKEN;
			} else {
				return NameChangedUpdatesMessageComposer.Status.OK;
			}
		}

		protected bool DoesExist(string name) {
			return UserRepository.Exists((x) => x.UserName == name);
		}

		protected bool ContainsInvalidChars(string name) {
			// TODO Use ASCII ???
			const string pattern = "[abcdefghijklmnopqrstuvwxyz1234567890.,_-;:?!@áéíóúÁÉÍÓÚñÑÜüÝý]+";

			string lowerName = name.ToLower ();

			string[] forbiddenWords = { "mod", "admin", "m0d" };

			foreach (string forbidden in forbiddenWords) {
				if (lowerName.Contains (forbidden)) {
					return true;
				}
			}

			return !Regex.IsMatch (name, pattern);
		}

		private List<string> GetAlternatives(string name) {
			List<string> alternatives = new List<string> ();
			// TODO Implement
			return alternatives;
		}
	}
}

