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
                //        var saveButton = new Button { Text = "Save" };
                //        saveButton.Clicked += (sender, e) => {
                //    DependencyService.Get<ISaveAndLoad>().SaveText("temp.txt", input.Text);
                //    };
                //    var loadButton = new Button { Text = "Load" };
                //    loadButton.Clicked += (sender, e) => {
                //    output.Text = DependencyService.Get<ISaveAndLoad>().LoadText("temp.txt");
                //};
        //void SaveText(string filename, string text);
        //string LoadText(string filename);

        //void saveModelXml(String location, Object inputObject);
        /// <summary>
        /// saves a Object to the location ()
        /// </summary>
        /// <param name="location"></param>
        /// <param name="inputObject"></param>
        void saveUserXml(String location, Object inputObject);
        /// <summary>
        /// load an xml file 
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Returns the remembered user and null if no user is saved.</returns>
        User loadUserXml(String location);


        /// <summary>
        /// saves a Object to the location ()
        /// </summary>
        /// <param name="location"></param>
        /// <param name="inputObject"></param>
        void savePContentsXml(String userPath, String location, Object inputObject);
        /// <summary>
        /// load an xml file 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        PContents loadPcontentsXml(String location, String userPath);
        /// <summary>
        /// saves a Object to the location ()
        /// </summary>
        /// <param name="location"></param>
        /// <param name="inputObject"></param>
        void saveProductsXml(String location, Object inputObject);
        /// <summary>
        /// load an xml file 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        Products loadProductsXml(String location);

        Boolean fileExist(String inputString);
        Boolean fileExistExact(String inputString);

        String getpath(String location);
        void createOrdner(String path);

        void deleteFile(String path);

        String pathCombine(String firstPath, String secondPath);
    }
}
