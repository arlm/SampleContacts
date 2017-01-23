using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Accounts;
using Android.Util;
using System;
using Android.Content.PM;

namespace ContactSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true,
              Icon = "@mipmap/icon", Exported = true,
              ScreenOrientation = ScreenOrientation.Portrait,
             Name = "br.com.alexandremarcondes.ContactSample.MainActivity")]
    public class MainActivity : Activity, IAccountManagerCallback
    {
        private Intent serviceIntent;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.main_activity);

            var button = FindViewById<Button>(Resource.Id.addContactButton);
            button.Click += (sender, e) => {
                AddNewAccount(AccountGeneral.ACCOUNT_TYPE, AccountGeneral.AUTHTOKEN_TYPE_FULL_ACCESS);
                ContactManager.AddContact(this, new MyContact("sample", "samplee"));
                /*if (serviceIntent == null)
                    serviceIntent = new Intent(SampleActivity.this, ContactUpdateService.class);
                stopService(serviceIntent);
                startService(serviceIntent);
                */
            };
        }

        private void AddNewAccount (string accountType, string authTokenType)
        {
            var future = AccountManager.Get(this).AddAccount(accountType, authTokenType, null, null, this, this, null);
        }

        public void Run (IAccountManagerFuture future)
        {
            try
            {
                var bnd = future.Result;
                Log.Info("br.com.alexandremarcondes.ContactSample", "Account was created");
            }
            catch (Exception e)
            {
                Log.Error("br.com.alexandremarcondes.ContactSample", e.StackTrace);
            }
        }
    }
}
