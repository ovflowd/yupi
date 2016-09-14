using System.Collections.Generic;

namespace Yupi.Model.Domain
{
    public class EcotronLevel
    {
        public EcotronLevel()
        {
            Rewards = new List<EcotronReward>();
        }

        public virtual int Id { get; protected set; }
        public virtual IList<EcotronReward> Rewards { get; protected set; }
    }
}