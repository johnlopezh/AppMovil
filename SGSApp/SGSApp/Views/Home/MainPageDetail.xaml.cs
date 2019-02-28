using System;
using SGSApp.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGSApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        public MainPageDetail()
        {
            InitializeComponent();
            LblUsername.Text = GlobalVariables.Usuario;
        }

    }
}