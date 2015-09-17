﻿using SMK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using SMK.Support;

using Xamarin.Forms;

namespace SMK.View
{
    public class DetailPage : ContentPage
    {
        String imagePfadContent = "SMK.FischerTechnik.PContent.";
        String imagePfadProduct = "SMK.FischerTechnik.Files.";
        Product product;
        localFileSystem files;
        List<PContent> pContent;
        bool owned;
        List<PContent> contentPictureGalarie;
        List<PContent> contentPdf;
        List<PContent> contentHtml;
        List<PContent> contentVideo;

        public DetailPage(Product ResourceProduct)
        {
            product = ResourceProduct;
            files = new localFileSystem();
            pContent = files.loadContentList(product);
            StackLayout stackLayout = new StackLayout();
            StackLayout imageStack = new StackLayout();
            ScrollView imageScroll = new ScrollView();
            TapGestureRecognizer gesture = new TapGestureRecognizer();
            contentPictureGalarie = new List<PContent>() ;
            contentPdf = new List<PContent>();
            contentHtml = new List<PContent>();
            contentVideo = new List<PContent>();
            owned = false;
            Color color = Color.FromHex("E2001A");

            initContentLists();
           
            String image_path = imagePfadProduct + product.product_ID.ToString() + ".png";
            stackLayout.Children.Add(
            new Image
            {
                Source = ImageSource.FromResource(image_path)
            }
            );//Child added: Image

            if(owned == true)
              color = Color.FromHex("006AB3");

            stackLayout.Children.Add(
                new ScrollView
                {
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

            //Images des Produktes werden initalisiert
            imageScroll.Orientation = ScrollOrientation.Horizontal;
            imageStack.Orientation = StackOrientation.Horizontal;
            List<ContentPage> pages = new List<ContentPage>();
            TapGestureRecognizer reco = new TapGestureRecognizer();
            CarouselPage carousel = new CarouselPage();

            foreach (PContent content in contentPictureGalarie)
            {
                foreach (string image_source in content.content_FileNames)
                {
                    string source = imagePfadContent + image_source;
                  
                    Frame frame = new Frame
                    {
                        OutlineColor = color,
                        Content = new Image
                        {
                            Source = ImageSource.FromResource(source)
                        }
                    };
                    imageStack.Children.Add(frame);
                    carousel.Children.Add(new ContentPage { Content = new Image { Source = ImageSource.FromResource(source) } });
                    
                }
            }

            
            reco.Tapped += async (sender, e) =>
            {
                await Navigation.PushAsync(carousel);
            };
            imageStack.GestureRecognizers.Add(reco);
            imageScroll.Content = imageStack;
            stackLayout.Children.Add(imageScroll);
            //Image scrolllayout hinzugefügt

            Content = stackLayout;
            BackgroundColor = Color.White;
            Padding = new Thickness(5, Device.OnPlatform(0, 15, 0), 5, 5);
        }//ende Construktor

        public void initContentLists()
        {
            if (pContent[0] != null)
            {
                owned = true;
                foreach (PContent content in pContent)
                {
                    if (content.content_Kind == 0)
                    {
                        contentPictureGalarie.Add(content);
                        //SMK.FischerTechnik.Files.1.png
                        //picture_galarie.Add(imagePfadContent+content.content_ID.ToString()+".png");

                    }
                    if (content.content_Kind == 1)
                    {
                        contentPdf.Add(content);

                    }
                    if (content.content_Kind == 2)
                    {
                        contentHtml.Add(content);

                    }
                    if (content.content_Kind == 3)
                    {
                        contentVideo.Add(content);

                    }

                }
            }
        }
    }
}
