using SMK;
using System;
using Xamarin.Forms;

namespace LoginPattern
{
    public class LoginPage : ContentPage
    {
        Entry username, password;
        public LoginPage(ILoginManager ilm)
        {
            BackgroundColor = new Color(255, 255, 255, 1);

            var button = new Button { Text = "Login", BackgroundColor = Color.FromHex("006AB3") };
            button.Clicked += (sender, e) => {
                if (String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Text))
                {
                    DisplayAlert("Abfragefehler", "E-Mail und Passwort bitte angeben", "Neue Eingabe");
                }
                else
                {
                    // saves the login status
                    App.Current.Properties["IsLoggedIn"] = true;
                    ilm.ShowMainPage();
                }
            };
            var create = new Button { Text = "Account erstellen", BackgroundColor = Color.FromHex("E2001A") };
            create.Clicked += (sender, e) => {
                MessagingCenter.Send<ContentPage>(this, "Erstellen");
            };

            username = new Entry { Text = "", BackgroundColor = Color.FromHex("3f3f3f") };
            password = new Entry { Text = "", BackgroundColor = Color.FromHex("3f3f3f") };


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
    }
}

