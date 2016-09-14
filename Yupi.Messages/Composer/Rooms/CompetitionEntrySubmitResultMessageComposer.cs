using System;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class CompetitionEntrySubmitResultMessageComposer : Contracts.CompetitionEntrySubmitResultMessageComposer
    {
        public override void Compose(ISender session, RoomCompetition competition, int status, RoomData room = null)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(competition.Id);
                message.AppendString(competition.Name);
                message.AppendInteger(status);

                if (status != 3)
                {
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                }
                else
                {
                    message.AppendInteger(competition.RequiredItems.Count);

                    foreach (var furni in competition.RequiredItems)
                        message.AppendString(furni.Name);

                    if (room == null)
                        message.AppendInteger(0);
                    else
                    {
                        message.StartArray();

                        foreach (var furni in competition.RequiredItems)
                            throw new NotImplementedException();

                        message.EndArray();
                    }
                }
                session.Send(message);
            }
        }
    }
}