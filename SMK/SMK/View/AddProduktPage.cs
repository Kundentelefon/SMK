using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using SMK.DataAccess;
using SMK.Model;
using Xamarin.Forms;

namespace SMK.View
{
    public class AddProduktPage : ContentPage
    {
        Color color;
        Button button;
        Entry entry;
        public AddProduktPage()
        {
            color = Color.FromHex("006AB3");
            entry = new Entry { BackgroundColor = Color.Gray, TextColor = Color.White, Placeholder = "Produktcode" };
            button = new Button
            {
                Text = "Produkt aktivieren",
                BackgroundColor = color
            };

            BackgroundColor = Color.White;
            Content = new StackLayout
            {
                Padding = new Thickness(5, Device.OnPlatform(5, 20, 5), 5, 5),
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.White,
                Children = {
                   entry,
                   button
                }
            };
            button.Clicked += (sender, e) => { AddProduct(entry.Text); };
        }

        public async void AddProduct(string productCode)
        {
            if (!await DataAccessHandler.DataAccess.IsValidKey(productCode))
            {
                //TODO: Alert
                return;
            }
            Product product = await DataAccessHandler.DataAccess.GetProductByKey(productCode);
            DataAccessHandler.DataAccess.AddProductToUser(product.product_ID, App.Current.CurrentUser);
        }
    }
}
