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
        
        
        public MainMenuPage(User user)
        {
            ////localFileSystem sys = new localFileSystem();
            ////sys.createInitalFolders();
            //User user = new User();
            //user.user_Email = "test";
            //user.user_Password = "test";
            //DependencyService.Get<ISaveAndLoad>().saveModelXml("User",user);//test
            //Object test= DependencyService.Get<ISaveAndLoad>().loadUserXml("User");//test
            

            files = new localFileSystem();
            String userPath = files.AdjustPath(user.user_Email);
            files.createInitalFolders(userPath);
            //+userId
            //läd dummies 
            // entfernen bevor go live
            files.initaldummies(userPath);



            //Toolbar
            ToolbarItem toolButton = new ToolbarItem
            {
                Name = "Hinzufügen",
                Order = ToolbarItemOrder.Primary,
                Icon = null,
                Command = new Command(() => Navigation.PushModalAsync(new AddProduktPage()))
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
                    
                //Boolean test2 = DependencyService.Get<ISaveAndLoad>().fileExist("Products");
                //Boolean test=DependencyService.Get<ISaveAndLoad>().fileExistExact("sdcard/Android/data/SMK.Droid/files/Products");
                //DependencyService.Get<ISaveAndLoad>().getpath("Product/") + product.product_ID + "." + product.product_Thumbnail
                if (owned == true)
                   color = Color.FromHex("006AB3");
                //var test = DependencyService.Get<ISaveAndLoad>().pathCombine(DependencyService.Get<ISaveAndLoad>().pathCombine(DependencyService.Get<ISaveAndLoad>().getpath(userPath) , "thumbnails" ), product.product_ID + product.product_Thumbnail);
                //var test = DependencyService.Get<ISaveAndLoad>().pathCombine(DependencyService.Get<ISaveAndLoad>().getpath("Produkt"), product.product_ID + product.product_Thumbnail);

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
                                //Source=ImageSource.FromResource("SMK.zeug.Product."+product.product_ID.ToString()+".png"),
                                //Source = ImageSource.FromResource(DependencyService.Get<ISaveAndLoad>().getpath(userPath)+"thumbnails/"+product.product_ID+"."+product.product_Thumbnail),
                                Source= ImageSource.FromFile(DependencyService.Get<ISaveAndLoad>().pathCombine(DependencyService.Get<ISaveAndLoad>().getpath("Produkt"), product.product_ID + product.product_Thumbnail)),
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
                Navigation.PushAsync(new LoginPage());
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
