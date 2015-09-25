using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Database
{
    class DatabaseUpdate
    {
        public void UpdatePContent(string ftpServer, string file, string path)
        {
            ftpServer = "http://smktest.bugs3.com";
            file = "FISCHERTECHNIK_Logo.JPG";
            path = @"C:\Users\Maxwell\Desktop\fisch.jpg";
            var client = new RestClient(ftpServer);
            var request = new RestRequest(file);
            //var request = new RestRequest("{id}", Method.GET);
            //request.AddUrlSegment("id", "\\FISCHERTECHNIK_Logo.JPG"); // replaces matching token in request.Resource

            client.DownloadData(request).SaveAs(path);
        }
    }
}
