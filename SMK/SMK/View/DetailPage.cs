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
            string source = null;

            if (pContent[0] != null)
            {
                foreach (PContent content in pContent)
                {
                    if (content.content_Kind == 0)
                    {
                        source = content.content_FileNames[0];
                        break;
                    }

                }
            }

            if (source != null)
            {

                stackLayout.Children.Add(
                    new Image
                    {
                        Source = source
                    }
                    );

            }
            Content = stackLayout;
        }//ende Construktor
    }
}
