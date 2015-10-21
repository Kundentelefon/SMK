using System.Collections.Generic;
using System.Threading.Tasks;
using SMK.Model;

namespace SMK.DataAccess
{
    public interface IDataAccess
    {
        /// <summary>
        ///  REST API: Checks if the user already exists in the Database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> ValidateUser(User user);

        /// <summary>
        /// REST API: Async Connects with the Server with URI (Emulator Standard is http://10.0.2.2) and receives the User Datainformation with a REST Web Request from a .php GET Method Request. Checks if the user if null. Returns true if the user exist and returns null if the user dont exist
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        Task<bool> IsDuplicatedUser(string username);

        /// <summary>
        ///  REST API: Async Checks if Database has the product key
        /// </summary>
        /// <param name="key"></param>
        Task<bool> IsValidKey(string key);

        /// <summary>
        /// REST API: Async Sets the received Product key invalid in the Database
        /// </summary>
        /// <param name="key"></param>
        void SetProductKeyInvalid(string key);

        /// <summary>
        /// REST API: Connects with the Server with URI (Emulator Standard is http://10.0.2.2) send with a REST Web Request from a .php POST Method Request. Creates on the Database a new User with a new ID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        void AddUserToDatabase(string username, string password);

        /// <summary>
        ///  REST API: Async Addproduct 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="user"></param>
        void AddProductToUser(int productId, User user);

        /// <summary>
        /// REST API: Async Returns the Product connected to the Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Task</returns>
        Task<Product> GetProductByKey(string key);

        /// <summary>
        /// REST API: Async Returns the List of the Product the user Owns
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<List<Product>> GetUserProducts(User user);

        /// <summary>
        /// REST API: Async Returns the Content of the Product id
        /// </summary>
        /// <param name="productid"></param>
        /// <returns>Task</returns>
        Task<List<PContent>> GetPContent(int productid);

        /// <summary>
        /// REST API: Async Returns all Files of a PContent as a List of strings
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        Task<List<string>> GetFileServerPath(int cid);
    }
}