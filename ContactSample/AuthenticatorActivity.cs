using Android.Accounts;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Util;

namespace ContactSample
{
    [Activity(Label = "@string/app_name", Exported =  true,
              Name = "br.com.alexandremarcondes.ContactSample.AuthenticatorActivity")]
    public class AuthenticatorActivity : AccountAuthenticatorActivity
    {
        private AccountManager accountManager;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Log.Info("br.com.alexandremarcondes.ContactSample", "AuthenticatorActivity");

            var res = new Intent();
            res.PutExtra(AccountManager.KeyAccountName, AccountGeneral.ACCOUNT_NAME);
            res.PutExtra(AccountManager.KeyAccountType, AccountGeneral.ACCOUNT_TYPE);
            res.PutExtra(AccountManager.KeyAuthtoken, AccountGeneral.ACCOUNT_TOKEN);

            var account = new Account(AccountGeneral.ACCOUNT_NAME, AccountGeneral.ACCOUNT_TYPE);

            accountManager = AccountManager.Get(this);
            accountManager.AddAccountExplicitly(account, null, null);
            //mAccountManager.setAuthToken(account, AccountGeneral.AUTHTOKEN_TYPE_FULL_ACCESS, AccountGeneral.ACCOUNT_TOKEN);
            ContentResolver.SetSyncAutomatically(account, ContactsContract.Authority, true);
            SetAccountAuthenticatorResult(res.Extras);
            SetResult(Result.Ok, res);
            Finish();
        }
    }
}
