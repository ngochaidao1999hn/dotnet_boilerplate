// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                  new List<IdentityResource>
                    {
                        new IdentityResource(
                            name: "custom",
                            userClaims: new[] { "sub","name","role","email" },
                            displayName: "Your user identifier")
                    };

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("custom","Custom API"){ApiSecrets = new List<Secret>(){new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) } }
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                //Block 2:  MVC client using hybrid flow
                new Client
                {
                    ClientId = "webclient",
                    ClientName = "Web Client",
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                    RedirectUris = { "https://localhost:5002/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "identity.api","test.api" }
                },
                //Block 3: SPA client using Code flow
                new Client
                {
                    ClientId = "spaclient",
                    ClientName = "SPA Client",
                    ClientUri = "https://localhost:5003",
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 86400,//3600 = 1 hour
                    RedirectUris =
                    {
                        "https://localhost:5003/index.html",
                        "https://localhost:5003/callback.html"
                    },
                    PostLogoutRedirectUris = { "https://localhost:5003/index.html" },
                    AllowedCorsOrigins = { "https://localhost:5003" },
                    AllowedScopes = { "openid", "profile", "identity.api" , "custom" }
                }
            };
        }
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>()
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "demo",
            Password = "demo".Sha256()
        }
    };
        }
    }
}