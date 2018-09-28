using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SGSApp.Helper;
using SGSApp.Models;
using Xamarin.Forms;

namespace SGSApp.ViewModel
{
    public class ExtensionesVM : BaseViewModel
    {
        private readonly INavigation Navigation;
        private ObservableCollection<Extensiones> extensionesItems = new ObservableCollection<Extensiones>();

        private Command loadItemsCommand;

        private Extensiones selectedFeedItem;

        public ExtensionesVM(INavigation navigation)
        {
            Navigation = navigation;
            HomeCommand = new Command(async () => await NavigateToNotificacionView());

        }

        public Command HomeCommand { get; set; }

        /// <summary>
        ///     gets or sets the feed items
        /// </summary>
        public ObservableCollection<Extensiones> ExtensionesItems
        {
            get => extensionesItems;
            set
            {
                extensionesItems = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the selected feed item
        /// </summary>
        public Extensiones SelectedFeedItem
        {
            get => selectedFeedItem;
            set
            {
                selectedFeedItem = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Command to load/refresh items
        /// </summary>
        public Command LoadItemsCommand
        {
            get
            {
                return loadItemsCommand ??
                       (loadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand()));
            }
        }

        public async Task NavigateToNotificacionView()
        {
            await Navigation.PopToRootAsync();
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var error = false;

            ExtensionesItems.Clear();
            try
            {
                string img;

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", GlobalVariables.TokenSha);
                var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
                mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));
                client.DefaultRequestHeaders.Accept.Add(mediaType);

                var result =
                    await client.GetAsync(
                        "https://sgsedu.sharepoint.com/sites/intranet/_api/web/lists/GetByTitle('Extensiones')/items?");
                var data = JsonConvert.DeserializeObject<ListExtensiones>(await result.Content.ReadAsStringAsync());

                if (result.StatusCode == HttpStatusCode.OK)

                    for (var i = 0; i < data.E.Results.Length; i++)
                    {
                        var resultExt = await client.GetAsync(
                            "https://sgsedu.sharepoint.com/sites/intranet/_api/web/SiteUsers/GetById('" +
                            data.E.Results[i].ResponsableId + "')/Title");
                        var dataExt =
                            JsonConvert.DeserializeObject<ListResponsableExtensiones>(
                                await resultExt.Content.ReadAsStringAsync());

                        var ent = new Extensiones();
                        ent.nombreExtension = data.E.Results[i].Title;
                        ent.extension = data.E.Results[i].Extensi_x00f3_n;
                        ent.responsable = dataExt.E.Title;
                        ExtensionesItems.Add(ent);
                    }

                ExtensionesItems =
                    new ObservableCollection<Extensiones>(ExtensionesItems.OrderBy(i => i.nombreExtension));
            }
            catch
            {
                error = true;
            }

            IsBusy = false;
        }
    }
}