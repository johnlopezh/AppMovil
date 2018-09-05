using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SGSApp.Helper;
using SGSApp.Models;
using SGSApp.Services;
using Xamarin.Forms;

namespace SGSApp.ViewModel
{
    public class DireccionVM : BaseViewModel
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Services

        private ApiService apiService;
        private DialogService dialogService;

        #endregion

        #region Variables

        private Direccion[] direccion;
        private int resultado;

        #endregion

        #region Metodos

        public async Task<Direccion[]> ConsultarDireccionesGrupo(int codigoFamiliar)
        {
            var rt = string.Empty;
            var client = new HttpClient();

            client.BaseAddress = new Uri(GlobalVariables.AcumenUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("FedAuth", GlobalVariables.TokenAcumen);
            var response = client.GetAsync("/api/direccion?codigoFamiliar=" + codigoFamiliar).Result;
            var stringData = response.Content.ReadAsStringAsync().Result;

            var data = JsonConvert.DeserializeObject<List<Direccion>>(await response.Content.ReadAsStringAsync());
            direccion = new Direccion[data.Count];
            if (response.StatusCode == HttpStatusCode.OK)

                for (var i = 0; i < data.Count; i++)
                {
                    var ent = new Direccion();
                    ent.IdDireccion = data[i].IdDireccion;
                    ent.DireccionGrupo = data[i].DireccionGrupo;
                    direccion[i] = data[i];
                }

            return direccion;
        }

        public async Task<int> RegistrarNuevaDireccion(Direccion nuevaDireccion)
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
                var sc = new StringContent(JsonConvert.SerializeObject(nuevaDireccion), Encoding.UTF8,
                    "application/json");
                var json = JsonConvert.SerializeObject(nuevaDireccion);
                var response = client.PostAsync("/api/direccion",
                        new StringContent(JsonConvert.SerializeObject(nuevaDireccion), Encoding.UTF8,
                            "application/json"))
                    .Result;
                var stringData = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                    resultado = 1;
                else
                    resultado = 0;
            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                await page.DisplayAlert(MessageSource.titleGeneral, MessageSource.messageDireccion,
                    MessageSource.buttonTextOk);
            }

            return resultado;
        }

        #endregion
    }
}