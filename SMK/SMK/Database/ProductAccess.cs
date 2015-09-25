using RestSharp;
using SMK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK.Database
{
    class ProductAccess
    {
        /// <summary>
        /// REST API: Async Checks if Database has the product key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Task<bool></returns>
        public async Task<bool> IsValidKey(string key)
        {
            var client = new RestClient("http://10.0.2.2");
            var request = new RestRequest("getProductKey.php", Method.GET);
            request.AddParameter("product_Key", key);

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            return response.Content != "0 results";
        }

        /// <summary>
        /// REST API: Async Returns the Product connected to the Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Task<Product></returns>
        public async Task<Product> GetProduct(string key)
        {
            var client = new RestClient("http://10.0.2.2");
            var request = new RestRequest("getProductKey.php", Method.GET);
            request.AddParameter("product_Key", key);

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(response.Content);

            return model;
        }

        /// <summary>
        /// REST API: Async Returns the Content of the Product id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task<Pcontent></returns>
        public async Task<PContent> GetPContent(string id)
        {
            var client = new RestClient("http://10.0.2.2");
            var request = new RestRequest("getPContent.php", Method.GET);
            request.AddParameter("pcontent_ID", id);

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<PContent>(response.Content);

            return model;
        }

        /// <summary>
        /// REST API: Async Returns the PContentFiles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PContentFiles> GetPContentFiles(string id)
        {
            var client = new RestClient("http://10.0.2.2");
            var request = new RestRequest("getFilePaths.php", Method.GET);
            request.AddParameter("pcontent_ID", id);

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<PContentFiles>(response.Content);

            return model;
        }

        /// <summary>
        /// REST API: Async Returns the List of the Product the user Owns
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public async Task<List<Product>> GetUserProducts(string userEmail)
        {
            var client = new RestClient("http://10.0.2.2");
            var request = new RestRequest("getUserProducts.php", Method.GET);
            request.AddParameter("user_Email", userEmail);

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(response.Content);

            return model;
        }
    }
}
