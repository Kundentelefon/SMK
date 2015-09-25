using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace SMK.Database
{
    //TODO: maybe this singleton dont have to be used and can be integrated in the Dataaccess interface
    class Database_handler
    {
        public void activateProduct(string key)
        {
            //ftpServer = "http://smktest.bugs3.com";
            //file = "FISCHERTECHNIK_Logo.JPG";
            //path = @"C:\Users\Maxwell\Desktop\fisch.jpg";
            //var client = new RestClient(ftpServer);
            //var request = new RestRequest(file);
            //var request = new RestRequest("{id}", Method.GET);
            //request.AddUrlSegment("id", "\\FISCHERTECHNIK_Logo.JPG"); // replaces matching token in request.Resource

            //client.DownloadData(request).SaveAs(path);
        }
    }

public class Placeobject
    {
        [JsonProperty("postalcodes")]
        public Place[] places { get; set; }
    }



    public class Place
    {
        public string placeName { get; set; }
    }

}
