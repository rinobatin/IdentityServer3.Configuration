using System;
using System.Configuration;

namespace IdentityServer3.Configuration
{
    internal class SecretConfigurationElement : ValueConfigurationElement
    {
        [ConfigurationProperty("description")]
        public string Description
        {
            get
            {
                return (string)this["description"];
            }
            set
            {
                this["description"] = value;
            }
        }

        [ConfigurationProperty("type", DefaultValue = "SharedSecret")]
        public string Type
        {
            get
            {
                return (string)this["type"] ;
            }
            set
            {
                this["type"] = value;
            }
        }

        [ConfigurationProperty("expiration")]
        public DateTimeOffset? Expiration
        {
            get
            {
                return (DateTimeOffset?) this["expiration"];
            }

            set
            {
                this["expiration"] = value;
            }
        }
    }
}