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
        //List<Product> ProductList;
        //List<PContent> ContentList;

        String productLocation = "Products";
        String pContentLocation = "Pcontent";
        String userLocation = "User";

        public localFileSystem()
        {
            //ProductList = loadProductList();
            //ContentList = loadContentList();

        }

        public void initaldummies()
        {
            string testText = "have some Text i hope you like it fighting and burning from turning from who we really are, flying to close to the sun as we were invincible, around the world we grow weaker as we exterminate, we are the children of the sun bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb";
            
            //List<Product> returnList = new List<Product> { new Product(0, "Test","SMK.FischerTechnik.Files.0.png", testText, new List<int>(new int[] { 0, 1, 2 })) ,
            //                                               new Product(1, "Test ProductNotOwned",  "SMK.FischerTechnik.Files.1.png", "havesomeTextShit i hope you like it", new List<int>(new int[] { 3, 4})),
            //                                               new Product(2, "Test ProductNotOwned",  "SMK.FischerTechnik.Files.1.png", "havesomeTextShit i hope you like it", new List<int>(new int[] { 5, 6})),
            //                                               new Product(3, "Test ProductNotDownloaded",  "SMK.FischerTechnik.Files.2.png", "havesomeTextShit i hope you like it", new List<int>(new int[] { 7, 8 }))
            //};

            List<Product> returnList = new List<Product>();
            Product pro1 = new Product();
            pro1.product_ID =0;
            pro1.product_Name= "Test";
            pro1.product_Thumbnail = "SMK.FischerTechnik.Product.0.png";
            pro1.product_Text = testText;
            pro1.product_PContents = new List<int>(new int[] { 0, 1, 2 });

            Product pro2 = new Product();
            pro2.product_ID =1;
            pro2.product_Name= "Test";
            pro2.product_Thumbnail = "SMK.FischerTechnik.Product.1.png";
            pro2.product_Text = testText;
            pro2.product_PContents = new List<int>(new int[] { 3, 4 });

            Product pro3 = new Product();
            pro3.product_ID =2;
            pro3.product_Name= "Test";
            pro3.product_Thumbnail = "SMK.FischerTechnik.Product.2.png";
            pro3.product_Text = testText;
            pro3.product_PContents = new List<int>(new int[] { 5, 6 });

            Product pro4 = new Product();
            pro4.product_ID =3;
            pro4.product_Name= "Test";
            pro4.product_Thumbnail = "SMK.FischerTechnik.Files.0.png";
            pro4.product_Text = testText;
            pro4.product_PContents = new List<int>(new int[] { 7, 8 });

            returnList.Add(pro1);
            returnList.Add(pro2);
            returnList.Add(pro3);
            returnList.Add(pro4);


            //List<PContent> returnList2 = new List<PContent> {new PContent(0,0,"BilderGalarie", new List<String>(new String[] {"SMK.FischerTechnik.0.da.jpg","haha.png","devil.png"})),//"Resources/FischerTechnik/PContent/0_Icon.png"
            //                                               new PContent(1,0,"BilderGalarie",new List<String>(new String[]{"da.png","haha.png","devil.png"})),
            //                                               new PContent(2,2,"WebView",new List<String>(new String[]{})),
            //                                               null,
            //                                               null,
            //                                               new PContent(5,0,"BilderGalarie",new List<String>(new String[]{"da.png","haha.png","devil.png"})),//für diese Fehlt noch der Content da dieser Noch nicht "gedownloaded" wurde
            //                                               new PContent(6,0,"BilderGalarie",new List<String>(new String[]{"da.png","haha.png","devil.png"})),//für diese Fehlt noch der Content da dieser Noch nicht "gedownloaded" wurde
            //                                               null,
            //                                               null
            //                                                };

            List<PContent> returnList2 = new List<PContent>();
            PContent content1 = new PContent();
            content1.content_FileNames= new List<String>(new String[] { "0.da.jpg", "0.haha.png", "0.devil.png" });
            content1.content_ID = 0;
            content1.content_Kind = 0;
            content1.content_Title = "BilderGalarie";

            PContent content2 = new PContent();
            content2.content_FileNames = new List<String>(new String[] { "1.da.png", "1.haha.png", "1.devil.png" });
            content2.content_ID = 1;
            content2.content_Kind = 0;
            content2.content_Title = "BilderGalarie";

            PContent content3 = new PContent();
            content3.content_FileNames = new List<String>(new String[] { });
            content3.content_ID = 2;
            content3.content_Kind = 2;
            content3.content_Title = "WebView";

            PContent content4 = null;

            PContent content5 = null;

            PContent content6 = new PContent();
            content6.content_FileNames = new List<String>(new String[] { "1.da.png", "1.haha.png", "1.devil.png" });
            content6.content_ID = 5;
            content6.content_Kind = 0;
            content6.content_Title = "BilderGalarie";

            PContent content7 = new PContent();
            content7.content_FileNames = new List<String>(new String[] { "0.da.png", "0.haha.png", "0.devil.png" });
            content7.content_ID = 6;
            content7.content_Kind = 0;
            content7.content_Title = "BilderGalarie";

            PContent content8 = null;
            PContent content9 = null;
            returnList2.Add(content1);
            returnList2.Add(content2);
            returnList2.Add(content3);
            returnList2.Add(content4);
            returnList2.Add(content5);
            returnList2.Add(content6);
            returnList2.Add(content7);
            returnList2.Add(content8);
            returnList2.Add(content9);
            //if (DependencyService.Get<ISaveAndLoad>().fileExist(pContentLocation))
            //{
            
            //}
            //else
            //{
                saveModelsLocal(returnList, returnList2);
            //}
            
        }
        public List<Product> loadProductList()
        {
            List<Product> returnList = new List<Product>();
            if (DependencyService.Get<ISaveAndLoad>().fileExist(productLocation))
            {
                returnList = DependencyService.Get<ISaveAndLoad>().loadProductsXml(productLocation).listProducts;
            }                                
            
            return (returnList);

        }
        public List<PContent> loadContentList() { 
            List<PContent> returnList = new List<PContent>();
            if (DependencyService.Get<ISaveAndLoad>().fileExist(productLocation))
            {
                returnList = DependencyService.Get<ISaveAndLoad>().loadPcontentsXml(pContentLocation).listPContent;
            }
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
        public Boolean hasContent(Product product, List<PContent> Pcontent)
        {
            List<PContent> content = new List<PContent>();
            foreach (var item in product.product_PContents)
            {
                content.Add(Pcontent[item]);
            }
            if (content.Count()>0)
        {
            if (content[0] != null)
                return true;
            }
            return false;
        }

        
        public void saveModelsLocal(List<Product> inputProducts, List<PContent> inputContents)
        {
            PContents savePcontents = new PContents();
            savePcontents.listPContent = inputContents;
            Products saveProducts = new Products();
            saveProducts.listProducts = inputProducts;

            DependencyService.Get<ISaveAndLoad>().savePContentsXml(pContentLocation,savePcontents);
            DependencyService.Get<ISaveAndLoad>().saveProductsXml(productLocation, saveProducts);

        }
        /// <summary>
        /// checks if user exists
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
        /// returns the User
        /// </summary>
        /// <returns></returns>
         public User getUser()
        {
            User returnUser;
            if (userExist())
            {
                returnUser=DependencyService.Get<ISaveAndLoad>().loadUserXml(userLocation);
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
        public void saveUser(User inputUser)
        {
            DependencyService.Get<ISaveAndLoad>().saveUserXml(userLocation,inputUser);
        }

        public void createInitalFolders()
        {
            
            DependencyService.Get<ISaveAndLoad>().createOrdner(productLocation+"folder");
            DependencyService.Get<ISaveAndLoad>().createOrdner(pContentLocation+"folder");
        }

        public void deleteUser()
        {
            DependencyService.Get<ISaveAndLoad>().deleteUser(userLocation);
        }

    }
}
