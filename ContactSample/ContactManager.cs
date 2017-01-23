using System;
using Android.Content;
using Android.Provider;
using System.Collections.Generic;
using Android.Util;

namespace ContactSample
{
    public class ContactManager
    {
        private const string MIMETYPE = "vnd.android.cursor.item/com.sample.profile";

        public static void AddContact (Context context, MyContact contact)
        {
            context.ContentResolver.Delete(ContactsContract.RawContacts.ContentUri, ContactsContract.RawContacts.InterfaceConsts.AccountType + " = ?", new String[] { AccountGeneral.ACCOUNT_TYPE });

            var ops = new List<ContentProviderOperation>();

            ops.Add(ContentProviderOperation.NewInsert(AddCallerIsSyncAdapterParameter(ContactsContract.RawContacts.ContentUri, true))
                    .WithValue(ContactsContract.RawContacts.InterfaceConsts.AccountName, AccountGeneral.ACCOUNT_NAME)
                    .WithValue(ContactsContract.RawContacts.InterfaceConsts.AccountType, AccountGeneral.ACCOUNT_TYPE)
                    //.withValue(RawContacts.SOURCE_ID, 12345)
                    //.withValue(RawContacts.AGGREGATION_MODE, RawContacts.AGGREGATION_MODE_DISABLED)
                    .Build());

            ops.Add(ContentProviderOperation.NewInsert(AddCallerIsSyncAdapterParameter(ContactsContract.Settings.ContentUri, true))
                    .WithValue(ContactsContract.RawContacts.InterfaceConsts.AccountName, AccountGeneral.ACCOUNT_NAME)
                    .WithValue(ContactsContract.RawContacts.InterfaceConsts.AccountType, AccountGeneral.ACCOUNT_TYPE)
                    .WithValue(ContactsContract.Settings.InterfaceConsts.UngroupedVisible, 1)
                    .Build());

            ops.Add(ContentProviderOperation.NewInsert(AddCallerIsSyncAdapterParameter(ContactsContract.Data.ContentUri, true))
                    .WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, 0)
                    .WithValue(ContactsContract.Data.InterfaceConsts.Mimetype, ContactsContract.CommonDataKinds.StructuredName.ContentItemType)
                    .WithValue(ContactsContract.CommonDataKinds.StructuredName.GivenName, contact.Name)
                    .WithValue(ContactsContract.CommonDataKinds.StructuredName.FamilyName, contact.LastName)
                    .Build());

            ops.Add(ContentProviderOperation.NewInsert(AddCallerIsSyncAdapterParameter(ContactsContract.Data.ContentUri, true))
                    .WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, 0)
                    .WithValue(ContactsContract.Data.InterfaceConsts.Mimetype, ContactsContract.CommonDataKinds.Phone.ContentItemType)
                    .WithValue(ContactsContract.CommonDataKinds.Phone.Number, "12342145")
                    .Build());


            ops.Add(ContentProviderOperation.NewInsert(AddCallerIsSyncAdapterParameter(ContactsContract.Data.ContentUri, true))
                    .WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, 0)
                    .WithValue(ContactsContract.Data.InterfaceConsts.Mimetype, ContactsContract.CommonDataKinds.Email.ContentItemType)
                    .WithValue(ContactsContract.CommonDataKinds.Email.InterfaceConsts.Data, "sample@email.com")
                    .Build());


            ops.Add(ContentProviderOperation.NewInsert(AddCallerIsSyncAdapterParameter(ContactsContract.Data.ContentUri, true))
                    .WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, 0)
                    .WithValue(ContactsContract.Data.InterfaceConsts.Mimetype, MIMETYPE)
                    .WithValue(ContactsContract.Data.InterfaceConsts.Data1, 12345)
                    .WithValue(ContactsContract.Data.InterfaceConsts.Data2, "sample")
                    .WithValue(ContactsContract.Data.InterfaceConsts.Data3, "sample")
                    .Build());
            try
            {
                var results = context.ContentResolver.ApplyBatch(ContactsContract.Authority, ops);

                if (results.Length == 0)
                {

                }
            }
            catch (Exception e)
            {
                Android.Util.Log.Error("br.com.alexandremarcondes.ContactSample", e.StackTrace);
            }
        }

        private static Android.Net.Uri AddCallerIsSyncAdapterParameter (Android.Net.Uri uri, bool isSyncOperation)
        {
            if (isSyncOperation)
            {
                return uri.BuildUpon()
                          .AppendQueryParameter(ContactsContract.CallerIsSyncadapter, "true")
                          .Build();
            }

            return uri;
        }

        public static List<MyContact> GetMyContacts ()
        {
            return null;
        }

        public static void UpdateMyContact (Context context, String name)
        {
            int id = -1;
            var cursor = context.ContentResolver.Query(ContactsContract.Data.ContentUri,
                                                       new String[] { ContactsContract.Data.InterfaceConsts.RawContactId, ContactsContract.Data.InterfaceConsts.DisplayName, ContactsContract.Data.InterfaceConsts.Mimetype, ContactsContract.Data.InterfaceConsts.ContactId }, ContactsContract.CommonDataKinds.StructuredName.DisplayName + "= ?",
                                                       new String[] { name }, null);

            if (cursor != null && cursor.MoveToFirst())
            {
                do
                {
                    id = cursor.GetInt(0);
                    Log.Info("br.com.alexandremarcondes.ContactSample", cursor.GetString(0));
                    Log.Info("br.com.alexandremarcondes.ContactSample", cursor.GetString(1));
                    Log.Info("br.com.alexandremarcondes.ContactSample", cursor.GetString(2));
                    Log.Info("br.com.alexandremarcondes.ContactSample", cursor.GetString(3));
                } while (cursor.MoveToNext());
            }

            if (id != -1)
            {
                var ops = new List<ContentProviderOperation>();

                ops.Add(ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri)
                    .WithValue(ContactsContract.Data.InterfaceConsts.RawContactId, id)
                        .WithValue(ContactsContract.Data.InterfaceConsts.Mimetype, ContactsContract.CommonDataKinds.Email.ContentItemType)
                        .WithValue(ContactsContract.CommonDataKinds.Email.InterfaceConsts.Data, "sample")
                    .Build());

                ops.Add(ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri)
                    .WithValue(ContactsContract.Data.InterfaceConsts.RawContactId, id)
                        .WithValue(ContactsContract.Data.InterfaceConsts.Mimetype, MIMETYPE)
                        .WithValue(ContactsContract.Data.InterfaceConsts.Data1, "profile")
                    .WithValue(ContactsContract.Data.InterfaceConsts.Data2, "profile")
                    .WithValue(ContactsContract.Data.InterfaceConsts.Data3, "profile")
                    .Build());

                try
                {
                    context.ContentResolver.ApplyBatch(ContactsContract.Authority, ops);
                }
                catch (Exception e)
                {
                    Log.Error("br.com.alexandremarcondes.ContactSample", e.StackTrace);
                }
            } else
            {
                Log.Info("br.com.alexandremarcondes.ContactSample", "id not found");
            }
        }
    }
}
