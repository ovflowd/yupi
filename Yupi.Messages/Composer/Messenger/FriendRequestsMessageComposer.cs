﻿using System;
using System.Collections.Generic;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Messenger
{
	public class FriendRequestsMessageComposer : Yupi.Messages.Contracts.FriendRequestsMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, IList<FriendRequest> requests)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(requests.Count);
				message.AppendInteger(requests.Count); // TODO why the same value twice?

				foreach (FriendRequest request in requests) {
					message.AppendInteger(request.From.Id);
					message.AppendString(request.From.UserName);
					message.AppendString(request.From.Look);
				}
				session.Send (message);
			}
		}
	}
}
