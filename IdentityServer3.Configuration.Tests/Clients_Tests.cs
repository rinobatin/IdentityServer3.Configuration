using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
namespace IdentityServer3.Configuration.Tests
{
    [TestClass]
    public class Clients_Tests
    {
        [TestMethod]
        public void Section_Tests()
        {
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.Sections.Get("identityServer3").ShouldNotBeNull();
        }

        [TestMethod]
        public void Client_Tests()
        {
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.Sections.Get("identityServer3") as IdentityServer3Section;

            section.Clients.Count.ShouldBe(3);

            var client1 = section.Clients.First();

            client1.Id.ShouldBe("123");
            client1.Name.ShouldBe("test app");
            client1.Enabled.ShouldBe(true);
            client1.Flow.ShouldBe(Flows.ResourceOwner);
            client1.AuthorizationCodeLifetime.ShouldBe(1600);
            client1.IdentityTokenLifetime.ShouldBe(1800);
            client1.AccessTokenLifetime.ShouldBe(2400);
            client1.ClientUri.ShouldBe("http://localhost:8080");
            client1.LogoutUri.ShouldBe("http://localhost:8080");
            client1.LogoutSessionRequired.ShouldBe(false);
            client1.EnableLocalLogin.ShouldBe(false);
            client1.AllowAccessToAllScopes.ShouldBe(true);
            client1.AllowAccessToAllCustomGrantTypes.ShouldBe(true);
            client1.AlwaysSendClientClaims.ShouldBe(true);
            client1.PrefixClientClaims.ShouldBe(false);
            client1.RequireConsent.ShouldBe(false);
            client1.IncludeJwtId.ShouldBe(true);
            client1.UpdateAccessTokenClaimsOnRefresh.ShouldBe(true);
            client1.RequireSignOutPrompt.ShouldBe(true);
            client1.AllowRememberConsent.ShouldBe(false);
            client1.AllowClientCredentialsOnly.ShouldBe(false);
            client1.AllowAccessTokenViaBrowser.ShouldBe(true);
            client1.AbsoluteRefreshTokenLifetime.ShouldBe(1000);
            client1.SlidingRefreshTokenLifetime.ShouldBe(2000);
            client1.RefreshTokenUsage.ShouldBe(TokenUsage.ReUse);
            client1.RefreshTokenExpiration.ShouldBe(TokenExpiration.Sliding);
            client1.AccessTokenType.ShouldBe(AccessTokenType.Reference);
            client1.LogoUri.ShouldBe("https://test.xyz/logo.png");
            

            var client2 = section.Clients.Skip(1).Take(1).First();

            client2.Id.ShouldBe("456");
            client2.Name.ShouldBe("name2");
            client2.Enabled.ShouldBe(true);
            client2.Flow.ShouldBe(Flows.Implicit);
            client2.AuthorizationCodeLifetime.ShouldBe(300);
            client2.IdentityTokenLifetime.ShouldBe(300);
            client2.AccessTokenLifetime.ShouldBe(3600);
            client2.ClientUri.ShouldBe("http://localhost:9090");
            client2.LogoutUri.ShouldBe("http://localhost:9090");
            client2.LogoutSessionRequired.ShouldBe(true);
            client2.EnableLocalLogin.ShouldBe(true);
            client2.AllowAccessToAllScopes.ShouldBe(false);
            client2.AllowAccessToAllCustomGrantTypes.ShouldBe(false);
            client2.AlwaysSendClientClaims.ShouldBe(false);
            client2.PrefixClientClaims.ShouldBe(true);
            client2.RequireConsent.ShouldBe(true);
            client2.AllowRememberConsent.ShouldBe(true);
            client1.AllowClientCredentialsOnly.ShouldBe(false);
            client1.AllowAccessTokenViaBrowser.ShouldBe(true);
            client2.AbsoluteRefreshTokenLifetime.ShouldBe(2592000);
            client2.SlidingRefreshTokenLifetime.ShouldBe(1296000);
            client2.RefreshTokenUsage.ShouldBe(TokenUsage.OneTimeOnly);
            client2.RefreshTokenExpiration.ShouldBe(TokenExpiration.Absolute);
            client2.AccessTokenType.ShouldBe(AccessTokenType.Jwt);
        }

        [TestMethod]
        public void AllowedScopes_Tests()
        {
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.Sections.Get("identityServer3") as IdentityServer3Section;

            section.Clients.ShouldNotBeEmpty();

            var client = section.Clients.First();
            var allowedScopes = client.AllowedScopes.ToList();
            allowedScopes.Count.ShouldBe(4);

            allowedScopes.Any(a => a.Value == Constants.StandardScopes.OpenId).ShouldBeTrue();
            allowedScopes.Any(a => a.Value == Constants.StandardScopes.Profile).ShouldBeTrue();
            allowedScopes.Any(a => a.Value == Constants.StandardScopes.Email).ShouldBeTrue();
            allowedScopes.Any(a => a.Value == "abc").ShouldBeTrue();

        }

