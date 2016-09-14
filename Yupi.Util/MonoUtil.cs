namespace Yupi.Util
{
    using System;

    public class MonoUtil
    {
        #region Methods

        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        #endregion Methods
    }
}