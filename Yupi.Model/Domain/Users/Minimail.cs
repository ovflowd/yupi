using System;

namespace Yupi.Model.Domain
{
    public class Minimail
    {
        public virtual int Id { get; protected set; }
        public virtual bool Seen { get; protected set; }
    }
}