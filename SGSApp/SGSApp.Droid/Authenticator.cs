using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using SGSApp.Droid;
using SGSApp.Droid.Helper;
using SGSApp.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(Authenticator))]

namespace SGSApp.Droid
{
    internal class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string tenantUrl, string graphResourceUri,
            string ApplicationID, string returnUri, string evento)
        {
            try
            {
                var pp = "";
                AuthenticationResult authResult2;
                var authContext = new AuthenticationContext(tenantUrl);
                if (authContext.TokenCache.ReadItems().Any())
                {
                    authContext =
                        new AuthenticationContext(authContext.TokenCache.ReadItems().FirstOrDefault().Authority);

                    var authResult = await authContext.AcquireTokenAsync(graphResourceUri, ApplicationID,
                        new Uri(returnUri), new PlatformParameters((Activity) Forms.Context));

                    GlobalVariables.grabarTokenOffice365(authResult.AccessToken);
                    var authResult1 = await AuthenticationHelper.GetAccessToken(AuthenticationHelper.SharePointURL,
                        new PlatformParameters((Activity) Forms.Context));
                    GlobalVariables.grabarTokenSharepoint(authResult1.AccessToken);

                    var authResult3 = await AuthenticationHelper.GetAccessToken(AuthenticationHelper.AcumenURL,
                        new PlatformParameters((Activity) Forms.Context));
                    GlobalVariables.grabarTokenAcumen(authResult3.AccessToken);
                    authResult2 = authResult;
                }
                else if (evento == "click")
                {
                    if (authContext.TokenCache.ReadItems().Any())
                        authContext =
                            new AuthenticationContext(authContext.TokenCache.ReadItems().FirstOrDefault().Authority);
                    var authResult = await authContext.AcquireTokenAsync(graphResourceUri, ApplicationID,
                        new Uri(returnUri), new PlatformParameters((Activity) Forms.Context));

                    GlobalVariables.grabarTokenOffice365(authResult.AccessToken);
                    var authResult1 = await AuthenticationHelper.GetAccessToken(AuthenticationHelper.SharePointURL,
                        new PlatformParameters((Activity) Forms.Context));
                    var authResult3 = await AuthenticationHelper.GetAccessToken(AuthenticationHelper.AcumenURL,
                        new PlatformParameters((Activity) Forms.Context));
                    GlobalVariables.grabarTokenSharepoint(authResult1.AccessToken);
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