using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volvo.CrudTruck.Application.Models;

namespace Volvo.CrudTruck.Application.Interfaces
{
    public interface IUserService
    {
        Task<BaseModel<UserModel.Response>> Login(UserModel.Request request);
    }
}
