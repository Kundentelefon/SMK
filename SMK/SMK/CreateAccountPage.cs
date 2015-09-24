//using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using SMK.Database;

using Xamarin.Forms;
using RestSharp;
using System.Threading.Tasks;

namespace SMK
{
    public class CreateAccountPage : ContentPage
    {
        Entry username, password1, password2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ilm"></param>
        /// <param name="task"></param>
        public CreateAccountPage(ILoginManager ilm)
        {
            
            //bool isDuplicated = true;

            var button = new Button { Text = "Account erstellen", BackgroundColor = Color.FromHex("E2001A") };
            button.Clicked += async (sender, e) =>
            {
                bool isDuplicated = await IsDuplicatedAsync(username.Text);
                if (isDuplicated)
                {
                    DisplayAlert("Account bereits vorhanden!", "Anderen account angeben", "OK");
                }
                else if (!LoginPage.IsValidEmail(username.Text))
                {
                    DisplayAlert("Ungültige E-Mail", "E-Mail ist in einem ungültigen Format angegeben worden", "Neue Eingabe");
                }
                else if (!(password1.Text.Equals(password2.Text)))
                {
                    DisplayAlert("Passwort wiederholen!", "Passwörter sind nicht identisch", "OK");
                }

                else
                {
                    AddUser(username.Text, password1.Text);
                    DisplayAlert("Account erstellt!", "Neuer Account wurde erstellt", "OK");
                    ilm.ShowMainPage();
                }
            };
            var cancel = new Button { Text = "Zurück", BackgroundColor = Color.FromHex("006AB3") };
            cancel.Clicked += (sender, e) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Login");
            };

            username = new Entry { Text = "", Placeholder = "Username ..." };
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


        public static async Task<bool> IsDuplicatedAsync(string strIn)
        {
            bool duplicated = false;
            try
            {
                
                var client = new RestClient("http://10.0.2.2");
                var request = new RestRequest("getUser.php", Method.GET);
                request.AddParameter("user_Email", strIn);
                System.Diagnostics.Debug.WriteLine("async start2");
                System.Diagnostics.Debug.WriteLine(request);

                var response = await client.ExecuteGetTaskAsync(request);
                //var response = client.Execute(request);

                System.Diagnostics.Debug.WriteLine("async end2");

                //Because of the isValidEmail Method, a Account with the name "0 results" can never happen
                return !response.Content.ToString().Equals("0 results");
            }

            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }

        }

        public void AddUser(string username, string password)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("here!");
                var client = new RestClient("http://10.0.2.2");
                    var req = new RestRequest("createUser.php", Method.POST);
                    req.AddParameter("user_Email", username);
                    req.AddParameter("user_Password", password);
                    client.Execute(req);
                
            }

            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

    }
}

