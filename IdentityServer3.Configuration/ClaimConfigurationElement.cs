using System.Configuration;

namespace IdentityServer3.Configuration
{
    internal class ClaimConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true, IsKey = true)]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }
}