using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Plugin.Messaging;
using SGSApp.Views.Acumen;
using SGSApp.Views.Master;
using SGSApp.Views.Sharepoint;
using MainPage = SGSApp.Views.Home.MainPage;

namespace SGSApp.ViewModel
{
    public class MenuOpcionesViewModel : INotifyPropertyChanged
    {
        #region Commands

        public ICommand GoToCommand => new RelayCommand<string>(GoTo);

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Metodos

        private void GoTo(string PageName)
        {
            string username = null;
            switch (PageName)
            {
                case "Noticias":
                    App.Navigator.PushAsync(new ListaNoticias());
                    break;
                case "Calendario":
                    App.Navigator.PushAsync(new Calendario());
                    break;
                case "Transporte":
                    App.Navigator.PushAsync(new DashboardConsultaTransporte());
                    break;
                case "Extensiones":
                    App.Navigator.PushAsync(new ListaExtensiones());
                    break;
                case "SGSMembers":
                    App.Navigator.PushAsync(new Intranet());
                    break;
                case "Home":
                    App.Navigator.PopToRootAsync();
                    App.Navigator.PushAsync(new MainPage(username));
                    break;
                case "CarnetVirtual":
                    App.Navigator.PushAsync(new CarnetVirtual());
                    break;
                case "Llamar":
                    var phoneCallTask = CrossMessaging.Current.PhoneDialer;
                    if (phoneCallTask.CanMakePhoneCall) phoneCallTask.MakePhoneCall("0314324000", "PBX SGS");
                    break;
                case "EMail":
                    App.Navigator.PushAsync(new Email());
                    break;
            }
        }

        #endregion

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}