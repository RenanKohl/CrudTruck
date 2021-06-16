using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volvo.CrudTruck.Data.Context;
using Volvo.CrudTruck.Domain;

namespace Volvo.CrudTruck.API.Helpers
{
    public class SeedingService
    {
        private DatabaseContext context;

        public SeedingService(DatabaseContext context)
        {
            this.context = context;
        }

        public void Seed()
        {

            if (context.Users.Any())
                return;

            context.Users.Add(new User("Renan", "Rkohl", "123"));

            context.SaveChangesAsync();
        }
    }
}
