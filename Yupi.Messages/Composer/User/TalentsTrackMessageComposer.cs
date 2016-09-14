using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.User
{
    public class TalentsTrackMessageComposer : Yupi.Messages.Contracts.TalentsTrackMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, TalentType trackType, IList<Talent> talents)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(trackType.DisplayName);
                message.AppendInteger(talents.Count);

                int failLevel = -1;

                foreach (Talent current in talents)
                {
                    message.AppendInteger(current.Id);
                    int nm = failLevel == -1 ? 1 : 0; // TODO What does this mean?
                    message.AppendInteger(nm);

                    message.AppendInteger(current.Levels.Count);

                    foreach (TalentLevel child in current.Levels)
                    {
                        /*	UserAchievement achievment = child.Achievement;
    
                            int num;
                            // TODO Refactor What does num mean?!
                            if (failLevel != -1 && failLevel < child.Level) {
                                num = 0;
                            } else if (achievment == null) {
                                num = 1;
                            } else if (achievment.Level >=
                                child.AchievementLevel) {
                                num = 2;
                            } else {
                                num = 1;
                            }
    
                            message.AppendInteger (child.Achievement.Id);
                            message.AppendInteger (0); // TODO Magic constant
    
                            message.AppendString(child.Achievement.GroupName+child.Achievement.DefaultLevel);
                            message.AppendInteger(num);
    
                            message.AppendInteger(achievment != null ? achievment.Progress : 0);
                            message.AppendInteger(child.GetAchievement() == null ? 0
                                : child.GetAchievement().Levels[child.AchievementLevel].Requirement);
    
                            if (num != 2 && failLevel == -1)
                                failLevel = child.Level;
                                */
                    }

                    throw new NotImplementedException();

                    message.AppendInteger(0); // TODO Magic constant

                    message.AppendInteger(1); // Count
                    message.AppendString(""); // Prize name ( HABBO_CLUB_VIP_7_DAYS )
                    message.AppendInteger(0); // Prize value (7)
                }

                session.Send(message);
            }
        }
    }
}