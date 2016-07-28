using System;
using Yupi.Protocol.Buffers;
using System.Data;


namespace Yupi.Messages.Support
{
	public class ModerationToolUserToolMessageComposer : AbstractComposer<uint>
	{
		public override void Compose ( Yupi.Protocol.ISender session, uint userId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
				{
						queryReactor.SetQuery(
							"SELECT id, username, mail, look, trade_lock, trade_lock_expire, rank, ip_last, " +
							"IFNULL(cfhs, 0) cfhs, IFNULL(cfhs_abusive, 0) cfhs_abusive, IFNULL(cautions, 0) cautions, IFNULL(bans, 0) bans, " +
							"IFNULL(reg_timestamp, 0) reg_timestamp, IFNULL(login_timestamp, 0) login_timestamp " +
							$"FROM users left join users_info on (users.id = users_info.user_id) WHERE id = '{userId}' LIMIT 1"
						);

						DataRow row = queryReactor.GetRow();

					if(row == null) {
						session.SendNotif (Yupi.GetLanguage ().GetVar ("help_information_error"));
						return;
					}

					// TODO Refactor
						uint id = Convert.ToUInt32(row["id"]);
						message.AppendInteger(id);
						message.AppendString(row["username"].ToString());
						message.AppendString(row["look"].ToString());
						double regTimestamp = (double) row["reg_timestamp"];
						double loginTimestamp = (double) row["login_timestamp"];
						int unixTimestamp = Yupi.GetUnixTimeStamp();
						message.AppendInteger(
							(int) (regTimestamp > 0 ? Math.Ceiling((unixTimestamp - regTimestamp)/60.0) : regTimestamp));
						message.AppendInteger(
							(int)
							(loginTimestamp > 0 ? Math.Ceiling((unixTimestamp - loginTimestamp)/60.0) : loginTimestamp));
						message.AppendBool(true);
						message.AppendInteger(Convert.ToInt32(row["cfhs"]));
						message.AppendInteger(Convert.ToInt32(row["cfhs_abusive"]));
						message.AppendInteger(Convert.ToInt32(row["cautions"]));
						message.AppendInteger(Convert.ToInt32(row["bans"]));

						message.AppendInteger(0);
						uint rank = (uint) row["rank"];
						message.AppendString(row["trade_lock"].ToString() == "1"
							? Yupi.UnixToDateTime(int.Parse(row["trade_lock_expire"].ToString())).ToLongDateString()
							: "Not trade-locked");
						message.AppendString(rank < 6 ? row["ip_last"].ToString() : "127.0.0.1");
						message.AppendInteger(id);
						message.AppendInteger(0);

						message.AppendString($"E-Mail:         {row["mail"]}");
						message.AppendString($"Rank ID:        {rank}");
				}

				session.Send(message);
			}
		}
	}
}

