using RestSharp;
using SMK;
using SMK.DataAccess;
using SMK.Support;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using System.Threading.Tasks;
using SMK.Model;
using SMK.View;

namespace SMK
{
    public class LoginPage : ContentPage
    {
        Entry username, password;

        /// <summary>
        /// Checks: if Empty Email or password, if valid Email, if valid Login. Password Entry is set as isPassword and the shown Passworddigits only contains zeros. Password will be stored as SHA512StringHash
        /// </summary>
        public LoginPage()
        {
            
            BackgroundColor = new Color(255, 255, 255, 1);

            var button = new Button { Text = "Login", BackgroundColor = Color.FromHex("006AB3") };
            button.Clicked += async (sender, e) =>
            {
                //Checks if the Entry for username or password is empty
                if (String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Text))
                {
                    await DisplayAlert("Abfragefehler", "E-Mail und Passwort bitte angeben", "Neue Eingabe");
                    return;
                }
                //Checks if the Email Entry is receiving a valid Email String
                if (!IsValidEmail(username.Text))
                {
                    await DisplayAlert("Ungültige E-Mail", "E-Mail ist in einem ungültigen Format angegeben worden", "Neue Eingabe");
                    return;
                }
                // Converts Password in SHA512StringHash
                string passwordHash = DependencyService.Get<IHash>().SHA512StringHash(password.Text);
                // Sets the Passworddigits to zero after receiving a letter
                password.Text = new string('0', password.Text.Length);
                User validUser = await IsValidLogin(new User(username.Text, passwordHash));
                // Checks with null parameter if user is valid
                if (null == validUser)
                {
                    await DisplayAlert("Ungültiger Login", "E-Mail oder Passwort falsch angegeben", "Neue Eingabe");
                }
                else
                {
                    App.Current.Login(validUser);
                    Navigation.PushModalAsync(new NavigationPage(new MainMenuPage(validUser)));
                }
            };
            var create = new Button { Text = "Account erstellen", BackgroundColor = Color.FromHex("E2001A") };
            create.Clicked += (sender, e) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Erstellen");
            };

            //Receives the CurrentUser and Set the Entry field as the Email from the last user with a valid Login
            username = new Entry { Text = App.Current.CurrentUser != null ? App.Current.CurrentUser.user_Email : "", BackgroundColor = Color.FromHex("3f3f3f") };
            password = new Entry { Text = "", BackgroundColor = Color.FromHex("3f3f3f"), IsPassword = true };

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

        /// <summary>
        /// Checks the if the inserted String is in a valid Email format
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

        /// <summary>
        /// Checks if the user is already in the Database with the ValidateUser REST API
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> IsValidLogin(User user)
        {
            return user;
            try
            {
                return await DataAccessHandler.DataAccess.ValidateUser(user);
            }
            catch (Exception)
            {
                await DisplayAlert("Verbindungsfehler", "Server ist nicht erreichtbar. Internetzugang aktiv?", "OK");
            }
            return null;
        }

    }
}

