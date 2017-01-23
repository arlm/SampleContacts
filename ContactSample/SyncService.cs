using Android.App;
using Android.Util;

namespace ContactSample
{
    [Service(Name = "br.com.alexandremarcondes.ContactSample.SyncService", Exported = true)]
    [IntentFilter(new string[] { "android.content.SyncAdapter" })]
    [MetaData("android.content.SyncAdapter", Resource = "@xml/syncadapter")]
    [MetaData("android.provider.CONTACTS_STRUCTURE", Resource = "@xml/contacts")]
    public class SyncService : Service
    {
        private object syncAdapterLock = new object();
        private SyncAdapter syncAdapter;

        public override void OnCreate ()
        {
            base.OnCreate();

            Log.Info("br.com.alexandremarcondes.ContactSample", "Sync service created");

            lock (syncAdapterLock)
            {
                if (syncAdapter == null)
                {
                    syncAdapter = new SyncAdapter(ApplicationContext, true);
                }
            }
        }

        public override Android.OS.IBinder OnBind (Android.Content.Intent intent)
        {
            Log.Info("br.com.alexandremarcondes.ContactSample", "Sync service binded");
            return syncAdapter.SyncAdapterBinder;
        }
    }
}
