using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SGSApp.Helper;
using SGSApp.Models;

namespace SGSApp.ViewModel
{
    internal class UserVM
    {
        private string numeroIdentificacion;

        public UserVM()
        {
            // Web service call to update list with new values
            Usuario.GetUsuarioAsync();
            //Task.Delay(5000).Wait();
        }

        public bool Error { get; private set; }


        public async Task<string> ConsultarInfoUsuario(string Username)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(GlobalVariables.AcumenUrl)
                };

                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("FedAuth", GlobalVariables.TokenAcumen);
                var response = client.GetAsync("/api/dashboard?Username=" + Username).Result;
                var stringData = response.Content.ReadAsStringAsync().Result;

                var data = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

                if (response.StatusCode == HttpStatusCode.OK)

                    numeroIdentificacion = data;
            }
            catch (Exception ex)
            {
                Error = true;
            }

            return numeroIdentificacion;
        }
    }
}