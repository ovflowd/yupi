using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RefusePollMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var pollId = request.GetUInt32();

            /*
            session.GetHabbo().AnsweredPolls.Add(pollId);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("INSERT INTO users_polls VALUES (@userid , @pollid , 0 , '0' , '')");
                queryReactor.AddParameter("userid", session.GetHabbo().Id);
                queryReactor.AddParameter("pollid", pollId);
                queryReactor.RunQuery();
            }
            */
            throw new NotImplementedException();
        }
    }
}