using Foundation;
using SMK.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BaseUrlWebView), typeof(BaseUrlWebViewRenderer))]

namespace SMK.iOS
{
    public class BaseUrlWebViewRenderer : WebViewRenderer
    {
        public override void LoadHtmlString(string s, NSUrl baseUrl)
        {
            baseUrl = new NSUrl(NSBundle.MainBundle.BundlePath, true);
            base.LoadHtmlString(s, baseUrl);
        }
    }
}