        [TestMethod]
        public void Secrets_Tests()
        {
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.Sections.Get("identityServer3") as IdentityServer3Section;

            section.Clients.ShouldNotBeEmpty();

            var client = section.Clients.First();
            var secrets = client.Secrets.ToList();
            secrets.Count.ShouldBe(4);

            var firstSecret = secrets.First();
            firstSecret.Value.ShouldBe("secret1");
            firstSecret.Description.ShouldBe("this is the first secret");
            firstSecret.Type.ShouldBe("AnotherType");
            firstSecret.Expiration.ShouldBe(new DateTimeOffset(new DateTime(2017, 1, 1, 12, 03, 0)));


            var secondSecret = secrets.Skip(1).Take(1).First();
            secondSecret.Value.ShouldBe("secret2");

            //About default empty string 
            //http://stackoverflow.com/questions/29799341/configuration-string-with-null-defaultvalue
            secondSecret.Description.ShouldBe("");
            secondSecret.Type.ShouldBe("SharedSecret");
            secondSecret.Expiration.ShouldBeNull();

            var secret256 = secrets.Skip(2).Take(1).First();
            secret256.HashType.ShouldBe("sha256");
            secret256.Value.ShouldBe("secret256".Sha256());

            var secret512 = secrets.Skip(3).Take(1).First();
            secret512.HashType.ShouldBe("sha512");
            secret512.Value.ShouldBe("secret512".Sha512());
        }

        [TestMethod]
        public void RedirectUris_Tests()
        {
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.Sections.Get("identityServer3") as IdentityServer3Section;

            var client = section.Clients.First();
            var redirectUris = client.RedirectUris.ToList();
            redirectUris.Count.ShouldBe(2);

            redirectUris[0].Value.ShouldBe("http://localhost:59328/");
            redirectUris[1].Value.ShouldBe("http://localhost:7777/webui/");
        }

        [TestMethod]
        public void PostLogoutRedirectUris_Tests()
        {
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.Sections.Get("identityServer3") as IdentityServer3Section;

            var client = section.Clients.First();
            var postLogoutRedirectUris = client.PostLogoutRedirectUris.ToList();
            postLogoutRedirectUris.Count.ShouldBe(2);

            postLogoutRedirectUris[0].Value.ShouldBe("http://localhost:59328/test");
            postLogoutRedirectUris[1].Value.ShouldBe("http://localhost:7777/webui/test");
        }

        [TestMethod]
        public void IdentityProviderRestrictions_Tests()
        {
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.Sections.Get("identityServer3") as IdentityServer3Section;

            var client = section.Clients.First();
            var idProviderRestrictions = client.IdentityProviderRestrictions.ToList();
            idProviderRestrictions.Count.ShouldBe(3);

            idProviderRestrictions[0].Value.ShouldBe("restriction1");
            idProviderRestrictions[1].Value.ShouldBe("restriction2");
            idProviderRestrictions[2].Value.ShouldBe("restriction3");
        }

        [TestMethod]
        public void AllowedCustomGrantTypes_Tests()
        {
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.Sections.Get("identityServer3") as IdentityServer3Section;

            var client = section.Clients.First();
            var allowCustomGrants = client.AllowedCustomGrantTypes.ToList();
            allowCustomGrants.Count.ShouldBe(3);

            allowCustomGrants[0].Value.ShouldBe("customgrant1");
            allowCustomGrants[1].Value.ShouldBe("customgrant2");
            allowCustomGrants[2].Value.ShouldBe("customgrant3");
        }

        [TestMethod]
        public void AllowedCorsOrigins_Tests()
        {
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.Sections.Get("identityServer3") as IdentityServer3Section;

            var client = section.Clients.First();
            var allowCorsOrigin = client.AllowedCorsOrigins.ToList();
            allowCorsOrigin.Count.ShouldBe(3);

            allowCorsOrigin[0].Value.ShouldBe("corsOrigin1");
            allowCorsOrigin[1].Value.ShouldBe("corsOrigin2");
            allowCorsOrigin[2].Value.ShouldBe("corsOrigin3");
        }

        [TestMethod]
        public void Claims_Tests()
        {
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.Sections.Get("identityServer3") as IdentityServer3Section;

            var client = section.Clients.First();
            var claims = client.Claims.ToList();
            claims.Count.ShouldBe(3);

            claims[0].Type.ShouldBe("sub");
            claims[0].Value.ShouldBe("123");

            claims[1].Type.ShouldBe("name");
            claims[1].Value.ShouldBe("Bob");

            claims[2].Type.ShouldBe("email");
            claims[2].Value.ShouldBe("test@test.com");

        }

        [TestMethod]
        public void GetClients_Tests()
        {
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = config.Sections.Get("identityServer3") as IdentityServer3Section;

            List<Client> clients = section.Clients.Select(a => a.GetClient()).ToList();

            clients.Count().ShouldBe(3);

        }
    }
}
