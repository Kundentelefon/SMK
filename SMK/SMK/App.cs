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

        /// <summary>
        /// Application Current: Checks _current and _current = new App()) and gives the parameter back which is not null
        /// </summary>
        public static App Current
        {
            get { return _current ?? (_current = new App()); }
        }

        /// <summary>
        /// Property: CurrentUser with private set
        /// </summary>
        public User CurrentUser { get; private set; }

        /// <summary>
        /// Property: Checks if
        /// </summary>
        public bool IsLoggedIn { get { return CurrentUser != null && CurrentUser.user_Password != null; } }

        private App()
        {}

        /// <summary>
        /// It will set the user_Password to null and creates then a new loginData.xml
        /// </summary>
        public void Logout()
        {
            // It will set true on the Main Page
            CurrentUser.user_Password = null;
            rememberLogin(CurrentUser);
        }

        /// <summary>
        /// Set current user with user parameter and saves the login in loginData.xml
        /// </summary>
        /// <param name="user"></param>
        public void Login(User user)
        {
            CurrentUser = user;
            rememberLogin(user);
        }

        /// <summary>
        /// Receives the User Login Status 
        /// </summary>
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

        /// <summary>
        /// Checks if loginData.xml exists and delete it if so. Definitaly creates a new loginData.xml file
        /// </summary>
        /// <param name="user"></param>
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

        /// <summary>
        ///  Returns the location of the loginData.xml
        /// </summary>
        /// <returns></returns>
        private string UserLoginDataFilePath()
        {
            return DependencyService.Get<ISaveAndLoad>().getpath("loginData.xml");
        }
    }
}
