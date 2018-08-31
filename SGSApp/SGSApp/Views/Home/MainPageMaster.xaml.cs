using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using SGSApp.Helper;
using SGSApp.Views.Acumen;
using SGSApp.Views.Logout;
using SGSApp.Views.Master;
using SGSApp.Views.Sharepoint;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGSApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView;

        public MainPageMaster()
        {
            InitializeComponent();
            LblUsername.Text = GlobalVariables.Usuario;
            BindingContext = new MainPageMasterViewModel();
            if (Device.RuntimePlatform == Device.iOS) Icon = "hamburger-icon-menu.png";
            ListView = MenuItemsListView;

        }

        public ImageSource ImageURL { get; set; }

        private class MainPageMasterViewModel : INotifyPropertyChanged
        {
            public MainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
                {
                    new MainPageMenuItem
                        {Id = 0, Icon = "ic_news.png", Title = "Noticias", TargetType = typeof(ListaNoticias)},
                    new MainPageMenuItem
                        {Id = 2, Icon = "ic_calendario.png", Title = "Calendario", TargetType = typeof(Calendario)},
                    new MainPageMenuItem
                    {
                        Id = 3, Icon = "ic_transporte.png", Title = "Transporte",
                        TargetType = typeof(DashboardConsultaTransporte)
                    },
                    new MainPageMenuItem
                    {
                        Id = 4, Icon = "ic_phonebook.png", Title = "Listado Teléfonico",
                        TargetType = typeof(ListaExtensiones)
                    },
                    new MainPageMenuItem
                    {
                        Id = 5, Icon = "ic_phonebook.png", Title = "Carnet Virtual",
                        TargetType = typeof(CarnetVirtual)
                    },
                    new MainPageMenuItem
                        {Id = 5, Icon = "ic_intranet.png", Title = "SGS Members", TargetType = typeof(Intranet)},
                    new MainPageMenuItem
                        {Id = 6, Icon = "ic_intranet.png", Title = "Notificaciones", TargetType = typeof(Prueba)},
                    //new MainPageMenuItem { Id = 7, Icon = "ic_intranet.png", Title = "Notificaciones-bd" , TargetType =  typeof(ListaNotificacionesView) },
                    new MainPageMenuItem
                        {Id = 8, Icon = "ic_vpn_key.png", Title = "Cerrar Sesión", TargetType = typeof(logout)}
                });

                ImageURL = ImageSource.FromStream(() => { return new MemoryStream(GlobalVariables.imagenUsuario); });
            }

            public ObservableCollection<MainPageMenuItem> MenuItems { get; }
            public ImageSource ImageURL { get; }

            #region INotifyPropertyChanged Implementation

            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion
        }
    }
}