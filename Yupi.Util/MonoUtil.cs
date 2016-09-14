using System;

namespace Yupi.Util
{
    public class MonoUtil
    {
        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }
    }
}