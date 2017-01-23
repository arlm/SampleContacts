using Android.App;
using Android.Util;

namespace ContactSample
{
    [Service(Name = "br.com.alexandremarcondes.ContactSample.AuthenticationService", Exported = true)]
    [IntentFilter(new string[] { "android.accounts.AccountAuthenticator" })]
    [MetaData("android.accounts.AccountAuthenticator", Resource = "@xml/authenticator")]
    public class AuthenticationService : Service
    {
        private const string TAG = nameof(AuthenticationService);

        private Authenticator authenticator;

        public override void OnCreate ()
        {
            base.OnCreate();

            if (Log.IsLoggable(TAG, LogPriority.Verbose))
            {
                Log.Verbose(TAG, "SampleSyncAdapter Authentication Service started.");
            }

            authenticator = new Authenticator(this);
        }

        public override void OnDestroy ()
        {
            if (Log.IsLoggable(TAG, LogPriority.Verbose))
            {
                Log.Verbose(TAG, "SampleSyncAdapter Authentication Service stopped.");
            }

            base.OnDestroy();
        }

        public override Android.OS.IBinder OnBind (Android.Content.Intent intent)
        {
            if (Log.IsLoggable(TAG, LogPriority.Verbose))
            {
                Log.Verbose(TAG, "getBinder()...  returning the AccountAuthenticator binder for intent "
                        + intent);
            }
            return authenticator.IBinder;
        }
    }
}
