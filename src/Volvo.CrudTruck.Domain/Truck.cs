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
        public Truck(string model, int modelYear)
        {
            Model = model;
            Fabrication = DateTime.Now.Year;
            ModelYear = modelYear;
            ChassisCode = Guid.NewGuid().ToString("N");
            EngineCode = Guid.NewGuid().ToString("N");
        }

        public string Model { get; private set; } // apenas FH e FM
        public int Fabrication { get; private set; } //deverá ser o atual
        public int ModelYear { get; private set; } // devera ser o atual ou o ano subsequente
        public string ChassisCode { get; private set; }
        public string EngineCode { get; private set; }

        public void ChangeModel(string model)
        {
            Model = model;
        }

        public void ChangeModelYear(int modelYear)
        {
            ModelYear = modelYear;
        }

    }
}
