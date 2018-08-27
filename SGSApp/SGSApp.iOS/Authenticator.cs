using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using SGSApp.Helper;
using SGSApp.iOS;
using SGSApp.iOS.Helper;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Authenticator))]

namespace SGSApp.iOS
{
    internal class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string tenantUrl, string graphResourceUri,
            string applicationId, string returnUri, string evento)
        {
            var authContext = new AuthenticationContext(tenantUrl);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters(controller);
            var authResult = await authContext.AcquireTokenAsync(graphResourceUri, applicationId, uri, platformParams);
            return authResult;
        }
    }
}