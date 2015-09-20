using SMK.Model;
using System;
using Xamarin.Forms;

namespace SMK.Support
{
    public interface IBaseUrl { string Get(); }

    // required temporarily for iOS, due to BaseUrl bug
    public class BaseUrlWebView : WebView { }

   
    public class LocalUrl : ContentPage
    {
       
        public LocalUrl(PContent content)
        {
            String imagePfadContent = "SMK.zeug.PContent.p";
            var browser = new BaseUrlWebView(); // temporarily use this so we can custom-render in iOS

            var htmlSource = new HtmlWebViewSource();
            var source = imagePfadContent + content.content_ID + content.content_FileNames[0] ;

            //htmlSource.Html = @"<a href="""+ source + @""">";
            htmlSource.Html = @"<html>
                                <head>
                                </head>
                                <body>
                                <h1>Xamarin.Forms</h1>
                                <p>The CSS and image are loaded from local files!</p>
                                <img src='XamarinLogo.png'/>
                                
                                </body>
                                </html>";


            if (Device.OS != TargetPlatform.iOS)
            {
                // the BaseUrlWebViewRenderer does this for iOS, until bug is fixed
                htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
            }

            browser.Source = htmlSource;

            Content = browser;
        }
    }
}
