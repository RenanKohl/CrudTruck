using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volvo.CrudTruck.Domain;

namespace Volvo.CrudTruck.Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Login(string login, string password);
    }
}
