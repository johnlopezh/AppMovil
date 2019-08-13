using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SGSApp.Helper;
using SGSApp.Views.Sharepoint;
using Xamarin.Forms;

namespace SGSApp.Models
{
    public class NoticiasSh
    {
        private INavigation _navigation;

        public string ScottHead =>
            "https://sgsedu.sharepoint.com/sites/intranet/PublishingImages/Afiche%20bandas%20nal%202015.jpg";


        public static async Task<List<Noticias>> GetNoticiasAsync()
        {
            var ListaNoticias = new List<Noticias>();
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
                        "https://sgsedu.sharepoint.com/sites/intranet/_api/web/lists/GetByTitle('Noticias%20Principales%20SGS')/items?$orderby=Created%20desc&$top=5");
                var data = JsonConvert.DeserializeObject<ListItemModels>(await result.Content.ReadAsStringAsync());
                //var pp = data.D.Results.Select(p => p.Body);

                if (result.StatusCode == HttpStatusCode.OK)
                    for (var i = 0; i < data.D.Results.Length; i++)
                    {
                        var resultNot =
                            await client.GetAsync(data.D.Results[i].FieldValuesAsHtml.Deferred.Uri +
                                                  "?$select=PublishingPageImage");
                        var not = JsonConvert.DeserializeObject<ListItemModelsImage>(
                            await resultNot.Content.ReadAsStringAsync());
                        //var let = not.D.PublishingPageImage;
                        char[] separadores = {' '};
                        char[] separadoresq = {'/'};
                        //var palabras = let.Split(separadores);
                        //var er = palabras[2].Replace("src=", " ");
                       // var pp = er.Substring(0, er.Length - 1);
                       // var er2 = pp.Split(separadoresq);

                        //var resultNot1 = await client.GetAsync(
                        //    "https://sgsedu.sharepoint.com/sites/intranet/_api/web/GetFolderByServerRelativeUrl('PublishingImages')/Files('" +
                        //    er2[4] + "')/$value");

                        var ent = new Noticias();
                        var image = new Image();

                        var webImage = new Image {Aspect = Aspect.AspectFit};


                        //using (var sourceStream = await resultNot1.Content.ReadAsStreamAsync())
                        //{
                        //    using (var newStream = new MemoryStream())
                        //    {
                        //        sourceStream.CopyTo(newStream);
                        //        var bytes = newStream.ToArray();
                        //        img = "data:image/png;base64, " + Convert.ToBase64String(bytes);
                        //        //webImage.Source = UriImageSource.FromUri(new Uri(img));
                        //        ent.ImageURL = ImageSource.FromStream(() => { return new MemoryStream(bytes); });
                        //    }
                        //}


                        var s = data.D.Results[i].PublishingPageContent;
                        s = s.Replace("/sites/intranet/", "https://sgsedu.sharepoint.com/sites/intranet/");
                        ent.TituloNoticia = data.D.Results[i].Title;
                        ent.Resumen = data.D.Results[i].Description;
                        ent.Descripcion = data.D.Results[i].CanvasContent1;
                        ent.TargetType = typeof(ListaNoticias);
                        ListaNoticias.Add(ent);
                    }
            }

            catch (Exception ex)
            {
                var msg = "Error al cargar la lista de noticias." + ex.Message;
            }

            return ListaNoticias;
        }
    }
}