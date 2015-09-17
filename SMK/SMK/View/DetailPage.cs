using SMK.Model;
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
        public DetailPage(Product ResourceProduct)
        {
            product = ResourceProduct;
            files = new localFileSystem();
            pContent = files.loadContentList(product);
            StackLayout stackLayout = new StackLayout();
            StackLayout buttonStack = new StackLayout();
            TapGestureRecognizer gesture = new TapGestureRecognizer();
            List<PContent> contentPictureGalarie = new List<PContent>() ;
            List<PContent> contentPdf = new List<PContent>();
            List<PContent> contentHtml = new List<PContent>();
            List<PContent> contentVideo = new List<PContent>();
            bool owned = false;
            Color color = Color.FromHex("E2001A");


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
           
            

            Content = stackLayout;
            BackgroundColor = Color.White;
            Padding = new Thickness(5, Device.OnPlatform(0, 15, 0), 5, 5);
        }//ende Construktor
    }
}
