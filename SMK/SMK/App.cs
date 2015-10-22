using SMK.Model;
using SMK.Support;
using SMK.View;
using System;
using System.Threading.Tasks;
using SMK.DataAccess;
using Xamarin.Forms;
using SMK;
using System.Diagnostics;
using System.Collections.Generic;

namespace SMK
{
    public class App : Application, ILoginManager
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
        /// Property: Checks if CurrentUser is null and if its password is null
        /// </summary>
        public bool IsLoggedIn { get { return CurrentUser != null && CurrentUser.user_Password != null; } }

        public App()
        {
            //wird für IOS benötigt!!!!
            //IOS basiert darauf das immer eine Seite initalisiert ist
            //deshalb muss inital auch eine Seite initalisiert werden da sonst die folgende Exception geworfen wird
            //System.NullReferenceException: Object reference not set to an instance of an object
            MainPage = new EmptyPage();
        }

        /// <summary>
        /// It will set the user_Password to null and creates then a new loginData.xml
        /// </summary>
        public void Logout()
        {
            // It will set true on the Main Page
            localFileSystem file = new localFileSystem();
            CurrentUser = file.GetUser();
            CurrentUser.user_Password = null;
            RememberLogin(CurrentUser);
            file.DeleteUser();
        }

        /// <summary>
        /// Set current user with user parameter and saves the login in loginData.xml
        /// </summary>
        /// <param name="user"></param>
        public void Login(User user)
        {
            localFileSystem file = new localFileSystem();
            file.SaveUser(user);
            RememberLogin(user);
        }

        /// <summary>
        /// Receives the User Login Status 
        /// </summary>
        protected override void OnStart()
        {
            DataAccessHandler.InitDataAccess(DataAccessHandler.InterfaceType.MySqlPhp);

            localFileSystem file = new localFileSystem();
            CurrentUser = file.GetUser();

            if (IsLoggedIn)
            {
                MainPage = new NavigationPage(new MainMenuPage(CurrentUser));
            }
            else
            {
                MainPage = new LoginModalPage();
            }
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
        public async void RememberLogin(User user)
        {
            await Task.Run(() =>
            {
                localFileSystem file = new localFileSystem();
                file.SaveUser(user);
                //string userLoginDataFilePath = UserLoginDataFilePath();
                //ISaveAndLoad saveAndLoad = DependencyService.Get<ISaveAndLoad>();
                //if(saveAndLoad.fileExist(userLoginDataFilePath))
                //    saveAndLoad.deleteFile(userLoginDataFilePath);
                //saveAndLoad.saveUserXml(userLoginDataFilePath, user);
            });
        }

        /// <summary>
        ///  Returns the location of the loginData.xml
        /// </summary>
        /// <returns></returns>
        //private string UserLoginDataFilePath(User user)
        //{
        //    localFileSystem file = new localFileSystem();
        //    return file.saveUser(user);
        //}

        //public void ShowMainPage()
        //{
        //    MainPage = new NavigationPage(new MainMenuPage());
        //}
    }
}
