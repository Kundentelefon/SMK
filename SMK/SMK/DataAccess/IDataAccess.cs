using System.Collections.Generic;
using System.Threading.Tasks;
using SMK.Model;

namespace SMK.DataAccess
{
    public interface IDataAccess
    {
        Task<User> ValidateUser(User user);
        Task<bool> IsDuplicatedUser(string username);
        Task<bool> IsValidKey(string key);
        void SetProductKeyInvalid(string key);
        void AddUserToDatabase(string username, string password);
        void AddProductToUser(int productId, User user);
        Task<Product> GetProductByKey(string key);
        Task<List<Product>> GetUserProducts(User user);
        Task<List<PContent>> GetPContent(int id);
        Task<List<string>> GetPContentFiles(int id);
    }
}