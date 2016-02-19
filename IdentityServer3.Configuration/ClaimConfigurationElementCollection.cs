using System.Configuration;

namespace IdentityServer3.Configuration
{
    internal class ClaimConfigurationElementCollection 
        : GenericConfigurationElementCollection<ClaimConfigurationElement> 
    {
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ClaimConfigurationElement)element).Type;
        }

    }
}