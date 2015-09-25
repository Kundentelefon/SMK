using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using SMK.Droid.Support;
using SMK.Support;
using Xamarin.Forms;

[assembly: Dependency(typeof(FtpClient))]
namespace SMK.Droid.Support
{
    internal class FtpClient : IFtpClient
    {
        public void DownloadFile(string src, string dest, string host, string user, string password)
        {
            WebRequest request = WebRequest.Create("ftp://" + host + "/" + src);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(user, password);

            var response = request.GetResponse();

            Stream reqStream = response.GetResponseStream(); ;

            // 
            FileStream fileStream = File.Create(dest);
            int blockSize = 2048;
            byte[] buffer = new byte[blockSize];
            int readBytes = reqStream.Read(buffer, 0, blockSize);
            while (readBytes != 0)
            {
                fileStream.Write(buffer, 0, readBytes);
                readBytes = reqStream.Read(buffer, 0, blockSize);
            }
            fileStream.Close();
        }

        public void DownloadDirectoryAsync(string src, string dest, string host, string user, string password)
        {
            WebRequest request = WebRequest.Create("ftp://" + host + "/" + src + "/");
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(user, password);

            var result = request.GetResponse();

            Directory.CreateDirectory(dest);

            var directories = new List<string>();
            var files = new List<string>();

            var stream = result.GetResponseStream();
            var reader = new StreamReader(result.GetResponseStream());

            string s = reader.ReadLine();
            while (s != null)
            {
                string fileName = s.Substring(49);
                if (s.StartsWith("d"))
                    directories.Add(fileName);
                else
                    files.Add(fileName);

                s = reader.ReadLine();
            }
            reader.Close();
            stream.Close();
            result.Close();

            foreach (var directory in directories)
            {
                DownloadDirectoryAsync(src + "/" + directory, Path.Combine(dest, directory), host, user, password);
            }

            foreach (var file in files)
            {
                DownloadFile(src + "/" + file, Path.Combine(dest, file), host, user, password);
            }
        }
    }
}