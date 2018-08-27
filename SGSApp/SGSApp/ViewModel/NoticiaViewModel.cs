using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SGSApp.Helper;
using SGSApp.Models;
using SGSApp.Views.Sharepoint;
using Xamarin.Forms;

namespace SGSApp.ViewModel
{
    public class NoticiasViewModel : BaseViewModel
    {
        private ObservableCollection<Noticias> feedItems = new ObservableCollection<Noticias>();

        private Command loadItemsCommand;

        private Noticias selectedFeedItem;

        /// <summary>
        ///     gets or sets the feed items
        /// </summary>
        public ObservableCollection<Noticias> FeedItems
        {
            get => feedItems;
            set
            {
                feedItems = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the selected feed item
        /// </summary>
        public Noticias SelectedFeedItem
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

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var error = false;
            FeedItems.Clear();
            try
            {
                //List<Noticias> ListaNoticias = new List<Noticias>();
                //int cantidad = 5;

                string img;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", GlobalVariables.TokenSha);
                var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
                mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));
                client.DefaultRequestHeaders.Accept.Add(mediaType);
                var result =
                    await client.GetAsync(
                        "https://sgsedu.sharepoint.com/sites/intranet/_api/web/lists/GetByTitle('Noticias%20Principales%20SGS')/items?$orderby=Created%20desc&$top=15");
                var data = JsonConvert.DeserializeObject<ListItemModels>(await result.Content.ReadAsStringAsync());

                if (result.StatusCode == HttpStatusCode.OK)
                    for (var i = 0; i < data.D.Results.Length; i++)
                    {
                        var resultNot =
                            await client.GetAsync(data.D.Results[i].FieldValuesAsHtml.Deferred.Uri +
                                                  "?$select=PublishingPageImage");
                        var not = JsonConvert.DeserializeObject<ListItemModelsImage>(
                            await resultNot.Content.ReadAsStringAsync());
                        var let = not.D.PublishingPageImage;
                        char[] separadores = {' '};
                        char[] separadoresq = {'/'};
                        var palabras = let.Split(separadores);
                        var er = palabras[2].Replace("src=", " ");
                        var pp = er.Substring(0, er.Length - 1);
                        var er2 = pp.Split(separadoresq);

                        var resultNot1 = await client.GetAsync(
                            "https://sgsedu.sharepoint.com/sites/intranet/_api/web/GetFolderByServerRelativeUrl('PublishingImages')/Files('" +
                            er2[4] + "')/$value");

                        var ent = new Noticias();
                        var image = new Image();

                        var webImage = new Image {Aspect = Aspect.AspectFit};


                        using (var sourceStream = await resultNot1.Content.ReadAsStreamAsync())
                        {
                            using (var newStream = new MemoryStream())
                            {
                                sourceStream.CopyTo(newStream);
                                var bytes = newStream.ToArray();
                                img = "data:image/png;base64, " + Convert.ToBase64String(bytes);
                                //webImage.Source = UriImageSource.FromUri(new Uri(img));
                                ent.ImageURL = ImageSource.FromStream(() => { return new MemoryStream(bytes); });
                            }
                        }


                        var s = data.D.Results[i].PublishingPageContent;
                        s = s.Replace("/sites/intranet/", "https://sgsedu.sharepoint.com/sites/intranet/");
                        ent.TituloNoticia = data.D.Results[i].Title;
                        ent.Resumen = data.D.Results[i].OData__Comments;
                        ent.Descripcion = s;
                        ent.TargetType = typeof(ListaNoticias);
                        //ListaNoticias.Add(ent);
                        FeedItems.Add(ent);
                    }
            }
            catch
            {
                var page = new ContentPage();
                await page.DisplayAlert(MessageSource.titleGeneral, MessageSource.messageNoticias,
                    MessageSource.buttonTextOk);
            }


            IsBusy = false;
        }
    }
}