using System;
using System.Threading.Tasks;
using SGSApp.Helper;
using SGSApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGSApp.Views.Sharepoint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendario : ContentPage
    {
        public Calendario()
        {
            InitializeComponent();


            BindingContext = new CalendarioVM();

            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;

                listView.SelectedItem = null;
            };
        }

        private CalendarioVM ViewModel => BindingContext as CalendarioVM;

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
            try
            {
                if (ViewModel == null || ViewModel.IsBusy || ViewModel.CalendarioItems.Count > 0)
                    return;
            }
            catch (Exception)
            {
                ShowMessage(MessageSource.messageNoticias, MessageSource.titleGeneral, MessageSource.buttonTextOk,
                    async () => { });
                //throw;
            }


            ViewModel.LoadItemsCommand.Execute(null);
            if (ViewModel.CalendarioItems.Count == 0)
            {
                LblSinFechas.IsVisible = true;
            }
        }
    }
}