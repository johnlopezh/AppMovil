using Microsoft.IdentityModel.Clients.ActiveDirectory;
using SGSApp.Services;
using SGSApp.Views.Home;
using SGSApp.Views.Master;
using Xamarin.Forms;

namespace SGSApp
{
    public partial class App : Application
    {
        public const string NotificationReceivedKey = "NotificationReceived";
        public const string MobileServiceUrl = "http://backendsgsappnotificaciones.azurewebsites.net";


        public static string ApplicationID = "b66320d2-777b-4f6f-b5a9-6976ead7217d";
        public static string tenanturl = "https://login.microsoftonline.com/common";
        public static string ReturnUri = "https://www.sgsapp.com/";

        public static string GraphResourceUri = "https://graph.microsoft.com";

        //public static AuthenticationResult AuthenticationResult = null;
        public static AuthenticationContext authContext = null;
        public static string SharePointURL = "https://sgsedu.sharepoint.com/";
        public static string AcumenURL = "http://naranjaweb.cloudapp.net/";

        public static NavigationPage Navigator { get; internal set; }
        public static MainPage Master { get; internal set; }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Welcome());
        }

        public static AuthenticationContext AuthenticationClient { get; private set; }

        internal static AuthenticationResult AuthenticationResult { get; set; }
        public static IAuthenticator Authenticator { get; private set; }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}