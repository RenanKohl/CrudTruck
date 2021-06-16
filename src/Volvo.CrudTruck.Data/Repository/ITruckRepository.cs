using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volvo.CrudTruck.Domain;

namespace Volvo.CrudTruck.Data.Repository
{
    public interface ITruckRepository: IRepository<Truck>
    {
        Task Insert(Truck truck);
        Task Update(Truck truck);
        Task Delete(Truck truck);
        Task<Truck> SelectById(int id);
        Task<IEnumerable<Truck>> SelectAll();
    }
}
