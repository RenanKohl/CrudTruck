using System;
using System.Collections.Generic;
using System.Text;

namespace Volvo.CrudTruck.Domain
{
    public class Truck : Entity
    {
        public Truck()
        {

        }
        public string Model { get; private set; }
        public DateTime Fabrication { get; private set; }
        public DateTime ModelYear { get; private set; }
        public Guid ChassisCode { get; private set; }
        public Guid EngineCode { get; private set; }

    }
}
