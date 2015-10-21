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
        /// <summary>
        /// Downloads a single File from an Ftp Server
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="host"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        void DownloadFile(string src, string dest, string host, string user, string password);

        /// <summary>
        /// Downloads a complete folder with all subfolders from a Ftp Server
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="host"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        void DownloadDirectoryAsync(string src, string dest, string host, string user, string password);
    }
}
