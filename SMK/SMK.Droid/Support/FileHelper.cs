using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using SMK.Droid.Support;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileWriter))]
namespace SMK.Droid.Support
{
    internal class FileWriter : SMK.Support.IFileWriter
    {
        private FileStream _fileStream;

        public void OpenFile(string file)
        {
            _fileStream = File.OpenWrite(file);
        }

        public void Write(byte[] bytes, int offset, int count)
        {
            _fileStream.Write(bytes, offset, count);
        }

        public void CloseFile()
        {
            _fileStream.Close();
        }
    }
}