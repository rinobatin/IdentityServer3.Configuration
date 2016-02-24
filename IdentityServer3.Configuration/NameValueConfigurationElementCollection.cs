using System.Configuration;

namespace IdentityServer3.Configuration
{
    internal class NameValueConfigurationElementCollection<T> : GenericConfigurationElementCollection<T>  where T: NameValueConfigurationElement, new()
    {

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NameValueConfigurationElement)element).Name;
        }
        
    }
}