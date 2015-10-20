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
        /// speichert den "User" an die übergebene "location" im AppOrdner
        /// </summary>
        /// <param name="location"></param>
        /// <param name="inputObject"></param>
        void saveUserXml(String location, Object inputObject);
        /// <summary>
        /// läd das User-xml File von der "location" ein und gibt es zurück 
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Returns the remembered user and null if no user is saved.</returns>
        User loadUserXml(String location);


        /// <summary>
        /// speichert den "Pcontents" an dem übergebene Pfad der aus "userPath" und "location" besteht, im AppOrdner ab
        /// </summary>
        /// <param name="location"></param>
        /// <param name="inputObject"></param>
        void savePContentsXml(String userPath, String location, Object inputObject);
        /// <summary>
        /// lädt den "Pcontents" aus dem Pcontent File welches im UserOrdner unter dem Pfad aus "location" und "userPath" abgespeichert wurde
        /// und gibt diesen "Pcontents" zurück
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        PContents loadPcontentsXml(String location, String userPath);
        /// <summary>
        /// speichert den "Product" an dem übergebene Pfad "location"  im AppOrdner ab
        /// </summary>
        /// <param name="location"></param>
        /// <param name="inputObject"></param>
        void saveProductsXml(String location, Object inputObject);
        /// <summary>
        /// lädt den "Products" aus dem Product File welches im UserOrdner unter dem Pfad "location"  abgespeichert wurde
        /// und gibt diesen "Products" zurück
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        Products loadProductsXml(String location);
        /// <summary>
        /// überprüft ob am dem übergeben Pfad im AppOrdner ein File liegt
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        Boolean fileExist(String inputString);
        /// <summary>
        /// überprüft ob am dem übergeben Pfad im AppOrdner ein File liegt
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        Boolean fileExistExact(String inputString);
        /// <summary>
        /// gibt den AppOrdner Pfad als String zurück mit der "location" hinten angefügt
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        String getpath(String location);
        /// <summary>
        /// erstellt einen Ordner an dem Übergeben Path in dem AppOrdner
        /// falls der Ordner schon existiert führt diese funktion nichts aus
        /// </summary>
        /// <param name="path"></param>
        void createOrdner(String path);
        /// <summary>
        /// löscht eine Datei am übergebenen Pfad im AppOrdner Pfad
        /// </summary>
        /// <param name="path"></param>
        void deleteFile(String path);
        /// <summary>
        /// Fügt an übergebenen "firstPath" den auch übergenen "secondPath" an und gibt diese zurück
        /// </summary>
        /// <param name="firstPath"></param>
        /// <param name="secondPath"></param>
        /// <returns></returns>
        String pathCombine(String firstPath, String secondPath);
        /// <summary>
        /// lädt einen Text aus der Datei mit dem Pfad "filename" im AppOrdner Verzeichniss
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string LoadText(string filename);
    }
}
