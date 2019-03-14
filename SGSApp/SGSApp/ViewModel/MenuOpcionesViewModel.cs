using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SGSApp.Helper;
using SGSApp.Views.Acumen;
using SGSApp.Views.Home;
using SGSApp.Views.Logout;
using SGSApp.Views.Master;
using SGSApp.Views.Sharepoint;
using Xamarin.Forms;
using MainPage = SGSApp.Views.Home.MainPage;

namespace SGSApp.ViewModel
{
    public class MenuOpcionesViewModel : INotifyPropertyChanged
    {
        #region Atributos

        public ObservableCollection<MainPageMenuItem> Menu { get; set;  }
        public ImageSource ImageURL { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Commands

        public ICommand GoToCommand => new RelayCommand<string>(GoTo);

        #endregion

        #region Metodos

        public MenuOpcionesViewModel()
        {
            cargarMenu();
            ImageURL = ImageSource.FromStream(() => { return new MemoryStream(GlobalVariables.imagenUsuario); });
        }

        private void GoTo(string PageName)
        {
            App.Master.IsPresented = false;
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
                    break;
                case "EMail":
                    App.Navigator.PushAsync(new Email());
                    break;
            }
        }

        private void cargarMenu()
        {
            Menu = new ObservableCollection<MainPageMenuItem>(new[]
            {
                new MainPageMenuItem
                {Id = 0, Icon = "ic_news.png", Title = "Noticias",
                    TargetType = typeof(ListaNoticias)},

                new MainPageMenuItem
                {Id = 1, Icon = "ic_calendario.png", Title = "Calendario",
                    TargetType = typeof(Calendario)},

                new MainPageMenuItem
                {
                    Id = 2, Icon = "ic_transporte.png", Title = "Transporte",
                    TargetType = typeof(DashboardConsultaTransporte)
                },
                new MainPageMenuItem
                {
                    Id = 3, Icon = "ic_phonebook.png", Title = "Listado Teléfonico",
                    TargetType = typeof(ListaExtensiones)
                },
                new MainPageMenuItem
                {
                    Id = 4, Icon = "ic_carnet_virtual.png", Title = "Carnet DragonMarket",
                    TargetType = typeof(CarnetVirtual)
                },
                new MainPageMenuItem
                {Id = 5, Icon = "ic_intranet.png", Title = "SGS Members",
                    TargetType = typeof(Intranet)},

                new MainPageMenuItem
                {Id = 6, Icon = "ic_vpn_key.png", Title = "Cerrar Sesión",
                    TargetType = typeof(logout)}
            });
        }      

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}