// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("Api"),
                // new ApiScope("ScopeConsoleApp"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
      new ApiResource[]
      {
                new ApiResource("Api", "Web Api") { Scopes = { "Api"} }
      };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Console Client",
                    ClientId = "ClientApp",
                    ClientSecrets = { new Secret("ClientSecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "Api" },
                },
                new Client
                {
                    ClientName = "Web Client",
                    ClientId = "WebApi",
                    ClientSecrets = { new Secret("ClientSecret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "https://localhost:5001/swagger/oauth2-redirect.html" },
                    AllowedCorsOrigins = { "https://localhost:5001" },
                    AllowedScopes = { "Api" }
                }
            };
    }
}