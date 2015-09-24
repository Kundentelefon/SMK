using RestSharp;
using SMK;
using SMK.Database;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SMK
{
    public class LoginPage : ContentPage
    {
        Entry username, password;
        
        public LoginPage(ILoginManager ilm)
        {
            
            BackgroundColor = new Color(255, 255, 255, 1);

            var button = new Button { Text = "Login", BackgroundColor = Color.FromHex("006AB3") };
            button.Clicked += (sender, e) =>
            {
                if (String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Text))
                {
                    DisplayAlert("Abfragefehler", "E-Mail und Passwort bitte angeben", "Neue Eingabe");
                }
                else if (!IsValidEmail(username.Text))
                {
                    DisplayAlert("Ungültige E-Mail", "E-Mail ist in einem ungültigen Format angegeben worden", "Neue Eingabe");
                }
                else if (!IsValidLogin(username.Text, password.Text))
                {
                    DisplayAlert("Ungültiger Login", "E-Mail oder Passwort falsch angegeben", "Neue Eingabe");
                }
                else
                {
                    //rememberLogin
                    rememberLogin(username.Text, username.Text);
                    // saves the login status
                    App.Current.Properties["IsLoggedIn"] = true;
                    ilm.ShowMainPage();
                }
            };
            var create = new Button { Text = "Account erstellen", BackgroundColor = Color.FromHex("E2001A") };
            create.Clicked += (sender, e) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Erstellen");
            };

            username = new Entry { Text = "dummy@rofl.lol", BackgroundColor = Color.FromHex("3f3f3f") };
            password = new Entry { Text = "bla", BackgroundColor = Color.FromHex("3f3f3f"), IsPassword = true };
            


            Content = new StackLayout
            {
                Padding = new Thickness(10, 40, 10, 10),
                //VerticalOptions = LayoutOptions.Center,
                Children = {
                    new Label { Text = "Login", TextColor = Color.FromHex("006AB3"), FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) },
                    new Label { Text = "E-Mail", TextColor = Color.FromHex("000000")},
                    username,
                    new Label { Text = "Passwort", TextColor = Color.FromHex("000000")},
                    password,
                    button, create
                }
            };
        }


        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

        public static bool IsValidLogin(string username, string password)
        {
            return true;
            String passwordHash = DependencyService.Get<Dataaccess>().PasswordHash(password);
            try
            {
                var client = new RestClient("http://10.0.2.2");
                var request = new RestRequest("getUser.php", Method.GET);
                request.AddParameter("user_Email", username);

                var response = client.Execute(request);

                // TODO: Display alert
                //if (response.ErrorException != null)
                //{
                //    DisplayAlert("Keine Verbindung zum Server", "Keine Rückmeldung vom Server erhalten.", "OK");
                //}

                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<user>>(response.Content);

                return model.Count > 0 && passwordHash.Equals(model[0].Password);
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public void rememberLogin(string username, string password)
        {
            //String passwordHash = DependencyService.Get<Dataaccess>().PasswordHash(password);
            //TODO: save with json


        }
    }
}

