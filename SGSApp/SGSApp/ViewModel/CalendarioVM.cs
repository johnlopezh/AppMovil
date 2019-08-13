using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    public class CalendarioVM : BaseViewModel
    {
        private ObservableCollection<CalendarioE> calendarioItems = new ObservableCollection<CalendarioE>();

        private Command loadItemsCommand;
        private int resultado;

        private CalendarioE selectedFeedItem;

        //CalendarioAcademico[] st = new CalendarioAcademico[60];
        private CalendarioAcademico[] st;

        /// <summary>
        ///     gets or sets the feed items
        /// </summary>
        public ObservableCollection<CalendarioE> CalendarioItems
        {
            get => calendarioItems;
            set
            {
                calendarioItems = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the selected feed item
        /// </summary>
        public CalendarioE SelectedFeedItem
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

            CalendarioItems.Clear();
            try
            {
                string img;
                var fechaInicio = DateTime.Now.AddDays(-7);
                var fechaFin = DateTime.Now.AddDays(+7);
                var todayStart = fechaInicio.ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture);
                var todayEnd = fechaFin.ToString("yyyy'-'MM'-'dd", CultureInfo.InvariantCulture);

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", GlobalVariables.TokenSha);
                var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
                mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));
                client.DefaultRequestHeaders.Accept.Add(mediaType);

                var result = await client.GetAsync(
                    "https://sgsedu.sharepoint.com/sites/Intranetsgs/_api/web/lists/GetByTitle('Días%20Academicos')/items?$orderby=EventDate%20desc&$filter=EventDate%20ge%20'" +
                    todayStart + "'and%20EventDate%20le%20'" + todayEnd + "'");

                var data = JsonConvert.DeserializeObject<ListEventsCalendar>(await result.Content.ReadAsStringAsync());

                if (result.StatusCode == HttpStatusCode.OK)

                    for (var i = 0; i < data.C.Results.Length; i++)
                    {
                        var localDate = DateTime.Now;
                        var ent = new CalendarioE();
                        ent.tituloEvento = data.C.Results[i].Title;
                        ent.diaEvento = data.C.Results[i].EventDate;
                        ent.duracion += data.C.Results[i].fAllDayEvent ? "Sí" : "No";
                        ent.descripcion = data.C.Results[i].Description;
                        ent.grupoEvento += ent.diaEvento == localDate ? "Hoy" : "Mañana";
                        CalendarioItems.Add(ent);
                    }


                var groupedData =
                    CalendarioItems.GroupBy(p => p.grupoEvento[0].ToString())
                        .Select(p => new ObservableGroupCollection<string, CalendarioE>(p))
                        .ToList();
                CalendarioItems.Clear();
                foreach (dynamic item in groupedData[0]) CalendarioItems.Add(item);
            }
            catch
            {
                var page = new ContentPage();
                await page.DisplayAlert(MessageSource.titleGeneral, MessageSource.messageCalendario,
                    MessageSource.buttonTextOk);
            }


            IsBusy = false;
        }


        public async Task<CalendarioAcademico[]> ConsultarFechasHabilesCalendario(long IdTipoSolicitudTransporte)
        {
            try
            {
                var rt = string.Empty;
                var client = new HttpClient();

                client.BaseAddress = new Uri(GlobalVariables.AcumenUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("FedAuth", GlobalVariables.TokenAcumen);
                var response = client.GetAsync("/api/calendario?IdTipoSolicitudTransporte="+ IdTipoSolicitudTransporte).Result;
                var stringData = response.Content.ReadAsStringAsync().Result;

                var data = JsonConvert.DeserializeObject<List<CalendarioAcademico>>(
                    await response.Content.ReadAsStringAsync());
                st = new CalendarioAcademico[data.Count];

                if (response.StatusCode == HttpStatusCode.OK)

                    for (var i = 0; i < data.Count; i++)
                    {
                        var ent = new CalendarioAcademico();
                        ent.FechaCalendario = data[i].FechaCalendario;
                        st[i] = data[i];
                    }
            }
            catch (Exception ex)
            {
                Error = true;
            }

            return st;
        }
    }
}