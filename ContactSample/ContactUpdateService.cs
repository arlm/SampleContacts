using Android.App;

namespace ContactSample
{
    [Service(Name = "br.com.alexandremarcondes.ContactSample.ContactUpdateService",
             IsolatedProcess = true,
             Process = ":contacts",
             Exported = true)]
    public class ContactUpdateService : Service
    {
        public override void OnCreate ()
        {
            base.OnCreate();

            //ContactsManager.updateMyContact(this, "Sample");
            /*AccountManager manager = AccountManager.get(this);
            AccountManagerFuture<Bundle> future = manager.addAccount(ContactsManager.accountType, null, null, null, null, null, null);
            Intent intent = new Intent(this, AuthenticatorActivity.class);
            try {
                intent.putExtras(future.getResult());
                startActivity(intent);
            }
            catch (Exception e) {
                e.printStackTrace();
            }*/

            ContactManager.AddContact(this, new MyContact("sample", "sample"));
        }

        public override Android.OS.IBinder OnBind (Android.Content.Intent intent)
        {
            return null;
        }
    }
}
