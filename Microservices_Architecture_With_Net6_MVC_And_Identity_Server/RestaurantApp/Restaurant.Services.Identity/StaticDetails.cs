using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Restaurant.Services.Identity
{
    public static class StaticDetails
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> 
            {
                new ApiScope("restaurant", "Restaurant Server"),
                new ApiScope(name:"read", displayName:"Read data"),
                new ApiScope("write", "Write data"),
                new ApiScope("delete", "Delete data")
            };

        // "secret". is interchangable with a more secure id
        // RedirectUris is the client URL with SSL Port
        // "restaurant" inside the AllowedScopes refers to restaurant ApiScope as defined above
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
               new Client
               {
                   ClientId ="client",
                   ClientSecrets = {new Secret("secret".Sha256())},
                   AllowedGrantTypes = GrantTypes.ClientCredentials,
                   AllowedScopes = {"read", "write", "profile"}
               },
               new Client
               {
                   ClientId = "restaurant",
                   ClientSecrets= {new Secret("restaurant".Sha256()) },
                   AllowedGrantTypes = GrantTypes.Code,
                   RedirectUris = { "https://localhost:7121/signin-oidc" },
                   PostLogoutRedirectUris = { "https://localhost:7121/signout-callback-oidc" },
                   AllowedScopes = new List<string>
                   {
                       IdentityServerConstants.StandardScopes.OpenId,
                       IdentityServerConstants.StandardScopes.Profile,
                       IdentityServerConstants.StandardScopes.Email,
                       "restaurant"
                   }
               },
            };
    }
}
