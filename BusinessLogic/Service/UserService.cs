using BusinessLogic.Models;
using BusinessLogic.Service.Common;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class UserService: IUserService
    {
        private YourDictionaryDbContext DB;
        public UserService(YourDictionaryDbContext _dbContext)
        {
            DB = _dbContext;
        }
        public async Task<UserModelBL> GetUsers()
        {
            var users = await this.DB.Users.ToListAsync();

            var user = users.Select(x => new UserModelBL
            {
                Id = x.Id,
                Email = x.Email
            }).FirstOrDefault();

            return user;
        }
    }
}
