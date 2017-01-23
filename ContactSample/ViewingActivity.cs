
using Android.App;
using Android.OS;
using Android.Content.PM;

namespace ContactSample
{
    [Activity(Label = "ViewingActivity",
              ScreenOrientation = ScreenOrientation.Portrait, Exported = true,
              Name = "br.com.alexandremarcondes.ContactSample.ViewingActivity")]
    [IntentFilter(new string[] { "android.intent.action.VIEW" },
                  Categories = new string[] { "android.intent.category.DEFAULT" },
                  DataMimeType = "vnd.android.cursor.item/com.sample.profile")]
    public class ViewingActivity : Activity
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.view_activity);
        }
    }
}
