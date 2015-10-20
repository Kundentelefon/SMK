﻿//using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using SMK.DataAccess;

using Xamarin.Forms;
using RestSharp;
using System.Threading.Tasks;
using SMK.Support;
using SMK.View;
using SMK.Model;
using System.Diagnostics;

namespace SMK
{
    public class CreateAccountPage : ContentPage
    {
        Entry username, password1, password2;
        // TODO: find better way to avoid giving null back to validUser
        private bool exception;

        /// <summary>
        /// Checks: if Account is already in the database, if inserted String is a valid Email format, if entry password1 is equal entry password2. Adds then the User to the Database with hashed Password and continous to the MainPage().
        /// </summary>
        /// <param name="ilm"></param>
        /// <param name="task"></param>
        public CreateAccountPage()
        {
            //bool isDuplicated = true;

            var button = new Button { Text = "Account erstellen", BackgroundColor = Color.FromHex("E2001A") };
            button.Clicked += async (sender, e) =>
            {
                //Checks if the User is already in the database 
                if (await IsDuplicatedUserAsync(username.Text))
                {
                    if (exception)
                        await DisplayAlert("Verbindungsfehler", "Server ist nicht erreichtbar. Internetzugang aktiv?", "OK");
                    else
                        await DisplayAlert("Account bereits vorhanden!", "Anderen account angeben", "OK");
                }
                //Checks if the User is in a Valid Email Format
                else if (!LoginPage.IsValidEmail(username.Text))
                {
                    await DisplayAlert("Ungültige E-Mail", "E-Mail ist in einem ungültigen Format angegeben worden", "Neue Eingabe");
                }
                // Checks if password1 equals password2
                else if (!(password1.Text.Equals(password2.Text)))
                {
                    await DisplayAlert("Passwort wiederholen!", "Passwörter sind nicht identisch", "OK");
                }

                // Adds the user to the database with hashed password
                else
                {
                    AddUser(username.Text, password1.Text);
                    if (exception)
                        await DisplayAlert("Verbindungsfehler", "Server ist nicht erreichtbar. Internetzugang aktiv?", "OK");
                    else
                    {
                        User user = new User(username.Text, password1.Text);
                        await DisplayAlert("Account erstellt!", "Neuer Account wurde erstellt", "OK");
                        DownloadInitialContent(user);
                        await Navigation.PushModalAsync(new NavigationPage(new MainMenuPage(user)));
                    }
                }
            };
            var cancel = new Button { Text = "Zurück", BackgroundColor = Color.FromHex("006AB3") };
            cancel.Clicked += (sender, e) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Login");
            };

            username = new Entry { Text = ""};
            password1 = new Entry { Text = "", IsPassword = true };
            password2 = new Entry { Text = "", IsPassword = true };

            Content = new StackLayout
            {
                Padding = new Thickness(10, 40, 10, 10),
                Children = {
                    new Label { Text = "Account erstellen", Font = Font.SystemFontOfSize(NamedSize.Large) },
                    new Label { Text = "Gib deine E-Mail Addresse an" },
                    username,
                    new Label { Text = "Passwort" },
                    password1,
                    new Label { Text = "Passwort wiederholen" },
                    password2,
                    button, cancel
                }
            };
        }

        /// <summary>
        /// Checks if the User is duplicated with a REST API
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public async Task<bool> IsDuplicatedUserAsync(string strIn)
        {
            try
            {
                return await DataAccessHandler.DataAccess.IsDuplicatedUser(strIn);
            }
            catch (Exception)
            {
                exception = true;
            }
            return false;
        }

        /// <summary>
        /// Adds the User to the Database with a REST API
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public async void AddUser(string username, string password)
        {
            try
            {
                DataAccessHandler.DataAccess.AddUserToDatabase(username, password);
                await Task.Delay(3000);
            }
            catch (Exception)
            {
                exception = true;
            }
        }

        public async void DownloadInitialContent(User user)
        {
            try
            {
                DataAccessHandler accessHandler = new DataAccessHandler();
                string serverAdress = accessHandler.ServerAdress;
                List<Product> listUserProducts = await DataAccessHandler.DataAccess.GetUserProducts(user);

                localFileSystem files = new localFileSystem();
                String userPath = files.AdjustPath(user.user_Email);
                //A newly created User cant have a folder with the same name in the folder so no check must be implemented
                files.createInitalFolders(userPath);
                Debug.WriteLine("Folder exist1 " + DependencyService.Get<ISaveAndLoad>().fileExist(DependencyService.Get<ISaveAndLoad>().pathCombine(user.user_Email, "PContent")));

                List<PContent> newlistPContents = new List<PContent>();

                Debug.WriteLine("CreateUser Downloade File start");
                IFtpClient client = DependencyService.Get<IFtpClient>();
                //Download all Products this user posses with all its Pcontent
                foreach (var product in listUserProducts)
                {
                    List<PContent> listUserPContents =
                        await DataAccessHandler.DataAccess.GetPContent(product.product_ID);

                    string thumbnailpath = await DataAccessHandler.DataAccess.getThumbnailPath(product.product_ID);
                    client.DownloadFile(thumbnailpath,
                    DependencyService.Get<ISaveAndLoad>().getpath(App.Current.CurrentUser.user_Email + @"\Thumbnail"), serverAdress, accessHandler.FtpName,
                    accessHandler.FtpPassword);

                    foreach (var pcontent in listUserPContents)
                    {
                        client.DownloadFile(pcontent.content_path,
                            DependencyService.Get<ISaveAndLoad>().getpath(App.Current.CurrentUser.user_Email) + @"\p" + pcontent.content_Kind, serverAdress, accessHandler.FtpName,
                            accessHandler.FtpPassword);
                        //inserts the new PContent to PContent list
                        newlistPContents.Add(pcontent);
                    }

                }
                Debug.WriteLine("CreateUser Downloade File end");
                files.saveModelsLocal(userPath, listUserProducts, newlistPContents);

                //Load initial testcontent
                //client.DownloadDirectoryAsync("zeug/PContent", DependencyService.Get<ISaveAndLoad>().getpath(user.user_Email), serverAdress, "SMKFTPUser", "");
                Debug.WriteLine("Test1 Downloade File end");
            }
            catch (Exception e)
            {
                Debug.WriteLine("catched DL Exception: " + e);
                await DisplayAlert("Fehler beim Downloaden", "Es gab einen Fehler beim Downloaden, bitte erneut versuchen", "OK");
            }
        }

    }
    
}

