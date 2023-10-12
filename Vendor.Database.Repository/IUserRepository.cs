using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendor.Database.Identity;

namespace Vendor.Database.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers(int pageNumber = 1, int pageSize = 10);
        Task<User> GetUser(string id);
        Task<bool> CheckUserExist(string email);
        Task<bool> UpdateUser(string id, User user);

    }
}
