using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using SGSApp.Views.Master;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGSApp.Views.Logout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class logout : ContentPage
    {
        public logout()
        {
            LogoutAsync();
            //InitializeComponent();
        }

        public async Task LogoutAsync()
        {
            //string requestUrl = "https://login.microsoftonline.com/f75993d5-7568-4d7d-9418-2dd2ebe111cc/";
            var authority = "https://login.microsoftonline.com/common";
            //AuthenticationContext _authenticationContext;
            //_authenticationContext = new AuthenticationContext(requestUrl, false);
            //_authenticationContext.TokenCache.Clear();

            AuthenticationContext _authenticationContext1;
            _authenticationContext1 = new AuthenticationContext(authority, false);
            _authenticationContext1.TokenCache.Clear();
            //AuthenticationContext authContext =   new AuthenticationContext("Authority", false, null);
            //authContext.TokenCache.Clear();
            //var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var ac = new AuthenticationContext(authority);
            ac.TokenCache.Clear();

            var requestUrl =
                "https://login.microsoftonline.com/b66320d2-777b-4f6f-b5a9-6976ead7217d/oauth2/logout?post_logout_redirect_uri=https://www.sgsapp.com";

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            var response = await client.SendAsync(request);
            Task.Delay(5000).Wait();
            await Navigation.PushModalAsync(new Welcome());
        }
    }
}