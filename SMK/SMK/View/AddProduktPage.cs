using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                //für testzwecke auskommentiert
                //if (!await DataAccessHandler.DataAccess.IsValidKey(productCode))
                //{
                //    await DisplayAlert("Produktcode ungültiger", "Es wurde ein ungültiger Produktcode eingegeben. Bitte anderen code eingeben.", "Neue Eingabe");
                //    return;
                //}
                localFileSystem file = new localFileSystem();
                Debug.WriteLine("getpath produkte: " + DependencyService.Get<ISaveAndLoad>().fileExist(@"User"));
                String userPath = file.AdjustPath(file.getUser().user_Email);
                DataAccessHandler accessHandler = new DataAccessHandler();
                string serverAdress = accessHandler.ServerAdress;

                Product product = await DataAccessHandler.DataAccess.GetProductByKey(productCode);
                DataAccessHandler.DataAccess.AddProductToUser(product.product_ID, App.Current.CurrentUser);
                List<Product> newUserProducts = file.loadProductList();
                //inserts the new Product in Productlist
                
                List<PContent> listPContents = await DataAccessHandler.DataAccess.GetPContent(product.product_ID);
                List<PContent> newlistPContents = file.loadContentList(userPath);

                IFtpClient client = DependencyService.Get<IFtpClient>();



                //Download Thumbnail in Produkte Folder
                client.DownloadFile(@"Thumbnail/" + product.product_Thumbnail,
                DependencyService.Get<ISaveAndLoad>().getpath(@"Produkte/") +product.product_Thumbnail, serverAdress, accessHandler.FtpName,
                accessHandler.FtpPassword);
                //Download Thumbnail in userName / thumbnail Folder
                client.DownloadFile(@"Thumbnail/" + product.product_Thumbnail,
                DependencyService.Get<ISaveAndLoad>().getpath(file.getUser().user_Email + @"/Thumbnail/") + product.product_Thumbnail, serverAdress, accessHandler.FtpName,
                accessHandler.FtpPassword);

                foreach (var pcontent in listPContents)
                {
                    if (pcontent.content_ID == 0) break;
                    List<string> contentPath =
                        await DataAccessHandler.DataAccess.GetFileServerPath(pcontent.content_ID);
                    newlistPContents.Add(pcontent);

                    //creates a new p folder if not exists
                    DependencyService.Get<ISaveAndLoad>().createOrdner(DependencyService.Get<ISaveAndLoad>().pathCombine(
                    DependencyService.Get<ISaveAndLoad>().getpath(userPath), "p" + pcontent.content_ID));

                    foreach (var path in contentPath)
                    {
                        if (pcontent.content_ID == 0) break;
                        DependencyService.Get<ISaveAndLoad>().createOrdner(userPath + @"/p" + pcontent.content_ID);
                        client.DownloadFile(path,
                            DependencyService.Get<ISaveAndLoad>().getpath(file.getUser().user_Email) + @"/p" +
                            pcontent.content_ID + @"/" + pcontent.content_Title, serverAdress, accessHandler.FtpName,
                            accessHandler.FtpPassword);
                    }
                }

                newUserProducts.Add(product);
                file.saveModelsLocal(userPath, newUserProducts, newlistPContents);
                DataAccessHandler.DataAccess.AddProductToUser(product.product_ID, file.getUser());
                DataAccessHandler.DataAccess.SetProductKeyInvalid(productCode);
                await DisplayAlert("Produkt aktiviert!", "Das Produkt wurde erfolgreiche aktiviert!", "OK");
                await Navigation.PushModalAsync(new NavigationPage(new MainMenuPage(file.getUser())));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Fehler beim hinzufügen von Produkt: " + e);
                await DisplayAlert("Fehler beim Downloaden", "Es gab einen Fehler beim Downloaden, bitte erneut versuchen", "OK");
            }
        }
    }
}
