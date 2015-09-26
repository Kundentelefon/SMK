using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using RestSharp;
using SMK.Model;

namespace SMK.DataAccess
{
    internal class DataAccessMySqlPhp : IDataAccess
    {
        //Emulator Address
        private const string ServerAdress = "http://10.0.2.2";

        /// <summary>
        /// REST API: Async Checks if Database has the product key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Task</returns>
        public async Task<bool> IsValidKey(string key)
        {
            var client = new RestClient(ServerAdress);
            var request = new RestRequest("getProductKey.php", Method.GET);
            request.AddParameter("product_Key", key);

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            return response.Content != "0 results";
        }

        /// <summary>
        /// REST API: Async Sets the received Product key invalid in the Database
        /// </summary>
        /// <param name="key"></param>
        public async void SetProductKeyInvalid(string key)
        {
            var client = new RestClient(ServerAdress);
            var request = new RestRequest("setProductKeyInvalid.php", Method.POST);
            request.AddParameter("product_Key", key);

            IRestResponse response = await client.ExecuteGetTaskAsync(request);
        }

        public async void AddProductToUser(int productId, User user)
        {

                var client = new RestClient(ServerAdress);
                var request = new RestRequest("AddProductToUser.php", Method.POST);
                request.AddParameter("user_Email", user.user_Email);
                request.AddParameter("product_ID", productId);
                //Debug.WriteLine("ex1" + ex);


                IRestResponse response = await client.ExecuteGetTaskAsync(request);
        }

        public async Task<User> ValidateUser(User user)
        {
            var client = new RestClient(ServerAdress);
            var request = new RestRequest("getUser.php", Method.GET);
            request.AddParameter("user_Email", user.user_Email);

            // Async Executes the .php Request
            IRestResponse response = await client.ExecuteGetTaskAsync(request);
                
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            if (response.Content == "0 results")
                return null;

            // Deserializes JSON String
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(response.Content);

            //Checks if the JSON String has a user and if so, then checks for valid password 
            return user.user_Password.Equals(model.user_Password) ? model : null;
        }

        /// <summary>
        /// REST API: Async Returns the Product connected to the Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Task</returns>
        public async Task<Product> GetProductByKey(string key)
        {
            var client = new RestClient(ServerAdress);
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
        /// <returns>Task</returns>
        public async Task<PContent> GetPContent(string id)
        {
            var client = new RestClient(ServerAdress);
            var request = new RestRequest("getPContent.php", Method.GET);
            request.AddParameter("pcontent_ID", id);

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<PContent>(response.Content);

            return model;
        }

        /// <summary>
        /// REST API: Async Returns all Files of a PContent as a List of strings
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<string>> GetPContentFiles(string id)
        {
            var client = new RestClient(ServerAdress);
            var request = new RestRequest("getFilePaths.php", Method.GET);
            request.AddParameter("pcontent_ID", id);

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(response.Content);

            return model;
        }

        /// <summary>
        /// REST API: Async Returns the List of the Product the user Owns
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<List<Product>> GetUserProducts(User user)
        {
            var client = new RestClient(ServerAdress);
            var request = new RestRequest("getUserProducts.php", Method.GET);
            request.AddParameter("user_Email", user.user_Email);

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(response.Content);

            return model;
        }
    }
}
