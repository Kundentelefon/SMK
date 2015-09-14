using SMK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Support
{
    class localFileSystem
    {
        public List<Product> loadProductList()
        {
            List<Product> returnList = new List<Product> { new Product(0, "Test","Resources/FischerTechnik/Files/0.png", "havesomeTextShit i hope you like it", new List<int>(new int[] { 0, 1, 2 })) ,
                                                           new Product(1, "TestProductNotOwned",  "Resources/FischerTechnik/Files/1.png", "havesomeTextShit i hope you like it", new List<int>(new int[] { 3, 4})),
                                                           new Product(2, "TestProductNotDownloaded",  "Resources/FischerTechnik/Files/2.png", "havesomeTextShit i hope you like it", new List<int>(new int[] { 5, 6 }))
                                                            };

            //dummies
            //Product testpro = new Product();
            //List<string> optionList = new List<string> { "string1", "string2", ..., "stringN"}; 
            //List<int> testintl = new List<int>(new int[] { 1, 2, 3 });
            //Product testpro = new Product(0, "Test", "NochKeinPlan",  "havesomeText", new List<int>(new int[] { 1, 2, 3 }));
            //210.210
            
            return (returnList);
        }
        public List<PContent> loadContentList() { 

            List<PContent> returnList = new List<PContent> {new PContent(0,0,"BilderGalarie", new List<String>(new String[] {"da.png","haha.png","devil.png"})),//"Resources/FischerTechnik/PContent/0_Icon.png"
                                                           new PContent(1,0,"BilderGalarie",new List<String>(new String[]{"da.png","haha.png","devil.png"})),
                                                           new PContent(2,2,"WebView",new List<String>(new String[]{})),
                                                           new PContent(5,0,"BilderGalarie",new List<String>(new String[]{"da.png","haha.png","devil.png"})),//für diese Fehlt noch der Content da dieser Noch nicht "gedownloaded" wurde
                                                           new PContent(6,0,"BilderGalarie",new List<String>(new String[]{"da.png","haha.png","devil.png"}))//für diese Fehlt noch der Content da dieser Noch nicht "gedownloaded" wurde
                                                            };
            return (returnList);
        }
        public Boolean saveModelsLocal(List<Product> inputProducts, List<PContent> inputContents)
        {
            Boolean returnSuccess = false;


            return (returnSuccess);
        }
    }
}
