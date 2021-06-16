using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Volvo.CrudTruck.Data.UoW
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
