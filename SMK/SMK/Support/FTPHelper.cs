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
    public interface IFileWriter
    {
        void OpenFile(string file);

        void Write(byte[] bytes, int offset, int count);

        void CloseFile();
    }

    public class FtpClient
    {
        private string _host;
        private string _user;
        private string _password;

        public FtpClient(string host, string user, string password)
        {
            _host = host;
            _user = user;
            _password = password;
        }

        public async void DownloadFile(string src, string dest)
        {
            WebRequest request = WebRequest.Create("ftp://" + _host + "/" + src);
            request.Method = FtpRequestMethods.DownloadFile;
            request.Credentials = new NetworkCredential(_user, _password);
            Stream reqStream = await request.GetRequestStreamAsync();

            IFileWriter fileWriter = DependencyService.Get<IFileWriter>();
            fileWriter.OpenFile(dest);
            int blockSize = 2048;
            byte[] buffer = new byte[blockSize];
            int offset = 0;
            int readBytes = reqStream.Read(buffer, offset, blockSize);
            while (readBytes != 0)
            {   
                fileWriter.Write(buffer, offset, readBytes);
                offset += blockSize;
                readBytes = reqStream.Read(buffer, offset, blockSize);
            }
            fileWriter.CloseFile();
        }

        public async void DownloadDirectory(string src, string dest)
        {
            WebRequest request = WebRequest.Create("ftp://" + _host + "/" + src);
            request.Method = FtpRequestMethods.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(_user, _password);

            var result = await request.GetResponseAsync();

            var reader = new StreamReader(result.GetResponseStream());

            string s = reader.ReadLine();
            while (s != null)
            {
                string fileName = s.Substring(49);
                if (s.StartsWith("d"))
                {
                    DownloadDirectory(src + "/" + fileName, Path.Combine(dest, fileName));
                }
                else
                {
                    DownloadFile(src + "/" + fileName, Path.Combine(dest, fileName));
                }
                s = reader.ReadLine();
            }
        }
    }

    public static class FtpRequestMethods
    {
        /// <summary>
        /// Stellt die FTP RETR-Protokollmethode dar, die zum Herunterladen einer Datei von einem FTP-Server verwendet wird.
        /// </summary>
        public const string DownloadFile = "RETR";
        /// <summary>
        /// Stellt die FTP NLIST-Protokollmethode dar, die eine kurze Auflistung der Dateien auf einem FTP-Server abruft.
        /// </summary>
        public const string ListDirectory = "NLST";
        /// <summary>
        /// Stellt die FTP STOR-Protokollmethode dar, die eine Datei auf einen FTP-Server hochlädt.
        /// </summary>
        public const string UploadFile = "STOR";
        /// <summary>
        /// Stellt die FTP DELE-Protokollmethode dar, die zum Löschen einer Datei auf einem FTP-Server verwendet wird.
        /// </summary>
        public const string DeleteFile = "DELE";
        /// <summary>
        /// Stellt die FTP APPE-Protokollmethode dar, die zum Anfügen einer Datei an eine auf einem FTP-Server vorhandene Datei verwendet wird.
        /// </summary>
        public const string AppendFile = "APPE";
        /// <summary>
        /// Stellt die FTP SIZE-Protokollmethode dar, die zum Abrufen der Größe einer Datei auf einem FTP-Server verwendet wird.
        /// </summary>
        public const string GetFileSize = "SIZE";
        /// <summary>
        /// Stellt die FTP STOU-Protokollmethode dar, die eine Datei mit einem eindeutigen Namen auf einen FTP-Server hochlädt.
        /// </summary>
        public const string UploadFileWithUniqueName = "STOU";
        /// <summary>
        /// Stellt die FTP MKD-Protokollmethode dar, die ein Verzeichnis auf einem FTP-Server erstellt.
        /// </summary>
        public const string MakeDirectory = "MKD";
        /// <summary>
        /// Stellt die FTP RMD-Protokollmethode dar, die ein Verzeichnis entfernt.
        /// </summary>
        public const string RemoveDirectory = "RMD";
        /// <summary>
        /// Stellt die FTP LIST-Protokollmethode dar, die eine ausführliche Auflistung der Dateien auf einem FTP-Server abruft.
        /// </summary>
        public const string ListDirectoryDetails = "LIST";
        /// <summary>
        /// Stellt die FTP MTDM-Protokollmethode dar, die zum Abrufen eines DateTime-Zeitstempels von einer Datei auf einem FTP-Server verwendet wird.
        /// </summary>
        public const string GetDateTimestamp = "MDTM";
        /// <summary>
        /// Stellt die FTP PWD-Protokollmethode dar, die den Namen des aktuellen Arbeitsverzeichnisses druckt.
        /// </summary>
        public const string PrintWorkingDirectory = "PWD";
        /// <summary>
        /// Stellt die FTP RENAME-Protokollmethode dar, die ein Verzeichnis umbenennt.
        /// </summary>
        public const string Rename = "RENAME";
    }
}
