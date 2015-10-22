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
using SMK.Droid;
using Xamarin.Forms;
using SMK.Support;
using SMK.Model;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

[assembly: Dependency(typeof(SaveAndLoad))]
namespace SMK.Droid
{
    public class SaveAndLoad : ISaveAndLoad
    {
        /// <summary>
        /// saves a Object to the location ()
        /// </summary>
        /// <param name="location"></param>
        /// <param name="inputObject"></param>
        public void SaveUserXml(String location, Object inputObject)
        {
            try {
                if (FileExist(location))
                {
                    DeleteFile(location);
                }
                XmlSerializer ser = new XmlSerializer(typeof(User));

                System.IO.FileStream file = System.IO.File.Create(Getpath(location));
                ser.Serialize(file, inputObject);
                file.Close();
            }
            catch(Exception e) { Console.WriteLine("" + e); }
        }
        /// <summary>
        /// load an xml file 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public User LoadUserXml(String location)
        {
            User returnUser = null;
            try {
                XmlSerializer ser = new XmlSerializer(typeof(User));
                FileStream fs = new FileStream(Getpath(location), FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);
                returnUser = (User)ser.Deserialize(reader);
                fs.Close();                
            }
            catch (Exception e) { Console.WriteLine("" + e); }
            return (returnUser);
        }


        /// <summary>
        /// saves a Object to the location ()
        /// </summary>
        /// <param name="location"></param>
        /// <param name="inputObject"></param>
        public void SavePContentsXml(String userPath, String location, Object inputObject)
        {
            location = Path.Combine(userPath, location);
            try
            {
                if (FileExist(location))
                {
                    DeleteFile(location);
                }
                XmlSerializer ser = new XmlSerializer(typeof(PContents));

                System.IO.FileStream file = System.IO.File.Create(Getpath(location));
                ser.Serialize(file, inputObject);
                file.Close();
            }
            catch (Exception e) { Console.WriteLine("" + e); }
        }
        /// <summary>
        /// load an xml file 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public PContents LoadPcontentsXml(String location, String userPath)
        {
            location = Path.Combine(userPath, location);
            PContents retrunObject = new PContents();
            try {
                XmlSerializer ser = new XmlSerializer(typeof(PContents));
                FileStream fs = new FileStream(Getpath(location), FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);
                retrunObject = (PContents)ser.Deserialize(reader);
                fs.Close();
                
            }
            catch (Exception e) { Console.WriteLine("" + e); }
            return (retrunObject);
        }
        /// <summary>
        /// saves a Object to the location ()
        /// </summary>
        /// <param name="location"></param>
        /// <param name="inputObject"></param>
        public void SaveProductsXml(String location, Object inputObject)
        {
            try {
                if (FileExist(location))
                {
                    DeleteFile(location);
                }
                XmlSerializer ser = new XmlSerializer(typeof(Products));

                System.IO.FileStream file = System.IO.File.Create(Getpath(location));
                ser.Serialize(file, inputObject);
                file.Close();
            }
            catch (Exception e) { Console.WriteLine("" + e); }
        }
        /// <summary>
        /// load an xml file 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public Products LoadProductsXml(String location)
        {
            Products retrunObject = new Products();
            try {
                XmlSerializer ser = new XmlSerializer(typeof(Products));
                FileStream fs = new FileStream(Getpath(location), FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);
                retrunObject = (Products)ser.Deserialize(reader);
                fs.Close();
      
            }
            catch (Exception e) { Console.WriteLine("" + e); }
            return (retrunObject);
        }







        /// <summary>
        /// get the local path
        /// </summary>
        /// <returns></returns>
        public String Getpath(String inputPath)
        {
            String contentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), inputPath);
            return (contentPath);
        }
        /// <summary>
        /// erstellt einen Folder an den standardpfad
        /// </summary>
        /// <param name="path"></param>
        public void CreateFolder(String path)
        {
            try {
                path = Getpath(path);
                if (!(Directory.Exists(path)))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception e) { Console.WriteLine("" + e); }

        }



        public Boolean FileExist(String inputString)
        {
            if (File.Exists(Getpath(inputString)))
            {
                return (true);
            }
            return (false);

        }

        public Boolean FileExistExact(String inputString)
        {
            //var test=Directory.GetCurrentDirectory();
            if (File.Exists(inputString))
            {
                return (true);
            }
            return (false);

        }

        public void DeleteFile(String location)
        {
            try {
                System.IO.File.Delete(Getpath(location));
            }
            catch (Exception e) { Console.WriteLine("" + e); }
        }

        public String PathCombine(String firstPath, String secondPath)
        {
            String path=Path.Combine(firstPath,secondPath);
            return (path);
        }

        public string LoadText(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);
        }
    }



}


        
