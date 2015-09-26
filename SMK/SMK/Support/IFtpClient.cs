using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SMK.Support
{
    public interface IFtpClient
    {
        void DownloadFile(string src, string dest, string host, string user, string password);

        void DownloadDirectoryAsync(string src, string dest, string host, string user, string password);
    }
}
