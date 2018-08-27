using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace SGSApp.iOS.Helper
{
    public class AuthenticationHelper
    {
        public const string Authority = "https://login.windows.net/common";
        public static Uri returnUri = new Uri("https://sgsapp.droid/");
        public static string clientId = "b66320d2-777b-4f6f-b5a9-6976ead7217d";
        public static AuthenticationContext authContext;
        public static string SharePointURL = "https://sgsedu.sharepoint.com/";
        public static string AcumenURL = "http://naranjaweb.cloudapp.net/";

        public static async Task<AuthenticationResult> GetAccessToken(string serviceResourceId,
            PlatformParameters param)
        {
            authContext = new AuthenticationContext(Authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            var authResult = await authContext.AcquireTokenAsync(serviceResourceId, clientId, returnUri, param);
            return authResult;
        }
    }
}