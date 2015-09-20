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

using SMK.Droid;
using Xamarin.Forms;
using SMK.Support;
using SMK.Model;

[assembly: Dependency(typeof(RotationHandler))]
namespace SMK.Droid
{
    public class RotationHandler : IRotation
    {
        public void disableRotation()
        {
               
           [ Activity(Label = "CodeLayoutActivity",
            ConfigurationChanges= Android.Content.PM.ConfigChanges.Orientation |Android.Content.PM.ConfigChanges.ScreenSize, 
            ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
        }
    }
}