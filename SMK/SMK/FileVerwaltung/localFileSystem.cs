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
        //Folder Namen 
        public static readonly String productLocation = "Produkt";
        public static readonly String productFolderLocation = "Produkte";
        public static readonly String pContentLocation = "PContent";
        public static readonly String userLocation = "User";

        public localFileSystem()
        { }
        /// <summary>
        /// initialises dummies for testing purpoess
        /// </summary>
        /// <param name="userPath"></param>
        public void Initaldummies(String userPath)
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

            SaveModelsLocal(userPath, returnList, returnList2);


        }
        /// <summary>
        /// loads the Product which was saved as XML-Product
        /// </summary>
        /// <returns></returns>
        public List<Product> LoadProductList()
        {
            List<Product> returnList = new List<Product>();
            if (DependencyService.Get<ISaveAndLoad>().FileExist(productLocation))
            {
                returnList = DependencyService.Get<ISaveAndLoad>().LoadProductsXml(productLocation).listProducts;
            }
            return (returnList);
        }
        /// <summary>
        /// laods the PContent from the PContent-File
        /// </summary>
        /// <param name="userPath">the spedific User-Folder</param>
        /// <returns></returns>
        public List<PContent> loadContentList(String userPath)
        {
            List<PContent> returnList = new List<PContent>();
            if (DependencyService.Get<ISaveAndLoad>().FileExist(productLocation))
            {
                returnList = DependencyService.Get<ISaveAndLoad>().LoadPcontentsXml(pContentLocation, userPath).listPContent;
            }
            return (returnList);
        }

        /// <summary>
        /// Receives a Product and returns a List of all it Contents back
        /// The returned List can be empty if the User dont own the Product
        /// </summary>
        /// <param name="product"></param>
        /// <param name="userPath"></param>
        /// <returns></returns>
        public List<PContent> loadContentList(Product product, string userPath)
        {
            List<PContent> completeList = loadContentList(userPath);
            List<PContent> returnList = new List<PContent>();

            foreach (var item in product.PContents)
            {
                if (completeList.Count>item)
                {
                    returnList.Add(completeList[item]);
                }
            }
            return returnList;
        }

        /// <summary>
        /// Checks if PContent has content 
        /// if it has content it also checks if the Product has content
        /// and checks if Pcontent has content from the Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Boolean HasContent(Product product, List<PContent> Pcontent)
        {
            if (Pcontent.Any() && product.PContents.Any() && Pcontent[product.PContents[0]] != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// saves all given Products in a File "Products" at the given path and
        /// saves all PContents in contents at the specific Folder
        /// </summary>
        /// <param name="userPath"></param>
        /// <param name="inputProducts"></param>
        /// <param name="inputContents"></param>
        public void SaveModelsLocal(String userPath, List<Product> inputProducts, List<PContent> inputContents)
        {
            PContents savePcontents = new PContents();
            savePcontents.listPContent = inputContents;
            Products saveProducts = new Products();
            saveProducts.listProducts = inputProducts;

            DependencyService.Get<ISaveAndLoad>().SavePContentsXml(userPath, pContentLocation, savePcontents);
            DependencyService.Get<ISaveAndLoad>().SaveProductsXml(productLocation, saveProducts);
        }
        /// <summary>
        /// Checks if a User exists
        /// </summary>
        /// <returns></returns>
        private Boolean userExist()
        {
            if (DependencyService.Get<ISaveAndLoad>().FileExist(userLocation))
            {
                return (true);
            }
            return (false);
        }
        /// <summary>
        /// loads the User from the File "User" and gives it back
        /// </summary>
        /// <returns></returns>
        public User GetUser()
        {
            User returnUser = null;
            if (userExist())
            {
                returnUser = DependencyService.Get<ISaveAndLoad>().LoadUserXml(userLocation);
            }
            return (returnUser);
        }

        /// <summary>
        /// saves the User as User-File-XML
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns></returns>
        public void SaveUser(User inputUser)
        {
            DependencyService.Get<ISaveAndLoad>().SaveUserXml(userLocation, inputUser);
        }
        /// <summary>
        /// creates the Produkt-Folder and the specific User-Folder,
        /// if they are not already created
        /// </summary>
        /// <param name="userFile"></param>
        public void CreateInitalFolders(String userFile)
        {
            DependencyService.Get<ISaveAndLoad>().CreateFolder(productFolderLocation);
            DependencyService.Get<ISaveAndLoad>().CreateFolder(userFile);
            DependencyService.Get<ISaveAndLoad>().CreateFolder(DependencyService.Get<ISaveAndLoad>().PathCombine( userFile, "Thumbnail"));
        }
        /// <summary>
        /// deletes the "User"-File
        /// </summary>
        public void DeleteUser()
        {
            DependencyService.Get<ISaveAndLoad>().DeleteFile(userLocation);
        }
        /// <summary>
        /// removes \\/:*?""<>| from the given string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string AdjustPath(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, @"[\\/:*?""<>|]", string.Empty);
        }
    }
}
