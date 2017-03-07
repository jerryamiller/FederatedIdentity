using IdentityServer3.Core;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Extensions
{
    public class OidcAuthNotificationHandler
    {
        public static Task Oidc(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> n)
        {
            var id = n.AuthenticationTicket.Identity;

            // we want to keep first name, last name, subject and roles
            var givenName = id.FindFirst(Constants.ClaimTypes.GivenName);
            var familyName = id.FindFirst(Constants.ClaimTypes.FamilyName);
            var sub = id.FindFirst(Constants.ClaimTypes.Subject);
            var upn = id.FindFirst("upn");

            // create new identity and set name and role claim type
            var nid = new ClaimsIdentity(
                id.AuthenticationType,
                Constants.ClaimTypes.GivenName,
                Constants.ClaimTypes.Role);

            nid.AddClaim(givenName);
            nid.AddClaim(familyName);
            nid.AddClaim(sub);
            nid.AddClaim(new Claim("email", upn.Value));

            // add some other app specific claim
            nid.AddClaim(new Claim("app_specific", "some data"));

            n.AuthenticationTicket = new AuthenticationTicket(
                nid,
                n.AuthenticationTicket.Properties);

            return Task.FromResult(0);
        }
    }
}