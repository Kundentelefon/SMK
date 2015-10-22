//using MySql.Data.MySqlClient;
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
using System.IO;

namespace SMK
{
    public class CreateAccountPage : ContentPage
    {
        Entry username, password1, password2;
        private bool _exception;

        /// <summary>
        /// Checks: if Account is already in the database, if inserted String is a valid Email format, if entry password1 is equal entry password2. Adds then the User to the Database with hashed Password and continous to the MainPage().
        /// </summary>
        public CreateAccountPage()
        {
            var button = new Button { Text = "Account erstellen", BackgroundColor = Color.FromHex("E2001A") };
            button.Clicked += async (sender, e) =>
            {
                //Checks if the User is already in the database 
                if (await IsDuplicatedUserAsync(username.Text))
                {
                    if (_exception)
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
                    if (_exception)
                        await DisplayAlert("Verbindungsfehler", "Server ist nicht erreichtbar. Internetzugang aktiv?", "OK");
                    else
                    {
                        User user = new User(username.Text, password1.Text);
                        await DisplayAlert("Account erstellt!", "Neuer Account wurde erstellt", "OK");
                        await DownloadInitialContent(user);
                        await Navigation.PushModalAsync(new NavigationPage(new MainMenuPage(user)));
                    }
                }
            };
            var cancel = new Button { Text = "Zurück", BackgroundColor = Color.FromHex("006AB3") };
            cancel.Clicked += (sender, e) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Login");
            };

            username = new Entry { Text = "" };
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
                _exception = true;
            }
            return false;
        }

        /// <summary>
        /// Adds the User to the Database with a REST API
        /// </summary>
        /// <param name="newusername"></param>
        /// <param name="password"></param>
        public async void AddUser(string newusername, string password)
        {
            try
            {
                DataAccessHandler.DataAccess.AddUserToDatabase(newusername, password);
                await Task.Delay(3000);
            }
            catch (Exception)
            {
                _exception = true;
            }
        }

        public async Task DownloadInitialContent(User user)
        {
            try
            {
                localFileSystem files = new localFileSystem();
                String userPath = files.AdjustPath(user.user_Email);
                //A newly created User cant have a folder with the same name in the folder so no check must be implemented
                files.CreateInitalFolders(userPath);

                DataAccessHandler accessHandler = new DataAccessHandler();
                string serverAdress = accessHandler.ServerAdress;

                IFtpClient client = DependencyService.Get<IFtpClient>();

                //Download all Products from the database
                List<Product> listAllProducts = await DataAccessHandler.DataAccess.GetAllProducts();
                
                List<PContent> newEmptyContent = new List<PContent>();
                foreach (var productAll in listAllProducts)
                {

                    if (productAll.product_ID == 0) break;
                    //Download Thumbnail in Produkte Folder
                    client.DownloadFile(@"Produkte/" + productAll.product_Thumbnail,
                    DependencyService.Get<ISaveAndLoad>().Getpath(@"Produkte/") + productAll.product_Thumbnail, serverAdress, accessHandler.FtpName,
                    accessHandler.FtpPassword);
                }
                files.SaveUser(user);
                files.SaveModelsLocal(userPath, listAllProducts, newEmptyContent);

                //Hier GetALLProducts ansttt nur user products
                List<Product> listUserProducts = await DataAccessHandler.DataAccess.GetUserProducts(user);
                List<PContent> newlistPContents = new List<PContent>();
                foreach (var product in listUserProducts)
                {
                    //loads the PContent from the server
                    List<PContent> listUserPContents =
                        await DataAccessHandler.DataAccess.GetPContent(product.product_ID);

                    if (product.product_ID == 0) break;

                    foreach (var pcontent in listUserPContents)
                    {
                        if (pcontent.content_ID == 0) break;
                        //creates a new p folder if not exists for content_Kind
                        DependencyService.Get<ISaveAndLoad>().CreateFolder(DependencyService.Get<ISaveAndLoad>().PathCombine(
                            DependencyService.Get<ISaveAndLoad>().Getpath(userPath), "p" + pcontent.content_ID));

                        List<string> contentPath = await DataAccessHandler.DataAccess.GetFileServerPath(pcontent.content_ID);
                        foreach (var path in contentPath)
                        {
                            //loads every content from PContent 
                            client.DownloadFile(path,
                            DependencyService.Get<ISaveAndLoad>().Getpath(files.GetUser().user_Email) + @"/p" + pcontent.content_ID + @"/" + Path.GetFileName(path), serverAdress, accessHandler.FtpName,
                            accessHandler.FtpPassword);
                        }

                        while (newlistPContents.Count <= pcontent.content_ID)
                        {
                            newlistPContents.Add(null);
                        }

                        //updates Pcontent
                        newlistPContents[pcontent.content_ID] = pcontent;
                        if (pcontent.content_Kind != 0)
                        {
                            client.DownloadFile(@"Thumbnail/" + pcontent.content_ID + ".png",
                            DependencyService.Get<ISaveAndLoad>().Getpath(files.GetUser().user_Email + @"/Thumbnail/") + pcontent.content_ID + ".png", serverAdress, accessHandler.FtpName,
                            accessHandler.FtpPassword);
                        }
                    }
                }
                //Saves User, Products and PContent XML
                files.SaveUser(user);
                files.SaveModelsLocal(userPath, listAllProducts, newlistPContents);
            }
            catch (Exception e)
            {
                Debug.WriteLine("catched DL Exception: " + e);
                await DisplayAlert("Fehler beim Downloaden", "Es gab einen Fehler beim Downloaden, bitte erneut versuchen", "OK");
            }
        }

    }

}

