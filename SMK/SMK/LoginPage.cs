using RestSharp;
using SMK;
using SMK.Database;
using SMK.Support;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using System.Threading.Tasks;
using SMK.Model;

namespace SMK
{
    public class LoginPage : ContentPage
    {
        Entry username, password;
        
        public LoginPage(ILoginManager ilm)
        {
            
            BackgroundColor = new Color(255, 255, 255, 1);

            var button = new Button { Text = "Login", BackgroundColor = Color.FromHex("006AB3") };
            button.Clicked += async (sender, e) =>
            {
                if (String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Text))
                {
                    await DisplayAlert("Abfragefehler", "E-Mail und Passwort bitte angeben", "Neue Eingabe");
                    return;
                }
                if (!IsValidEmail(username.Text))
                {
                    await DisplayAlert("Ungültige E-Mail", "E-Mail ist in einem ungültigen Format angegeben worden", "Neue Eingabe");
                    return;
                }
                string passwordHash = DependencyService.Get<IHash>().SHA512StringHash(password.Text);
                password.Text = new string('0', password.Text.Length);
                if (!await IsValidLogin(username.Text, passwordHash))
                {
                    await DisplayAlert("Ungültiger Login", "E-Mail oder Passwort falsch angegeben", "Neue Eingabe");
                }
                else
                {
                    //rememberLogin
                    rememberLogin(username.Text, passwordHash);
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

        public async Task<bool> IsValidLogin(string username, string passwordHash)
        {
            return true;
            try
            {
                var client = new RestClient("http://10.0.2.2");
                var request = new RestRequest("getUser.php", Method.GET);
                request.AddParameter("user_Email", username);

                IRestResponse response = await client.ExecuteGetTaskAsync(request);

                // TODO: Display alert
                if (response.ErrorException != null)
                {
                    await DisplayAlert("Keine Verbindung zum Server", "Keine Rückmeldung vom Server erhalten.", "OK");
                    return false;
                }

                if (response.Content == "0 results")
                    return false;

                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(response.Content);

                return model.Count > 0 && passwordHash.Equals(model[0].user_Password);
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public async void rememberLogin(string username, string passwordHash)
        {
            await Task.Run(() => {
                User user = new User();
                user.user_Email = username;
                user.user_Password = passwordHash;

                ISaveAndLoad saveAndLoad = DependencyService.Get<ISaveAndLoad>();

                string filePath = saveAndLoad.getpath("loginData.xml");

                saveAndLoad.saveUserXml(filePath, user);
            });
        }
    }
}

