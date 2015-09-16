using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Specialized;
using Android.Content;

namespace SMK.Droid
{
    [Activity(Label = "SMK", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {

        private List<Contact> mContacts;
        private WebClient mclient;
        private Uri mUrl;
        private BaseAdapter<Contact> mAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            // server connection to list
            mContacts = new List<Contact>();

            mclient = new WebClient();
            mUrl = new Uri("http://localhost/UpdateContact.php");

            //Call the PHP file and get the json string
            mclient.DownloadDataAsync(mUrl); // no premeters to give to select context, because json is used
            mclient.DownloadDataCompleted += mclient_DownloadDataCompleted;

        }

        private void mclient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                //deserializes json
                string json = Encoding.UTF8.GetString(e.Result);
                // parse it
                mContacts = JsonConvert.DeserializeObject<List<Contact>>(json);

                // TODO: Implement Action
                //mAdapter = new ContactListAdapter(this, Resource.Layout.row_contact, mContacts, action);
            });
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {

                WebClient client = new WebClient();
                System.Uri uri = new System.Uri("http://localhost/UpdateContact.php");

                NameValueCollection parameters = new NameValueCollection();

                //TODO: ContactID
                //parameters.Add("ContactID", contactID.ToString());

                client.UploadValuesAsync(uri, parameters);
                client.UploadValuesCompleted += Client_UploadValuesCompleted;
            }
        }

        private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                Console.WriteLine(Encoding.UTF8.GetString(e.Result));
            });
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                // TODO: using Xamarin Forms XAML 
                //case Resource.Id.add:

                //    CreateContactDialog dialog = new CreateContactDialog();
                //    FragmentTransaction transaction = FragmentManager.BeginTransaction();

                //    //Subscribe to event
                //    dialog.OnCreateContact += dialog_OnCreateContact;
                //    dialog.Show(transaction, "create contact");
                //    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }

        }

        private void dialog_OnCreateContact(object sender, CreateContactEventArgs e)
        {
            mContacts.Add(new Contact() { ID = e.ID, Name = e.Name, Number = e.Number });
            mAdapter.NotifyDataSetChanged();
        }
    }

}

