using System.Collections.Generic;
using Cwork.Domain.Models.Output;

namespace Cwork.Service.Interface
{
    public interface IVehicleRepository
    {
        int CreateVehicle(VehicleDTO model);
        List<VehicleListDTO> GetAllVehicles();
    }
}