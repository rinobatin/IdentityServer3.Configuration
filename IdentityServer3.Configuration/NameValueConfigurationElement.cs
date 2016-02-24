using System.Configuration;

namespace IdentityServer3.Configuration
{
    internal class NameValueConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        } 

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get{ return (string)this["value"]; }
            set{ this["value"] = value; }
        }
    }
}