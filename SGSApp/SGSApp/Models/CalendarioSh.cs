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
    internal class CalendarioSh
    {
        public static async Task GetCalendarioAsync(Action<List<CalendarioE>> action)
        {
            var ListaEventos = new List<CalendarioE>();

            var fechaInicio = DateTime.Now.AddDays(-7);
            var fechaFin = DateTime.Now.AddDays(+7);
            try
            {
                string img;

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", GlobalVariables.TokenSha);
                var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
                mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));
                client.DefaultRequestHeaders.Accept.Add(mediaType);


                var result = await client.GetAsync(
                    "https://sgsedu.sharepoint.com/sites/Intranetsgs/_api/web/lists/GetByTitle('Días%20Academicos')/items?$orderby=EventDate%20desc&$filter=EventDate%20ge%20'" +
                    fechaInicio + "'and%20EventDate%20le%20'" + fechaFin + "'");
                var data = JsonConvert.DeserializeObject<ListEventsCalendar>(await result.Content.ReadAsStringAsync());

                if (result.StatusCode == HttpStatusCode.OK)

                    for (var i = 0; i < 30; i++)
                    {
                        var ent = new CalendarioE();
                        ent.tituloEvento = data.C.Results[i].Title;
                        ent.diaEvento = data.C.Results[i].EventDate;
                        ent.duracion += data.C.Results[i].fAllDayEvent ? "Sí" : "No";
                        ent.descripcion = data.C.Results[i].Description;
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