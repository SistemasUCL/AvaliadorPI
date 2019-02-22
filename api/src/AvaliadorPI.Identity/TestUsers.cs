using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace AvaliadorPI.Identity
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser{SubjectId = "818727", Username = "alielson", Password = "123",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Alielson"),
                    new Claim(JwtClaimTypes.Role, "Professor"),
                    new Claim(JwtClaimTypes.GivenName, "Alielson"),
                    new Claim(JwtClaimTypes.FamilyName, "Piffer"),
                    new Claim(JwtClaimTypes.Email, "alielson@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alielson.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                }
            },
            new TestUser{SubjectId = "88421113", Username = "tariton", Password = "123",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Táriton"),
                    new Claim(JwtClaimTypes.Role, "Avaliador"),
                    new Claim(JwtClaimTypes.Role, "Administrador"),
                    new Claim(JwtClaimTypes.GivenName, "Táriton"),
                    new Claim(JwtClaimTypes.FamilyName, "Avelar"),
                    new Claim(JwtClaimTypes.Email, "tariton@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://tariton.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim("location", "somewhere")
                }
            }
        };
    }
}
