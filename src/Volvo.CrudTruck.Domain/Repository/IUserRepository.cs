using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Volvo.CrudTruck.Domain.Repository
{
    public interface IUserRepository
    {
        Task<User> Login(string login, string password);
    }
}
