using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using FFImageLoading;
using Firebase.Iid;
using ImageCircle.Forms.Plugin.Droid;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.MobileServices;
using RoundedCornerViewDemo.ControlsToolkit.Custom;
using UXDivers.Gorilla;
using UXDivers.Gorilla.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XLabs.Forms.Controls;
using ZXing.Net.Mobile.Forms;
using CircleImage = ImageCircle.Forms.Plugin.Abstractions.CircleImage;
using Platform = ZXing.Net.Mobile.Forms.Android.Platform;

namespace SGSApp.Droid
{
    [Activity(Label = "SGS App", Icon = "@drawable/ic_sgs_escudo_blanco", Theme = "@style/MainTheme",
        MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Platform.Init();
            ImageCircleRenderer.Init();
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            FFImageLoading.Forms.Droid.CachedImageRenderer.Init();
            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = false,
                VerbosePerformanceLogging = false,
                VerboseMemoryCacheLogging = false,
                VerboseLoadingCancelledLogging = false
            };
            ImageService.Instance.Initialize(config);

            // Initialization for Azure Mobile Apps
            CurrentPlatform.Init();
            // This MobileServiceClient has been configured to communicate with the Azure Mobile App and
            // Azure Gateway using the application url. You're all set to start working with your Mobile App!
            var sgsappClient = new MobileServiceClient(
                "https://sgsapp.azurewebsites.net");
            try
            {
                Forms.Init(this, bundle);
                
                //XamForms.Controls.Droid.Calendar.Init();
                LoadApplication(new App());
                // LoadApplication(Player.CreateApplication(this, new Config("Good Gorilla").RegisterAssembliesFromTypes<CircleImage, RoundedCornerView, ZXingBarcodeImageView>()));


                // Force refresh of the token. If we redeploy the app, no new token will be sent but the old one will
                // be invalid.
#if DEBUG
                Task.Run(() =>
                {
                    // This may not be executed on the main thread.
                    FirebaseInstanceId.Instance.DeleteInstanceId();
                    Console.WriteLine("Forced token: " + FirebaseInstanceId.Instance.Token);
                });
#endif
            }
            catch (Exception ex)
            {
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode,
                data);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}