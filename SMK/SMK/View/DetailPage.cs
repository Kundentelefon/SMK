﻿using SMK.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using SMK.Support;

using Xamarin.Forms;

namespace SMK.View
{
    public class DetailPage : ContentPage
    {

        Product product;
        localFileSystem files;
        List<PContent> pContent;
        bool owned;
        IRotation rotHandler;
        StackLayout imageStack;
        StackLayout WebStack;
        StackLayout pdfStack;
        StackLayout convertedStack;
        StackLayout stackLayout;
        ScrollView imageScrollConverted;
        ScrollView imageScroll;
        ScrollView imageScrollWeb;
        ScrollView imageScrollPDF;
        List<PContent> contentPictureGalarie;
        List<PContent> contentPdf;
        List<PContent> contentHtml;
        List<PContent> contentVideo;
        List<PContent> contentConvertedPdf;
        Color color;
        /// <summary>
        /// Initialisiert ein Objekt der Klasse DetailPage
        /// </summary>
        /// <param name="ResourceProduct"></param>
        /// <param name="userPath"></param>
        public DetailPage(Product ResourceProduct, String userPath)
        {
            product = ResourceProduct;
            files = new localFileSystem();
            pContent = files.loadContentList(product, userPath);
            stackLayout = new StackLayout();
            imageStack = new StackLayout();
            WebStack = new StackLayout();
            pdfStack = new StackLayout();
            convertedStack = new StackLayout();
            imageScroll = new ScrollView();
            imageScrollWeb = new ScrollView();
            imageScrollPDF = new ScrollView();
            imageScrollConverted = new ScrollView();
            TapGestureRecognizer gesture = new TapGestureRecognizer();
            contentPictureGalarie = new List<PContent>();
            contentPdf = new List<PContent>();
            contentHtml = new List<PContent>();
            contentVideo = new List<PContent>();
            contentConvertedPdf = new List<PContent>();
            owned = false;
            color = Color.FromHex("E2001A");

            initContentLists();
      
            String image_path = DependencyService.Get<ISaveAndLoad>().PathCombine(DependencyService.Get<ISaveAndLoad>().Getpath(localFileSystem.productFolderLocation), product.product_Thumbnail);
            //(DependencyService.Get<ISaveAndLoad>().pathCombine(
            //(DependencyService.Get<ISaveAndLoad>().pathCombine(
            //DependencyService.Get<ISaveAndLoad>().getpath(userPath), product.product_ID.ToString()))+".png"));
            //String image_path = "/sdcard/data/0.png";

            stackLayout.Children.Add(
            new Image
            {
                Source = ImageSource.FromFile(image_path),
                //Source = ImageSource.FromFile(image_path)
                HeightRequest = 110
                
            }
            );//Child added: Image

            if (owned == true)
                color = Color.FromHex("006AB3");

            stackLayout.Children.Add(
                new ScrollView
                {
                    MinimumHeightRequest = 100,
                    Content = new Frame
              
                    {
                        OutlineColor = color,
                        BackgroundColor = color,
                        Content = new Label
                        {
                            Text = product.product_Text,
                            TextColor = Color.Black
                        }
                    }
                }
                );//Child Added: Klappentext


            initImageStack(userPath);
            //Image scrolllayout hinzugefügt
            initHTMLStack(userPath);
            //initPDFStack();
            initPictureStack(userPath);

            Content = stackLayout;
            BackgroundColor = Color.White;
            Padding = new Thickness(5, Device.OnPlatform(0, 15, 0), 5, 5);

            //rotHandler.disableRotation();
        }//ende Construktor

