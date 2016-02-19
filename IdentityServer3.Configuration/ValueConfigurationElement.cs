using System.Configuration;

namespace IdentityServer3.Configuration
{
    internal class ValueConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("value", IsRequired = true, IsKey = true)]
        public string Value
        {
            get{ return (string)this["value"]; }
            set{ this["value"] = value; }
        }
    }
}