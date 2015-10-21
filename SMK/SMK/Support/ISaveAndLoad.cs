using SMK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Support
{
    public interface ISaveAndLoad
    {
        /// <summary>
        /// saves the "User" at "location" in AppFolder
        /// </summary>
        /// <param name="location"></param>
        /// <param name="inputObject"></param>
        void SaveUserXml(String location, Object inputObject);
        /// <summary>
        /// load the User-XML File from "location" and gives it back
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Returns the remembered user and null if no user is saved.</returns>
        User LoadUserXml(String location);

        /// <summary>
        /// saves "PContents" at the combined location "userPath" and "location" in AppFolder
        /// </summary>
        /// <param name="userPath"></param>
        /// <param name="location"></param>
        /// <param name="inputObject"></param>
        void SavePContentsXml(String userPath, String location, Object inputObject);

        /// <summary>
        /// loads the "PContent" from the PContent-File in (which is located in the combined Path "userpath" and "location") and saves it back in "PContents"
        /// </summary>
        /// <param name="location"></param>
        /// <param name="userPath"></param>
        /// <returns></returns>
        PContents LoadPcontentsXml(String location, String userPath);
        /// <summary>
        /// saves "Product" at the given "location" in AppFolder
        /// </summary>
        /// <param name="location"></param>
        /// <param name="inputObject"></param>
        void SaveProductsXml(String location, Object inputObject);
        /// <summary>
        /// loads the "Product" File from "location" (which is located in UserFolder) and gives this "Products" back
        /// lädt den "Products" aus dem Product File welches im UserFolder unter dem Pfad "location"  abgespeichert wurde
        /// und gibt diesen "Products" zurück
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        Products LoadProductsXml(String location);
        /// <summary>
        /// Checks if the file exists at "location"
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        Boolean FileExist(String inputString);
        /// <summary>
        /// Checks if the File exists at "inputString" in AppFolder
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        Boolean FileExistExact(String inputString);
        /// <summary>
        /// Returns the AppFolder-Path as string which is located in "location"
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        String Getpath(String location);
        /// <summary>
        /// creates a Folder at the given "path" in AppFolder
        /// if the Folder already exists, it does nothing
        /// </summary>
        /// <param name="path"></param>
        void CreateFolder(String path);
        /// <summary>
        /// deletes a File at the given Path in the AppFolder-Path
        /// </summary>
        /// <param name="path"></param>
        void DeleteFile(String path);
        /// <summary>
        /// Combines "firstPath" and "secondPath" to one Path
        /// </summary>
        /// <param name="firstPath"></param>
        /// <param name="secondPath"></param>
        /// <returns></returns>
        String PathCombine(String firstPath, String secondPath);
        /// <summary>
        /// loads the Text from a File in AppFolder from the path "filename"
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string LoadText(string filename);
    }
}
