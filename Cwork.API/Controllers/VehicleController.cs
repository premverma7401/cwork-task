﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwork.Domain.Models.Input;
using Cwork.Domain.Models.Output;
using Cwork.Persistance;
using Cwork.Service.Implimentation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleRepository _repo;
        public VehicleController(VehicleRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("createVehicle")]

        public IActionResult CreateNewVehicle(VehicleDTO model)
        {
            return Ok(_repo.CreateVehicle(model));
        }

        [HttpPost]
        [Route("listVehicles")]
        public IActionResult ListVehicles()
        {
            return Ok(_repo.GetAllVehicles());
        }
    }
}
