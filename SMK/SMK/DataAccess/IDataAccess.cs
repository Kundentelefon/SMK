using System.Collections.Generic;
using System.Threading.Tasks;
using SMK.Model;

namespace SMK.DataAccess
{
    public interface IDataAccess
    {
        Task<PContent> GetPContent(string id);
        Task<List<string>> GetPContentFiles(string id);
        Task<Product> GetProductByKey(string key);
        Task<List<Product>> GetUserProducts(User user);
        Task<bool> IsValidKey(string key);
        void SetProductKeyInvalid(string key);
        void AddProductToUser(int productId, User user);
        Task<User> ValidateUser(User user);

    }
}