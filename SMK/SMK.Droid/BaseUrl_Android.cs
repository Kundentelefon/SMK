using System;
using Xamarin.Forms;
using TestLocalWebsite.Droid;
using TestLocalWebsite;
using SMK.Support;

[assembly: Dependency(typeof(BaseUrl_Android))]
namespace TestLocalWebsite.Droid
{
    public class BaseUrl_Android : IBaseUrl
    {
        public string Get()
        {
            
            //definiert den Pfad wo Xamarin nachschaut wo die Bilder/Css/Java Schit liegt
            return "file:////sdcard/test/";
            // brauch ma nichmehr sobald kunde funkt

            //"file:///android_asset/";
            // Filepfad/Appname/Source/Content/
        }
    }
}