        /// <summary>
        /// Läd die Inhalte des Produktes auf der Detailseite
        /// </summary>
        public void initContentLists()
        {
            if ((pContent.Any()))
            {
                if ((pContent != null) && (pContent[0] != null))
                {
                    owned = true;
                    foreach (PContent content in pContent)
                    {
                        if (content.content_Kind == 0)
                        {
                            contentPictureGalarie.Add(content);
                        }
                        if (content.content_Kind == 2)
                        {
                            contentHtml.Add(content);
                        }
                        if (content.content_Kind == 4)
                        {
                            contentConvertedPdf.Add(content);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Erstellt eine Liste mit den Produktbildern des Produktes, welches die Detailseite behandelt
        /// </summary>
        /// <param name="userPath"></param>
        public void initImageStack(string userPath)
        {
            //Images des Produktes werden initalisiert
            imageScroll.Orientation = ScrollOrientation.Horizontal;
            imageStack.Orientation = StackOrientation.Horizontal;
            List<ContentPage> pages = new List<ContentPage>();
            TapGestureRecognizer reco = new TapGestureRecognizer();
            CarouselPage carousel = new CarouselPage();

            foreach (PContent content in contentPictureGalarie)
            {
                foreach (string imageSource in content.files)
                {
                    string source = DependencyService.Get<ISaveAndLoad>().PathCombine(
                        (DependencyService.Get<ISaveAndLoad>().PathCombine(
                            DependencyService.Get<ISaveAndLoad>().Getpath(userPath), "p" + content.content_ID)), imageSource);
                    Frame frame = new Frame
                    {
                        OutlineColor = color,
                        //IsClippedToBounds = true
                        Padding = 1,
                        Content = new Image
                        {
                            Source = ImageSource.FromFile(source),

                        }
                    };
                    imageStack.Children.Add(frame);
                    carousel.Children.Add(new ContentPage { Content = new Image { Source = ImageSource.FromFile(source) } });
                }
            }


            reco.Tapped += async (sender, e) =>
            {
                await Navigation.PushAsync(carousel);
            };

            if (owned == true)
                stackLayout.Children.Add(new StackLayout { Orientation = StackOrientation.Horizontal, Children = { new Label { Text = "Bilder", TextColor = Color.Black, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) }, new Label { Text = "(" + imageStack.Children.Count + ")", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), TextColor = Color.Black } } });


            imageStack.GestureRecognizers.Add(reco);
            imageScroll.Content = imageStack;
            stackLayout.Children.Add(imageScroll);
            //Image scrolllayout 

        }

        /// <summary>
        /// Erstellt eine Liste mit Webviewelementen des Produktes, welches von der Detailseite behandelt wird
        /// </summary>
        /// <param name="userPath"></param>
        public void initHTMLStack(string userPath)
        {
            imageScrollWeb.Orientation = ScrollOrientation.Horizontal;
            WebStack.Orientation = StackOrientation.Horizontal;
            List<ContentPage> pages = new List<ContentPage>();
            TapGestureRecognizer reco = new TapGestureRecognizer();
            foreach (PContent content in contentHtml)
            {
                //string source = thumbnail + content.content_ID.ToString() + ".png";

                string source = (DependencyService.Get<ISaveAndLoad>().PathCombine(
                        (DependencyService.Get<ISaveAndLoad>().PathCombine(
                            DependencyService.Get<ISaveAndLoad>().Getpath(userPath), "Thumbnail")),
                        content.content_ID.ToString())
                        + ".png");
                // if file doesn´t exist don`t create a frame
                //if (DependencyService.Get<ISaveAndLoad>().fileExistExact(source))
                //{
                Frame frame = new Frame
                {
                    OutlineColor = color,
                    Padding = 1,
                    Content = new Image
                    {
                        Source = ImageSource.FromFile(source)
                    }
                };
                //}


                WebStack.Children.Add(frame);
                reco.Tapped += async (sender, e) =>
                {
                    await Navigation.PushAsync(new LocalHtml(content, userPath));
                };
            }

            if (owned == true)
                stackLayout.Children.Add(new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    { new Label { Text = "WebView", TextColor = Color.Black, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) },
                        new Label { Text = "(" + WebStack.Children.Count + ")", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                            TextColor = Color.Black } }
                });


            WebStack.GestureRecognizers.Add(reco);
            imageScrollWeb.Content = WebStack;
            stackLayout.Children.Add(imageScrollWeb);
        }

        /// <summary>
        /// Erstellt eine Liste mit Lehrmaterial des Produktes, welches von der Detailseite behandelt wird
        /// </summary>
        /// <param name="userPath"></param>
        public void initPictureStack(string userPath)
        {
            imageScrollConverted.Orientation = ScrollOrientation.Horizontal;
            convertedStack.Orientation = StackOrientation.Horizontal;
            CarouselPage carousel;

            foreach (PContent content in contentConvertedPdf)
            {
                carousel = new CarouselPage();
                TapGestureRecognizer reco = new TapGestureRecognizer();
                string source_thumb = ((DependencyService.Get<ISaveAndLoad>().PathCombine(
                    (DependencyService.Get<ISaveAndLoad>().PathCombine(
                            DependencyService.Get<ISaveAndLoad>().Getpath(userPath), "Thumbnail")), content.content_ID.ToString()) + ".png"));
                string source_image;

                Frame frame = new Frame
                {
                    OutlineColor = color,
                    Padding = 1,
                    Content = new Image
                    {
                        Source = ImageSource.FromFile(source_thumb)
                    }
                };

                foreach (string source in content.files)
                {
                    //source_image = imagePfadContent + "p" + content.content_ID.ToString()+ "." + source;
                    source_image = (DependencyService.Get<ISaveAndLoad>().PathCombine(
                        (DependencyService.Get<ISaveAndLoad>().PathCombine(
                            DependencyService.Get<ISaveAndLoad>().Getpath(userPath), "p" + content.content_ID)), source));
                    carousel.Children.Add(new ContentPage { Content = new Image { Source = ImageSource.FromFile(source_image) } });
                }

                reco.Tapped += async (sender, e) =>
                {
                    await Navigation.PushAsync(carousel);
                };

                frame.GestureRecognizers.Add(reco);
                convertedStack.Children.Add(frame);
            }
            if (owned == true)
                stackLayout.Children.Add(new StackLayout { Orientation = StackOrientation.Horizontal, Children = { new Label { Text = "Lernmaterial", TextColor = Color.Black, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) }, new Label { Text = "(" + convertedStack.Children.Count + ")", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), TextColor = Color.Black } } });
            imageScrollConverted.Content = convertedStack;
            stackLayout.Children.Add(imageScrollConverted);
        }
    }
}
