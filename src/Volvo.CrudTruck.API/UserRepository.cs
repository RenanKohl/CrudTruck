using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volvo.CrudTruck.Domain;
using Volvo.CrudTruck.Domain.Repository;

namespace Volvo.CrudTruck.API
{
    public class UserRepository : IUserRepository
    {
        public Task<User> Login(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}
