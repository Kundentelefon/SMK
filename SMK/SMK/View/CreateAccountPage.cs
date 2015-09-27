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

namespace SMK
{
    public class CreateAccountPage : ContentPage
    {
        Entry username, password1, password2;

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
                bool isDuplicated = await IsDuplicatedUserAsync(username.Text);
                //Checks if the User is already in the database 
                if (isDuplicated)
                {
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
                    await DisplayAlert("Account erstellt!", "Neuer Account wurde erstellt", "OK");
                    await Navigation.PushModalAsync(new MainMenuPage());
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
            bool duplicated = false;
            try
            {
                duplicated = await DataAccessHandler.DataAccess.IsDuplicatedUser(strIn);
            }
            catch (Exception)
            {
                await DisplayAlert("Connection Error", "Unable to connect to Server", "OK");
            }
            return duplicated;

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
            }
            catch (Exception)
            {
                await DisplayAlert("Connection Error", "Unable to connect to Server", "OK");
            }
        }

    }
    
}

