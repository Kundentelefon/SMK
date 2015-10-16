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
            DataAccessHandler.InitDataAccess(DataAccessHandler.InterfaceType.MySqlPhp);

            DatabaseTest();

            CurrentUser = DependencyService.Get<ISaveAndLoad>().loadUserXml(UserLoginDataFilePath());
            if (IsLoggedIn)
                MainPage = new NavigationPage(new MainMenuPage());
            else
                MainPage = new LoginModalPage();
            // Handle when your app starts
        }

        //Testmethod activated
        //Problem: gets called twice
        public async void DatabaseTest()
        {
            try
            {
                Debug.WriteLine("Start Test Database");

                //Adds user to database
                //DataAccessHandler.DataAccess.AddUserToDatabase("testF4@web.de", "nohash");
                //Debug.WriteLine("Test User added");

                //checks if user is duplicated
                //bool b1;
                //b = await DataAccessHandler.DataAccess.IsDuplicatedUser("testF4@web.de");
                //Debug.WriteLine("Test1 duplicated user " + b1);

                //checks if password and username is correct
                //User u1;
                //User validUser = new User("testF4@web.de", "sdfsd");
                //u1 = await DataAccessHandler.DataAccess.ValidateUser(validUser);
                //Debug.WriteLine("Test1 validuser " + u1);
                //if (null == u1)
                //    Debug.WriteLine("invalid user cause null response");


                //gets all userprodcuts
                //Debug.WriteLine("Test1 getAllUserProducts");
                //User u2 = new User("testF4@web.de", "sdfsd");
                //Debug.WriteLine("Test1 getAllUserProducts2");
                //List<Product> p3 = await DataAccessHandler.DataAccess.GetUserProducts(u2);
                //Debug.WriteLine("Test1 getAllUserProducts3");
                //Debug.WriteLine("Product list -> " + p3[1].product_ID);

                //getPcontent
                //Debug.WriteLine("Test1 getPcontent");
                //List<PContent> pc1 = await DataAccessHandler.DataAccess.GetPContent("1");
                //Debug.WriteLine("PContent inhalt -> " + pc1[0].content_Title);

                //getPcontent filepath
                //Debug.WriteLine("Test1 getPath");
                //List<string> str1 = new List<string>();
                //str1 = await DataAccessHandler.DataAccess.GetPContentFiles("12");
                //Debug.WriteLine("Getpathes -> " + str1[0]);


                //checks if key is valid (already activated)
                //bool b2 = await DataAccessHandler.DataAccess.IsValidKey("2222");
                //Debug.WriteLine("Test1 isvalidkay " + b2);

                //adds Products to user
                //Debug.WriteLine("Test1 product to user added");
                //CurrentUser = DependencyService.Get<ISaveAndLoad>().loadUserXml(UserLoginDataFilePath());
                //DataAccessHandler.DataAccess.AddProductToUser(1, CurrentUser);

                //set key invalid
                //Debug.WriteLine("Test1 key set invalid");
                //DataAccessHandler.DataAccess.SetProductKeyInvalid("2222");



                //Get product of specific key 
                //Debug.WriteLine("Test1 get product_id");
                //Debug.WriteLine("Test1 id -> " + DataAccessHandler.DataAccess.GetProductByKey("2222"));
                //Product p0 = new Product();
                //p0 = await DataAccessHandler.DataAccess.GetProductByKey("2222");
                //Debug.WriteLine("Test1 id2 -> " + p0.product_ID);

                //Set used key invalid
                //DataAccessHandler.DataAccess.SetProductKeyInvalid("2222");



                //example of downloading file. change host to http://10.0.2.2 for emulator
                //string filename = "FISCHERTECHNIK_Logo.JPG";
                //    Debug.WriteLine("Test1 Downloade File start");
                //    IFtpClient client = DependencyService.Get<IFtpClient>();
                //    client.DownloadFile(filename, @"C:\Users\Maxwell\Desktop\fdbfdg\" + filename, "localhost", "SMKFTPUser", "");
                //    Debug.WriteLine("Test1 Downloade File end");



                //Test For Downloading the whole content of a directory path
                //IFtpClient client = DependencyService.Get<IFtpClient>();
                //client.DownloadDirectoryAsync(
                //    "Folder",
                //    DependencyService.Get<ISaveAndLoad>().getpath("zeug2"),
                //    "ipfromServer", "UserFTPserver", "PasswordFTPserver");

            }
            catch (Exception e)
            {
                Debug.WriteLine("Test1 Connection error " + e);
            }

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

        public void ShowMainPage()
        {
            MainPage = new NavigationPage(new MainMenuPage());
        }
    }
}
