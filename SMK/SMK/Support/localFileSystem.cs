using SMK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SMK.Support
{
    class localFileSystem
    {
        //Ordner Namen 
        public static readonly String productLocation = "Produkt";
        public static readonly String productFolderLocation = "Produkte";
        public static readonly String pContentLocation = "PContent";
        public static readonly String userLocation = "User";

        public localFileSystem()
        {}
        /// <summary>
        /// initalisiert ein paar dummies
        /// </summary>
        /// <param name="userPath"></param>
        public void initaldummies(String userPath)
        {
            string testText = "have some Text i hope you like it fighting and burning from turning from who we really are, flying to close to the sun as we were invincible, around the world we grow weaker as we exterminate, we are the children of the sun bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb";

            List<Product> returnList = new List<Product>();
            var pro1 = new Product();
            pro1.product_ID = 0;
            pro1.product_Name = "Test";
            pro1.product_Thumbnail = ".png";
            pro1.product_Text = testText;
            pro1.PContents = new List<int>(new int[] { 0, 1, 2 });            

            Product pro2 = new Product();
            pro2.product_ID = 1;
            pro2.product_Name = "Test";
            pro2.product_Thumbnail = ".png";
            pro2.product_Text = testText;
            pro2.PContents = new List<int>(new int[] { 3 });

            Product pro3 = new Product();
            pro3.product_ID = 2;
            pro3.product_Name = "Test";
            pro3.product_Thumbnail = ".png";
            pro3.product_Text = testText;
            pro3.PContents = new List<int>(new int[] { 4 });

            Product pro4 = new Product();
            pro4.product_ID = 3;
            pro4.product_Name = "Test";
            pro4.product_Thumbnail = ".png";
            pro4.product_Text = testText;
            pro4.PContents = new List<int>(new int[] { 5, 6 });

            returnList.Add(pro1);
            returnList.Add(pro2);
            returnList.Add(pro3);
            returnList.Add(pro4);

            List<PContent> returnList2 = new List<PContent>();
            PContent content1 = new PContent();
            content1.files = new List<string>(new String[] { "da.jpg", "haha.png", "devil.png" });
            content1.content_ID = 0;
            content1.content_Kind = 0;
            content1.content_Title = "BilderGalarie";

            PContent content2 = new PContent();
            content2.files = new List<string>(new String[] { "da.jpg", "haha.png", "devil.png" });
            content2.content_ID = 1;
            content2.content_Kind = 0;
            content2.content_Title = "BilderGalarie";

            PContent content3 = new PContent();
            content3.files = new List<string>(new String[] { "1 Profi Pneum-III_D-3_Üb.html" });
            content3.content_ID = 2;
            content3.content_Kind = 2;
            content3.content_Title = "WebView";


            PContent content4 = new PContent();
            content4.files = new List<string>(new String[] { "test.png", "test1.png" });
            content4.content_ID = 3;
            content4.content_Kind = 4;
            content4.content_Title = "Gallary";

            PContent content5 = new PContent();
            content5.files = new List<string>(new String[] { "da.jpg", "haha.png", "devil.png" });
            content5.content_ID = 4;
            content5.content_Kind = 0;
            content5.content_Title = "BilderGalarie";

            PContent content6 = null;
            PContent content7 = null;
            returnList2.Add(content1);
            returnList2.Add(content2);
            returnList2.Add(content3);
            returnList2.Add(content4);
            returnList2.Add(content5);
            returnList2.Add(content6);
            returnList2.Add(content7);

            saveModelsLocal(userPath,returnList, returnList2);
 
            
        }
        /// <summary>
        /// liest den Produkt ein von dem als xml gespeicherten Produkt 
        /// </summary>
        /// <returns></returns>
        public List<Product> loadProductList()
        {
            List<Product> returnList = new List<Product>();
            if (DependencyService.Get<ISaveAndLoad>().fileExist(productLocation))
            {
                returnList = DependencyService.Get<ISaveAndLoad>().loadProductsXml(productLocation).listProducts;
            }            
            return (returnList);
        }
        /// <summary>
        /// liest den PContent ein von dem als xml gespeicherten PContent 
        /// </summary>
        /// <param name="userPath">der Spezifische User Ordner</param>
        /// <returns></returns>
        public List<PContent> loadContentList(String userPath) { 
            List<PContent> returnList = new List<PContent>();
            if (DependencyService.Get<ISaveAndLoad>().fileExist(productLocation))
            {
                returnList = DependencyService.Get<ISaveAndLoad>().loadPcontentsXml(pContentLocation, userPath).listPContent;
            }
            return (returnList);
        }

        /// <summary>
        /// Übergebe ein Product und bekomme eine Liste mit allen Content für dieses Produkt
        /// der zurückgegebene Content kann leer sein falls der User das Product nicht besitzt
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public List<PContent> loadContentList(Product product,String userPath)
        {
            List<PContent> completeList = loadContentList(userPath);
            List<PContent> returnList = new List<PContent>();
            foreach (int item in product.PContents)
            {
                returnList.Add(completeList.Find(content => content.content_Kind == item));
            }
            return returnList;
        }

        /// <summary>
        /// überprüft ob in Pcontent entwas enthalten ist und 
        /// falls ja ob das Produkt auch Content besitzt und 
        /// ob in Pcontent, Content von dem Produkt enthält.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Boolean hasContent(Product product, List<PContent> Pcontent)
        {
            if (Pcontent.Count()>0&& product.PContents.Count() > 0 && Pcontent[product.PContents[0]] != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// speichert alle Übergeben Produkte in der Datei "Products" vogegebenen Pfad ab 
        /// speichert alle Pcontents in Contents im spezifischen User Verzeichnis übergeben in userPath ab
        /// </summary>
        /// <param name="userPath"></param>
        /// <param name="inputProducts"></param>
        /// <param name="inputContents"></param>
        public void saveModelsLocal(String userPath,List<Product> inputProducts, List<PContent> inputContents)
        {
            PContents savePcontents = new PContents();
            savePcontents.listPContent = inputContents;
            Products saveProducts = new Products();
            saveProducts.listProducts = inputProducts;

            DependencyService.Get<ISaveAndLoad>().savePContentsXml(userPath,pContentLocation, savePcontents);
            DependencyService.Get<ISaveAndLoad>().saveProductsXml(productLocation, saveProducts);

        }
        /// <summary>
        /// überprüft ob ein user existiert
        /// </summary>
        /// <returns></returns>
        private Boolean userExist()
        {
            if (DependencyService.Get<ISaveAndLoad>().fileExist(userLocation))
            {
                return (true);
            }
            return (false);
        }
        /// <summary>
        /// lädt den User aus dem File User und gibt diesen zurück
        /// </summary>
        /// <returns></returns>
         public User getUser()
        {
            User returnUser=null;
            if (userExist())
            {
                returnUser=DependencyService.Get<ISaveAndLoad>().loadUserXml(userLocation);
            }
            return (returnUser);
        }

        /// <summary>
        /// speichert den User in der User File als xml ab
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns></returns>
        public void saveUser(User inputUser)
        {
            DependencyService.Get<ISaveAndLoad>().saveUserXml(userLocation,inputUser);
        }
        /// <summary>
        /// erstellt den Produkt Ordner und den Userspezifischen Ordner,
        /// fals die Ordner noch nicht vorhanden sind.
        /// </summary>
        /// <param name="userFile"></param>
        public void createInitalFolders(String userFile)
        {
                DependencyService.Get<ISaveAndLoad>().createOrdner(productFolderLocation);
                DependencyService.Get<ISaveAndLoad>().createOrdner(userFile);
        }
        /// <summary>
        /// entfärnt die User Xml Datei
        /// </summary>
        public void deleteUser()
        {
            DependencyService.Get<ISaveAndLoad>().deleteFile(userLocation);
        }
        /// <summary>
        /// entfärnt \\/:*?""<>| aus dem übergebenen String
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public string AdjustPath(string Input)
        {
            return System.Text.RegularExpressions.Regex.Replace(Input, @"[\\/:*?""<>|]", string.Empty);
        }
    }
}
