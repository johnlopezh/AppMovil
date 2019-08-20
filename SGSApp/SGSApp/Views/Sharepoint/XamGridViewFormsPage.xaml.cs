using Rg.Plugins.Popup.Services;
using SGSApp.Models;
using SGSApp.ViewModel;
using Xamarin.Forms;


namespace SGSApp.Views.Sharepoint
{
    public partial class XamGridViewFormsPage : ContentPage
    {
        public XamGridViewFormsPage()
        {
            InitializeComponent();
            BindingContext = new XamGridViewViewModel();

            GrdView.ItemSelected += async (object sender, object e) =>
            {
                var currentModel = e as XamGridModel;
                //await App.Current.MainPage.DisplayAlert("Clicked", "Current image position is " + currentModel.Position, "OK");
                var page = new XFSlideShowPage(currentModel.Position);
                //PopupNavigation.pop(); 
                await PopupNavigation.PushAsync(page);
            };
        }
    }
}