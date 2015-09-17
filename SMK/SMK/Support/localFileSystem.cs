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
        List<Product> ProductList;
        List<PContent> ContentList;
        public localFileSystem()
        {
            //ProductList = loadProductList();
            //ContentList = loadContentList();
        }
        public List<Product> loadProductList()
        {
            List<Product> returnList = new List<Product> { new Product(0, "Test","SMK.FischerTechnik.Files.0.png", "have some Text i hope you like it fighting and burning from turning from who we really are, flying to close to the sun as we were invincible, around the world we grow weaker as we exterminate, we are the children of the sun", new List<int>(new int[] { 0, 1, 2 })) ,
                                                           new Product(1, "Test ProductNotOwned",  "SMK.FischerTechnik.Files.1.png", "havesomeTextShit i hope you like it", new List<int>(new int[] { 3, 4})),
                                                           new Product(2, "Test ProductNotOwned",  "SMK.FischerTechnik.Files.1.png", "havesomeTextShit i hope you like it", new List<int>(new int[] { 5, 6})),
                                                           new Product(3, "Test ProductNotDownloaded",  "SMK.FischerTechnik.Files.2.png", "havesomeTextShit i hope you like it", new List<int>(new int[] { 7, 8 }))
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

            List<PContent> returnList = new List<PContent> {new PContent(0,0,"BilderGalarie", new List<String>(new String[] {"SMK.FischerTechnik.0.da.jpg","haha.png","devil.png"})),//"Resources/FischerTechnik/PContent/0_Icon.png"
                                                           new PContent(1,0,"BilderGalarie",new List<String>(new String[]{"da.png","haha.png","devil.png"})),
                                                           new PContent(2,2,"WebView",new List<String>(new String[]{})),
                                                           null,
                                                           null,
                                                           new PContent(5,0,"BilderGalarie",new List<String>(new String[]{"da.png","haha.png","devil.png"})),//für diese Fehlt noch der Content da dieser Noch nicht "gedownloaded" wurde
                                                           new PContent(6,0,"BilderGalarie",new List<String>(new String[]{"da.png","haha.png","devil.png"})),//für diese Fehlt noch der Content da dieser Noch nicht "gedownloaded" wurde
                                                           null,
                                                           null
                                                            };
            return (returnList);
        }

        /// <summary>
        /// Übergebe ein Product und bekomme eine Liste mit Allen Content für dieses Produkt
        /// Content kann Null sein falls User Product nicht besitzt
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public List<PContent> loadContentList(Product product)
        {
            List<PContent> completeList = loadContentList();
            List<PContent> returnList = new List<PContent>();
            foreach (var item in product.product_PContents)
            {
                returnList.Add(completeList[item]);
            }
            return returnList;
        }

        /// <summary>
        /// falls Content nicht null gibt true zurück
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Boolean hasContent(Product product)
        {
            List<PContent> content = loadContentList(product);
            if (content[0] != null)
                return true;

            return false;
        }

        
        public Boolean saveModelsLocal(List<Product> inputProducts, List<PContent> inputContents)
        {
            Boolean returnSuccess = false;


            return (returnSuccess);
        }
        /// <summary>
        /// checks if user exists
        /// </summary>
        /// <returns></returns>
        private Boolean userExist()
        {
            return (false);
        }
        /// <summary>
        /// returns the User
        /// </summary>
        /// <returns></returns>
         public User getUser()
        {
            User returnUser;
            if (userExist())
            {
                returnUser = new User("","");
            }
            else
            {
                returnUser = null;
            }
            return (returnUser);
        }

        /// <summary>
        /// saves the User to a local xml file and returns true or false
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns></returns>
        public Boolean saveUser(User inputUser)
        {
            return (true);
        }
    }
}
