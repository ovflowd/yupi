using System;

namespace Yupi.Model
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public sealed class OneToManyAttribute : Attribute
    {
        public OneToManyAttribute()
        {
        }
    }
}