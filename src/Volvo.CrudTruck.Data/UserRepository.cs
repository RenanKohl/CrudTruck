using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volvo.CrudTruck.Data.Context;
using Volvo.CrudTruck.Data.Repository;
using Volvo.CrudTruck.Data.UoW;
using Volvo.CrudTruck.Domain;

namespace Volvo.CrudTruck.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<User> Login(string login, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));
        }
    }
}
