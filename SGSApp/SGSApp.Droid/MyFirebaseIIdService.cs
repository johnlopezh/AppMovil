using Android.App;
using Firebase.Iid;

namespace SGSApp.Droid
{
    [Service]
    [IntentFilter(new[] {"com.google.firebase.INSTANCE_ID_EVENT"})]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        private const string TAG = "MyFirebaseIIDService";

        public override void OnTokenRefresh()
        {
            // Guardamos el token en el almacenamiento de nuestra app para poder usarlo más adelante
            var token = FirebaseInstanceId.Instance.Token;
        }
    }
}