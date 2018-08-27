using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGSApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Prueba : ContentPage
    {
        private readonly HttpClient _client = new HttpClient();

        public Prueba()
        {
            InitializeComponent();
            _client.BaseAddress = new Uri(App.MobileServiceUrl);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.Timeout = TimeSpan.FromSeconds(120);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<object, string>(this, App.NotificationReceivedKey, OnMessageReceived);
            btnSend.Clicked += OnBtnSendClicked;
        }

        private void OnMessageReceived(object sender, string msg)
        {
            Device.BeginInvokeOnMainThread(() => { lblMsg.Text = msg; });
        }

        public async void OnBtnSendClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Sending message: " + txtMsg.Text);
            var content = new StringContent("\"" + txtMsg.Text + "\"", Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("SGSAppNotificacionHub", content);
            Debug.WriteLine("Resultado Envio: " + result.IsSuccessStatusCode);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object>(this, App.NotificationReceivedKey);
        }
    }
}