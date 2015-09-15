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
            files = new localFileSystem();
            //Toolbar
            ToolbarItem toolButton = new ToolbarItem
            {
                Name = "Hinzufügen",
                Order = ToolbarItemOrder.Primary,
                Icon = null,
                Command = new Command(() => Navigation.PushAsync(new AddProduktPage()))
            };
            //Navigation.ToolbarItems.Add(toolButton); 
            
            //Ende Toolbar

            //View
            ProductCollection = new Collection<Product>();
            ProductCollection = files.loadProductList();

            ScrollView scrollView = new ScrollView();
            StackLayout stackLayout = new StackLayout();

            foreach (Product product in ProductCollection)
            {
                TapGestureRecognizer gesture = new TapGestureRecognizer();

                Frame frame = new Frame
                {
                    
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,       
                    BackgroundColor = Color.Red,
                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Children =
                        {
                            new Image
                            {
                                Source = ImageSource.FromResource(product.product_Thumbnail),
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Center
                            },

                            //new StackLayout
                            //{
                            //    VerticalOptions = LayoutOptions.Center,
                            //    HorizontalOptions = LayoutOptions.Center,
                            //    Children =
                            //    {
                                    new Label
                                    {
                                        //Text = product.product_Name,
                                        FormattedText = product.product_Name,
                                        TextColor = Color.Black,
                                        VerticalOptions = LayoutOptions.Center,
                                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                        HorizontalOptions = LayoutOptions.Center
                                    },

                                //    new Label
                                //    {
                                //        Text = product.product_Text,
                                //        //VerticalOptions = LayoutOptions.Center,
                                //        //HorizontalOptions = LayoutOptions.End
                                //    }
                                //}
                            }//ende stacklayout (innen)
                       // }
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
            //View Ende

        }

      
    }
}
