using Cwork.Domain.Models.Input;
using Cwork.Domain.Models.Output;
using Cwork.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cwork.Service.Implimentation
{
    public class VehicleRepository
    {
        private readonly DataContext _data;

        public VehicleRepository(DataContext data)
        {
            _data = data;
        }

        public int CreateVehicle(VehicleDTO model)
        {

            var vehicle = new VehicleModel
            {
                OwnerName = model.OwnerName,
                Weight = model.Weight,
                Year = model.Year,
                CategoryId = model.CategoryId,
                ManufacturingId = model.ManufacturingId
            };
            _data.Vehicles.Add(vehicle);
            _data.SaveChanges();
            return 1;
        }

        public List<VehicleListDTO> GetAllVehicles()
        {
            var vehicles = _data.Vehicles.Include(c => c.CategoryModel).Include(m => m.ManufacturerModel).ToList();
            Console.WriteLine(vehicles);
            return vehicles.Select(c => new VehicleListDTO
            {
                OwnerName = c.OwnerName,
                Weight = c.Weight,
                Year = c.Year,
                CategoryName = c.CategoryModel.CategoryName,
                CategoryIcon = c.CategoryModel.Icon,
                ManufacturerName = c.ManufacturerModel.ManufacturerName

            }).ToList();
        }
    }
}

