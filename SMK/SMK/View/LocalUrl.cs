using SMK.Model;
using System;
using Xamarin.Forms;

namespace SMK.Support
{


    // required temporarily for iOS, due to BaseUrl bug
    public class BaseUrlWebView : WebView { }

   
    public class LocalUrl : ContentPage
    {
       
        public LocalUrl(PContent content)
        {
            String imagePfadContent = "SMK.zeug.PContent.p";
            var browser = new BaseUrlWebView(); // temporarily use this so we can custom-render in iOS

            var htmlSource = new HtmlWebViewSource();
            //var source ="/p"+ content.content_ID + "/" + content.content_FileNames[0] + ".html";
            var source = @"local.html";
            // < meta http - equiv = "refresh" content = "0; url=http://example.com/" />
            //htmlSource.Html = @"<html> < a href=""" + source + @""">";
            htmlSource.Html = @"<meta http-equiv=""refresh"" content=""0; url ="+source+@""" />"; 



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
