using Yupi.Core.Settings;

namespace Yupi.Core.Security
{
    /// <summary>
    ///     Class CrossdomainPolicy.
    /// </summary>
    internal static class CrossDomainSettings
    {
        internal static byte[] XmlPolicyBytes;

        internal static void Set()
        {
            XmlPolicyBytes =
                Yupi.GetDefaultEncoding()
                    .GetBytes(
                        "<?xml version=\"1.0\"?>\r\n<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n<cross-domain-policy>\r\n<allow-access-from domain=\"*\" to-ports=\"" +
                        ServerConfigurationSettings.Data["game.tcp.port"] + "\" />\r\n</cross-domain-policy>\0");
        }
    }
}