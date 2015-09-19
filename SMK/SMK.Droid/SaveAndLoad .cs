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
        public void saveUserXml(String location, Object inputObject)
        {
            try {
                if (fileExist(location))
                {
                    deleteFile(location);
                }
                XmlSerializer ser = new XmlSerializer(typeof(User));

                System.IO.FileStream file = System.IO.File.Create(getpath(location));
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
        public User loadUserXml(String location)
        {
            User retrunObject = new User();
            try {
                XmlSerializer ser = new XmlSerializer(typeof(User));
                FileStream fs = new FileStream(getpath(location), FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);
                retrunObject = (User)ser.Deserialize(reader);
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
        public void savePContentsXml(String location, Object inputObject)
        {
            try {
                if (fileExist(location))
                {
                    deleteFile(location);
                }
                XmlSerializer ser = new XmlSerializer(typeof(PContents));

                System.IO.FileStream file = System.IO.File.Create(getpath(location));
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
        public PContents loadPcontentsXml(String location)
        {
            PContents retrunObject = new PContents();
            try {
                XmlSerializer ser = new XmlSerializer(typeof(PContents));
                FileStream fs = new FileStream(getpath(location), FileMode.Open);
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
        public void saveProductsXml(String location, Object inputObject)
        {
            try {
                if (fileExist(location))
                {
                    deleteFile(location);
                }
                XmlSerializer ser = new XmlSerializer(typeof(Products));

                System.IO.FileStream file = System.IO.File.Create(getpath(location));
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
        public Products loadProductsXml(String location)
        {
            Products retrunObject = new Products();
            try {
                XmlSerializer ser = new XmlSerializer(typeof(Products));
                FileStream fs = new FileStream(getpath(location), FileMode.Open);
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
        public String getpath(String inputPath)
        {
            String contentlPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + inputPath;
            return (contentlPath);
        }
        /// <summary>
        /// erstellt einen ordner an den standardpfad
        /// </summary>
        /// <param name="path"></param>
        public void createOrdner(String path)
        {
            try {
                path = getpath(path);
                if (!(Directory.Exists(path)))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception e) { Console.WriteLine("" + e); }

        }



        public Boolean fileExist(String inputString)
        {
            if (File.Exists(getpath(inputString)))
            {
                return (true);
            }
            return (false);

        }

        public Boolean fileExistExact(String inputString)
        {
            if (File.Exists(inputString))
            {
                return (true);
            }
            return (false);

        }

        public void deleteFile(String location)
        {
            try {
                System.IO.File.Delete(getpath(location));
            }
            catch (Exception e) { Console.WriteLine("" + e); }
        }
    }



}


        
