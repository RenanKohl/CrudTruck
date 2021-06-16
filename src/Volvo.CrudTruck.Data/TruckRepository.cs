using Microsoft.EntityFrameworkCore;
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
    public class TruckRepository : ITruckRepository
    {
        private readonly DatabaseContext _context;

        public TruckRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Insert(Truck truck)
        {
             _context.Trucks.Add(truck);
            await Task.CompletedTask;
        }
        public async Task Update(Truck truck)
        {
            _context.Trucks.Update(truck);
            await Task.CompletedTask;
        }
        public async Task Delete(Truck truck)
        {
            _context.Trucks.Remove(truck);
            await Task.CompletedTask;
        }

        public async Task<Truck> SelectById(int id)
        {
            return await _context.Trucks.FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<IEnumerable<Truck>> SelectAll()
        {
            return await _context.Trucks.ToListAsync();
        }
    }
}
