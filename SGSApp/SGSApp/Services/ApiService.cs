using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using SGSApp.Helper;
using SGSApp.Models;

namespace SGSApp.Services
{
    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet settings."
                };

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
                "google.com");
            if (!isReachable)
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check you internet connection."
                };

            return new Response
            {
                IsSuccess = true,
                Message = "Ok"
            };
        }


        public async Task<Response> Post<T>(T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.AcumenUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("FedAuth", GlobalVariables.TokenAcumen);

                var response = client.PostAsync("/api/direccion", content).Result;
                var stringData = response.Content.ReadAsStringAsync().Result;


                if (!response.IsSuccessStatusCode)
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString()
                    };

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = newRecord
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}