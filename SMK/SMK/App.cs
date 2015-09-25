﻿using SMK.Model;
using SMK.Support;
using SMK.View;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace SMK
{
    public class App : Application
    {
        private static App _current;
        public static App Current
        {
            get { return _current ?? (_current = new App()); }
        }

        public User CurrentUser { get; private set; }

        public bool IsLoggedIn { get { return CurrentUser != null && CurrentUser.user_Password != null; } }

        private App()
        {}

        public void Logout()
        {
            // It will set true on the Main Page
            CurrentUser.user_Password = null;
            rememberLogin(CurrentUser);
        }

        public void Login(User user)
        {
            CurrentUser = user;
            rememberLogin(user);
        }

        protected override void OnStart()
        {
            CurrentUser = DependencyService.Get<ISaveAndLoad>().loadUserXml(UserLoginDataFilePath());

            if (IsLoggedIn)
                MainPage = new NavigationPage(new MainMenuPage());
            else
                MainPage = new NavigationPage(new LoginPage());
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

        public async void rememberLogin(User user)
        {
            await Task.Run(() => {
                string userLoginDataFilePath = UserLoginDataFilePath();
                ISaveAndLoad saveAndLoad = DependencyService.Get<ISaveAndLoad>();
                if(saveAndLoad.fileExist(userLoginDataFilePath))
                    saveAndLoad.deleteFile(userLoginDataFilePath);
                saveAndLoad.saveUserXml(userLoginDataFilePath, user);
            });
        }

        private string UserLoginDataFilePath()
        {
            return DependencyService.Get<ISaveAndLoad>().getpath("loginData.xml");
        }
    }
}
