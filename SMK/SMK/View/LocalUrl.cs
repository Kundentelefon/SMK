using SMK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SMK.Support
{
    public interface IBaseUrl { string Get(); }

    // required temporarily for iOS, due to BaseUrl bug
    public class BaseUrlWebView : WebView { }

   
    public class LocalUrl : ContentPage
    {
        String imagePfadContent = "SMK.zeug.PContent.p";
        public LocalUrl(PContent content)
        {
            var browser = new BaseUrlWebView(); // temporarily use this so we can custom-render in iOS

            var htmlSource = new HtmlWebViewSource();
            var source = imagePfadContent + content.content_ID + content.content_FileNames[0] ;

            htmlSource.Html = @"<a href="""+ source + @""">";


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
