using Android.Accounts;
using Android.Content;
using Android.OS;
using System;

namespace ContactSample
{
    public class Authenticator : AbstractAccountAuthenticator
    {
        private readonly Context context;

        public Authenticator (Context context) : base(context)
        {
            this.context = context;
        }

        public override Bundle AddAccount (AccountAuthenticatorResponse response, string accountType, string authTokenType, string[] requiredFeatures, Bundle options)
        {
            var intent = new Intent(context, typeof(AuthenticatorActivity));
            intent.PutExtra(AccountManager.KeyAccountAuthenticatorResponse, response);

            var bundle = new Bundle();
            bundle.PutParcelable(AccountManager.KeyIntent, intent);
            return bundle;
        }

        public override Bundle ConfirmCredentials (AccountAuthenticatorResponse response, Account account, Bundle options)
        {
            return null;
        }

        public override Bundle EditProperties (AccountAuthenticatorResponse response, string accountType)
        {
            throw new InvalidOperationException();
        }

        public override Bundle GetAuthToken (AccountAuthenticatorResponse response, Account account, string authTokenType, Bundle options)
        {
            var result = new Bundle();
            result.PutString(AccountManager.KeyAccountName, account.Name);
            result.PutString(AccountManager.KeyAccountType, AccountGeneral.ACCOUNT_TYPE);
            return result;
        }

        public override string GetAuthTokenLabel (string authTokenType)
        {
            // null means we don't support multiple authToken types
            return null;
        }

        public override Bundle HasFeatures (AccountAuthenticatorResponse response, Account account, string[] features)
        {
            // This call is used to query whether the Authenticator supports
            // specific features. We don't expect to get called, so we always
            // return false (no) for any queries.
            var result = new Bundle();
            result.PutBoolean(AccountManager.KeyBooleanResult, false);
            return result;
        }

        public override Bundle UpdateCredentials (AccountAuthenticatorResponse response, Account account, string authTokenType, Bundle options)
        {
            return null;
        }
    }
}
