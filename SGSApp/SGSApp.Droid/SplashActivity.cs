using Android.App;
using Android.OS;

namespace SGSApp.Droid
{
    [Activity(Label = "SGS", Icon = "@drawable/sgs_lite_launcher", Theme = "@style/Theme.Splash", MainLauncher = true,
        NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
            Finish();
            OverridePendingTransition(0, 0);
        }
    }
}