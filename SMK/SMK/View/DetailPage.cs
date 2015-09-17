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
            TapGestureRecognizer gesture = new TapGestureRecognizer();
            List<string> source= new List<string>() ;

            if (pContent[0] != null)
            {
                foreach (PContent content in pContent)
                {
                    if (content.content_Kind == 0)
                    {
                        //
                        source.Add(imagePfadContent+content.content_ID.ToString()+".png");
                        break;
                    }

                }
            }
            String test = imagePfadProduct + product.product_ID.ToString() + ".png";
            stackLayout.Children.Add(
            new Image
            {
                Source = imagePfadProduct + product.product_ID.ToString() + ".png"
            }
            );
            //if (source.Count() >= 0)
            //{
            //    foreach (var item in source)
            //    {
            //        stackLayout.Children.Add(
            //        new Image
            //        {
            //            Source = item
            //        }
            //        );
            //    }

            //}
            Content = stackLayout;
        }//ende Construktor
    }
}
