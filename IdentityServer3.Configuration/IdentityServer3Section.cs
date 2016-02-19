using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer3.Configuration
{
    internal class IdentityServer3Section : ConfigurationSection
    {
        [ConfigurationProperty("clients", IsRequired = true)]
        [ConfigurationCollection(typeof(ClientConfigurationElement), 
            AddItemName = "client")]
        public ClientConfigurationElementCollection Clients => (ClientConfigurationElementCollection)this["clients"];
    }
}
