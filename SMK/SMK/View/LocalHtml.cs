using SMK.Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SMK.Support
{
    // required temporarily for iOS, due to BaseUrl bug
    public class BaseUrlWebView : WebView { }

    public class LocalHtml : ContentPage
    {
        public LocalHtml(PContent content, String userPath)
        {
            //var browser = new WebView();
            //String imagePfadContent = "SMK.zeug.PContent.p";
            String source = (DependencyService.Get<ISaveAndLoad>().PathCombine(
                        (DependencyService.Get<ISaveAndLoad>().PathCombine(
                            DependencyService.Get<ISaveAndLoad>().Getpath(userPath), "p" + content.content_ID.ToString())), content.files[0]));
            var browser = new BaseUrlWebView(); // temporarily use this so we can custom-render in iOS

            var htmlSource = new HtmlWebViewSource();

            if (DependencyService.Get<ISaveAndLoad>().FileExistExact(source))
            {
                htmlSource.Html = DependencyService.Get<ISaveAndLoad>().LoadText(source);
            }

            if (Device.OS != TargetPlatform.iOS)
            {
                // the BaseUrlWebViewRenderer does this for iOS, until bug is fixed
                htmlSource.BaseUrl = (DependencyService.Get<ISaveAndLoad>().PathCombine(
                            DependencyService.Get<ISaveAndLoad>().Getpath(userPath), "p" + content.content_ID.ToString()));
            }

            browser.Source = htmlSource;

            Content = browser;
        }
    }
}

//var source ="/p"+ content.content_ID + "/" + content.content_FileNames[0] + ".html";
//var source = @"local.html";
// < meta http - equiv = "refresh" content = "0; url=http://example.com/" />
//htmlSource.Html = @"<html> < a href=""" + source + @""">";
//htmlSource.Html = @"<meta http-equiv=""refresh"" content=""0; url ="+source+@""" />";