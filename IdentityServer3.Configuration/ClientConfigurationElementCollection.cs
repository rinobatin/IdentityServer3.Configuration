using System.Configuration;

namespace IdentityServer3.Configuration
{
    internal class ClientConfigurationElementCollection : GenericConfigurationElementCollection<ClientConfigurationElement>
    {
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ClientConfigurationElement) element).Id;
        }
    }
}