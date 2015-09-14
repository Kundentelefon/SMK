using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using SMK.Model;

using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace SMK.View
{
    public class MainMenuPage : ContentPage
    {
        ICollection<Product> ProductCollection;
        public MainMenuPage()
        {
            //Toolbar
            ToolbarItem toolButton = new ToolbarItem
            {
                Name = "Hinzufügen",
                Order = ToolbarItemOrder.Primary,
                Icon = null,
                Command = new Command(() => Navigation.PushAsync(new AddProduktPage()))
            };
            //Ende Toolbar

            //View
            ProductCollection = new Collection<Product>();
            ProductCollection = initProducts();

            ScrollView scrollView = new ScrollView();
            StackLayout stackLayout = new StackLayout();

            foreach (Product product in ProductCollection)
            {
                TapGestureRecognizer gesture = new TapGestureRecognizer();

                Frame frame = new Frame
                {
                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        Children =
                        {
                            new Image
                            {
                                Source = product.thumbnail,
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Center
                            },

                            new StackLayout
                            {
                                Children =
                                {
                                    new Label
                                    {
                                        Text = product.Product_Name,
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalOptions = LayoutOptions.Center
                                    },

                                    new Label
                                    {
                                        Text = product.Product_Text,
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalOptions = LayoutOptions.Center
                                    }
                                }
                            }//ende stacklayout (innen)
                        }
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
            //View Ende

        }

        public ICollection<Product> initProducts()
        {
            ICollection<Product> Products = new Collection<Product>();

            //to do

            return Products;
        }
    }
}
