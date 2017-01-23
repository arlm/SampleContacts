using Android.Content;
using Android.Util;

namespace ContactSample
{
    public class SyncAdapter : AbstractThreadedSyncAdapter
    {
        public SyncAdapter (Context context, bool autoInitialize) : base(context, autoInitialize)
        {
            Log.Info("br.com.alexandremarcondes.ContactSample", "Sync adapter created");
        }

        public override void OnPerformSync (Android.Accounts.Account account, Android.OS.Bundle extras, string authority, ContentProviderClient provider, SyncResult syncResult)
        {
            Log.Info("br.com.alexandremarcondes.ContactSample", "Sync adapter called");
            ContactManager.AddContact(Context, new MyContact("sample", "sampleee"));
        }
    }
}
