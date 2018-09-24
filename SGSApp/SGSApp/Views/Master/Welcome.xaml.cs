using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using SGSApp.Helper;
using SGSApp.ViewModel;
using SGSApp.Views.Home;
using SGSApp.Views.Textos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGSApp.Views.Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Welcome : ContentPage
    {
        private UserVM NOvm;

        public Welcome()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(2000);
            await Task.WhenAll(
                // EscudoSGS.ScaleTo(10,2000)
            );

            Ind.IsRunning = true;
            Stack.IsVisible = true;

            //await Navigation.PushModalAsync(new Loading());
            //var data = await DependencyService.Get<IAuthenticator>()
            //.Authenticate(App.tenanturl, App.GraphResourceUri, App.ApplicationID, App.ReturnUri);
            var data = App.AuthenticationResult = await DependencyService.Get<IAuthenticator>()
                .Authenticate(App.tenanturl, App.GraphResourceUri, App.ApplicationID, App.ReturnUri, "");
            App.AuthenticationResult = data;

            if (data != null)
            {
                await Task.Factory.StartNew(() =>
                {
                    NOvm = new UserVM();
                    Task.Delay(5000).Wait();
                    Device.BeginInvokeOnMainThread(() => { BindingContext = NOvm; });
                });

                await NavigateTopage(data);
            }
            else
            {
                await Navigation.PushModalAsync(new Login());
            }

            Ind.IsRunning = false;
            Stack.IsVisible = false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                // var data = await DependencyService.Get<IAuthenticator>()
                //   .Authenticate(App.tenanturl, App.GraphResourceUri, App.ApplicationID, App.ReturnUri);
                var data = App.AuthenticationResult = await DependencyService.Get<IAuthenticator>()
                    .Authenticate(App.tenanturl, App.GraphResourceUri, App.ApplicationID, App.ReturnUri, "click");
                App.AuthenticationResult = data;

                if (data != null)
                {
                    await Task.Factory.StartNew(() =>
                    {
                        NOvm = new UserVM();
                        Task.Delay(3000).Wait();
                        Device.BeginInvokeOnMainThread(() => { BindingContext = NOvm; });
                    });

                    await NavigateTopage(data);
                }
            }
            catch (Exception ex)
            {
                await ShowMessage(MessageSource.messageIniciarSesion, MessageSource.titleInicarSesion,
                    MessageSource.buttonTextOk, async () => { });
            }

            Ind.IsRunning = false;
            Stack.IsVisible = false;
        }

        public async Task NavigateTopage(AuthenticationResult data)
        {
            string nombreUsuario;
            var userName = data.UserInfo.GivenName;
            if (userName.Contains(" "))
            {
                var partes = userName.Split(' ');
                nombreUsuario = data.UserInfo.GivenName;
                //partes[0];
            }
            else
            {
                nombreUsuario = data.UserInfo.GivenName;
            }

            GlobalVariables.grabarUsuario(nombreUsuario, data.UserInfo.DisplayableId);
            await Navigation.PushModalAsync(new MainPage(userName));
        }


        private async void TerminosConidicones_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TerminosCondiciones());
        }


        /// <summary>
        ///     Metodo que renderiza los mensajes de alerta de la aplicación.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttonText"></param>
        /// <param name="afterHideCallback"></param>
        /// <returns></returns>
        public async Task ShowMessage(string message,
            string title,
            string buttonText,
            Action afterHideCallback)
        {
            await DisplayAlert(
                title,
                message,
                buttonText);

            afterHideCallback?.Invoke();

            await Navigation.PushModalAsync(new MainPage(GlobalVariables.Usuario));
        }
    }
}