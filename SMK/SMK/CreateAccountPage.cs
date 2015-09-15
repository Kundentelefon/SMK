using System;
using Xamarin.Forms;

namespace LoginPattern
{
    public class CreateAccountPage : ContentPage
    {
        public CreateAccountPage(ILoginManager ilm)
        {
            var button = new Button { Text = "Account erstellen", BackgroundColor = Color.FromHex("E2001A") };
            button.Clicked += (sender, e) => {
                DisplayAlert("Account erstellt!", "Login hier hinzufügen", "OK");
                ilm.ShowMainPage();
            };
            var cancel = new Button { Text = "Zurück", BackgroundColor = Color.FromHex("006AB3") };
            cancel.Clicked += (sender, e) => {
                MessagingCenter.Send<ContentPage>(this, "Login");
            };
            Content = new StackLayout
            {
                Padding = new Thickness(10, 40, 10, 10),
                Children = {
                    new Label { Text = "Account erstellen", Font = Font.SystemFontOfSize(NamedSize.Large) },
                    new Label { Text = "Gib deine E-Mail Addresse an" },
                    new Entry { Text = "" },
                    new Label { Text = "Passwort" },
                    new Entry { Text = "" },
                    new Label { Text = "Passwort wiederholen" },
                    new Entry { Text = "" },
                    button, cancel
                }
            };
        }
    }
}

