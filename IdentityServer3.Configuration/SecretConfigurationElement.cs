using System;
using System.Configuration;
using IdentityServer3.Core.Models;

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

        [ConfigurationProperty("value")]
        new public string Value
        {
            get
            {
                if (this["value"] != null)
                {
                    if (HashType.Equals("sha256", StringComparison.CurrentCultureIgnoreCase))
                        return ((string)(this["value"])).Sha256();
                    else if (HashType.Equals("sha512", StringComparison.CurrentCultureIgnoreCase))
                        return ((string)(this["value"])).Sha512();
                }

                return (string)this["value"];
            }
            set
            {
                this["value"] = value;
            }
        }

        [ConfigurationProperty("hashtype")]
        public string HashType
        {
            get
            {
                return (string)this["hashtype"] ?? string.Empty;
            }
            set
            {
                this["hashtype"] = value;
            }
        }
    }
}