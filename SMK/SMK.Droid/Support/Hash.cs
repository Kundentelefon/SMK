using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Security.Cryptography;
using Xamarin.Forms;
using SMK.Droid.Support;

[assembly: Dependency(typeof(Hash))]
namespace SMK.Droid.Support
{
    class Hash : SMK.Support.IHash
    {
        public string Sha512StringHash(string inputString)
        {
            SHA512 shaM = new SHA512Managed();
            // Convert the inputString string to a byte array and compute the hash.
            byte[] data = shaM.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            inputString = sBuilder.ToString();
            return (inputString);
        }
    }
}