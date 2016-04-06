namespace Yupi.Emulator.Game.Users.Subscriptions
{
    /// <summary>
    ///     Class Subscription.
    /// </summary>
     public class Subscription
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Subscription" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="activated">The activated.</param>
        /// <param name="timeExpire">The time expire.</param>
        /// <param name="timeLastGift">The time last gift.</param>
     public Subscription(int id, int activated, int timeExpire, int timeLastGift)
        {
            SubscriptionId = id;
            ActivateTime = activated;
            ExpireTime = timeExpire;
            LastGiftTime = timeLastGift;
        }

        /// <summary>
        ///     Gets the subscription identifier.
        /// </summary>
        /// <value>The subscription identifier.</value>
     public int SubscriptionId { get; private set; }

        /// <summary>
        ///     Gets the expire time.
        /// </summary>
        /// <value>The expire time.</value>
     public int ExpireTime { get; }

        /// <summary>
        ///     Gets the activate time.
        /// </summary>
        /// <value>The activate time.</value>
     public int ActivateTime { get; private set; }

        /// <summary>
        ///     Gets the last gift time.
        /// </summary>
        /// <value>The last gift time.</value>
     public int LastGiftTime { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
     public bool IsValid => ExpireTime > Yupi.GetUnixTimeStamp();
    }
}