﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using SMK.Model;
using SMK.Support;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;
using SMK.DataAccess;

namespace SMK.View
{
    public class MainMenuPage : ContentPage
    {
        ICollection<Product> ProductCollection;
        localFileSystem files;
        
        
        public MainMenuPage(User user)
        {
            ////localFileSystem sys = new localFileSystem();
            ////sys.createInitalFolders();
            //User user = new User();
            //user.user_Email = "test";
            //user.user_Password = "test";
            //DependencyService.Get<ISaveAndLoad>().saveModelXml("User",user);//test
            //Object test= DependencyService.Get<ISaveAndLoad>().loadUserXml("User");//test

            DataAccessHandler accessHandler = new DataAccessHandler();
            string serverAdress = accessHandler.ServerAdress;
            files = new localFileSystem();
            String userPath = files.AdjustPath(user.user_Email);
            files.createInitalFolders(userPath);
            //Debug.WriteLine("Test1 Downloade File start");
            Debug.WriteLine("Folder exist1 " + DependencyService.Get<ISaveAndLoad>().fileExist("User"));
            //Debug.WriteLine("Folder exist2 " + DependencyService.Get<ISaveAndLoad>().fileExistExact(user.user_Email + @"/FISCHERTECHNIK_Logo.JPG"));
            //Debug.WriteLine("File getpath " + DependencyService.Get<ISaveAndLoad>().getpath(user.user_Email));

            //DependencyService.Get<ISaveAndLoad>()
            //    .createOrdner(DependencyService.Get<ISaveAndLoad>().getpath("testa1@web.de"));
            //string folderpath = DependencyService.Get<ISaveAndLoad>().getpath("testa1@web.de");
            //IFtpClient client = DependencyService.Get<IFtpClient>();
            //client.DownloadFile("FISCHERTECHNIK_Logo.JPG", folderpath + @"/FISCHERTECHNIK_Logo.JPG", serverAdress, accessHandler.FtpName, accessHandler.FtpPassword);

            Debug.WriteLine("fileexist2: " + DependencyService.Get<ISaveAndLoad>().fileExist("testa1@web.de" + @"/User"));

            //IFtpClient client = DependencyService.Get<IFtpClient>();
            //client.DownloadDirectoryAsync("zeug/PContent", DependencyService.Get<ISaveAndLoad>().getpath(user.user_Email), serverAdress, "SMKFTPUser", "");
            //Debug.WriteLine("Test1 Downloade File end");

            //+userId
            //läd dummies 
            // entfernen bevor go live
                        //files.initaldummies(userPath);



            //Toolbar
            ToolbarItem toolButton = new ToolbarItem
            {
                Name = "Hinzufügen",
                Order = ToolbarItemOrder.Primary,
                Icon = null,
                Command = new Command(() => Navigation.PushAsync(new AddProduktPage()))
            };
            this.ToolbarItems.Add(toolButton);

            initLogout();
            
            //Ende Toolbar

            //View
            ProductCollection = new Collection<Product>();
            ProductCollection = files.loadProductList();
            List<PContent> PcontentCollection = files.loadContentList(userPath);

            ScrollView scrollView = new ScrollView();
            StackLayout stackLayout = new StackLayout();
            


            foreach (Product product in ProductCollection)
            {
                TapGestureRecognizer gesture = new TapGestureRecognizer();
                bool owned = files.hasContent(product, PcontentCollection);
                Color color = Color.FromHex("E2001A");
                DetailPage detailPage = new DetailPage(product, userPath);//nicht schön , einmal pcontent lesen und zwischenspeichern

                if (owned == true)
                   color = Color.FromHex("006AB3");
                Frame frame = new Frame
                {                    
                    BackgroundColor = color,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,       
                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Children =
                        {
                            new Image
                            {
                                Source = ImageSource.FromFile(DependencyService.Get<ISaveAndLoad>().pathCombine(DependencyService.Get<ISaveAndLoad>().getpath(localFileSystem.productFolderLocation), product.product_ID + product.product_Thumbnail)),
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Center
                            },

                                    new Label
                                    {
                                        FormattedText = product.product_Name,
                                        TextColor = Color.Black,
                                        VerticalOptions = LayoutOptions.Center,
                                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                        HorizontalOptions = LayoutOptions.Center
                                    },
                            }//ende stacklayout (innen)
                    }//ende stacklayout
                };//frame ende

                stackLayout.Children.Add(frame);
                gesture.Tapped += async (sender, e) =>
                {
                    await Navigation.PushAsync(detailPage);
                };
                frame.GestureRecognizers.Add(gesture);
            }
                        
            scrollView.Content = stackLayout;
            Content = scrollView;
            BackgroundColor = Color.White;
            Padding = new Thickness(5, Device.OnPlatform(0, 15, 0), 5, 5);
            //View Ende

        }

        public void initLogout()
        {
            Command logoutCommand = new Command(() =>
            {
                App.Current.Logout();
                Navigation.PushAsync(new LoginModalPage());
            });
            ToolbarItem logoutButton = new ToolbarItem
            {
                Text = "Logout",
                Order = ToolbarItemOrder.Primary,
                Command = logoutCommand
            };
            this.ToolbarItems.Add(logoutButton);

        }
    }
}
