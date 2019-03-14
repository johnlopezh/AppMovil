using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using SGSApp.Helper;
using SGSApp.ViewModel;
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
        // Se instancia ListView del menu para después añadir el compartimiento del botón
        public ListView ListView;

        public MainPageMaster()
        {
            InitializeComponent();
            LblUsername.Text = GlobalVariables.Usuario;
            BindingContext = new MenuOpcionesViewModel();
            if (Device.RuntimePlatform == Device.iOS) Icon = "hamburger-icon-menu.png";
            ListView = MenuItemsListView;
        }

    }
}