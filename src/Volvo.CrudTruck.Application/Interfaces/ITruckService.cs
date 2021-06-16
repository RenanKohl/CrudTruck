using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volvo.CrudTruck.Application.Models;

namespace Volvo.CrudTruck.Application.Interfaces
{
    public interface ITruckService
    {
        Task<BaseModel<TruckModel.Response>> Insert(TruckModel.Request truck);
        Task<BaseModel<TruckModel.Response>> Update(int id, TruckModel.Request truck);
        Task<BaseModel<TruckModel.Response>> Delete(int id);
        Task<BaseModel<TruckModel.Response>> GetById(int id);
        Task<BaseModel<IEnumerable<TruckModel.Response>>> GetAll();
    }
}
