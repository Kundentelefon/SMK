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
using System.Threading;
using SMK.Droid;

namespace ImageGallary.Droid
{
    [Activity(Label = "SMK", Theme = "@style/Theme.Splash", Icon = "@drawable/icon", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Thread.Sleep(10000); // Simulate a long loading process on app startup.
            StartActivity(typeof(MainActivity));
        }
    }
}