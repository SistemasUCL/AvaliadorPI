using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AvaliadorPI.Identity
{
    public class Config
    {
        public static IEnumerable<Client> Clients = new List<Client>
        {
            new Client
            {
                ClientId = "app",
                ClientName = "Avaliador PI App",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,

                RequireConsent = false,
                AlwaysSendClientClaims = true,
                AlwaysIncludeUserClaimsInIdToken = true,

                // AccessTokenLifetime = 20,

                RedirectUris = {
                    "http://localhost:4200/callback",
                    "https://localhost:4200/callback",

                    "http://localhost:4200/silent",
                    "https://localhost:4200/silent",
                },
                PostLogoutRedirectUris = {
                    "http://localhost:4200/home",
                    "https://localhost:4200/home",
                },
                AllowedCorsOrigins = {
                    "http://localhost:4200",
                    "https://localhost:4200",
                },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "role",
                    "avaliadorpi"
                },
            },

            new Client
            {
                ClientId = "pwa",
                ClientName = "Avaliador PI PWA",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,

                RequireConsent = false,
                AlwaysSendClientClaims = true,
                AlwaysIncludeUserClaimsInIdToken = true,

                RedirectUris =
                {
                    "http://localhost:4200/callback",
                    "https://localhost:4200/callback",

                    "http://localhost:4200/silent",
                    "https://localhost:4200/silent",
                },
                PostLogoutRedirectUris =
                {
                    "http://localhost:4200/home"
                },
                AllowedCorsOrigins =
                {
                    "http://localhost:4200"
                },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "role",
                    "avaliadorpi"
                },
            },

            new Client
            {
                ClientId = "api",
                ClientSecrets = { new Secret("avaliadorpiucl".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "role", "avaliadorpi" }
            },

            new Client
            {
                ClientId = "postman",
                ClientSecrets = { new Secret("avaliadorpiucl".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = { "role", "avaliadorpi" }
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource { Name = "role", UserClaims = new List<string> {"role"} }
        };

        public static IEnumerable<ApiResource> Apis = new List<ApiResource>
        {
            new ApiResource("avaliadorpi", "Avaliador PI")
            {
                UserClaims = { "role" }
            }
        };
    }
}
