using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;

namespace SGSApp
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void Login_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var data = await DependencyService.Get<IAuthenticator>()
                    .Authenticate(App.tenanturl, App.GraphResourceUri, App.ApplicationID, App.AcumenURL, "click");
                App.AuthenticationResult = data;
                NavigateTopage(data);
            }
            catch (Exception)
            {
            }
        }

        public async void NavigateTopage(AuthenticationResult data)
        {
            var userName = data.UserInfo.GivenName + " " + data.UserInfo.FamilyName;
            await Navigation.PushModalAsync(new HomePage(userName));
        }
    }
}