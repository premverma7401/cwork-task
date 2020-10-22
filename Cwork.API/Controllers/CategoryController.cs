﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwork.Domain.Models.Input;
using Cwork.Service.Implimentation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _repo;

        public CategoryController(CategoryRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("createCategory")]

        public IActionResult CreateNewCategory(CategoryModel model)
        {
            return Ok(_repo.CreateCategory(model));
        }

        [HttpPost]
        [Route("listCategories")]
        public IActionResult ListCategories()
        {
            return Ok(_repo.GetAllCategories());
        }
        [HttpPost]
        [Route("recentCategory")]
        public IActionResult RecentCategories()
        {
            return Ok(_repo.GetRecentCategory());
        }
        [HttpPost]
        [Route("deleteCategory")]
        public IActionResult DeleteCategory(int id)
        {
            return Ok(_repo.DeleteCategory(id));
        }


    }
}