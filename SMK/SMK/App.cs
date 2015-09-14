using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMK.View;

using Xamarin.Forms;

namespace SMK
{
    public class App : Application
    {
        public App()
        {
            NavigationPage Navigation_Page = new NavigationPage(new LoginPage_Xaml());
            MainPage = Navigation_Page;
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
