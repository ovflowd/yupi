namespace Yupi.Model
{
    using System;

    [System.AttributeUsage(System.AttributeTargets.Property)]
    public sealed class OneToManyAttribute : Attribute
    {
        #region Constructors

        public OneToManyAttribute()
        {
        }

        #endregion Constructors
    }
}