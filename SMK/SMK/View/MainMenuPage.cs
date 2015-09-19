using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using SMK.Model;
using SMK.Support;

using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace SMK.View
{
    public class MainMenuPage : ContentPage
    {
        ICollection<Product> ProductCollection;
        localFileSystem files;
        
        public MainMenuPage()
        {
            ////localFileSystem sys = new localFileSystem();
            ////sys.createInitalFolders();
            //User user = new User();
            //user.user_Email = "test";
            //user.user_Password = "test";
            //DependencyService.Get<ISaveAndLoad>().saveModelXml("User",user);//test
            //Object test= DependencyService.Get<ISaveAndLoad>().loadUserXml("User");//test


            files = new localFileSystem();
            //läd dummies 
            // entfernen bevor go live
            files.initaldummies();
            //files.createInitalFolders();//+userId
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
            List<PContent> PcontentCollection = files.loadContentList();

            ScrollView scrollView = new ScrollView();
            StackLayout stackLayout = new StackLayout();
            


            foreach (Product product in ProductCollection)
            {
                TapGestureRecognizer gesture = new TapGestureRecognizer();
                bool owned = files.hasContent(product, PcontentCollection);
                Color color = Color.FromHex("E2001A");

                //Boolean test2 = DependencyService.Get<ISaveAndLoad>().fileExist("Products");
                //Boolean test=DependencyService.Get<ISaveAndLoad>().fileExistExact("sdcard/Android/data/SMK.Droid/files/Products");
                //DependencyService.Get<ISaveAndLoad>().getpath("Product/") + product.product_ID + "." + product.product_Thumbnail
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
                                //"SMK.FischerTechnik.Product.0.png"
                                Source=ImageSource.FromResource("SMK.zeug.Product."+product.product_ID.ToString()+".png"),
                                //Source = ImageSource.FromResource(DependencyService.Get<ISaveAndLoad>().getpath()+User.Email+"Product/"+product.product_ID+"."+product.product_Thumbnail),
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Center
                            },

                                    new Label
                                    {
                                        //Text = product.product_Name,
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
                    await Navigation.PushAsync(new DetailPage(product));
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
            //Command logoutCommand = new Command(() => { Navigation.PushAsync(new LoginPage(ILoginManager ilm));
            //funktion für user löschen einfügen
            //                                            });
            
                ToolbarItem logoutButton = new ToolbarItem
                {
                    Text = "Logout",
                    Order = ToolbarItemOrder.Primary,
                    
                   // Command = logoutCommand
                };
                this.ToolbarItems.Add(logoutButton);

            }
    }
}
