using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using IdentityServer3.Core.Models;

namespace IdentityServer3.Configuration
{
    public class Configuration
    {
        private readonly static string _sectionName = "identityServer3";
        public static IList<Client> GetClients()
        {
            var section = ConfigurationManager.GetSection(_sectionName) as IdentityServer3Section;

            if(section == null)
                throw new ConfigurationErrorsException($"Configuration section '{_sectionName}' does not exists");

            List<Client> clients = section.Clients.Select(a => a.GetClient()).ToList();

            return clients;
        } 
    }
}