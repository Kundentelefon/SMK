using SMK.iOS;
using SMK.Model;
using SMK.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveAndLoad))]
namespace SMK.iOS
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
            try
            {
                if (FileExist(location))
                {
                    DeleteFile(location);
                }
                XmlSerializer ser = new XmlSerializer(typeof(User));

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
        public User LoadUserXml(String location)
        {
            User returnObject = null;
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(User));
                FileStream fs = new FileStream(Getpath(location), FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);
                returnObject = (User)ser.Deserialize(reader);
                fs.Close();
            }
            catch (Exception e) { Console.WriteLine("" + e); }
            return (returnObject);
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
            try
            {
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
            try
            {
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
            try
            {
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
            String contentlPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), inputPath);
            return (contentlPath);
        }
        /// <summary>
        /// erstellt einen Folder an den standardpfad
        /// </summary>
        /// <param name="path"></param>
        public void CreateFolder(String path)
        {
            try
            {
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
            try
            {
                System.IO.File.Delete(Getpath(location));
            }
            catch (Exception e) { Console.WriteLine("" + e); }
        }
        public String PathCombine(String firstPath, String secondPath)
        {
            String path = Path.Combine(firstPath, secondPath);
            return (path);
        }
        public string LoadText(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);
        }
    }



}



