using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;
using UXDivers.Gorilla;
using UXDivers.Gorilla.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamForms.Controls.iOS;
using XLabs.Forms.Controls;
using Platform = ZXing.Net.Mobile.Forms.iOS.Platform;

namespace SGSApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Platform.Init();
            Forms.Init();
            Calendar.Init();
            ImageCircleRenderer.Init();
            //LoadApplication(new App());
            //LoadApplication(Player.CreateApplication(new Config("Good Gorilla").RegisterAssemblyFromType<CircleImage>()));

            return base.FinishedLaunching(app, options);
        }
    }
}