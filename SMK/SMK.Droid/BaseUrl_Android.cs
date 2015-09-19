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
using SMK.Support;

namespace SMK.Droid
{
    public class BaseUrl_Android : IBaseUrl
    {
        public string Get()
        {
            //definiert den Pfad wo Xamarin nachschaut wo die Bilder/Css/Java Schit liegt
            return "file:////sdcard/test/";
            //"file:///android_asset/";
            // Filepfad/Appname/Source/Content/
        }
    }
}