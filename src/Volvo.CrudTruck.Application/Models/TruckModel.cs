using System;
using System.Collections.Generic;
using System.Text;
using Volvo.CrudTruck.Domain;

namespace Volvo.CrudTruck.Application.Models
{
    public class TruckModel
    {
        public class Request
        {
            public string Model { get; set; }
            public int ModelYear { get; set; }
        }
        public class Response
        {
            public Response(Truck entity)
            {
                if (entity == null)
                    return;

                Id = entity.Id;
                Model = entity.Model;
                Fabrication = entity.Fabrication;
                ModelYear = entity.ModelYear;
                ChassisCode = entity.ChassisCode;
                EngineCode = entity.EngineCode;
            }
            public int Id { get; set; }
            public string Model { get; private set; }
            public int Fabrication { get; private set; } 
            public int ModelYear { get; private set; }
            public string ChassisCode { get; private set; }
            public string EngineCode { get; private set; }
        }
    }
}
