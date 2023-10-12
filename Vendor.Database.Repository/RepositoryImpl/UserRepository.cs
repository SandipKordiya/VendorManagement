using Vendor.Database.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Vendor.Database.Repository.RepositoryImpl
{
    public class UserRepository : IUserRepository
    {
        #region Variables
        //---------------------------------------------------------------------------
        //inject db contect
        ApplicationDbContext _dbContext;
        //---------------------------------------------------------------------------
        #endregion

        #region Constructor
        //---------------------------------------------------------------------------
        public UserRepository(ApplicationDbContext _db)
        {
            _dbContext = _db;
        }

        public async Task<bool> CheckUserExist(string email)
        {
            if (_dbContext != null)
            {
                var user = await _dbContext.Users.FindAsync(email);
                return user != null;
            }
            return false;
        }

        public async Task<User> GetUser(string id)
        {
            if (_dbContext != null)
            {
                User res;
                res = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

                return res;
            }
            return null;
        }

        public  async Task<IEnumerable<User>> GetUsers(int pageNumber = 1, int pageSize = 10)
        {
            if (_dbContext != null)
            {
                IEnumerable<User> res;
                res = await _dbContext.Users.OrderByDescending(x => x.Id)
                                                    .Skip((pageNumber - 1) * pageSize)
                                                    .Take(pageSize)
                                                   .ToListAsync();

                return res;
            }
            return null;
        }

        public async Task<bool> UpdateUser(string id, User user)
        {
            if (_dbContext == null)
                return false;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        //---------------------------------------------------------------------------
        #endregion
    }
}
