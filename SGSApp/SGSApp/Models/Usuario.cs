using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SGSApp.Helper;
using Xamarin.Forms;

namespace SGSApp.Models
{
    public class Usuario
    {
        public ImageSource ImageURL { get; set; }

        public static async Task GetUsuarioAsync()
        {
            var client = new HttpClient();
            string img;
            using (var newStream = new MemoryStream())
            {
                var bytes = newStream.ToArray();
                img = "data:image/png;base64, " +
                      "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";
                GlobalVariables.grabarImagenUsuario(bytes);
            }

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalVariables.Token);
            var meDataPhoto = await client.GetAsync("https://graph.microsoft.com/beta/me/Photo/$value");

            using (var sourceStream = await meDataPhoto.Content.ReadAsStreamAsync())
            {
                using (var newStream = new MemoryStream())
                {
                    sourceStream.CopyTo(newStream);
                    var bytes = newStream.ToArray();
                    img = "data:image/png;base64, " + Convert.ToBase64String(bytes);
                    GlobalVariables.grabarImagenUsuario(bytes);
                }
            }
        }
    }
}