using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuscany.DataAccess.DB;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;

namespace Tuscany.DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbTuscanyContext db) : base(db)
        {
        }

        public void Update(User user)
        {
            _db.Update(user);
        }

        public async Task<User?> FindByNameOrEmail(string email, string username)
        {
            User? user = await _db.Users
                .FirstAsync(x => x.Name == username || x.Email == email);
            return user;
        }
    }
}
