using System;
using System.Collections.Generic;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Users.Guides
{
    /// <summary>
    ///     Class GuideManager.
    /// </summary>
    internal class GuideManager
    {
        /// <summary>
        ///     The en cours
        /// </summary>
        public Dictionary<uint, GameClient> EnCours = new Dictionary<uint, GameClient>();

        /// <summary>
        ///     The guardians on duty
        /// </summary>
        internal List<GameClient> GuardiansOnDuty = new List<GameClient>();

        /// <summary>
        ///     The guides on duty
        /// </summary>
        internal List<GameClient> GuidesOnDuty = new List<GameClient>();

        /// <summary>
        ///     The helpers on duty
        /// </summary>
        internal List<GameClient> HelpersOnDuty = new List<GameClient>();

        /// <summary>
        ///     Gets or sets the guides count.
        /// </summary>
        /// <value>The guides count.</value>
        public int GuidesCount => GuidesOnDuty.Count;

        /// <summary>
        ///     Gets or sets the helpers count.
        /// </summary>
        /// <value>The helpers count.</value>
        public int HelpersCount => HelpersOnDuty.Count;

        /// <summary>
        ///     Gets or sets the guardians count.
        /// </summary>
        /// <value>The guardians count.</value>
        public int GuardiansCount => GuardiansOnDuty.Count;

        /// <summary>
        ///     Gets the random guide.
        /// </summary>
        /// <returns>GameClient.</returns>
        public GameClient GetRandomGuide() => GuidesOnDuty[new Random().Next(0, GuidesCount - 1)];

        /// <summary>
        ///     Adds the guide.
        /// </summary>
        /// <param name="guide">The guide.</param>
        public void AddGuide(GameClient guide)
        {
            switch (guide.GetHabbo().DutyLevel)
            {
                case 1:
                    if (!GuidesOnDuty.Contains(guide))
                        GuidesOnDuty.Add(guide);
                    break;

                case 2:
                    if (!HelpersOnDuty.Contains(guide))
                        HelpersOnDuty.Add(guide);
                    break;

                case 3:
                    if (!GuardiansOnDuty.Contains(guide))
                        GuardiansOnDuty.Add(guide);
                    break;

                default:
                    if (!GuidesOnDuty.Contains(guide))
                        GuidesOnDuty.Add(guide);
                    break;
            }
        }

        /// <summary>
        ///     Removes the guide.
        /// </summary>
        /// <param name="guide">The guide.</param>
        public void RemoveGuide(GameClient guide)
        {
            switch (guide.GetHabbo().DutyLevel)
            {
                case 1:
                    if (GuidesOnDuty.Contains(guide))
                        GuidesOnDuty.Remove(guide);
                    break;

                case 2:
                    if (HelpersOnDuty.Contains(guide))
                        HelpersOnDuty.Remove(guide);
                    break;

                case 3:
                    if (GuardiansOnDuty.Contains(guide))
                        GuardiansOnDuty.Remove(guide);
                    break;

                default:
                    if (GuidesOnDuty.Contains(guide))
                        GuidesOnDuty.Remove(guide);
                    break;
            }
        }
    }
}