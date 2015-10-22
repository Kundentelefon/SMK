using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        /// <summary>
        /// Konstruktor der AddProductPage, welcher eine Instanz der Seite erstellt
        /// </summary>
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
                localFileSystem file = new localFileSystem();
                String userPath = file.AdjustPath(file.GetUser().user_Email);

                //Gets the Serveradress 
                DataAccessHandler accessHandler = new DataAccessHandler();
                string serverAdress = accessHandler.ServerAdress;

                IFtpClient client = DependencyService.Get<IFtpClient>();
                Product product = await DataAccessHandler.DataAccess.GetProductByKey(productCode);
                //Download Thumbnail in Produkte Folder
                //client.DownloadFile(@"Produkte/" + product.product_Thumbnail,
                //DependencyService.Get<ISaveAndLoad>().Getpath(@"Produkte/") + product.product_Thumbnail, serverAdress, accessHandler.FtpName,
                //accessHandler.FtpPassword);
                //Download Thumbnail in userName / thumbnail Folder
                //client.DownloadFile(@"Thumbnail/" + product.product_Thumbnail,
                //DependencyService.Get<ISaveAndLoad>().Getpath(file.GetUser().user_Email + @"/Thumbnail/") + product.product_Thumbnail, serverAdress, accessHandler.FtpName,
                //accessHandler.FtpPassword);

                //Loads the List<PContent> of the Product from the server
                List<PContent> listPContents = await DataAccessHandler.DataAccess.GetPContent(product.product_ID);
                //Loads the List<PContent> of the Product from the the User-Folder
                List<PContent> newlistPContents = file.loadContentList(userPath);

                foreach (PContent pcontent in listPContents)
                {                    
                    //stops if the pcontent is empty
                    if (pcontent.content_ID == 0) break;
                    //adds new Pcontent if necessary
                    while (newlistPContents.Count <= pcontent.content_ID)
                    {
                        newlistPContents.Add(null);
                    }
                    //updates Pcontent
                    newlistPContents[pcontent.content_ID] = pcontent;
                    if (pcontent.content_Kind!=0) {
                        client.DownloadFile(@"Thumbnail/" + pcontent.content_ID + ".png",
                        DependencyService.Get<ISaveAndLoad>().Getpath(file.GetUser().user_Email + @"/Thumbnail/") + pcontent.content_ID + ".png", serverAdress, accessHandler.FtpName,
                        accessHandler.FtpPassword);
                    }

                    List<string> contentPath =
                        await DataAccessHandler.DataAccess.GetFileServerPath(pcontent.content_ID);                   

                    //creates a new p folder for the content_Kind if not exists
                    DependencyService.Get<ISaveAndLoad>().CreateFolder(DependencyService.Get<ISaveAndLoad>().PathCombine(
                    DependencyService.Get<ISaveAndLoad>().Getpath(userPath), "p" + pcontent.content_ID));

                    foreach (var path in contentPath)
                    {
                        if (pcontent.content_ID == 0) break;
                        //Download for each PContent its Files
                        DependencyService.Get<ISaveAndLoad>().CreateFolder(userPath + @"/p" + pcontent.content_ID);
                        client.DownloadFile(path,
                            DependencyService.Get<ISaveAndLoad>().Getpath(file.GetUser().user_Email) + @"/p" +
                            pcontent.content_ID + @"/" + Path.GetFileName(path), serverAdress, accessHandler.FtpName,
                            accessHandler.FtpPassword);
                    }
                }
                //Adds the Product to the Product-File from the User
                List<Product> newUserProducts = file.LoadProductList();
                //newUserProducts.Add(product);
                file.SaveModelsLocal(userPath, newUserProducts, newlistPContents);
                //Adds the Product to the User in the Database
                DataAccessHandler.DataAccess.AddProductToUser(product.product_ID, file.GetUser());
                DataAccessHandler.DataAccess.SetProductKeyInvalid(productCode);
                await DisplayAlert("Produkt aktiviert!", "Das Produkt wurde erfolgreiche aktiviert!", "OK");
                await Navigation.PushModalAsync(new NavigationPage(new MainMenuPage(file.GetUser())));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Fehler beim hinzufügen von Produkt: " + e);
                await DisplayAlert("Fehler beim Downloaden", "Es gab einen Fehler beim Downloaden, bitte erneut versuchen", "OK");
            }
        }
    }
}
