using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.Collections.Specialized;

namespace SMK.Droid
{
    public class CreateContactEventArgs : EventArgs
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public CreateContactEventArgs(int id, string name, string number)
        {
            ID = id;
            Name = name;
            Number = number;
        }
    }

    class CreateContactDialog : DialogFragment
    {
        private Button mButtonCreateContact;
        private EditText txtName;
        private EditText txtNumber;
        private ProgressBar mProgressBar;

        public event EventHandler<CreateContactEventArgs> OnCreateContact;

        void mButtonCreateContact_Click(object sender, EventArgs e)
        {
            // TODO: Progressbar implement
            mProgressBar.Visibility = ViewStates.Visible;
            //Start WebRequest
            //use webclient (webclient mor mighty than webrequest)
            WebClient client = new WebClient();

            Uri uri = new Uri("http://localhost/CreateContact.php");
            NameValueCollection parameters = new NameValueCollection();

            //strings have to match with the isset in CreateContact.php
            parameters.Add("Name", txtName.Text);
            parameters.Add("Number", txtNumber.Text);

            client.UploadValuesCompleted += Client_UploadValuesCompleted;
            //will execute webrequest on another thread
            client.UploadValuesAsync(uri, parameters);


        }

        //is called when task is done, when the webrequest is finished and than codes can be executed afterwards
        private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            //It is not possible to run it direct on the UI ID so a reference is needed
            Activity.RunOnUiThread(() =>
            {
                //get the contact ID returned from the server
                //Get String gets a byte array and convert it into a string
                string id = Encoding.UTF8.GetString(e.Result);
                int newID = 0;

                int.TryParse(id, out newID);
                if (OnCreateContact != null)
                {
                    //Broadcast event
                    OnCreateContact.Invoke(this, new CreateContactEventArgs(newID, txtName.Text, txtNumber.Text));
                }

                mProgressBar.Visibility = ViewStates.Invisible;
                this.Dismiss();
            });

        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }
    }
}