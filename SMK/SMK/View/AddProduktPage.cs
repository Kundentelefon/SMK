using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using SMK.DataAccess;
using SMK.Model;
using Xamarin.Forms;
using SMK.Support;

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
            try
            {
                if (!await DataAccessHandler.DataAccess.IsValidKey(productCode))
                {
                    await DisplayAlert("Produktcode ungültiger", "Es wurde ein ungültiger Produktcode eingegeben. Bitte anderen code eingeben.", "Neue Eingabe");
                    return;
                }
                localFileSystem files = new localFileSystem();
                String userPath = files.AdjustPath(App.Current.CurrentUser.user_Email);
                DataAccessHandler accessHandler = new DataAccessHandler();
                string serverAdress = accessHandler.ServerAdress;

                Product product = await DataAccessHandler.DataAccess.GetProductByKey(productCode);
                DataAccessHandler.DataAccess.AddProductToUser(product.product_ID, App.Current.CurrentUser);
                List<Product> newUserProducts = files.loadProductList();
                //inserts the new Product in Productlist
                
                List<PContent> listPContents = await DataAccessHandler.DataAccess.GetPContent(product.product_ID);
                List<PContent> newlistPContents = files.loadContentList(userPath);

                IFtpClient client = DependencyService.Get<IFtpClient>();

                string thumbnailpath = await DataAccessHandler.DataAccess.getThumbnailPath(product.product_ID);

                //Download thumbnail
                client.DownloadFile(thumbnailpath,
                DependencyService.Get<ISaveAndLoad>().getpath(App.Current.CurrentUser.user_Email + @"\Thumbnail"), serverAdress, accessHandler.FtpName,
                accessHandler.FtpPassword);
                foreach (var pcontent in listPContents)
                {
                    client.DownloadFile(pcontent.content_path,
                        DependencyService.Get<ISaveAndLoad>().getpath(App.Current.CurrentUser.user_Email) + @"\p" + pcontent.content_Kind, serverAdress, accessHandler.FtpName,
                        accessHandler.FtpPassword);
                    newlistPContents.Add(pcontent);
                }
                newUserProducts.Add(product);
                files.saveModelsLocal(userPath, newUserProducts, newlistPContents);
                DataAccessHandler.DataAccess.SetProductKeyInvalid(productCode);
                await DisplayAlert("Produkt aktiviert!", "Das Produkt wurde erfolgreiche aktiviert!", "OK");
            }
            catch (Exception e)
            {
                await DisplayAlert("Fehler beim Downloaden", "Es gab einen Fehler beim Downloaden, bitte erneut versuchen", "OK");
            }
        }
    }
}
