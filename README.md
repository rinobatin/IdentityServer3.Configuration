# IdentityServer3.Configuration

This library lets you configure IdentityServer3 clients in the web.config file.

Example
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="identityServer3" 
             type="IdentityServer3.Configuration.IdentityServer3Section,IdentityServer3.Configuration"/>
  </configSections>
  <identityServer3>
    <clients>
      <client id="abc123"
          name="test app"
          enabled="true"
          flow="ResourceOwner"
          authorizationCodeLifetime="1600"
          identityTokenLifetime="1800"
          accessTokenLifetime="2400"
          clientUri="http://localhost:8080"
          logoutUri="http://localhost:8080"
          logoutSessionRequired="false"
          enableLocalLogin="false"
          allowAccessToAllScopes="true"
          allowAccessToAllCustomGrantTypes="true"
          alwaysSendClientClaims="true"
          prefixClientClaims="false"
          requireConsent="false"
          requireSignOutPrompt="true"
          includeJwtId="true"
          updateAccessTokenClaimsOnRefresh="true"
          allowRememberConsent="false"
          absoluteRefreshTokenLifetime="1000"
          slidingRefreshTokenLifetime="2000"
          refreshTokenUsage="ReUse"
          refreshTokenExpiration="Sliding"
          accessTokenType="Reference"
          allowClientCredentialsOnly="false"
          allowAccessTokensViaBrowser="true"
          logoUri="https://test.xyz/logo.png">
        <allowedScopes>
          <add  value="openid" />
          <add value="profile" />
          <add value="email" />
          <add value="abc" />
        </allowedScopes>
        <secrets>
          <add value="secret1" 
               description="this is the first secret"  
               type="AnotherType" 
               expiration="2017-01-01 12:03 PM"/>
          <add value="secret2" />
          <add value="secret256" hashtype="sha256" />
          <add value="secret512" hashtype="sha512" />
       </secrets>
        <redirectUris>
          <add name="website1" value="http://localhost:59328/" />
          <add name="website2" value="http://localhost:7777/webui/" />
        </redirectUris>
        <postLogoutRedirectUris>
          <add name="website1" value="http://localhost:59328/test" />
          <add name="website2" value="http://localhost:7777/webui/test" />
        </postLogoutRedirectUris>
        <identityProviderRestrictions>
          <add value="restriction1" />
          <add value="restriction2" />
          <add value="restriction3" />
        </identityProviderRestrictions>
        <allowedCustomGrantTypes>
          <add value="customgrant1" />
          <add value="customgrant2" />
          <add value="customgrant3" />
        </allowedCustomGrantTypes>
        <allowedCorsOrigins>
          <add value="corsOrigin1" />
          <add value="corsOrigin2" />
          <add value="corsOrigin3" />
        </allowedCorsOrigins>

        <claims>
          <add type="sub" value="123" />
          <add type="name" value="Bob" />
          <add type="email" value="test@test.com" />
        </claims>
      </client>

      <client id="def456" name="name2" clientUri="http://localhost:9090">
        <allowedScopes>
          <add value="openid" />
          <add value="123" />
        </allowedScopes>
        <secrets>
          <add value="secret2" />
        </secrets>
      </client>

      <!--minimum configuration-->
      <client id="ghi789">
      </client>

    </clients>
  </identityServer3>

</configuration>
```

To get the clients in your code, 
```c#
IdentityServer3.Configuration.Configuration.GetClients()
```

MIT License
