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
                bool isDuplicated = await IsDuplicatedAsync(username.Text);
                //Checks if the User is already in the database 
                if (isDuplicated)
                {
                    DisplayAlert("Account bereits vorhanden!", "Anderen account angeben", "OK");
                }
                //Checks if the User is in a Valid Email Format
                else if (!LoginPage.IsValidEmail(username.Text))
                {
                    DisplayAlert("Ungültige E-Mail", "E-Mail ist in einem ungültigen Format angegeben worden", "Neue Eingabe");
                }
                // Checks if password1 equals password2
                else if (!(password1.Text.Equals(password2.Text)))
                {
                    DisplayAlert("Passwort wiederholen!", "Passwörter sind nicht identisch", "OK");
                }

                // Adds the user to the database with hashed password
                else
                {
                    AddUser(username.Text, password1.Text);
                    DisplayAlert("Account erstellt!", "Neuer Account wurde erstellt", "OK");
                    Navigation.PushModalAsync(new MainMenuPage());
                }
            };
            var cancel = new Button { Text = "Zurück", BackgroundColor = Color.FromHex("006AB3") };
            cancel.Clicked += (sender, e) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Login");
            };

            username = new Entry { Text = "",};
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
        /// Async Connects with the Server with URI (Emulator Standard is http://10.0.2.2) and receives the User Datainformation with a REST Web Request from a .php GET Method Request. Checks if the user if null. Returns true if the user exist and returns null if the user dont exist
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static async Task<bool> IsDuplicatedAsync(string strIn)
        {
            bool duplicated = false;
            try
            {
                var client = new RestClient("http://10.0.2.2");
                var request = new RestRequest("getUser.php", Method.GET);
                request.AddParameter("user_Email", strIn);

                var response = await client.ExecuteGetTaskAsync(request);

                //Because of the isValidEmail Method, a Account with the name "0 results" can never happen
                return !response.Content.ToString().Equals("0 results");
            }

            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }

        }

        /// <summary>
        /// Connects with the Server with URI (Emulator Standard is http://10.0.2.2) send with a REST Web Request from a .php POST Method Request. Creates on the Database a new User with a new ID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void AddUser(string username, string password)
        {
            try
            {
                var client = new RestClient("http://10.0.2.2");
                    var req = new RestRequest("createUser.php", Method.POST);
                    req.AddParameter("user_Email", username);
                    req.AddParameter("user_Password", DependencyService.Get<IHash>().SHA512StringHash(password));
                    client.Execute(req);
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

    }
}

