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
    class Contact
    {
        //when contact gets updated int gets updated by ID
        // must match with the phpContact exactly for Json

        public int ID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        
    }
}