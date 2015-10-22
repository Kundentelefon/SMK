using System;
using Xamarin.Forms;

namespace SMK
{
    public class LoginModalPage : CarouselPage
    {
        /// <summary>
        /// Needed for the swipe event Login/Create View
        /// </summary>
        ContentPage login, create;
        public LoginModalPage()
        {
            login = new LoginPage();
            create = new CreateAccountPage();
            this.Children.Add(login);
            this.Children.Add(create);

            MessagingCenter.Subscribe<ContentPage>(this, "Login", (sender) =>
            {
                this.SelectedItem = login;
            });
            MessagingCenter.Subscribe<ContentPage>(this, "Erstellen", (sender) =>
            {
                this.SelectedItem = create;
            });
        }
    }
}
