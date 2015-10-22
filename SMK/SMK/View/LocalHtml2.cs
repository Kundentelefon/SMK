using SMK.Model;
using SMK.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SMK.View
{
    class LocalHtml2 : ContentPage
    {
        public LocalHtml2(PContent content, String userPath)
        {
            //Pfad setzen
            String source = (DependencyService.Get<ISaveAndLoad>().PathCombine(
                        (DependencyService.Get<ISaveAndLoad>().PathCombine(
                            DependencyService.Get<ISaveAndLoad>().Getpath(userPath), "p" + content.content_ID.ToString())), content.files[0]));
            //browser und viewsource initalisieren
            var browser = new WebView();
            var htmlSource = new HtmlWebViewSource();
            // fals Datei vorhanden einlesen
            if (DependencyService.Get<ISaveAndLoad>().FileExistExact(source))
            {
                htmlSource.Html = DependencyService.Get<ISaveAndLoad>().LoadText(source);
            }
            //BaseUrl setzen um auf andere Dateien referenzieren zu können(Css, javascript oder html)
            htmlSource.BaseUrl = (DependencyService.Get<ISaveAndLoad>().PathCombine(
                            DependencyService.Get<ISaveAndLoad>().Getpath(userPath), "p" + content.content_ID.ToString()));

            browser.Source = htmlSource;
            Content = browser;
        }
    }
}
