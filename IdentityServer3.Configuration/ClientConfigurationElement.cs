using System;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using IdentityServer3.Core.Models;

namespace IdentityServer3.Configuration
{
    internal class ClientConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("id", IsRequired = true, IsKey = true)]
        public string Id => base["id"] as string;

        [ConfigurationProperty("name")]
        public string Name => base["name"] as string;

        [ConfigurationProperty("enabled", DefaultValue = true)]
        public bool Enabled => Convert.ToBoolean(base["enabled"]);

        [ConfigurationProperty("flow", DefaultValue = Flows.Implicit)]
        public Flows Flow => (Flows)Enum.Parse(typeof(Flows), base["flow"].ToString());

        [ConfigurationProperty("authorizationCodeLifetime", DefaultValue = 300)]
        public int AuthorizationCodeLifetime => Convert.ToInt32(base["authorizationCodeLifetime"]);

        [ConfigurationProperty("identityTokenLifetime", DefaultValue = 300)]
        public int IdentityTokenLifetime => Convert.ToInt32(base["identityTokenLifetime"]);

        [ConfigurationProperty("accessTokenLifetime", DefaultValue = 3600)]
        public int AccessTokenLifetime => Convert.ToInt32(base["accessTokenLifetime"]);

        [ConfigurationProperty("absoluteRefreshTokenLifetime", DefaultValue = 2592000)]
        public int AbsoluteRefreshTokenLifetime => Convert.ToInt32(base["absoluteRefreshTokenLifetime"]);

        [ConfigurationProperty("slidingRefreshTokenLifetime", DefaultValue = 1296000)]
        public int SlidingRefreshTokenLifetime => Convert.ToInt32(base["slidingRefreshTokenLifetime"]);


        [ConfigurationProperty("refreshTokenUsage", DefaultValue = TokenUsage.OneTimeOnly)]
        public TokenUsage RefreshTokenUsage => (TokenUsage)Enum.Parse(typeof(TokenUsage), base["refreshTokenUsage"].ToString());

        [ConfigurationProperty("refreshTokenExpiration", DefaultValue = TokenExpiration.Absolute)]
        public TokenExpiration RefreshTokenExpiration => (TokenExpiration)Enum.Parse(typeof(TokenExpiration), base["refreshTokenExpiration"].ToString());

        [ConfigurationProperty("accessTokenType", DefaultValue = AccessTokenType.Jwt)]
        public AccessTokenType AccessTokenType => (AccessTokenType)Enum.Parse(typeof(AccessTokenType), base["accessTokenType"].ToString());

        [ConfigurationProperty("clientUri")]
        public string ClientUri => base["clientUri"] as string;

        [ConfigurationProperty("logoUri")]
        public string LogoUri => base["logoUri"] as string;

        [ConfigurationProperty("logoutSessionRequired", DefaultValue = true)]
        public bool LogoutSessionRequired => Convert.ToBoolean(base["logoutSessionRequired"]);

        [ConfigurationProperty("enableLocalLogin", DefaultValue = true)]
        public bool EnableLocalLogin => Convert.ToBoolean(base["enableLocalLogin"]);
        
        [ConfigurationProperty("allowAccessToAllScopes", DefaultValue = false)]
        public bool AllowAccessToAllScopes => Convert.ToBoolean(base["allowAccessToAllScopes"]);

        [ConfigurationProperty("allowAccessToAllCustomGrantTypes", DefaultValue = false)]
        public bool AllowAccessToAllCustomGrantTypes => Convert.ToBoolean(base["allowAccessToAllCustomGrantTypes"]);

        [ConfigurationProperty("alwaysSendClientClaims", DefaultValue = false)]
        public bool AlwaysSendClientClaims => Convert.ToBoolean(base["alwaysSendClientClaims"]);

        [ConfigurationProperty("prefixClientClaims", DefaultValue = true)]
        public bool PrefixClientClaims => Convert.ToBoolean(base["prefixClientClaims"]);

        [ConfigurationProperty("requireConsent", DefaultValue = true)]
        public bool RequireConsent => Convert.ToBoolean(base["requireConsent"]);

        [ConfigurationProperty("requireSignOutPrompt", DefaultValue = false)]
        public bool RequireSignOutPrompt => Convert.ToBoolean(base["requireSignOutPrompt"]);

        [ConfigurationProperty("updateAccessTokenClaimsOnRefresh", DefaultValue = false)]
        public bool UpdateAccessTokenClaimsOnRefresh => Convert.ToBoolean(base["updateAccessTokenClaimsOnRefresh"]);

        [ConfigurationProperty("includeJwtId", DefaultValue = false)]
        public bool IncludeJwtId => Convert.ToBoolean(base["includeJwtId"]);

        [ConfigurationProperty("allowRememberConsent", DefaultValue = true)]
        public bool AllowRememberConsent => Convert.ToBoolean(base["allowRememberConsent"]);


        [ConfigurationProperty("allowedScopes")]
        [ConfigurationCollection(typeof(ClientConfigurationElement), AddItemName = "add")]
        public ValueConfigurationElementCollection<AllowedScopeConfigurationElement> AllowedScopes => 
            (ValueConfigurationElementCollection<AllowedScopeConfigurationElement>)this["allowedScopes"];

        [ConfigurationProperty("secrets")]
        [ConfigurationCollection(typeof(ClientConfigurationElement), AddItemName = "add")]
        public ValueConfigurationElementCollection<SecretConfigurationElement> Secrets => 
            (ValueConfigurationElementCollection<SecretConfigurationElement>)this["secrets"];

        [ConfigurationProperty("redirectUris")]
        [ConfigurationCollection(typeof(ClientConfigurationElement), AddItemName = "add")]
        public NameValueConfigurationElementCollection<NameValueConfigurationElement> RedirectUris => 
            (NameValueConfigurationElementCollection<NameValueConfigurationElement>)this["redirectUris"];

        [ConfigurationProperty("postLogoutRedirectUris")]
        [ConfigurationCollection(typeof(ClientConfigurationElement), AddItemName = "add")]
        public NameValueConfigurationElementCollection<NameValueConfigurationElement> PostLogoutRedirectUris => 
            (NameValueConfigurationElementCollection<NameValueConfigurationElement>)this["postLogoutRedirectUris"];


        [ConfigurationProperty("identityProviderRestrictions")]
        [ConfigurationCollection(typeof(ClientConfigurationElement), AddItemName = "add")]
        public ValueConfigurationElementCollection<ValueConfigurationElement> IdentityProviderRestrictions =>
            (ValueConfigurationElementCollection<ValueConfigurationElement>)this["identityProviderRestrictions"];


        [ConfigurationProperty("allowedCustomGrantTypes")]
        [ConfigurationCollection(typeof(ClientConfigurationElement), AddItemName = "add")]
        public ValueConfigurationElementCollection<ValueConfigurationElement> AllowedCustomGrantTypes =>
            (ValueConfigurationElementCollection<ValueConfigurationElement>)this["allowedCustomGrantTypes"];

        [ConfigurationProperty("allowedCorsOrigins")]
        [ConfigurationCollection(typeof(ClientConfigurationElement), AddItemName = "add")]
        public ValueConfigurationElementCollection<ValueConfigurationElement> AllowedCorsOrigins =>
            (ValueConfigurationElementCollection<ValueConfigurationElement>)this["allowedCorsOrigins"];

        [ConfigurationProperty("claims")]
        [ConfigurationCollection(typeof(ClientConfigurationElement), AddItemName = "add")]
        public ClaimConfigurationElementCollection Claims =>
            (ClaimConfigurationElementCollection)this["claims"];


        public Client GetClient()
        {
            var client = new Client();
            client.ClientId = Id;
            client.ClientName = Name;

            client.Flow = Flow;
            client.ClientSecrets = Secrets.Select(a => new Secret()
            {
                Value = a.Value,
                Description = a.Description,
                Expiration = a.Expiration,
                Type = a.Type
            }).ToList();

            client.AllowedScopes = AllowedScopes.Select(a => a.Value).ToList();
            client.RedirectUris = RedirectUris.Select(a => a.Value).ToList();
            client.PostLogoutRedirectUris = PostLogoutRedirectUris.Select(a => a.Value).ToList();
            client.IdentityProviderRestrictions = IdentityProviderRestrictions.Select(a => a.Value).ToList();
            client.AllowedCustomGrantTypes = AllowedCustomGrantTypes.Select(a => a.Value).ToList();
            client.AllowedCorsOrigins = AllowedCorsOrigins.Select(a => a.Value).ToList();
            client.LogoutSessionRequired = LogoutSessionRequired;
            client.Enabled = Enabled;
            client.EnableLocalLogin = EnableLocalLogin;
            client.AllowAccessToAllScopes = AllowAccessToAllScopes;
            client.AllowAccessToAllCustomGrantTypes = AllowAccessToAllCustomGrantTypes;
            client.Claims = Claims.Select(a => new Claim(a.Type, a.Value)).ToList();
            client.AlwaysSendClientClaims = AlwaysSendClientClaims;
            client.PrefixClientClaims = PrefixClientClaims;
            client.AuthorizationCodeLifetime = AuthorizationCodeLifetime;
            client.IdentityTokenLifetime = IdentityTokenLifetime;
            client.AccessTokenLifetime = AccessTokenLifetime;
            client.AbsoluteRefreshTokenLifetime = AbsoluteRefreshTokenLifetime;
            client.SlidingRefreshTokenLifetime = SlidingRefreshTokenLifetime;
            client.RefreshTokenUsage = RefreshTokenUsage;
            client.RefreshTokenExpiration = RefreshTokenExpiration;
            client.AccessTokenType = AccessTokenType;
            client.RequireConsent = RequireConsent;
            client.AllowRememberConsent = AllowRememberConsent;
            client.LogoUri = LogoUri;

            return client;
        }

    }
}