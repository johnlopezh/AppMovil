using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SGSApp.Helper;
using SGSApp.Models;

namespace SGSApp.ViewModel
{
    public class ComunVM : BaseViewModel
    {
        private readonly Dominio[] dm = new Dominio[8];
        private string[] arr4 = new string[8];

        public async Task<Dominio[]> GetDominios(string LugarResidencia)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.AcumenUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("FedAuth", GlobalVariables.TokenAcumen);
                var response = client.GetAsync("/api/dominio?valorDominio=" + LugarResidencia).Result;
                var stringData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Dominio>>(await response.Content.ReadAsStringAsync());

                if (response.StatusCode == HttpStatusCode.OK)

                    for (var i = 0; i < data.Count; i++)
                    {
                        var ent = new Dominio();
                        ent.Descripcion = data[i].Descripcion;
                        dm[i] = data[i];
                    }
            }
            catch (Exception ex)
            {
                Error = true;
            }

            return dm;
        }
    }
}