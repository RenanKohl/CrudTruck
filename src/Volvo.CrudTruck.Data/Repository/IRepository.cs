using System;
using System.Collections.Generic;
using System.Text;
using Volvo.CrudTruck.Data.UoW;
using Volvo.CrudTruck.Domain;

namespace Volvo.CrudTruck.Data.Repository
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
