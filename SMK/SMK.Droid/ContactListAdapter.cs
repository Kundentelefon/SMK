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

namespace SMK.Droid
{
    class ContactListAdapter : BaseAdapter<Contact>
    {
        private Context mContext;
        private int mLayout;
        private List<Contact> mContacts;

        public ContactListAdapter(Context context, int layout, List<Contact> contacts)
        {
            mContext = context;
            mLayout = layout;
            mContacts = contacts;
        }

        public override Contact this[int position]
        {
            get { return mContacts[position]; }
        }

        public override int Count
        {
            get { return mContacts.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        // No View Implemented Yet
        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }
    }
}