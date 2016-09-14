using Yupi.Model.Domain;

namespace Yupi.Model
{
    public class GroupMember
    {
        public GroupMember(UserInfo user) : this()
        {
            User = user;
        }

        protected GroupMember()
        {
            // NHibernate
        }

        public virtual int Id { get; protected set; }
        public virtual UserInfo User { get; protected set; }
    }
}