using System.Configuration;

namespace IdentityServer3.Configuration
{
    internal class ValueConfigurationElementCollection<T> : GenericConfigurationElementCollection<T>  where T: ValueConfigurationElement, new()
    {

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ValueConfigurationElement)element).Value;
        }
        
    }
}