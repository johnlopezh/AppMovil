using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SGSApp.Helper;
using SGSApp.Models;
using Xamarin.Forms;

namespace SGSApp.ViewModel
{
    public class TransporteVM : BaseViewModel
    {
        private readonly Estudiante[] est = new Estudiante[1500];

        private string[] arr4 = new string[8];

        private ObservableCollection<Estudiante> estudianteItems = new ObservableCollection<Estudiante>();

        private Command loadItemsCommand;
        private Dictionary<int, string> myList;
        private int resultado;

        private Estudiante selectedFeedItem;

        private int selectedIndex;
        private Array tipoSolicitudTransporteItems;
        private TipoSolicitudTransporte[] st;

        public TransporteVM()
        {
            myList = new Dictionary<int, string>();
            selectedIndex = -1;
            LoadData();
        }

        //bool error = false;
        /// <summary>
        ///     gets or sets the feed items
        /// </summary>
        public ObservableCollection<Estudiante> EstudianteItems
        {
            get => estudianteItems;
            set
            {
                estudianteItems = value;
                OnPropertyChanged();
            }
        }

        public Array TipoSolicitudTransporteItems
        {
            get => tipoSolicitudTransporteItems;
            set
            {
                tipoSolicitudTransporteItems = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the selected feed item
        /// </summary>
        public Estudiante SelectedFeedItem
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

        public Dictionary<int, string> MyList
        {
            get => myList;
            set
            {
                myList = value;
                NotifyPropertyChanged("MyList");
            }
        }

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (value == selectedIndex) return;
                selectedIndex = value;
                NotifyPropertyChanged("SelectedIndex");
            }
        }

        private void LoadData()
        {
            for (var i = 0; i < 3; i++) MyList.Add(i, "Item " + i);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (null != handler) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;


            EstudianteItems.Clear();
            try
            {
                var rt = string.Empty;
                var client = new HttpClient();

                client.BaseAddress = new Uri(GlobalVariables.AcumenUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("FedAuth", GlobalVariables.TokenAcumen);
                var response = client.GetAsync("/api/dashboard?rol=6&user=" + GlobalVariables.Email).Result;
                var stringData = response.Content.ReadAsStringAsync().Result;

                var data = JsonConvert.DeserializeObject<List<Estudiante>>(await response.Content.ReadAsStringAsync());

                if (response.StatusCode == HttpStatusCode.OK)

                    for (var i = 0; i < data.Count; i++)
                    {
                        var ent = new Estudiante();
                        ent.UrlFoto = data[i].UrlFoto;
                        ent.NombreCompleto = data[i].NombreCompleto;
                        ent.NombreCurso = data[i].NombreCurso;
                        ent.CodigoEstudiante = data[i].CodigoEstudiante;
                        ent.TipoIdentificacion = data[i].TipoIdentificacion;
                        ent.NumeroIdentificacion = data[i].NumeroIdentificacion;
                        ent.TipoPaciente = data[i].TipoPaciente;
                        ent.IdFamilia = data[i].IdFamilia;
                        ent.AddImage = ImageSource.FromFile("signo-mas.jpg");
                        EstudianteItems.Add(ent);
                    }
            }
            catch (Exception ex)
            {
                // Error = true;
                var page = new ContentPage();
                await page.DisplayAlert(MessageSource.titleGeneral, MessageSource.messageDashboardConsulta,
                    MessageSource.buttonTextOk);
            }

            IsBusy = false;
        }

        private void volver()
        {
        }


        public async Task<TipoSolicitudTransporte[]> ConsultarTiposSolicitud(string tipoPaciente)
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
                var response = client.GetAsync("/api/transporte?tipoPersona=" + tipoPaciente).Result;
                var stringData = response.Content.ReadAsStringAsync().Result;


                var data = JsonConvert.DeserializeObject<List<TipoSolicitudTransporte>>(
                            await response.Content.ReadAsStringAsync());

                st = new TipoSolicitudTransporte[data.Count];

                if (response.StatusCode == HttpStatusCode.OK)

                    for (var i = 0; i < st.Length; i++)
                    {
                        var ent = new TipoSolicitudTransporte();
                        ent.NombreTipo = data[i].NombreTipo;
                        st[i] = data[i];
                    }
            }
            catch (Exception ex)
            {
                Error = true;
            }
            return st;
        }

        public async Task<Estudiante[]> ConsultarEstudiantesCompañeroTransporte()
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
                var response = client.GetAsync("/api/persona").Result;
                var stringData = response.Content.ReadAsStringAsync().Result;

                var data = JsonConvert.DeserializeObject<List<Estudiante>>(await response.Content.ReadAsStringAsync());

                if (response.StatusCode == HttpStatusCode.OK)

                    for (var i = 0; i < data.Count; i++)
                    {
                        //-arr4[i] = data[i].NombreTipo + "sd";
                        var ent = new Estudiante();
                        ent.TipoIdentificacion = data[i].TipoIdentificacion;
                        ent.NumeroIdentificacion = data[i].NumeroIdentificacion;
                        ent.NombreCompleto = data[i].NombreCompleto;

                        est[i] = data[i];
                        //TipoSolicitudTransporteItems[i] = ent.NombreTipo;
                    }
            }
            catch (Exception ex)
            {
                Error = true;
            }

            return est;
        }

        public async Task<int> GuardarSolicitudTransporte(SolicitudTransporteApp solicitudTranspote)
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

                var p = new person { name = "Sourav", surname = "Kayal" };
                var sc = new StringContent(JsonConvert.SerializeObject(solicitudTranspote), Encoding.UTF8,
                    "application/json");
                var json = JsonConvert.SerializeObject(solicitudTranspote);
                var response = client.PostAsync("/api/transporte",
                    new StringContent(JsonConvert.SerializeObject(solicitudTranspote), Encoding.UTF8,
                        "application/json")).Result;
                var stringData = response.Content.ReadAsStringAsync().Result;

                //var response = client.PostAsync("api/transporte", jso).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());
                    resultado = 1;
                }
                else
                {
                    resultado = 0;
                }


                //using (var formData = new MultipartFormDataContent())
                //{
                //    formData.Add(new StringContent(JsonConvert.SerializeObject(solicitudTranspote), UnicodeEncoding.UTF8, "application/json"), "solicitudTranspote", "solicitudTranspote");
                //    formData.Add(new StringContent(JsonConvert.SerializeObject(fechas), UnicodeEncoding.UTF8, "application/json"), "fechas", "fechas");
                //    formData.Add(new StringContent(JsonConvert.SerializeObject(rol), UnicodeEncoding.UTF8, "application/json"), "rol", "rol");
                //    //var response = await client.PostAsync("api/transporte", solicitudTranspote);
                //    var response = client.PostAsync("api/transporte", formData).Result;
                //    resultado = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
                //}
            }
            catch (Exception ex)
            {
                Error = true;
            }

            return resultado;
        }
    }

    public class person
    {
        public string name { get; set; }
        public string surname { get; set; }
    }
}