// ---------------------------------------------------------------------------------
// <copyright file="TalentManager.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class TalentManager
    {
        #region Constructors

        //private IRepository<Talent> TalentRepository;
        public TalentManager()
        {
            //TalentRepository = DependencyFactory.Resolve<IRepository<Talent>> ();
        }

        #endregion Constructors

        #region Methods

        public void CompleteUserTalent(Habbo user, Talent talent)
        {
            throw new NotImplementedException();
            /*if (user.Info.Talents.Any ((x) => x.Talent == talent))
                return;

            if (!LevelIsCompleted (user, talent.Type, talent.Level))
                return;

            if (talent.PrizeBaseItem != null)
                Yupi.GetGame ()
                    .GetCatalogManager ()
                    .DeliverItems (session, talent.PrizeBaseItem, 1,
                    string.Empty, 0, 0, string.Empty);

            user.Info.Talents.Add (talent.Id, new UserTalent (talent.Id, 1));

            user.Session.Router.GetComposer<AchievementTalentComposer> ().Compose (user, talent);

            if (talent.Type == "citizenship") {
                switch (talent.Level) {
                case 3:
                    Yupi.GetGame ().GetAchievementManager ().ProgressUserAchievement (session, "ACH_Citizenship", 1);
                    break;
                case 4:
                    session.GetHabbo ().GetSubscriptionManager ().AddSubscription (7);

                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ())
                        queryReactor.RunFastQuery (
                                $"UPDATE users SET talent_status = 'helper' WHERE id = '{session.GetHabbo().Id}'");

                    break;
                }
            }*/
        }

        public bool LevelIsCompleted(UserInfo user, TalentType trackType, int talentLevel)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}