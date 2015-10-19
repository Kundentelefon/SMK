using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using RestSharp;
using SMK.Model;
using Xamarin.Forms;
using SMK.Support;

namespace SMK.DataAccess
{
    internal class DataAccessMySqlPhp : IDataAccess
    {
        //Emulator Address
        //private const string ServerAdress = "http://10.0.2.2";
        private const string ServerAdress = "http://169.254.80.80";

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

            request.Timeout = 5000;

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            //only throws the exception. Let target choose what to do
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

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
            request.AddParameter("productkeys_Key", key);

            request.Timeout = 5000;

            IRestResponse response = await client.ExecutePostTaskAsync(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
        }
        /// <summary>
        ///  REST API: Async Addproduct 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="user"></param>
        public async void AddProductToUser(int productId, User user)
        {

            var client = new RestClient(ServerAdress);
            var request = new RestRequest("AddProductToUser.php", Method.POST);
            request.AddParameter("user_Email", user.user_Email);
            request.AddParameter("product_ID", productId);

            request.Timeout = 5000;

            IRestResponse response = await client.ExecutePostTaskAsync(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

        }
        /// <summary>
        ///  REST API: Checks if the user already exists in the Database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> ValidateUser(User user)
        {
            try { 
            var client = new RestClient(ServerAdress);
            var request = new RestRequest("getUser.php", Method.GET);
            request.AddParameter("user_Email", user.user_Email);

            request.Timeout = 5000;

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
            catch (Exception e)
            {
                Debug.WriteLine("Catched Timout");
                throw new Exception();
    }
}

        /// <summary>
        /// REST API: Connects with the Server with URI (Emulator Standard is http://10.0.2.2) send with a REST Web Request from a .php POST Method Request. Creates on the Database a new User with a new ID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public async void AddUserToDatabase(string username, string password)
        {
            try { 
            var client = new RestClient(ServerAdress);
            var request = new RestRequest("createUser.php", Method.POST);
            request.AddParameter("user_Email", username);
            request.AddParameter("user_Password", DependencyService.Get<IHash>().SHA512StringHash(password));

            request.Timeout = 5000;

            IRestResponse response = await client.ExecutePostTaskAsync(request);
                await Task.Delay(3000);


                if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            }
            catch (Exception e)
            {
                //Extra Exceptionhandling for void async
                new Exception("AddUser Exception " + e);
            }
        }

        /// <summary>
        /// REST API: Async Connects with the Server with URI (Emulator Standard is http://10.0.2.2) and receives the User Datainformation with a REST Web Request from a .php GET Method Request. Checks if the user if null. Returns true if the user exist and returns null if the user dont exist
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public async Task<bool> IsDuplicatedUser(string strIn)
        {
                var client = new RestClient(ServerAdress);
                var request = new RestRequest("getUser.php", Method.GET);
                request.AddParameter("user_Email", strIn);

                request.Timeout = 5000;

                IRestResponse response = await client.ExecuteGetTaskAsync(request);

                if (response.ErrorException != null)
                {
                    throw response.ErrorException;
                }

                //Because of the isValidEmail Method, a Account with the name "0 results" can never happen
                return !response.Content.ToString().Equals("0 results");
            
        }

        /// <summary>
        /// REST API: Async Returns the Product connected to the Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Task</returns>
        public async Task<Product> GetProductByKey(string key)
        {
            var client = new RestClient(ServerAdress);
            var request = new RestRequest("GetProductByKey.php", Method.GET);
            request.AddParameter("productkeys_Key", key);

            request.Timeout = 5000;

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(response.Content);
            return model;
        }

        /// <summary>
        /// REST API: Async Returns the Content of the Product id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task</returns>
        public async Task<List<PContent>> GetPContent(int id)
        {
            var client = new RestClient(ServerAdress);
            var request = new RestRequest("getPContent.php", Method.GET);
            request.AddParameter("product_ID", id);

            request.Timeout = 5000;

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PContent>>(response.Content);

            return model;
        }

        /// <summary>
        /// REST API: Async Returns all Files of a PContent as a List of strings
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<string>> GetPContentFiles(int id)
        {
            var client = new RestClient(ServerAdress);
            var request = new RestRequest("getFilePaths.php", Method.GET);
            request.AddParameter("content_ID", id);

            request.Timeout = 5000;

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

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

            request.Timeout = 5000;

            IRestResponse response = await client.ExecuteGetTaskAsync(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(response.Content);

            return model;
        }
    }
}
