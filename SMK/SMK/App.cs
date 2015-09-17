using SMK.View;
using System;
using Xamarin.Forms;


namespace SMK
{
    public class App : Application, ILoginManager
    {
        static ILoginManager loginManager;
        public static App Current;


        public App()
        {
            
            NavigationPage Navigation_Page = new NavigationPage(new MainMenuPage());
            MainPage = Navigation_Page;
            Current = this;

            var isLoggedIn = Properties.ContainsKey("IsLoggedIn") ? (bool)Properties["IsLoggedIn"] : false;

            // after login the next site to go
            if (isLoggedIn)
                //  MainPage = Navigation_Page;
                MainPage = Navigation_Page;
            else
                MainPage = new LoginModalPage(this);
        }

        public void ShowMainPage()
        {
            MainPage = new NavigationPage(new MainMenuPage());
        }

        public void Logout()
        {
            // It will set true on the Main Page
            Properties["IsLoggedIn"] = false;
            MainPage = new LoginModalPage(this);
        }

    }
}
