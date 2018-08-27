using System;
using System.Threading.Tasks;
using SGSApp.Models;
using SGSApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGSApp.Views.Sharepoint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaNoticias : ContentPage
    {
        public ListaNoticias()
        {
            InitializeComponent();
            BindingContext = new NoticiasViewModel();

            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;

                listView.SelectedItem = null;
            };
        }

        private NoticiasViewModel ViewModel => BindingContext as NoticiasViewModel;

        private void listNoticias_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Noticias;
            if (item == null)
                return;

            Navigation.PushAsync(
                new NavigationPage(new DetalleNoticia(item.TituloNoticia, item.Descripcion, item.ImageURL)));
            //var page = (Page)Activator.CreateInstance(item.TargetType);
            //Navigation.PushModalAsync(new SGSApp.Views.Home.MainPageMaster());
            //Navigation.PushAsync(new NavigationPage(page));
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

            await Navigation.PushModalAsync(new MainPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel == null || ViewModel.IsBusy || ViewModel.FeedItems.Count > 0)
                return;

            ViewModel.LoadItemsCommand.Execute(null);
        }
    }
}