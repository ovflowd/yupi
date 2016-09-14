namespace Yupi.Model
{
    using System;

    [System.AttributeUsage(System.AttributeTargets.Property)]
    public sealed class ManyToManyAttribute : Attribute
    {
        #region Constructors

        public ManyToManyAttribute()
        {
        }

        #endregion Constructors
    }
}