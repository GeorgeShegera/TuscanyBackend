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
            List<User> users = await _db.Users.ToListAsync();
            return users.FirstOrDefault(x => x.Name is not null &&
                                    x.Email is not null &&
                                    (x.Name == username ||
                                    x.Email == email));
        }
    }
}
