using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SGSApp.Helper;

namespace SGSApp.Models
{
    internal class ExtensionesSh
    {
        public string nombreExtension { get; set; }
        public string extension { get; set; }
        public string responsable { get; set; }

        public static async Task GetExtensionesAsync(Action<List<ExtensionesSh>> action)
        {
            var ListaEventos = new List<ExtensionesSh>();
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
                        "https://sgsedu.sharepoint.com/sites/Intranetsgs/_api/web/lists/GetByTitle('Extensiones%20Telefonicas')/items?");
                var data = JsonConvert.DeserializeObject<ListExtensiones>(await result.Content.ReadAsStringAsync());

                if (result.StatusCode == HttpStatusCode.OK)

                    for (var i = 0; i < data.E.Results.Length; i++)
                    {
                        var ent = new ExtensionesSh();
                        ent.nombreExtension = data.E.Results[i].Title;
                        ent.extension = data.E.Results[i].Extension;
                        ent.responsable = data.E.Results[i].ResponsableId;

                        ListaEventos.Add(ent);
                    }
            }

            catch (Exception ex)
            {
                var msg = "Error al cargar la lista del calendario. " + ex.Message;
            }

            action(ListaEventos);
        }
    }
}