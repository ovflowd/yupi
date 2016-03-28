namespace Yupi.Game.Quests
{
    /// <summary>
    ///     Class Quest.
    /// </summary>
    public class Quest
    {
        /// <summary>
        ///     The category
        /// </summary>
        internal readonly string Category;

        /// <summary>
        ///     The data bit
        /// </summary>
        internal readonly string DataBit;

        /// <summary>
        ///     The goal data
        /// </summary>
        internal readonly uint GoalData;

        /// <summary>
        ///     The goal type
        /// </summary>
        internal readonly QuestType GoalType;

        /// <summary>
        ///     The has ended
        /// </summary>
        internal readonly bool HasEnded;

        /// <summary>
        ///     The identifier
        /// </summary>
        internal readonly int Id;

        /// <summary>
        ///     The name
        /// </summary>
        internal readonly string Name;

        /// <summary>
        ///     The number
        /// </summary>
        internal readonly int Number;

        /// <summary>
        ///     The reward
        /// </summary>
        internal readonly uint Reward;

        /// <summary>
        ///     The reward type
        /// </summary>
        internal readonly int RewardType;

        /// <summary>
        ///     The time unlock
        /// </summary>
        internal readonly int TimeUnlock;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Quest" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="category">The category.</param>
        /// <param name="number">The number.</param>
        /// <param name="goalType">Type of the goal.</param>
        /// <param name="goalData">The goal data.</param>
        /// <param name="name">The name.</param>
        /// <param name="reward">The reward.</param>
        /// <param name="dataBit">The data bit.</param>
        /// <param name="rewardType">Type of the reward.</param>
        /// <param name="timeUnlock">The time unlock.</param>
        /// <param name="timeLock">The time lock.</param>
        public Quest(int id, string category, int number, QuestType goalType, uint goalData, string name, int reward, string dataBit, int rewardType, int timeUnlock, int timeLock)
        {
            Id = id;
            Category = category;
            Number = number;
            GoalType = goalType;
            GoalData = goalData;
            Name = name;
            Reward = reward;
            DataBit = dataBit;
            RewardType = rewardType;
            TimeUnlock = timeUnlock;
            HasEnded = timeLock >= Yupi.GetUnixTimeStamp() && timeLock > 0;
        }

        /// <summary>
        ///     Gets the name of the action.
        /// </summary>
        /// <value>The name of the action.</value>
        public string ActionName => QuestTypeUtillity.GetString(GoalType);

        /// <summary>
        ///     Determines whether the specified user progress is completed.
        /// </summary>
        /// <param name="userProgress">The user progress.</param>
        /// <returns><c>true</c> if the specified user progress is completed; otherwise, <c>false</c>.</returns>
        public bool IsCompleted(int userProgress)
        {
            var goalType = GoalType;

            if (goalType != QuestType.ExploreFindItem)
                return userProgress >= GoalData;

            return userProgress >= 1;
        }
    }
}