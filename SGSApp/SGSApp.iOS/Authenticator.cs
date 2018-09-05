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
            AuthenticationResult authResult2;

            var authContext = new AuthenticationContext(tenantUrl);
            if(authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters(controller);
            //var authResult = await authContext.AcquireTokenAsync(graphResourceUri, applicationId, uri, platformParams);
            //return authResult;

        
                 
                    var authResult = await authContext.AcquireTokenAsync(graphResourceUri, applicationId, uri, platformParams);

                    GlobalVariables.grabarTokenOffice365(authResult.AccessToken);

                    var authResult1 = await authContext.AcquireTokenAsync(AuthenticationHelper.SharePointURL, applicationId, uri, platformParams);

                    GlobalVariables.grabarTokenSharepoint(authResult1.AccessToken);


                    var authResult3 = await authContext.AcquireTokenAsync(AuthenticationHelper.AcumenURL, applicationId, uri, platformParams);
                    GlobalVariables.grabarTokenSharepoint(authResult1.AccessToken);

                    GlobalVariables.grabarTokenAcumen(authResult3.AccessToken);

                    authResult2 = authResult;

            return authResult;

                /* 
                 * 

                 else if (evento == "click")
                 {
                     if (authContext.TokenCache.ReadItems().Any())
                         authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

                         var authResult = await authContext.AcquireTokenAsync(graphResourceUri, applicationId, uri, platformParams);
                         GlobalVariables.grabarTokenOffice365(authResult.AccessToken);

                         var authResult1 = await authContext.AcquireTokenAsync(AuthenticationHelper.SharePointURL, applicationId, uri, platformParams);

                         var authResult3 = await authContext.AcquireTokenAsync(AuthenticationHelper.AcumenURL, applicationId, uri, platformParams);
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

   */



}
    }
}
