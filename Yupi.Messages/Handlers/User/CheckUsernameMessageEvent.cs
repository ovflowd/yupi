using System;

using System.Data;
using System.Collections.Generic;

namespace Yupi.Messages.User
{
	public class CheckUsernameMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string newName = message.GetString();
							
			List<string> alternatives;
			NameChangedUpdatesMessageComposer.Status status = Validate (newName, session.GetHabbo().UserName, out alternatives);

			router.GetComposer<NameChangedUpdatesMessageComposer> ()
				.Compose (session, status, newName, ref alternatives);
		}

		protected NameChangedUpdatesMessageComposer.Status Validate(string newName, string oldName, ref List<string> alternatives) {
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
			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery("SELECT COUNT(username) FROM users WHERE username=@name LIMIT 1");
				queryReactor.AddParameter("name", name);
				return queryReactor.GetInteger() != 0;
			}
		}

		protected bool ContainsInvalidChars(string name) {
			// TODO Use ASCII ???
			const string source = "abcdefghijklmnopqrstuvwxyz1234567890.,_-;:?!@áéíóúÁÉÍÓÚñÑÜüÝý";

			string lowerName = name.ToLower ();

			foreach (char letter in lowerName) {
				if (!source.Contains (letter)) {
					return true;
				}
			}

			string[] forbiddenWords = { "mod", "admin", "m0d" };

			foreach (string forbidden in forbiddenWords) {
				if (lowerName.Contains (forbidden)) {
					return true;
				}
			}

			return false;
		}

		private List<string> GetAlternatives(string name) {
			List<string> alternatives = new List<string> ();

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery ("SELECT tag FROM users_tags ORDER BY RAND() LIMIT 3");
				DataTable table = queryReactor.GetTable ();

				foreach (DataRow dataRow in table.Rows) {
					alternatives.Add (name + dataRow ["tag"]);
				}

				return alternatives;
			}
		}
	}
}

