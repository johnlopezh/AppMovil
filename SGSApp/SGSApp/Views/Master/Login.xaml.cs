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
    public partial class Login : ContentPage
    {
        private UserVM NOvm;

        public Login()
        {
            InitializeComponent();
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
                //await NavigateTopage(data);

                if (data != null)
                    await Task.Factory.StartNew(() =>
                    {
                        NOvm = new UserVM();
                        Task.Delay(5000).Wait();
                        Device.BeginInvokeOnMainThread(() => { BindingContext = NOvm; });
                    });
                await NavigateTopage(data);
            }
            catch (Exception ex)
            {
                await ShowMessage(MessageSource.messageIniciarSesion, MessageSource.titleInicarSesion,
                    MessageSource.buttonTextOk, async () => { });
            }

            //this.Ind.IsRunning = false;
            //this.Stack.IsVisible = false;
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