using Dal.DataObject;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DalImplements
{
    public class UserRepository : IUserRepository
    {
        General general;
        public UserRepository(General general)
        {
            this.general = general;
        }
        #region basic-CRUD

        public async Task<int> CreateAsync(User item)
        {
            var newItem = general.Users.Add(item);
            await general.SaveChangesAsync();
            return newItem.Entity.Code;
        }

        public async Task<User> ReadByIdAsync(int code)
        {
            return await general.Users.FindAsync(code);
        }
        public async Task<List<User>> ReadAllAsync()
        {
            return await general.Users.ToListAsync<User>();
        }

        public async Task<bool> UpdateAsync(User newItem)
        {
            try
            {
                general.Users.Update(newItem);
                await general.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int code)
        {
            try
            {
                User user = general.Users.Find(code);
                general.Users.Remove(user);
                await general.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }


        }
        #endregion
        public async Task<User> ReadByPasswordAsync(string password)
        {
            return await general.Users.Where(x => x.Password == password).FirstOrDefaultAsync();
        }


    }
}
