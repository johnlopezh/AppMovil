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
            try
            {
                AuthenticationResult authResult2;
                var authContext = new AuthenticationContext(tenantUrl);
                if (authContext.TokenCache.ReadItems().Any())
                {
                    authContext =
                        new AuthenticationContext(authContext.TokenCache.ReadItems().FirstOrDefault()?.Authority);
                    var authResult = await authContext.AcquireTokenAsync(graphResourceUri, applicationId,
                        new Uri(returnUri),
                        new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController));

                    GlobalVariables.grabarTokenOffice365(authResult.AccessToken);

                    var authResult1 = await AuthenticationHelper.GetAccessToken(AuthenticationHelper.SharePointURL,
                        new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController));
                    GlobalVariables.grabarTokenSharepoint(authResult1.AccessToken);


                    var authResult3 = await AuthenticationHelper.GetAccessToken(AuthenticationHelper.AcumenURL,
                        new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController));
                    GlobalVariables.grabarTokenSharepoint(authResult1.AccessToken);

                    GlobalVariables.grabarTokenAcumen(authResult3.AccessToken);

                    authResult2 = authResult;
                }
                else if (evento == "click")
                {
                    if (authContext.TokenCache.ReadItems().Any())
                        authContext =
                             new AuthenticationContext(authContext.TokenCache.ReadItems().FirstOrDefault()?.Authority);

                    var authResult = await authContext.AcquireTokenAsync(graphResourceUri, applicationId,
                        new Uri(returnUri),
                        new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController));

                    GlobalVariables.grabarTokenOffice365(authResult.AccessToken);

                    authResult2 = authResult;
                }
                else
                {
                    authResult2 = null;
                }

                return authResult2;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}