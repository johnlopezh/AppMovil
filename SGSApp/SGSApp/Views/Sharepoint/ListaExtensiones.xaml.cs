using System;
using System.Threading.Tasks;
using SGSApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGSApp.Views.Sharepoint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaExtensiones : ContentPage
    {
        public ListaExtensiones()
        {
            InitializeComponent();
            BindingContext = new ExtensionesVM(Navigation);
            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;

                listView.SelectedItem = null;
            };
        }

        private ExtensionesVM ViewModel => BindingContext as ExtensionesVM;

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

            await Navigation.PushModalAsync(new MainPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel == null || ViewModel.IsBusy || ViewModel.ExtensionesItems.Count > 0)
                return;

            ViewModel.LoadItemsCommand.Execute(null);
        }

        private void LlamarExtension(Object sender, EventArgs e)
        {
            var extension = (Button) sender;     
        }
    }
}