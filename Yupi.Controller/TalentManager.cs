/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using System;

namespace Yupi.Controller
{
	public class TalentManager
	{
		//private Repository<Talent> TalentRepository;

		public TalentManager ()
		{
			//TalentRepository = DependencyFactory.Resolve<Repository<Talent>> ();
		}

		public bool LevelIsCompleted (UserInfo user, Talent.TalentType trackType, int talentLevel)
		{
			throw new NotImplementedException ();
		}

		public void CompleteUserTalent (Habbo user, Talent talent)
		{
			throw new NotImplementedException ();
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
	}
}